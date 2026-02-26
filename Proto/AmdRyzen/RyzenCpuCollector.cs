using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LibreHardwareMonitor.Hardware;

class RyzenCpuCollector
{

    private Cpu cpu { get; set; }
    private CpuMeasures cpuMeasures { get; set; }

    private List<CoreMeasures> coreMeasures { get; set; } = new List<CoreMeasures>();

    private Dictionary<string, ICollection> map;

    private Computer computer;

    private static readonly Regex CoreSensorRegex = new(@"(?:CPU\s+)?Core #(?<core>\d+)(?: Thread #(?<thread>\d+))?", RegexOptions.Compiled | RegexOptions.CultureInvariant);


    public RyzenCpuCollector(Computer computer)
    {
        this.computer = computer;
    }

    public Process setupProcess()
    {
        Process process = new Process();
        process.StartInfo.FileName = "AMDRyzenMasterMonitoringSampleApp";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        return process;
    }

    public void collectData()
    {
        Process process = setupProcess();
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();
        computer.Open();
        this.map = CpuDataMapParser.ParseToMap(output);
        if (cpu == null)
        {
            firstParse();
        }
        updateParameters();
        process.Dispose();
    }

    private void firstParse()
    {
        this.cpu = new((Dictionary<string, object>)map.GetValueOrDefault("cpu_info"));
    }

    private void updateParameters()
    {
        this.coreMeasures = new List<CoreMeasures>();
        this.cpuMeasures = new CpuMeasures((Dictionary<string, object>)map.GetValueOrDefault("cpu_measures"), cpu, DateTime.UtcNow);
        ICollection coreMeasuresList = map.GetValueOrDefault("core_measures");
        foreach (var coreInfo in coreMeasuresList)
        {
            CoreMeasures coreMeasures = new CoreMeasures((Dictionary<string, float>)coreInfo, cpu, DateTime.UtcNow);
            this.coreMeasures.Add(coreMeasures);
        }
        addLoadPerThread();
    }

    public void addLoadPerThread()
    {
        IHardware cpuHardware = computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Cpu);
        if (cpuHardware == null)
        {
            return;
        }

        cpuHardware.Update();
        List<ISensor> loadSensors = cpuHardware.Sensors
            .Where(sensor => sensor.SensorType == SensorType.Load)
            .ToList();

        if (loadSensors.Count == 0 || cpuMeasures == null || coreMeasures.Count == 0)
        {
            return;
        }

        DateTime timestamp = DateTime.UtcNow;
        Dictionary<int, CoreMeasures> coreLookup = coreMeasures
            .GroupBy(core => core.CoreID)
            .ToDictionary(group => group.Key, group => group.First());

        foreach (ISensor sensor in loadSensors)
        {
            if (!sensor.Value.HasValue)
            {
                continue;
            }

            if (TryAssignCoreLoad(sensor, timestamp, coreLookup))
            {
                continue;
            }

            string sensorName = sensor.Name ?? string.Empty;
            if (IsTotalLoadSensor(sensorName))
            {
                cpuMeasures.setTotalLoad(sensor.Value.Value);
                continue;
            }

            if (IsMaxLoadSensor(sensorName))
            {
                cpuMeasures.setMaxLoadOfOneCore(sensor.Value.Value);
            }
        }
    }

    private bool TryAssignCoreLoad(ISensor sensor, DateTime timestamp, Dictionary<int, CoreMeasures> coreLookup)
    {
        string sensorName = sensor.Name ?? string.Empty;
        Match match = CoreSensorRegex.Match(sensorName);
        if (!match.Success)
        {
            return false;
        }

        if (!int.TryParse(match.Groups["core"].Value, out int parsedCore))
        {
            return false;
        }

        CoreMeasures? targetCore = SelectCoreForSensor(parsedCore, coreLookup);
        if (targetCore == null)
        {
            return false;
        }

        int threadIdentifier = parsedCore;
        if (match.Groups["thread"].Success && int.TryParse(match.Groups["thread"].Value, out int parsedThread))
        {
            threadIdentifier = parsedThread;
        }

        targetCore.setThreadLoad(sensor.Value!.Value, threadIdentifier, timestamp);
        return true;
    }

    private CoreMeasures? SelectCoreForSensor(int parsedCore, Dictionary<int, CoreMeasures> coreLookup)
    {
        int zeroBasedIndex = parsedCore - 1;
        if (zeroBasedIndex >= 0 && zeroBasedIndex < coreMeasures.Count)
        {
            return coreMeasures[zeroBasedIndex];
        }

        if (coreLookup.TryGetValue(parsedCore, out CoreMeasures exactMatch))
        {
            return exactMatch;
        }

        if (coreLookup.TryGetValue(parsedCore - 1, out CoreMeasures zeroBasedMatch))
        {
            return zeroBasedMatch;
        }

        return null;
    }

    private static bool IsTotalLoadSensor(string sensorName)
    {
        return sensorName.IndexOf("total", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static bool IsMaxLoadSensor(string sensorName)
    {
        return sensorName.IndexOf("max", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public Cpu GetCpu()
    {
        return cpu;
    }
    public CpuMeasures GetCpuMeasures()
    {
        return cpuMeasures;
    }

    public List<CoreMeasures> GetCoreMeasures()
    {
        return coreMeasures;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"CPU: {cpu}");
        sb.AppendLine($"{cpuMeasures}");
        sb.AppendLine("Core Measures:");
        foreach (var core in coreMeasures)
        {
            sb.AppendLine($"  {core}");
        }
        return sb.ToString();
    }
}
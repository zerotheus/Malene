using System.Collections;
using System.Diagnostics;
using System.Text;
using LibreHardwareMonitor.Hardware;

class RyzenCpuCollector
{

    private Cpu cpu { get; set; }
    private CpuMeasures cpuMeasures { get; set; }

    private List<CoreMeasures> coreMeasures { get; set; } = new List<CoreMeasures>();

    private Dictionary<string, ICollection> map;

    private Computer computer;


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
        this.cpuMeasures = new CpuMeasures((Dictionary<string, object>)map.GetValueOrDefault("cpu_measures"), cpu, DateTime.Now);
        ICollection coreMeasuresList = map.GetValueOrDefault("core_measures");
        foreach (var coreInfo in coreMeasuresList)
        {
            CoreMeasures coreMeasures = new CoreMeasures((Dictionary<string, float>)coreInfo, cpu, DateTime.Now);
            this.coreMeasures.Add(coreMeasures);
        }
        addLoadPerThread();
    }

    public void addLoadPerThread()
    {
        IHardware cpu = computer.Hardware.FirstOrDefault(hardware => hardware.HardwareType == HardwareType.Cpu);
        cpu.Update();
        List<ISensor> loadSensors = cpu.Sensors.ToList().FindAll(sensor => sensor.SensorType == SensorType.Load);
        for (int index = 0; index < loadSensors.Count; index++)
        {
            ISensor sensor = loadSensors[index];
            if (index < 12)
            {
                CoreMeasures coreMeasures = this.coreMeasures[index % 6];
                coreMeasures.setThreadLoad(sensor.Value.Value, index, DateTime.Now);
            }
            if (index == 12) { cpuMeasures.setTotalLoad(sensor.Value.Value); }
            if (index == 13) { cpuMeasures.setMaxLoadOfOneCore(sensor.Value.Value); }
        }
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
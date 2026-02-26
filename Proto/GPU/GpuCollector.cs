using LibreHardwareMonitor.Hardware;

class GpuCollector
{
    private readonly List<GPUMeasure> gpuMeasures;

    private readonly Computer computer;

    private static readonly string[] MemoryTotalSensorNames = ["GPU Memory Total"];
    private static readonly string[] MemoryUsedSensorNames = ["GPU Memory Used"];
    private static readonly string[] MemoryFreeSensorNames = ["GPU Memory Free"];
    private static readonly string[] CoreClockSensorNames = ["GPU Core"];
    private static readonly string[] CoreTemperatureSensorNames = ["GPU Core"];
    private static readonly string[] MemoryTemperatureSensorNames = ["GPU Memory"];
    private static readonly string[] HotspotTemperatureSensorNames = ["GPU Hot Spot"];
    private static readonly string[] PowerSensorNames = ["GPU Chip Power", "GPU Core Power", "GPU Total Power", "GPU SOC Power"];
    private static readonly string[] EnergySensorNames = ["GPU Chip Energy", "GPU Total Energy", "GPU SOC Energy"];

    public GpuCollector(Computer computer)
    {
        this.computer = computer;
        gpuMeasures = new List<GPUMeasure>();
    }

    public void CollectData()
    {
        gpuMeasures.Clear();
        List<IHardware> graphicsCards = computer.Hardware
            .Where(h => h.HardwareType == HardwareType.GpuAmd || h.HardwareType == HardwareType.GpuNvidia)
            .ToList();

        foreach (IHardware gpu in graphicsCards)
        {
            UpdateHardwareTree(gpu);
            List<ISensor> sensors = EnumerateSensors(gpu).ToList();
            if (!sensors.Any())
            {
                continue;
            }

            GPUMeasure measure = BuildGpuMeasure(gpu, sensors);
            gpuMeasures.Add(measure);
        }
    }

    private static void UpdateHardwareTree(IHardware hardware)
    {
        hardware.Update();
        foreach (IHardware subHardware in hardware.SubHardware)
        {
            UpdateHardwareTree(subHardware);
        }
    }

    private static IEnumerable<ISensor> EnumerateSensors(IHardware hardware)
    {
        foreach (ISensor sensor in hardware.Sensors)
        {
            yield return sensor;
        }

        foreach (IHardware subHardware in hardware.SubHardware)
        {
            foreach (ISensor sensor in EnumerateSensors(subHardware))
            {
                yield return sensor;
            }
        }
    }

    private static GPUMeasure BuildGpuMeasure(IHardware hardware, List<ISensor> sensors)
    {
        float totalMemory = GetRequiredSensorValue(sensors, SensorType.SmallData, MemoryTotalSensorNames);
        float memoryUsed = GetRequiredSensorValue(sensors, SensorType.SmallData, MemoryUsedSensorNames);
        float memoryFree = GetRequiredSensorValue(sensors, SensorType.SmallData, MemoryFreeSensorNames);
        float gpuCoreClock = GetRequiredSensorValue(sensors, SensorType.Clock, CoreClockSensorNames);
        float gpuCoreTemperature = GetRequiredSensorValue(sensors, SensorType.Temperature, CoreTemperatureSensorNames);
        float memoryTemperature = GetRequiredSensorValue(sensors, SensorType.Temperature, MemoryTemperatureSensorNames);
        float hotspotTemperature = GetRequiredSensorValue(sensors, SensorType.Temperature, HotspotTemperatureSensorNames);
        float? powerDraw = TryGetSensorValue(sensors, SensorType.Power, PowerSensorNames)
            ?? TryGetSensorValue(sensors, SensorType.Power);
        float? energyJoules = TryGetSensorValue(sensors, SensorType.Energy, EnergySensorNames)
            ?? TryGetSensorValue(sensors, SensorType.Energy);

        return new GPUMeasure(hardware.Name, memoryUsed, hotspotTemperature, memoryTemperature, gpuCoreTemperature,
            gpuCoreClock, totalMemory, memoryUsed, memoryFree, powerDraw, energyJoules);
    }

    private static float GetRequiredSensorValue(List<ISensor> sensors, SensorType sensorType, params string[] sensorNames)
    {
        return TryGetSensorValue(sensors, sensorType, sensorNames) ?? 0f;
    }

    private static float? TryGetSensorValue(List<ISensor> sensors, SensorType? sensorType, params string[] sensorNames)
    {
        IEnumerable<ISensor> candidates = sensors;
        if (sensorType.HasValue)
        {
            candidates = candidates.Where(sensor => sensor.SensorType == sensorType.Value);
        }

        if (sensorNames.Length > 0)
        {
            foreach (string sensorName in sensorNames)
            {
                ISensor? match = candidates.FirstOrDefault(sensor => sensor.Name.Equals(sensorName, StringComparison.OrdinalIgnoreCase));
                if (match?.Value is float value)
                {
                    return value;
                }
            }

            return null;
        }

        ISensor? fallback = candidates.FirstOrDefault(sensor => sensor.Value.HasValue);
        return fallback?.Value;
    }

    public List<GPUMeasure> GetGpuMeasures()
    {
        return gpuMeasures;
    }
}

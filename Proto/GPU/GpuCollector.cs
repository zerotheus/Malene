using LibreHardwareMonitor.Hardware;

class GpuCollector
{
    private List<GPUMeasure> gpuMeasures;

    private Computer computer;
    public GpuCollector(Computer computer)
    {
        this.computer = computer;
        gpuMeasures = new List<GPUMeasure>();
    }

    public void CollectData()
    {
        gpuMeasures = new List<GPUMeasure>();
        List<IHardware> graphicsCards = computer.Hardware.Where(h => h.HardwareType == HardwareType.GpuAmd || h.HardwareType == HardwareType.GpuNvidia).ToList();
        IHardware gpu = graphicsCards.First(gpu => gpu.Name == "AMD Radeon RX 7600");
        gpu.Update();
        getDataOfSensors(gpu);
    }

    public void getDataOfSensors(IHardware hardware)
    {
        List<ISensor> sensors = hardware.Sensors.ToList();
        sensors = sensors.FindAll(sensor => IsRelevantSensor(sensor) && isRelevantType(sensor));
        float memoryTotal = sensors.ElementAt(0).Value.GetValueOrDefault();
        float memoryUsed = sensors.ElementAt(1).Value.GetValueOrDefault();
        float memoryFree = sensors.ElementAt(2).Value.GetValueOrDefault();
        float gpuCoreClock = sensors.ElementAt(3).Value.GetValueOrDefault();
        float gpuMemoryClock = sensors.ElementAt(4).Value.GetValueOrDefault();
        float gpuCoreTemperature = sensors.ElementAt(5).Value.GetValueOrDefault();
        float memoryTemperature = sensors.ElementAt(6).Value.GetValueOrDefault();
        float gpuHotSpotTemperature = sensors.ElementAt(7).Value.GetValueOrDefault();

        GPUMeasure gPUMeasure = new("AMD Radeon RX 7600", memoryUsed, gpuHotSpotTemperature, memoryTemperature, gpuCoreTemperature, memoryTotal, memoryUsed, memoryFree);
        gpuMeasures.Add(gPUMeasure);
    }

    private bool IsRelevantSensor(ISensor sensor)
    {
        String[] relevantSensorNames = new String[] {
            "GPU Memory Total",
            "GPU Memory Used",
            "GPU Memory Free",
            "GPU Core",
            "GPU Hot Spot",
            "GPU Memory"
        };
        return relevantSensorNames.Contains(sensor.Name);
    }

    private bool isRelevantType(ISensor sensor)
    {
        SensorType[] relevantType = [SensorType.SmallData, SensorType.Clock, SensorType.Temperature];
        return relevantType.Contains(sensor.SensorType);
    }

    public List<GPUMeasure> GetGpuMeasures()
    {
        return gpuMeasures;
    }
}

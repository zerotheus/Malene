// See https://aka.ms/new-console-template for more information
using LibreHardwareMonitor.Hardware;

Computer computer = new()
{
    IsCpuEnabled = true,
    IsPsuEnabled = false,
    IsGpuEnabled = true,
    IsMemoryEnabled = true,
    IsMotherboardEnabled = false,
    IsStorageEnabled = false,
    IsNetworkEnabled = false,


};

while (true)
{
    computer.Open();

    RyzenCpuCollector cpuCollector = new(computer);
    cpuCollector.collectData();
    Console.WriteLine(cpuCollector.ToString());

    MemoryCollector memoryCollector = new(computer);
    memoryCollector.CollectData();
    Console.WriteLine(memoryCollector.ToString());

    GpuCollector gpuCollector = new(computer);
    gpuCollector.CollectData();
    gpuCollector.GetGpuMeasures().ForEach(m => Console.WriteLine(m.ToString()));

    Console.WriteLine("Detectando hardware...");

    MeleneDbContext meleneDbContext = new();
    Persistency persistency = new(meleneDbContext);
    persistency.persistAll(cpuCollector, memoryCollector, gpuCollector);
    Thread.Sleep(1000);
}


foreach (IHardware hardware in computer.Hardware)
{
    hardware.Update(); // atualizar sensores do componente
    Console.WriteLine($"\n== {hardware.HardwareType} : {hardware.Name}");
    foreach (IHardware sub in hardware.SubHardware)
    {
        sub.Update();
        //PrintSensors(sub);
    }
    PrintSensors(hardware);

}

computer.Close();
Console.WriteLine("\nFim.");


static void PrintSensors(IHardware hw)
{
    // Console.WriteLine(hw.Name);
    Console.WriteLine(hw.Sensors.Length);
    //Console.WriteLine(hw.GetReport());
    foreach (var sensor in hw.Sensors)
    {
        string sensorName = sensor.Name;
        float? value = sensor.Value;
        //Console.WriteLine($"  {sensor.SensorType,-10} | {sensorName,-30} : {(value.HasValue ? $"{value.Value:F1}" : "n/a")}");
    }
}
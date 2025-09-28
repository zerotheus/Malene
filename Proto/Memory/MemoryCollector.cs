using System.Runtime.Intrinsics.X86;
using LibreHardwareMonitor.Hardware;

class MemoryCollector
{
    public MemoryMeasure physicalMemory { get; set; }
    public MemoryMeasure virtualMemory { get; set; }
    private Computer computer { get; set; }


    public MemoryCollector(Computer computer)
    {
        this.computer = computer;
    }

    public void CollectData()
    {
        computer.Hardware.ToList()
            .FindAll(hardware => hardware.HardwareType == HardwareType.Memory)
            .ForEach(setMemoryValues);
    }

    private void setMemoryValues(IHardware memory)
    {
        memory.Update();
        DateTime timeStamp = DateTime.UtcNow;
        float memoryUsed = memory.Sensors.ElementAt(0).Value.Value;
        float memoryAvaliable = memory.Sensors.ElementAt(1).Value.Value;
        float memoryUsagePercentage = memory.Sensors.ElementAt(2).Value.Value;
        if (memory.Name.Equals("Virtual Memory"))
        {
            this.virtualMemory = new("Virtual Memory", timeStamp, memoryUsed, memoryUsagePercentage, memoryAvaliable);
        }
        this.physicalMemory = new("Physical Memory", timeStamp, memoryUsed, memoryUsagePercentage, memoryAvaliable);
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("=== Memory Collector ===");

        if (physicalMemory != null)
        {
            sb.AppendLine($"Physical Memory: {physicalMemory}");
        }
        if (virtualMemory != null)
        {
            sb.AppendLine($"Virtual Memory: {virtualMemory}");
        }

        return sb.ToString().TrimEnd();
    }
}
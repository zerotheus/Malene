using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using LibreHardwareMonitor.Hardware;

public class GPUMeasure
{
    [Key]
    public DateTime TimeStamp { get; set; }
    public string GpuName { get; set; } = string.Empty;
    public float MemoryUsage { get; set; }
    public float TemperatureHotspot { get; set; }

    public float MemoryTemperature { get; set; }

    public float GpuCoreTemperature { get; set; }

    public float GpuCoreClock { get; set; }

    public float TotalMemory { get; set; }

    public float TotalMemoryUsage { get; set; }

    public float FreeMemory { get; set; }

    public float? GpuPowerDrawWatts { get; set; }

    public float? GpuChipEnergyJoules { get; set; }

    public float? GpuLoadPercent { get; set; }

    public GPUMeasure(string gpuName, float memoryUsage, float temperatureHotspot, float memoryTemperature, float gpuCoreTemperature, float gpuCoreClock, float totalMemory, float totalMemoryUsage, float freeMemory, float? gpuPowerDrawWatts = null, float? gpuChipEnergyJoules = null, float? gpuLoadPercent = null)
    {
        this.GpuName = gpuName;
        this.MemoryUsage = memoryUsage;
        this.TemperatureHotspot = temperatureHotspot;
        this.MemoryTemperature = memoryTemperature;
        this.GpuCoreTemperature = gpuCoreTemperature;
        this.GpuCoreClock = gpuCoreClock;
        this.TotalMemory = totalMemory;
        this.TotalMemoryUsage = totalMemoryUsage;
        this.FreeMemory = freeMemory;
        this.GpuPowerDrawWatts = gpuPowerDrawWatts;
        this.GpuChipEnergyJoules = gpuChipEnergyJoules;
        this.GpuLoadPercent = gpuLoadPercent;
        this.TimeStamp = DateTime.UtcNow;
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"=== GPU: {GpuName ?? "Unknown"} ===");

        sb.AppendLine("Memory:");
        sb.AppendLine($"  Usage: {MemoryUsage / TotalMemory * 100:F1}%");
        sb.AppendLine($"  Total: {TotalMemory:F1} GB");
        sb.AppendLine($"  Used: {TotalMemoryUsage:F1} GB");
        sb.AppendLine($"  Free: {FreeMemory:F1} GB");

        sb.AppendLine("Temperature:");
        sb.AppendLine($"  GPU Core: {GpuCoreTemperature:F1}°C");
        sb.AppendLine($"  Memory: {MemoryTemperature:F1}°C");
        sb.AppendLine($"  Hotspot: {TemperatureHotspot:F1}°C");

        sb.AppendLine("Clocks:");
        sb.AppendLine($"  Core Clock: {GpuCoreClock:F0} MHz");

        if (GpuLoadPercent.HasValue)
        {
            sb.AppendLine("Load:");
            sb.AppendLine($"  GPU Load: {GpuLoadPercent.Value:F1}%");
        }

        if (GpuPowerDrawWatts.HasValue || GpuChipEnergyJoules.HasValue)
        {
            sb.AppendLine("Power:");
            if (GpuPowerDrawWatts.HasValue)
            {
                sb.AppendLine($"  Instantaneous: {GpuPowerDrawWatts.Value:F1} W");
            }

            if (GpuChipEnergyJoules.HasValue)
            {
                sb.AppendLine($"  Accumulated: {GpuChipEnergyJoules.Value:F0} J");
            }
        }

        return sb.ToString().TrimEnd();
    }
}
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

    public float TotalMemory { get; set; }

    public float TotalMemoryUsage { get; set; }

    public float FreeMemory { get; set; }

    public GPUMeasure(string gpuName, float memoryUsage, float temperatureHotspot, float memoryTemperature, float gpuCoreTemperature, float totalMemory, float totalMemoryUsage, float freeMemory)
    {
        this.GpuName = gpuName;
        this.MemoryUsage = memoryUsage;
        this.TemperatureHotspot = temperatureHotspot;
        this.MemoryTemperature = memoryTemperature;
        this.GpuCoreTemperature = gpuCoreTemperature;
        this.TotalMemory = totalMemory;
        this.TotalMemoryUsage = totalMemoryUsage;
        this.FreeMemory = freeMemory;
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

        return sb.ToString().TrimEnd();
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CoreMeasures
{
    [Key, Column(Order = 0)]
    public int CoreID { get; set; }
    [Key, Column(Order = 1)]
    public DateTime Timestamp { get; set; }
    public float Frequency { get; set; }
    public float FrequencyEff { get; set; }

    public float C0Residency { get; set; }
    public float Temperature { get; set; }

    public Cpu Cpu { get; set; }

    public CoreThread? PhysicalThread { get; set; }
    public CoreThread? VirtualThread { get; set; }
    public List<CoreThread> CoreThreads { get; set; } = new List<CoreThread>();

    public float Load { get; set; }

    public CoreMeasures(Dictionary<string, float> coreInfo, Cpu cpu, DateTime timestamp)
    {
        this.Cpu = cpu;
        this.Timestamp = timestamp;
        parseCoreInfo(coreInfo);
    }

    private void parseCoreInfo(Dictionary<string, float> coreInfo)
    {
        this.CoreID = (int)coreInfo.GetValueOrDefault("core_id");
        this.Frequency = coreInfo.GetValueOrDefault("frequency_mhz");
        this.FrequencyEff = coreInfo.GetValueOrDefault("frequency_effective_mhz");
        this.C0Residency = coreInfo.GetValueOrDefault("c0_residency_percent");
        this.Temperature = coreInfo.GetValueOrDefault("temperature_celsius");
    }

    public void setThreadLoad(float load, int threadId, DateTime timeStamp)
    {
        if (PhysicalThread == null)
        {
            PhysicalThread = new CoreThread(threadId, load, this);
            CoreThreads.Add(PhysicalThread);
            return;
        }
        VirtualThread = new CoreThread(threadId, load, this);
        CoreThreads.Add(VirtualThread);
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"=== CORE {CoreID} MEASURES ===");
        sb.AppendLine($"Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"Frequency: {Frequency:F1} MHz");
        sb.AppendLine($"Effective Frequency: {FrequencyEff:F1} MHz");
        sb.AppendLine($"C0 Residency: {C0Residency:F1}%");
        sb.AppendLine($"Temperature: {Temperature:F1}°C");
        sb.AppendLine($"Load: {Load:F1}%");
        if (PhysicalThread != null)
        {
            sb.AppendLine($"Physical Thread: {PhysicalThread}");
        }
        if (VirtualThread != null)
        {
            sb.AppendLine($"Virtual Thread: {VirtualThread}");
        }
        return sb.ToString();
    }
}
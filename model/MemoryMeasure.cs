using System.ComponentModel.DataAnnotations;

public class MemoryMeasure
{
    public string MemoryID { get; set; } = string.Empty;
    [Key]
    public DateTime Timestamp { get; set; }
    public float Used { get; set; }

    public float LoadInPercentage { get; set; }

    public float Available { get; set; }
    public float Total { get; set; }

    public MemoryMeasure(string memoryID, DateTime timestamp, float used, float loadInPercentage, float available)
    {
        this.MemoryID = memoryID;
        this.Timestamp = timestamp;
        this.Used = used;
        this.LoadInPercentage = loadInPercentage;
        this.Available = available;
        this.Total = used + available;
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Memory: {MemoryID}");
        sb.AppendLine($"  Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"  Used: {Used:F1} GB ({LoadInPercentage:F1}%)");
        sb.AppendLine($"  Available: {Available:F1} GB");
        sb.AppendLine($"  Total: {Total:F1} GB");

        return sb.ToString().TrimEnd();
    }
}
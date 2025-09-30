using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CoreThread
{
    [Key, Column(Order = 0)]
    public int ThreadID { get; set; }
    public float Load { get; set; }

    [Key, Column(Order = 1)]
    public DateTime TimeStamp { get; set; }
    public int CoreID { get; set; }
    public CoreMeasures CoreMeasures { get; set; }


    public CoreThread(int threadID, float load, CoreMeasures coreMeasures)
    {
        this.ThreadID = threadID;
        this.Load = load;
        this.TimeStamp = DateTime.UtcNow;
        this.CoreMeasures = coreMeasures;
        this.CoreID = coreMeasures.CoreID;
    }

    public override string ToString()
    {
        return $"Thread {ThreadID}: Load={Load:F1}%, Timestamp={TimeStamp:yyyy-MM-dd HH:mm:ss}";
    }
}
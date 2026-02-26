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


    public CoreThread(int threadID, float load, CoreMeasures coreMeasures, DateTime? timeStamp = null)
    {
        this.ThreadID = threadID;
        this.Load = load;
        this.TimeStamp = timeStamp ?? DateTime.UtcNow;
        this.CoreMeasures = coreMeasures;
        this.CoreID = coreMeasures.CoreID;
    }

    public void Update(float load, DateTime timestamp)
    {
        this.Load = load;
        this.TimeStamp = timestamp;
    }

    public override string ToString()
    {
        return $"Thread {ThreadID}: Load={Load:F1}%, Timestamp={TimeStamp:yyyy-MM-dd HH:mm:ss}";
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CoreThread
{
    [Key, Column(Order = 0)]
    public int ThreadID { get; set; }
    public float Load { get; set; }

    [Key, Column(Order = 1)]
    public DateTime TimeStamp { get; set; }

    // Propriedades adicionais para chave estrangeira composta
    public string CoreID { get; set; } = string.Empty;
    public DateTime CoreTimestamp { get; set; }

    public CoreMeasures CoreMeasures { get; set; }


    public CoreThread(int threadID, float load, DateTime timeStamp, CoreMeasures coreMeasures)
    {
        this.ThreadID = threadID;
        this.Load = load;
        this.TimeStamp = timeStamp;
        this.CoreMeasures = coreMeasures;
        this.CoreID = coreMeasures.CoreID;
        this.CoreTimestamp = coreMeasures.Timestamp;
    }

    public override string ToString()
    {
        return $"Thread {ThreadID}: Load={Load:F1}%, Timestamp={TimeStamp:yyyy-MM-dd HH:mm:ss}";
    }
}
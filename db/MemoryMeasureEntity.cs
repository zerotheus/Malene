using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melene.Db
{
    [Table("memory_measures")]
    public class MemoryMeasureEntity
    {
        [Key]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("memory_id")]
        public string MemoryID { get; set; } = string.Empty;

        [Column("used")]
        public float Used { get; set; }

        [Column("load_in_percentage")]
        public float LoadInPercentage { get; set; }

        [Column("available")]
        public float Available { get; set; }

        [Column("total")]
        public float Total { get; set; }

        // Construtor parameterless para EF
        public MemoryMeasureEntity() { }

        // Construtor para criar a partir do modelo
        public MemoryMeasureEntity(MemoryMeasure memoryMeasure)
        {
            Timestamp = memoryMeasure.Timestamp;
            MemoryID = memoryMeasure.MemoryID;
            Used = memoryMeasure.Used;
            LoadInPercentage = memoryMeasure.LoadInPercentage;
            Available = memoryMeasure.Available;
            Total = memoryMeasure.Total;
        }

        // Método para converter para o modelo
        public MemoryMeasure ToModel()
        {
            return new MemoryMeasure(MemoryID, Timestamp, Used, LoadInPercentage, Available)
            {
                MemoryID = this.MemoryID,
                Timestamp = this.Timestamp,
                Used = this.Used,
                LoadInPercentage = this.LoadInPercentage,
                Available = this.Available,
                Total = this.Total
            };
        }
    }
}
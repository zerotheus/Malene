using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melene.Db
{
    [Table("core_measures")]
    public class CoreMeasuresEntity
    {
        [Key, Column("core_id", Order = 0)]
        public string CoreID { get; set; } = string.Empty;

        [Key, Column("timestamp", Order = 1)]
        public DateTime Timestamp { get; set; }

        [Column("cpu_name")]
        [ForeignKey(nameof(Cpu))]
        public string CpuName { get; set; } = string.Empty;

        [Column("frequency")]
        public float Frequency { get; set; }

        [Column("frequency_eff")]
        public float FrequencyEff { get; set; }

        [Column("c0_residency")]
        public float C0Residency { get; set; }

        [Column("temperature")]
        public float Temperature { get; set; }

        [Column("load")]
        public float Load { get; set; }

        // Relacionamentos de navegação
        public virtual CpuEntity Cpu { get; set; } = null!;
        public virtual ICollection<CoreThreadEntity> CoreThreads { get; set; } = new List<CoreThreadEntity>();

        // Construtor parameterless para EF
        public CoreMeasuresEntity() { }

        // Construtor para criar a partir do modelo
        public CoreMeasuresEntity(CoreMeasures coreMeasures)
        {
            CoreID = coreMeasures.CoreID;
            Timestamp = coreMeasures.Timestamp;
            CpuName = coreMeasures.Cpu.Name;
            Frequency = coreMeasures.Frequency;
            FrequencyEff = coreMeasures.FrequencyEff;
            C0Residency = coreMeasures.C0Residency;
            Temperature = coreMeasures.Temperature;
            Load = coreMeasures.Load;
            CoreThreads.Add(new CoreThreadEntity(coreMeasures.PhysicalThread));
            CoreThreads.Add(new CoreThreadEntity(coreMeasures.VirtualThread));
        }

        // Método para converter para o modelo
        public CoreMeasures ToModel()
        {
            var cpu = Cpu?.ToModel() ?? new Cpu { Name = CpuName };

            return new CoreMeasures(new Dictionary<string, float>(), cpu, Timestamp)
            {
                CoreID = this.CoreID,
                Frequency = this.Frequency,
                FrequencyEff = this.FrequencyEff,
                C0Residency = this.C0Residency,
                Temperature = this.Temperature,
                Load = this.Load
            };
        }
    }
}
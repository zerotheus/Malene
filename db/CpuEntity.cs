using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melene.Db
{
    [Table("cpus")]
    public class CpuEntity
    {
        [Key]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("core_count")]
        public int CoreCount { get; set; }

        [Column("l1_size")]
        public string L1Size { get; set; } = string.Empty;

        [Column("l1_instruction")]
        public string L1Instruction { get; set; } = string.Empty;

        [Column("l2_size")]
        public string L2Size { get; set; } = string.Empty;

        [Column("l3_size")]
        public string L3Size { get; set; } = string.Empty;

        [Column("socket")]
        public string Socket { get; set; } = string.Empty;

        [Column("frequency_limit")]
        public float FrequencyLimit { get; set; }

        // Relacionamentos de navegação
        public virtual ICollection<CpuMeasuresEntity> CpuMeasures { get; set; } = new List<CpuMeasuresEntity>();
        public virtual ICollection<CoreMeasuresEntity> CoreMeasures { get; set; } = new List<CoreMeasuresEntity>();

        // Construtor parameterless para EF
        public CpuEntity() { }

        // Construtor para criar a partir do modelo
        public CpuEntity(Cpu cpu)
        {
            Name = cpu.Name;
            CoreCount = cpu.CoreCount;
            L1Size = cpu.L1Size;
            L1Instruction = cpu.L1Instruction;
            L2Size = cpu.L2Size;
            L3Size = cpu.L3Size;
            Socket = cpu.Socket;
            FrequencyLimit = cpu.FrequencyLimit;
        }

        // Método para converter para o modelo
        public Cpu ToModel()
        {
            return new Cpu
            {
                Name = this.Name,
                CoreCount = this.CoreCount,
                L1Size = this.L1Size,
                L1Instruction = this.L1Instruction,
                L2Size = this.L2Size,
                L3Size = this.L3Size,
                Socket = this.Socket,
                FrequencyLimit = this.FrequencyLimit
            };
        }
    }
}
    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melene.Db
{
    [Table("core_threads")]
    public class CoreThreadEntity
    {
        [Key, Column("thread_id", Order = 0)]
        public int ThreadID { get; set; }

        [Key, Column("time_stamp", Order = 1)]
        public DateTime TimeStamp { get; set; }

        [Column("load")]
        public float Load { get; set; }

        // Chaves estrangeiras compostas para CoreMeasures
        [Column("core_id")]
        public string CoreID { get; set; } = string.Empty;

        [Column("core_timestamp")]
        public DateTime CoreTimestamp { get; set; }

        // Relacionamento de navegação
        public virtual CoreMeasuresEntity CoreMeasures { get; set; } = null!;

        // Construtor parameterless para EF
        public CoreThreadEntity() { }

        // Construtor para criar a partir do modelo
        public CoreThreadEntity(CoreThread coreThread)
        {
            ThreadID = coreThread.ThreadID;
            TimeStamp = coreThread.TimeStamp;
            Load = coreThread.Load;
            CoreID = coreThread.CoreID;
            CoreTimestamp = coreThread.CoreTimestamp;
        }

        // Método para converter para o modelo (simplificado)
        public CoreThread ToModel()
        {
            // Como CoreThread tem um construtor complexo, criamos uma versão simplificada
            return new CoreThread(ThreadID, Load, TimeStamp, CoreMeasures?.ToModel() ?? new CoreMeasures(new Dictionary<string, float>(), new Cpu(), TimeStamp))
            {
                ThreadID = this.ThreadID,
                Load = this.Load,
                TimeStamp = this.TimeStamp,
                CoreID = this.CoreID,
                CoreTimestamp = this.CoreTimestamp
            };
        }
    }
}
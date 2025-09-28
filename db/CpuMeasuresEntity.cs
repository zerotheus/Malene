using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melene.Db
{
    [Table("cpu_measures")]
    public class CpuMeasuresEntity
    {
        [Key]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("cpu_name")]
        [ForeignKey(nameof(Cpu))]
        public string CpuName { get; set; } = string.Empty;

        [Column("ppt_current_value")]
        public float PptCurrentValue { get; set; }

        [Column("ppt_current_limit")]
        public float PptCurrentLimit { get; set; }

        [Column("edc_current_limit")]
        public string EdcCurrentLimit { get; set; } = string.Empty;

        [Column("edc_current_value")]
        public string EdcCurrentValue { get; set; } = string.Empty;

        [Column("tdc_current_limit")]
        public string TdcCurrentLimit { get; set; } = string.Empty;

        [Column("tdc_current_value")]
        public string TdcCurrentValue { get; set; } = string.Empty;

        [Column("chtc_limit")]
        public string CHTCLimit { get; set; } = string.Empty;

        [Column("vddcr_power")]
        public float VDDCRPower { get; set; }

        [Column("vddcr_soc_power")]
        public float VDDCRSOCPower { get; set; }

        [Column("current_mode")]
        public string CurrentMode { get; set; } = string.Empty;

        [Column("peak_core_speed")]
        public float PeakCoreSpeed { get; set; }

        [Column("core_speed_average")]
        public float CoreSpeedAverage { get; set; }

        [Column("peak_core_voltage")]
        public float PeakCoreVoltage { get; set; }

        [Column("temperature")]
        public float Temperature { get; set; }

        [Column("total_load")]
        public float TotalLoad { get; set; }

        [Column("max_load_of_one_core")]
        public float MaxLoadOfOneCore { get; set; }

        // Relacionamento de navegação
        public virtual CpuEntity Cpu { get; set; } = null!;

        // Construtor parameterless para EF
        public CpuMeasuresEntity() { }

        // Construtor para criar a partir do modelo
        public CpuMeasuresEntity(CpuMeasures cpuMeasures)
        {
            Timestamp = cpuMeasures.Timestamp;
            CpuName = cpuMeasures.Cpu.Name;
            PptCurrentValue = cpuMeasures.PptCurrentValue;
            PptCurrentLimit = cpuMeasures.PptCurrentLimit;
            EdcCurrentLimit = cpuMeasures.EdcCurrentLimit;
            EdcCurrentValue = cpuMeasures.EdcCurrentValue;
            TdcCurrentLimit = cpuMeasures.TdcCurrentLimit;
            TdcCurrentValue = cpuMeasures.TdcCurrentValue;
            CHTCLimit = cpuMeasures.CHTCLimit;
            VDDCRPower = cpuMeasures.VDDCRPower;
            VDDCRSOCPower = cpuMeasures.VDDCRSOCPower;
            CurrentMode = cpuMeasures.CurrentMode;
            PeakCoreSpeed = cpuMeasures.PeakCoreSpeed;
            CoreSpeedAverage = cpuMeasures.CoreSpeedAverage;
            PeakCoreVoltage = cpuMeasures.PeakCoreVoltage;
            Temperature = cpuMeasures.Temperature;
            TotalLoad = cpuMeasures.TotalLoad;
            MaxLoadOfOneCore = cpuMeasures.MaxLoadOfOneCore;
        }

        // Método para converter para o modelo
        public CpuMeasures ToModel()
        {
            var cpu = Cpu?.ToModel() ?? new Cpu { Name = CpuName };

            return new CpuMeasures(new Dictionary<string, object>(), cpu, Timestamp)
            {
                PptCurrentValue = this.PptCurrentValue,
                PptCurrentLimit = this.PptCurrentLimit,
                EdcCurrentLimit = this.EdcCurrentLimit,
                EdcCurrentValue = this.EdcCurrentValue,
                TdcCurrentLimit = this.TdcCurrentLimit,
                TdcCurrentValue = this.TdcCurrentValue,
                CHTCLimit = this.CHTCLimit,
                VDDCRPower = this.VDDCRPower,
                VDDCRSOCPower = this.VDDCRSOCPower,
                CurrentMode = this.CurrentMode,
                PeakCoreSpeed = this.PeakCoreSpeed,
                CoreSpeedAverage = this.CoreSpeedAverage,
                PeakCoreVoltage = this.PeakCoreVoltage,
                Temperature = this.Temperature,
                TotalLoad = this.TotalLoad,
                MaxLoadOfOneCore = this.MaxLoadOfOneCore
            };
        }
    }
}
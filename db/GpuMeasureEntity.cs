using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melene.Db
{
    [Table("gpu_measures")]
    public class GpuMeasureEntity
    {
        [Key]
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        [Column("gpu_name")]
        public string GpuName { get; set; } = string.Empty;

        [Column("memory_usage")]
        public float MemoryUsage { get; set; }

        [Column("temperature_hotspot")]
        public float TemperatureHotspot { get; set; }

        [Column("memory_temperature")]
        public float MemoryTemperature { get; set; }

        [Column("gpu_core_temperature")]
        public float GpuCoreTemperature { get; set; }

        [Column("total_memory")]
        public float TotalMemory { get; set; }

        [Column("total_memory_usage")]
        public float TotalMemoryUsage { get; set; }

        [Column("free_memory")]
        public float FreeMemory { get; set; }

        // Construtor parameterless para EF
        public GpuMeasureEntity() { }

        // Construtor para criar a partir do modelo
        public GpuMeasureEntity(GPUMeasure gpuMeasure)
        {
            TimeStamp = gpuMeasure.TimeStamp;
            GpuName = gpuMeasure.GpuName;
            MemoryUsage = gpuMeasure.MemoryUsage;
            TemperatureHotspot = gpuMeasure.TemperatureHotspot;
            MemoryTemperature = gpuMeasure.MemoryTemperature;
            GpuCoreTemperature = gpuMeasure.GpuCoreTemperature;
            TotalMemory = gpuMeasure.TotalMemory;
            TotalMemoryUsage = gpuMeasure.TotalMemoryUsage;
            FreeMemory = gpuMeasure.FreeMemory;
        }

        // Método para converter para o modelo
        public GPUMeasure ToModel()
        {
            return new GPUMeasure(GpuName, MemoryUsage, TemperatureHotspot, MemoryTemperature,
                                 GpuCoreTemperature, TotalMemory, TotalMemoryUsage, FreeMemory)
            {
                TimeStamp = this.TimeStamp,
                GpuName = this.GpuName,
                MemoryUsage = this.MemoryUsage,
                TemperatureHotspot = this.TemperatureHotspot,
                MemoryTemperature = this.MemoryTemperature,
                GpuCoreTemperature = this.GpuCoreTemperature,
                TotalMemory = this.TotalMemory,
                TotalMemoryUsage = this.TotalMemoryUsage,
                FreeMemory = this.FreeMemory
            };
        }
    }
}
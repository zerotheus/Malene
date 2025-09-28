using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MeleneDbContext : DbContext
{
    // DbSets para cada entidade
    public DbSet<Cpu> Cpus { get; set; }
    public DbSet<CpuMeasures> CpuMeasures { get; set; }
    public DbSet<CoreMeasures> CoreMeasures { get; set; }
    public DbSet<CoreThread> CoreThreads { get; set; }
    public DbSet<MemoryMeasure> MemoryMeasures { get; set; }
    public DbSet<GPUMeasure> GpuMeasures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuração para PostgreSQL
        optionsBuilder.UseNpgsql("Host=localhost;Database=hardware;Username=postgres;Password=acwr");

        // Configuração para logs (opcional - remover em produção)
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração da entidade Cpu
        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.HasKey(e => e.Name);
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CoreCount).HasColumnName("core_count");
            entity.Property(e => e.L1Size).HasColumnName("l1_size");
            entity.Property(e => e.L1Instruction).HasColumnName("l1_instruction");
            entity.Property(e => e.L2Size).HasColumnName("l2_size");
            entity.Property(e => e.L3Size).HasColumnName("l3_size");
            entity.Property(e => e.Socket).HasColumnName("socket");
            entity.Property(e => e.FrequencyLimit).HasColumnName("frequency_limit");
        });

        // Configuração da entidade CpuMeasures
        modelBuilder.Entity<CpuMeasures>(entity =>
        {
            entity.HasKey(e => e.Timestamp);
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.PptCurrentValue).HasColumnName("ppt_current_value");
            entity.Property(e => e.PptCurrentLimit).HasColumnName("ppt_current_limit");
            entity.Property(e => e.EdcCurrentLimit).HasColumnName("edc_current_limit");
            entity.Property(e => e.EdcCurrentValue).HasColumnName("edc_current_value");
            entity.Property(e => e.TdcCurrentLimit).HasColumnName("tdc_current_limit");
            entity.Property(e => e.TdcCurrentValue).HasColumnName("tdc_current_value");
            entity.Property(e => e.CHTCLimit).HasColumnName("chtc_limit");
            entity.Property(e => e.VDDCRPower).HasColumnName("vddcr_power");
            entity.Property(e => e.VDDCRSOCPower).HasColumnName("vddcr_soc_power");
            entity.Property(e => e.CurrentMode).HasColumnName("current_mode");
            entity.Property(e => e.PeakCoreSpeed).HasColumnName("peak_core_speed");
            entity.Property(e => e.CoreSpeedAverage).HasColumnName("core_speed_average");
            entity.Property(e => e.PeakCoreVoltage).HasColumnName("peak_core_voltage");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.TotalLoad).HasColumnName("total_load");
            entity.Property(e => e.MaxLoadOfOneCore).HasColumnName("max_load_of_one_core");
            
            // Relacionamento com Cpu
            entity.HasOne(e => e.Cpu)
                  .WithMany()
                  .HasForeignKey("CpuName")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração da entidade CoreMeasures
        modelBuilder.Entity<CoreMeasures>(entity =>
        {
            entity.HasKey(e => new { e.CoreID, e.Timestamp });
            entity.Property(e => e.CoreID).HasColumnName("core_id");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.Frequency).HasColumnName("frequency");
            entity.Property(e => e.FrequencyEff).HasColumnName("frequency_eff");
            entity.Property(e => e.C0Residency).HasColumnName("c0_residency");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.Load).HasColumnName("load");

            // Relacionamento com Cpu
            entity.HasOne(e => e.Cpu)
                  .WithMany()
                  .HasForeignKey("CpuName")
                  .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com CoreThread
            entity.HasMany(e => e.CoreThreads)
                  .WithOne(ct => ct.CoreMeasures)
                  .HasForeignKey(ct => new { ct.CoreID, ct.CoreTimestamp })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração da entidade CoreThread
        modelBuilder.Entity<CoreThread>(entity =>
        {
            entity.HasKey(e => new { e.ThreadID, e.TimeStamp });
            entity.Property(e => e.ThreadID).HasColumnName("thread_id");
            entity.Property(e => e.Load).HasColumnName("load");
            entity.Property(e => e.TimeStamp).HasColumnName("time_stamp");
            entity.Property(e => e.CoreID).HasColumnName("core_id");
            entity.Property(e => e.CoreTimestamp).HasColumnName("core_timestamp");
        });

        // Configuração da entidade MemoryMeasure
        modelBuilder.Entity<MemoryMeasure>(entity =>
        {
            entity.HasKey(e => e.Timestamp);
            entity.Property(e => e.MemoryID).HasColumnName("memory_id");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.Used).HasColumnName("used");
            entity.Property(e => e.LoadInPercentage).HasColumnName("load_in_percentage");
            entity.Property(e => e.Available).HasColumnName("available");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        // Configuração da entidade GPUMeasure
        modelBuilder.Entity<GPUMeasure>(entity =>
        {
            entity.HasKey(e => e.TimeStamp);
            entity.Property(e => e.TimeStamp).HasColumnName("time_stamp");
            entity.Property(e => e.GpuName).HasColumnName("gpu_name");
            entity.Property(e => e.MemoryUsage).HasColumnName("memory_usage");
            entity.Property(e => e.TemperatureHotspot).HasColumnName("temperature_hotspot");
            entity.Property(e => e.MemoryTemperature).HasColumnName("memory_temperature");
            entity.Property(e => e.GpuCoreTemperature).HasColumnName("gpu_core_temperature");
            entity.Property(e => e.TotalMemory).HasColumnName("total_memory");
            entity.Property(e => e.TotalMemoryUsage).HasColumnName("total_memory_usage");
            entity.Property(e => e.FreeMemory).HasColumnName("free_memory");
        });

        base.OnModelCreating(modelBuilder);
    }
}
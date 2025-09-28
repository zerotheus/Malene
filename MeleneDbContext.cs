using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Melene.Db;

public class MeleneDbContext : DbContext
{
    // DbSets para cada entidade de banco de dados
    public DbSet<CpuEntity> Cpus { get; set; }
    public DbSet<CpuMeasuresEntity> CpuMeasures { get; set; }
    public DbSet<CoreMeasuresEntity> CoreMeasures { get; set; }
    public DbSet<CoreThreadEntity> CoreThreads { get; set; }
    public DbSet<MemoryMeasureEntity> MemoryMeasures { get; set; }
    public DbSet<GpuMeasureEntity> GpuMeasures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuração para PostgreSQL
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=hardware;Username=postgres;Password=acwr;Include Error Detail=True");

        // Configuração para logs (opcional - remover em produção)
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração da entidade CpuEntity
        modelBuilder.Entity<CpuEntity>(entity =>
        {
            entity.HasKey(e => e.Name);

            // Relacionamentos
            entity.HasMany(e => e.CpuMeasures)
                  .WithOne(cm => cm.Cpu)
                  .HasForeignKey(cm => cm.CpuName)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.CoreMeasures)
                  .WithOne(cm => cm.Cpu)
                  .HasForeignKey(cm => cm.CpuName)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração da entidade CpuMeasuresEntity
        modelBuilder.Entity<CpuMeasuresEntity>(entity =>
        {
            entity.HasKey(e => e.Timestamp);

            // Relacionamento com CpuEntity
            entity.HasOne(e => e.Cpu)
                  .WithMany(c => c.CpuMeasures)
                  .HasForeignKey(e => e.CpuName)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração da entidade CoreMeasuresEntity
        modelBuilder.Entity<CoreMeasuresEntity>(entity =>
        {
            entity.HasKey(e => new { e.CoreID, e.Timestamp });

            // Relacionamento com CpuEntity
            entity.HasOne(e => e.Cpu)
                  .WithMany(c => c.CoreMeasures)
                  .HasForeignKey(e => e.CpuName)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com CoreThreadEntity
            entity.HasMany(e => e.CoreThreads)
                  .WithOne(ct => ct.CoreMeasures)
                  .HasForeignKey(ct => new { ct.CoreID, ct.CoreTimestamp })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração da entidade CoreThreadEntity
        modelBuilder.Entity<CoreThreadEntity>(entity =>
        {
            entity.HasKey(e => new { e.ThreadID, e.TimeStamp });

            // Relacionamento com CoreMeasuresEntity
            entity.HasOne(e => e.CoreMeasures)
                  .WithMany(cm => cm.CoreThreads)
                  .HasForeignKey(e => new { e.CoreID, e.CoreTimestamp })
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração da entidade MemoryMeasureEntity
        modelBuilder.Entity<MemoryMeasureEntity>(entity =>
        {
            entity.HasKey(e => e.Timestamp);
        });

        // Configuração da entidade GpuMeasureEntity
        modelBuilder.Entity<GpuMeasureEntity>(entity =>
        {
            entity.HasKey(e => e.TimeStamp);
        });

        base.OnModelCreating(modelBuilder);
    }
}
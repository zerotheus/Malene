using System.Data.Common;
using Melene.Db;

class Persistency
{
    private readonly MeleneDbContext _dbContext;

    public Persistency(MeleneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CpuEntity? GetCpuByName(string cpuName)
    {
        return _dbContext.Cpus.FirstOrDefault(cpu => cpu.Name == cpuName);
    }


    public void SaveCpuData(CpuEntity cpu)
    {
        if (GetCpuByName(cpu.Name) == null)
        {
            _dbContext.Cpus.Add(cpu);
            _dbContext.SaveChanges();
            return;
        }
        Console.WriteLine("Cpu data Already exists.");
    }

    public void SaveCpuMeasuresData(CpuMeasuresEntity cpuMeasures)
    {
        _dbContext.CpuMeasures.Add(cpuMeasures);
        _dbContext.SaveChanges();
    }

    public void SaveMemoryData(MemoryMeasureEntity memory)
    {
        _dbContext.MemoryMeasures.Add(memory);
        _dbContext.SaveChanges();
    }

    public void SaveGpuData(GpuMeasureEntity gpu)
    {
        _dbContext.GpuMeasures.Add(gpu);
        _dbContext.SaveChanges();
    }

    public void SaveCoreData(CoreMeasuresEntity core)
    {
        _dbContext.CoreMeasures.Add(core);
        _dbContext.SaveChanges();
    }

    public void SaveCoreEntity(CoreThreadEntity coreThread)
    {
        _dbContext.CoreThreads.Add(coreThread);
        _dbContext.SaveChanges();
    }

    public void SaveCoreThread(CoreThreadEntity coreThread)
    {
        _dbContext.CoreThreads.Add(coreThread);
        _dbContext.SaveChanges();
    }

    public void persistAll(RyzenCpuCollector cpuCollector, MemoryCollector memoryCollector, GpuCollector gpuCollector)
    {
        CpuEntity cpu = new CpuEntity(cpuCollector.GetCpu());
        CpuMeasuresEntity cpuMeasuresEntity = new CpuMeasuresEntity(cpuCollector.GetCpuMeasures());
        List<CoreMeasures> cores = cpuCollector.GetCoreMeasures();
        List<CoreMeasuresEntity> coreMeasuresEntities = cores.Select(core => new CoreMeasuresEntity(core)).ToList();
        List<CoreThreadEntity> threadsToPersist = new();
        coreMeasuresEntities.ForEach(core =>
        {
            core.CoreThreads.ToList().ForEach(coreThread => threadsToPersist.Add(coreThread));
        });
        MemoryMeasureEntity memoryMeasureEntity = new MemoryMeasureEntity(memoryCollector.virtualMemory);
        MemoryMeasureEntity physicalMemoryEntity = new MemoryMeasureEntity(memoryCollector.physicalMemory);
        List<GpuMeasureEntity> gpuMeasureEntities = gpuCollector.GetGpuMeasures().Select(gpu => new GpuMeasureEntity(gpu)).ToList();
        SaveCpuData(cpu);
        SaveCpuMeasuresData(cpuMeasuresEntity);
        SaveMemoryData(memoryMeasureEntity);
        SaveMemoryData(physicalMemoryEntity);
        coreMeasuresEntities.ForEach(SaveCoreData);
        //threadsToPersist.ForEach(SaveCoreThread);
        gpuMeasureEntities.ForEach(SaveGpuData);


    }
}
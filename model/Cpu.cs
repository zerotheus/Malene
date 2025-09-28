

using System.ComponentModel.DataAnnotations;

public class Cpu
{
    [Key]
    public string Name { get; set; } = string.Empty;

    public int CoreCount { get; set; }

    public string L1Size { get; set; } = string.Empty;
    public string L1Instruction { get; set; } = string.Empty;

    public string L2Size { get; set; } = string.Empty;
    public string L3Size { get; set; } = string.Empty;

    public string Socket { get; set; } = string.Empty;

    public float FrequencyLimit { get; set; }


    public Cpu() { }
    public Cpu(Dictionary<string, object> cpuInfo)
    {
        parseProcessorInfo(cpuInfo);
    }

    private void parseProcessorInfo(Dictionary<string, object> cpuInfo)
    {
        this.Name = cpuInfo.GetValueOrDefault("name")?.ToString() ?? string.Empty;
        this.CoreCount = cpuInfo.GetValueOrDefault("core_count") is int coreCount ? coreCount : 0;
        this.FrequencyLimit = cpuInfo.GetValueOrDefault("max_frequency_mhz") is float frequency ? frequency : 0f;
        this.Socket = cpuInfo.GetValueOrDefault("socket")?.ToString() ?? string.Empty;
        var cacheInfo = cpuInfo.GetValueOrDefault("cache") as Dictionary<string, string>;
        if (cacheInfo != null)
            this.parseCacheInfo(cacheInfo);
        Console.WriteLine(this.Name);
    }

    private void parseCacheInfo(Dictionary<string, string> cache)
    {
        this.L1Size = cache.GetValueOrDefault("l1_data") ?? string.Empty;
        this.L1Instruction = cache.GetValueOrDefault("l1_instruction") ?? string.Empty;
        this.L2Size = cache.GetValueOrDefault("l2") ?? string.Empty;
        this.L3Size = cache.GetValueOrDefault("l3") ?? string.Empty;
    }


    public override string ToString()
    {
        return $"CPU: {Name}, Cores: {CoreCount}, Frequency Limit: {FrequencyLimit} MHz, L1: {L1Size}, L1 Instruction: {L1Instruction}, L2: {L2Size}, L3: {L3Size}, Socket: {Socket}";
    }

}
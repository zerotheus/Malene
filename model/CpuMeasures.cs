using System.ComponentModel.DataAnnotations;

public class CpuMeasures
{
    public Cpu Cpu { get; set; }
    [Key]
    public DateTime Timestamp { get; set; }
    public float PptCurrentValue { get; set; }

    public float PptCurrentLimit { get; set; }

    public string EdcCurrentLimit { get; set; } = string.Empty;
    public string EdcCurrentValue { get; set; } = string.Empty;

    public string TdcCurrentLimit { get; set; } = string.Empty;

    public string TdcCurrentValue { get; set; } = string.Empty;

    public string CHTCLimit { get; set; } = string.Empty;

    public float VDDCRPower { get; set; }

    public float VDDCRSOCPower { get; set; }

    public string CurrentMode { get; set; } = string.Empty;

    public float PeakCoreSpeed { get; set; }

    public float CoreSpeedAverage { get; set; }

    public float PeakCoreVoltage { get; set; }
    public float Temperature { get; set; }

    public float TotalLoad { get; set; }

    public float MaxLoadOfOneCore { get; set; }

    public CpuMeasures(Dictionary<string, object> cpuInfo, Cpu cpu, DateTime timestamp)
    {
        this.Cpu = cpu;
        this.Timestamp = timestamp;
        this.CurrentMode = cpuInfo.GetValueOrDefault("mode")?.ToString() ?? string.Empty;

        var powerMeasures = cpuInfo.GetValueOrDefault("power") as Dictionary<string, float>;
        if (powerMeasures != null) parsePowerMeasures(powerMeasures);

        var temperatureMeasures = cpuInfo.GetValueOrDefault("temperature") as Dictionary<string, float>;
        if (temperatureMeasures != null) parseTemperatureMeasures(temperatureMeasures);

        var currentMeasures = cpuInfo.GetValueOrDefault("current") as Dictionary<string, float>;
        if (currentMeasures != null) parseCurrent(currentMeasures);

        var voltageMeasures = cpuInfo.GetValueOrDefault("voltage") as Dictionary<string, float>;
        if (voltageMeasures != null) parseVoltageMeasures(voltageMeasures);

        var frequencyMeasures = cpuInfo.GetValueOrDefault("frequency") as Dictionary<string, float>;
        if (frequencyMeasures != null) parseFrequencyMeasures(frequencyMeasures);
    }

    private void parseFrequencyMeasures(Dictionary<string, float> frequencyMeasures)
    {
        this.PeakCoreSpeed = frequencyMeasures.GetValueOrDefault("peak_speed_mhz");
    }

    private void parsePowerMeasures(Dictionary<string, float> powerMeasures)
    {
        this.VDDCRPower = powerMeasures.GetValueOrDefault("vddcr_power_w");
        this.PptCurrentLimit = powerMeasures.GetValueOrDefault("ppt_limit_w");
        this.PptCurrentValue = powerMeasures.GetValueOrDefault("ppt_current_w");
        this.VDDCRSOCPower = powerMeasures.GetValueOrDefault("vddcr_soc_power_w");
    }

    private void parseTemperatureMeasures(Dictionary<string, float> temperatureMeasures)
    {
        this.Temperature = temperatureMeasures.GetValueOrDefault("current_celsius");
    }

    private void parseCurrent(Dictionary<string, float> currentMeasures)
    {
        EdcCurrentValue = currentMeasures.GetValueOrDefault("edc_current_a").ToString();
        EdcCurrentLimit = currentMeasures.GetValueOrDefault("edc_limit_a").ToString();
        TdcCurrentValue = currentMeasures.GetValueOrDefault("tdc_limit_a").ToString();
        TdcCurrentLimit = currentMeasures.GetValueOrDefault("tdc_current_a").ToString();
    }

    private void parseVoltageMeasures(Dictionary<string, float> voltageMeasures)
    {
        this.PeakCoreVoltage = voltageMeasures.GetValueOrDefault("peak_core_voltage_v");
        this.CoreSpeedAverage = voltageMeasures.GetValueOrDefault("average_core_voltage_v");
        this.VDDCRSOCPower = voltageMeasures.GetValueOrDefault("vddcr_soc_v");
    }

    public void setTotalLoad(float totalLoad)
    {
        this.TotalLoad = totalLoad;
    }

    public void setMaxLoadOfOneCore(float load)
    {
        this.MaxLoadOfOneCore = load;
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("=== CPU MEASURES ===");
        sb.AppendLine($"Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"Mode: {CurrentMode ?? "N/A"}");

        sb.AppendLine("\n--- POWER ---");
        sb.AppendLine($"PPT Current: {PptCurrentValue:F1} W");
        sb.AppendLine($"PPT Limit: {PptCurrentLimit:F1} W");
        sb.AppendLine($"VDDCR Power: {VDDCRPower:F1} W");
        sb.AppendLine($"VDDCR SOC Power: {VDDCRSOCPower:F1} W");

        sb.AppendLine("\n--- CURRENT ---");
        sb.AppendLine($"EDC Current: {EdcCurrentValue ?? "N/A"} A");
        sb.AppendLine($"EDC Limit: {EdcCurrentLimit ?? "N/A"} A");
        sb.AppendLine($"TDC Current: {TdcCurrentValue ?? "N/A"} A");
        sb.AppendLine($"TDC Limit: {TdcCurrentLimit ?? "N/A"} A");

        sb.AppendLine("\n--- TEMPERATURE ---");
        sb.AppendLine($"Current: {Temperature:F1} °C");
        sb.AppendLine($"cHTC Limit: {CHTCLimit ?? "N/A"}");

        sb.AppendLine("\n--- FREQUENCY ---");
        sb.AppendLine($"Peak Core Speed: {PeakCoreSpeed:F1} MHz");
        sb.AppendLine($"Core Speed Average: {CoreSpeedAverage:F1} MHz");

        sb.AppendLine("\n--- VOLTAGE ---");
        sb.AppendLine($"Peak Core Voltage: {PeakCoreVoltage:F3} V");

        return sb.ToString();
    }

}
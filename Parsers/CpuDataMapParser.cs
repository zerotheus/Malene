using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

class CpuDataMapParser
{
    public static Dictionary<string, ICollection> ParseToMap(string cpuOutput)
    {
        var result = new Dictionary<string, ICollection>
        {
            ["cpu_info"] = ParseCpuInfoToMap(cpuOutput),
            ["cpu_measures"] = ParseCpuMeasuresToMap(cpuOutput),
            ["core_measures"] = ParseCoreMeasuresToMap(cpuOutput),
            ["memory_info"] = ParseMemoryInfoToMap(cpuOutput),
            ["system_info"] = ParseSystemInfoToMap(cpuOutput),
        };
        return result;
    }

    public static Dictionary<string, object> ParseCpuInfoToMap(string cpuOutput)
    {
        return new Dictionary<string, object>
        {
            ["name"] = ExtractValue(cpuOutput, @"GetName\s+\.+\s*(.+)") ?? "Unknown",
            ["family"] = ExtractIntValue(cpuOutput, @"GetFamily\s+\.+\s*(\d+)") ?? 0,
            ["model"] = ExtractIntValue(cpuOutput, @"GetModel\s+\.+\s*(\d+)") ?? 0,
            ["stepping"] = ExtractIntValue(cpuOutput, @"GetStepping\s+\.+\s*(\d+)") ?? 0,
            ["vendor"] = ExtractValue(cpuOutput, @"GetVendor\s+\.+\s*(.+)") ?? "Unknown",
            ["class_name"] = ExtractValue(cpuOutput, @"GetClassName\s+\.+\s*(.+)") ?? "Unknown",
            ["core_count"] = ExtractIntValue(cpuOutput, @"GetCoreCount\s+\.+\s*(\d+)") ?? 0,
            ["cores_parked"] = ExtractIntValue(cpuOutput, @"GetCorePark\s+\.+\s*(\d+)") ?? 0,
            ["socket"] = ExtractValue(cpuOutput, @"GetPackage\s+\.+\s*(.+)") ?? "Unknown",
            ["max_frequency_mhz"] = ExtractFloatValue(cpuOutput, @"Fmax\(CPU Clock\) frequency\s+:\s*([\d.]+)") ?? 0f,
            ["cache"] = new Dictionary<string, string>
            {
                ["l1_data"] = ExtractValue(cpuOutput, @"GetL1DataCache\s+\.+\s*(.+)") ?? "Unknown",
                ["l1_instruction"] = ExtractValue(cpuOutput, @"GetL1InstructionCache\s+\.+\s*(.+)") ?? "Unknown",
                ["l2"] = ExtractValue(cpuOutput, @"GetL2Cache\s+\.+\s*(.+)") ?? "Unknown",
                ["l3"] = ExtractValue(cpuOutput, @"GetL3Cache\s+\.+\s*(.+)") ?? "Unknown"
            }
        };
    }

    public static Dictionary<string, object> ParseCpuMeasuresToMap(string cpuOutput)
    {
        return new Dictionary<string, object>
        {
            ["power"] = new Dictionary<string, float>
            {
                ["ppt_limit_w"] = ExtractFloatValue(cpuOutput, @"PPT Current Limit\s+:\s*([\d.]+)") ?? 0f,
                ["ppt_current_w"] = ExtractFloatValue(cpuOutput, @"PPT Current Value\s+:\s*([\d.]+)") ?? 0f,
                ["vddcr_vdd_power_w"] = ExtractFloatValue(cpuOutput, @"VDDCR\(VDD\) Power\s+:\s*([\d.]+)") ?? 0f,
                ["vddcr_soc_power_w"] = ExtractFloatValue(cpuOutput, @"VDDCR\(SOC\) Power\s+:\s*([\d.]+)") ?? 0f
            },
            ["current"] = new Dictionary<string, float>
            {
                ["edc_limit_a"] = ExtractFloatValue(cpuOutput, @"EDC\(VDD\) Current Limit\s+:\s*([\d.]+)") ?? 0f,
                ["edc_current_a"] = ExtractFloatValue(cpuOutput, @"EDC\(VDD\) Current Value\s+:\s*([\d.]+)") ?? 0f,
                ["tdc_limit_a"] = ExtractFloatValue(cpuOutput, @"TDC\(VDD\) Current Limit\s+:\s*([\d.]+)") ?? 0f,
                ["tdc_current_a"] = ExtractFloatValue(cpuOutput, @"TDC\(VDD\) Current Value\s+:\s*([\d.]+)") ?? 0f
            },
            ["temperature"] = new Dictionary<string, float>
            {
                ["current_celsius"] = ExtractFloatValue(cpuOutput, @"GetCurrentTemperature\s+\.+\s*([\d.]+)") ?? 0f,
                ["chtc_limit_celsius"] = ExtractFloatValue(cpuOutput, @"cHTC Limit\s+:\s*([\d.]+)") ?? 0f
            },
            ["voltage"] = new Dictionary<string, float>
            {
                ["peak_core_voltage_v"] = ExtractFloatValue(cpuOutput, @"GetPeakCore\(s\)Voltage\s+\.+\s*([\d.]+)") ?? 0f,
                ["average_core_voltage_v"] = ExtractFloatValue(cpuOutput, @"GetAverageCoreVoltage\s+\.+\s*([\d.]+)") ?? 0f,
                ["vddcr_soc_v"] = ExtractFloatValue(cpuOutput, @"GetVDDCR_SOC\s+\.+\s*([\d.]+)") ?? 0f
            },
            ["frequency"] = new Dictionary<string, float>
            {
                ["fabric_clock_mhz"] = ExtractFloatValue(cpuOutput, @"Fabric Clock Frequency\s+:\s*([\d.]+)") ?? 0f,
                ["peak_speed_mhz"] = ExtractFloatValue(cpuOutput, @"GetPeakSpeed\s+\.+\s*([\d.]+)") ?? 0f
            },
            ["mode"] = ExtractValue(cpuOutput, @"GetCurrentOCMode\s+\.+\s*(.+)") ?? "Unknown"
        };
    }

    public static List<Dictionary<string, float>> ParseCoreMeasuresToMap(string cpuOutput)
    {
        var coresList = new List<Dictionary<string, float>>();

        var corePattern = @"(\d+)\s+([\d.]+)\s+([\d.]+)\s+([\d.]+)\s+([\d.]+)";
        var matches = Regex.Matches(cpuOutput, corePattern);

        foreach (Match match in matches)
        {
            if (match.Groups.Count >= 6)
            {
                var coreData = new Dictionary<string, float>
                {
                    ["core_id"] = int.Parse(match.Groups[1].Value),
                    ["frequency_mhz"] = ParseFloat(match.Groups[2].Value),
                    ["effective_frequency_mhz"] = ParseFloat(match.Groups[3].Value),
                    ["c0_residency_percent"] = ParseFloat(match.Groups[4].Value),
                    ["temperature_celsius"] = ParseFloat(match.Groups[5].Value),
                    ["load_percent"] = ParseFloat(match.Groups[4].Value), // C0 como aproximação do load
                };
                coresList.Add(coreData);
            }
        }
        return coresList;
    }
    public static Dictionary<string, object> ParseMemoryInfoToMap(string cpuOutput)
    {
        return new Dictionary<string, object>
        {
            ["vddio_mv"] = ExtractIntValue(cpuOutput, @"GetMemVDDIO\s+\.+\s*(\d+)") ?? 0,
            ["current_clock_mhz"] = ExtractIntValue(cpuOutput, @"GetCurrentMemClock\s+\.+\s*(\d+)") ?? 0,
            ["timings"] = new Dictionary<string, int>
            {
                ["tcl_cycles"] = ExtractIntValue(cpuOutput, @"GetMemCtrlTcl\s+\.+\s*(\d+)") ?? 0,
                ["tras_cycles"] = ExtractIntValue(cpuOutput, @"GetMemCtrlTras\s+\.+\s*(\d+)") ?? 0,
                ["trp_cycles"] = ExtractIntValue(cpuOutput, @"GetMemCtrlTrp\s+\.+\s*(\d+)") ?? 0
            }
        };
    }
    public static Dictionary<string, object> ParseSystemInfoToMap(string cpuOutput)
    {
        var biosMatch = Regex.Match(cpuOutput, @"GetBIOSInfo\s+\.+\s*Version\s*:\s*([^,]+),\s*Vendor\s*:\s*([^,]+),\s*Date\s*:\s*(.+)");

        return new Dictionary<string, object>
        {
            ["chipset"] = ExtractValue(cpuOutput, @"GetChipsetName\s+\.+\s*(.+)") ?? "Unknown",
            ["bios"] = new Dictionary<string, string>
            {
                ["version"] = biosMatch.Success ? biosMatch.Groups[1].Value.Trim() : "Unknown",
                ["vendor"] = biosMatch.Success ? biosMatch.Groups[2].Value.Trim() : "Unknown",
                ["date"] = biosMatch.Success ? biosMatch.Groups[3].Value.Trim() : "Unknown"
            }
        };
    }
    public static void DisplayMap(Dictionary<string, ICollection> dataMap)
    {
        Console.WriteLine("🖥️  DADOS DA CPU EM MAP");
        Console.WriteLine(new string('=', 50));

        DisplayMapSection("CPU INFO", (Dictionary<string, object>)dataMap["cpu_info"]);
        DisplayMapSection("CPU MEASURES", (Dictionary<string, object>)dataMap["cpu_measures"]);
        DisplayMapSection("MEMORY INFO", (Dictionary<string, object>)dataMap["memory_info"]);
        DisplayMapSection("SYSTEM INFO", (Dictionary<string, object>)dataMap["system_info"]);

        var cores = (List<Dictionary<string, float>>)dataMap["core_measures"];
        Console.WriteLine("\n📊 CORE MEASURES:");
        foreach (var core in cores)
        {
            Console.WriteLine($"  Core {core["core_id"]}: {core["frequency_mhz"]}MHz, {core["temperature_celsius"]}°C, {core["c0_residency_percent"]}%");
        }
    }
    private static void DisplayMapSection(string title, Dictionary<string, object> section)
    {
        Console.WriteLine($"\n📋 {title}:");
        DisplayMapRecursive(section, "  ");
    }

    private static void DisplayMapRecursive(Dictionary<string, object> dict, string indent)
    {
        foreach (var kvp in dict)
        {
            if (kvp.Value is Dictionary<string, object> subDict)
            {
                Console.WriteLine($"{indent}{kvp.Key}:");
                DisplayMapRecursive(subDict, indent + "  ");
            }
            else
            {
                Console.WriteLine($"{indent}{kvp.Key}: {kvp.Value}");
            }
        }
    }

    private static string? ExtractValue(string input, string pattern)
    {
        var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : null;
    }

    private static int? ExtractIntValue(string input, string pattern)
    {
        var value = ExtractValue(input, pattern);
        return int.TryParse(value, out int result) ? result : null;
    }

    private static float? ExtractFloatValue(string input, string pattern)
    {
        var value = ExtractValue(input, pattern);
        return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? result : null;
    }

    private static float ParseFloat(string value)
    {
        return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? result : 0f;
    }

}
# 🖥️ CPU Data Map Parser - Documentação

## 📋 Visão Geral

O `CpuDataMapParser` é uma classe que converte o output de informações da CPU AMD Ryzen em Maps (Dicionários) estruturados para fácil consumo e manipulação dos dados.

## 🚀 Como Usar

### 1. Parse Completo para Map

```csharp
string cpuOutput = "..."; // Seu output da CPU
var dataMap = CpuDataMapParser.ParseToMap(cpuOutput);

// Agora você tem um Map estruturado com todas as informações!
```

### 2. Acesso Fácil aos Dados

```csharp
// Informações básicas da CPU
var cpuInfo = (Dictionary<string, object>)dataMap["cpu_info"];
string cpuName = cpuInfo["name"].ToString();
int coreCount = (int)cpuInfo["core_count"];

// Medições de potência
var cpuMeasures = (Dictionary<string, object>)dataMap["cpu_measures"];
var power = (Dictionary<string, object>)cpuMeasures["power"];
float currentPower = (float)power["ppt_current_w"];

// Informações por núcleo
var cores = (List<Dictionary<string, object>>)dataMap["core_measures"];
foreach (var core in cores)
{
    int coreId = (int)core["core_id"];
    float frequency = (float)core["frequency_mhz"];
    float temperature = (float)core["temperature_celsius"];
}
```

### 3. Converter para Classes Modelo

```csharp
var (cpu, measures, cores) = CpuDataMapParser.CreateModelObjects(dataMap);
// Agora você tem objetos das classes Cpu, CpuMeasures e List<CoreMeasures>
```

## 📊 Estrutura do Map Retornado

```
dataMap
├── cpu_info
│   ├── name: "AMD Ryzen 5 7600X 6-Core Processor"
│   ├── core_count: 6
│   ├── socket: "Socket AM5"
│   ├── max_frequency_mhz: 5400.0
│   └── cache
│       ├── l1_data: "6 x 32KB"
│       ├── l2: "6 x 1024KB"
│       └── l3: "32768 KB"
├── cpu_measures
│   ├── power
│   │   ├── ppt_limit_w: 142.0
│   │   ├── ppt_current_w: 48.5
│   │   ├── vddcr_vdd_power_w: 26.8
│   │   └── vddcr_soc_power_w: 10.9
│   ├── temperature
│   │   ├── current_celsius: 59.0
│   │   └── chtc_limit_celsius: 85.0
│   ├── voltage
│   │   ├── peak_core_voltage_v: 1.248
│   │   └── average_core_voltage_v: 1.248
│   └── frequency
│       ├── fabric_clock_mhz: 2000.0
│       └── peak_speed_mhz: 2514.5
├── core_measures (Lista)
│   ├── [0]
│   │   ├── core_id: 0
│   │   ├── frequency_mhz: 4914.6
│   │   ├── effective_frequency_mhz: 1323.1
│   │   ├── c0_residency_percent: 26.9
│   │   └── temperature_celsius: 45.6
│   └── [1...5] (outros cores)
├── memory_info
│   ├── vddio_mv: 1250
│   ├── current_clock_mhz: 2600
│   └── timings
│       ├── tcl_cycles: 40
│       ├── tras_cycles: 80
│       └── trp_cycles: 40
├── system_info
│   ├── chipset: "AMD A620"
│   └── bios
│       ├── version: "2.02"
│       ├── vendor: "American Megatrends International, LLC."
│       └── date: "2023/11/17"
└── timestamp: DateTime.Now
```

## 🎯 Métodos Disponíveis

### Parsing Individual
```csharp
// Parse apenas informações básicas da CPU
var cpuInfo = CpuDataMapParser.ParseCpuInfoToMap(cpuOutput);

// Parse apenas medições atuais
var measures = CpuDataMapParser.ParseCpuMeasuresToMap(cpuOutput);

// Parse apenas cores
var cores = CpuDataMapParser.ParseCoreMeasuresToMap(cpuOutput);

// Parse apenas memória
var memory = CpuDataMapParser.ParseMemoryInfoToMap(cpuOutput);

// Parse apenas sistema
var system = CpuDataMapParser.ParseSystemInfoToMap(cpuOutput);
```

### Utilitários
```csharp
// Exibir Map de forma organizada
CpuDataMapParser.DisplayMap(dataMap);

// Converter Map para objetos das classes modelo
var (cpu, measures, cores) = CpuDataMapParser.CreateModelObjects(dataMap);
```

## 🔧 Exemplos Práticos

### Exemplo 1: Monitoramento Básico
```csharp
var dataMap = CpuDataMapParser.ParseToMap(cpuOutput);
var cpuMeasures = (Dictionary<string, object>)dataMap["cpu_measures"];
var temperature = (Dictionary<string, object>)cpuMeasures["temperature"];
var currentTemp = (float)temperature["current_celsius"];

if (currentTemp > 80)
{
    Console.WriteLine("⚠️ CPU muito quente!");
}
```

### Exemplo 2: Análise de Performance
```csharp
var cores = (List<Dictionary<string, object>>)dataMap["core_measures"];
var avgFreq = cores.Average(c => (float)c["frequency_mhz"]);
var maxFreq = cores.Max(c => (float)c["frequency_mhz"]);

Console.WriteLine($"Freq. Média: {avgFreq:F1}MHz | Máxima: {maxFreq:F1}MHz");
```

### Exemplo 3: Encontrar Core Mais Ativo
```csharp
var cores = (List<Dictionary<string, object>>)dataMap["core_measures"];
var mostActiveCore = cores
    .OrderByDescending(c => (float)c["c0_residency_percent"])
    .First();

int coreId = (int)mostActiveCore["core_id"];
float usage = (float)mostActiveCore["c0_residency_percent"];
Console.WriteLine($"Core mais ativo: {coreId} ({usage:F1}%)");
```

## 🧪 Como Testar

### Teste Simples
```csharp
CpuMapExample.RunExample(); // Executa exemplo completo
```

### Teste com Seus Dados
```csharp
string meuCpuOutput = "..."; // Seu output real
CpuMapExample.RunPartialExample(meuCpuOutput);
```

### Testing Class
```csharp
TestCpuParser.TestWithSampleData(); // Usa dados de exemplo
TestCpuParser.TestWithRealData(meuOutput); // Usa seus dados
```

## ✅ Vantagens do Parser de Map

1. **🚀 Acesso Direto**: Não precisa de reflection para acessar dados
2. **📊 Estruturado**: Dados organizados hierarquicamente
3. **🔧 Flexível**: Pode acessar qualquer campo facilmente
4. **📈 Escalável**: Fácil de adicionar novos campos
5. **💾 Serializável**: Pode converter para JSON facilmente
6. **🎯 Tipado**: Cada valor tem seu tipo correto (int, float, string)

## 📱 Integração no Seu Projeto

Para usar no seu `Program.cs`:

```csharp
// Onde você tem o output da CPU
string cpuOutput = GetCpuOutput(); // Seu método que obtém os dados

// Parse para Map
var dataMap = CpuDataMapParser.ParseToMap(cpuOutput);

// Use os dados como precisar
var cpuInfo = (Dictionary<string, object>)dataMap["cpu_info"];
Console.WriteLine($"CPU: {cpuInfo["name"]} ({cpuInfo["core_count"]} cores)");

// OU criar objetos das classes modelo
var (cpu, measures, cores) = CpuDataMapParser.CreateModelObjects(dataMap);
```

Agora você tem acesso fácil e estruturado a todas as informações da sua CPU AMD Ryzen! 🎉
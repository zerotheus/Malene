# Monitor de Clock da CPU por Núcleo

Este diretório contém scripts Python para monitorar o clock (frequência) de cada núcleo da CPU em tempo real.

## Arquivos

- `advanced_cpu_monitor.py` - 🌟 **RECOMENDADO** - Monitor avançado com todos os núcleos
- `cpu_clock_monitor.py` - Script completo com informações detalhadas
- `simple_cpu_monitor.py` - Script simples e direto
- `executar_monitor.bat` - Arquivo batch para Windows (duplo-click para executar)
- `requirements.txt` - Dependências necessárias

## Instalação

1. Certifique-se de ter Python 3.6+ instalado
2. Instale as dependências:
   ```
   pip install -r requirements.txt
   ```

## Como usar

### Script Avançado (🌟 RECOMENDADO)
```bash
python advanced_cpu_monitor.py
```
**OU no Windows:** Duplo-click em `executar_monitor.bat`

### Script Simples
```bash
python simple_cpu_monitor.py
```

### Script Completo
```bash
python cpu_clock_monitor.py
```

## O que os scripts mostram

- **Clock atual** de cada núcleo em MHz
- **Uso** de cada núcleo em percentual
- **Média** de clock e uso
- **Atualização em tempo real** (a cada 2 segundos)

## Comandos úteis

- **Ctrl+C** - Para o monitoramento
- Os dados são atualizados automaticamente

## Notas importantes

- No **Windows**: Pode usar informações do WMI para dados mais precisos
- No **Linux**: Usa informações do `/proc/cpuinfo` e psutil
- **CPUs modernas**: Podem não mostrar clock individual por núcleo devido ao power management
- **AMD Ryzen**: Em idle, pode mostrar clocks baixos ou iguais para todos os núcleos

## Exemplo de saída

```
Monitor de Clock dos Núcleos da CPU
========================================
Sistema: Windows
Núcleos: 12

[14:30:15]
Core | Clock (MHz) | Uso (%)
------------------------------
   0 |       3600 |    5.2
   1 |       3600 |   12.1
   2 |       3600 |    8.5
   3 |       3600 |   15.3
   ...

Média: 3600 MHz | 8.5%
```

## Troubleshooting

- Se não conseguir ver clocks individuais, o script mostrará informações gerais da CPU
- Alguns sistemas podem precisar de privilégios administrativos
- Em sistemas virtuais, as informações podem ser limitadas
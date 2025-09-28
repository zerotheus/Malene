#!/usr/bin/env python3
"""
Monitor avançado de clock da CPU por núcleo
Versão melhorada que detecta todos os núcleos
"""

import psutil
import time
import platform
import subprocess
import json

def get_detailed_cpu_info():
    """Obtém informações detalhadas da CPU no Windows"""
    if platform.system() == "Windows":
        try:
            # PowerShell command para obter informações da CPU
            ps_cmd = '''
            Get-WmiObject -Class Win32_Processor | Select-Object Name, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed | ConvertTo-Json
            '''
            result = subprocess.run(
                ["powershell", "-Command", ps_cmd],
                capture_output=True, text=True, shell=True
            )
            if result.returncode == 0:
                return json.loads(result.stdout)
        except:
            pass
    return None

def monitor_cpu_cores():
    """Monitor principal dos núcleos da CPU"""
    print("🖥️  MONITOR AVANÇADO DE CPU POR NÚCLEO")
    print("=" * 50)
    
    # Informações do sistema
    cpu_info = get_detailed_cpu_info()
    if cpu_info and not isinstance(cpu_info, list):
        print(f"CPU: {cpu_info.get('Name', 'N/A')}")
        print(f"Velocidade máxima: {cpu_info.get('MaxClockSpeed', 'N/A')} MHz")
    
    print(f"Sistema: {platform.system()} {platform.release()}")
    print(f"Núcleos físicos: {psutil.cpu_count(logical=False)}")
    print(f"Núcleos lógicos: {psutil.cpu_count(logical=True)}")
    print(f"Timestamp: {time.strftime('%Y-%m-%d %H:%M:%S')}")
    print("\nPressione Ctrl+C para parar\n")
    
    try:
        while True:
            # Obter dados de todos os núcleos
            cpu_percent = psutil.cpu_percent(interval=1, percpu=True)
            cpu_freq = psutil.cpu_freq(percpu=True)
            
            print(f"⏰ [{time.strftime('%H:%M:%S')}]")
            print("─" * 50)
            
            if cpu_freq and len(cpu_freq) > 1:
                # Mostrar dados por núcleo quando disponível
                print(f"{'Núcleo':<8} {'Clock (MHz)':<12} {'Uso (%)':<10}")
                print("─" * 35)
                
                total_freq = 0
                valid_cores = 0
                
                for i, (freq, usage) in enumerate(zip(cpu_freq, cpu_percent)):
                    if freq and freq.current:
                        print(f"{i:<8} {freq.current:<12.0f} {usage:<10.1f}")
                        total_freq += freq.current
                        valid_cores += 1
                    else:
                        print(f"{i:<8} {'N/A':<12} {usage:<10.1f}")
                
                # Estatísticas
                print("─" * 35)
                if valid_cores > 0:
                    avg_freq = total_freq / valid_cores
                    print(f"Freq. Média: {avg_freq:.0f} MHz")
                
                avg_usage = sum(cpu_percent) / len(cpu_percent)
                max_usage = max(cpu_percent)
                min_usage = min(cpu_percent)
                
                print(f"Uso Médio: {avg_usage:.1f}% | Max: {max_usage:.1f}% | Min: {min_usage:.1f}%")
                
            else:
                # Fallback para sistemas que não suportam freq por núcleo
                general_freq = psutil.cpu_freq()
                print(f"{'Núcleo':<8} {'Clock (MHz)':<12} {'Uso (%)':<10}")
                print("─" * 35)
                
                for i, usage in enumerate(cpu_percent):
                    freq_display = f"{general_freq.current:.0f}" if general_freq else "N/A"
                    print(f"{i:<8} {freq_display:<12} {usage:<10.1f}")
                
                print("─" * 35)
                if general_freq:
                    print(f"Frequência geral: {general_freq.current:.0f} MHz")
                
                avg_usage = sum(cpu_percent) / len(cpu_percent)
                print(f"Uso médio: {avg_usage:.1f}%")
            
            print("=" * 50)
            time.sleep(2)
            
    except KeyboardInterrupt:
        print("\n\n✅ Monitor finalizado pelo usuário.")
    except Exception as e:
        print(f"\n❌ Erro: {e}")

if __name__ == "__main__":
    monitor_cpu_cores()
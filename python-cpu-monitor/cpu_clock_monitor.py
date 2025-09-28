#!/usr/bin/env python3
"""
Script para monitorar o clock de cada núcleo da CPU
Funciona no Windows, Linux e macOS
"""

import psutil
import time
import platform
import subprocess
import json
from typing import Dict, List, Optional

def get_cpu_freq_per_core() -> List[Dict]:
    """
    Obtém a frequência de cada núcleo da CPU
    """
    try:
        # psutil.cpu_freq() pode retornar frequência por core em alguns sistemas
        freq_info = psutil.cpu_freq(percpu=True)
        
        if freq_info:
            cores_info = []
            for i, freq in enumerate(freq_info):
                cores_info.append({
                    'core': i,
                    'current_mhz': round(freq.current, 2) if freq.current else 0,
                    'min_mhz': round(freq.min, 2) if freq.min else 0,
                    'max_mhz': round(freq.max, 2) if freq.max else 0
                })
            return cores_info
        else:
            # Fallback: frequência geral da CPU
            general_freq = psutil.cpu_freq()
            if general_freq:
                cpu_count = psutil.cpu_count(logical=True)
                cores_info = []
                for i in range(cpu_count):
                    cores_info.append({
                        'core': i,
                        'current_mhz': round(general_freq.current, 2),
                        'min_mhz': round(general_freq.min, 2) if general_freq.min else 0,
                        'max_mhz': round(general_freq.max, 2) if general_freq.max else 0
                    })
                return cores_info
    except Exception as e:
        print(f"Erro ao obter frequência da CPU: {e}")
        return []

def get_cpu_usage_per_core() -> List[float]:
    """
    Obtém o uso de cada núcleo da CPU
    """
    try:
        return psutil.cpu_percent(interval=1, percpu=True)
    except Exception as e:
        print(f"Erro ao obter uso da CPU: {e}")
        return []

def get_windows_cpu_info():
    """
    Método alternativo para Windows usando WMI
    """
    try:
        if platform.system() == "Windows":
            # Usando PowerShell para obter informações detalhadas
            ps_command = """
            Get-WmiObject -Class Win32_Processor | Select-Object Name, MaxClockSpeed, CurrentClockSpeed, NumberOfCores, NumberOfLogicalProcessors | ConvertTo-Json
            """
            
            result = subprocess.run(
                ["powershell", "-Command", ps_command],
                capture_output=True,
                text=True,
                shell=True
            )
            
            if result.returncode == 0:
                data = json.loads(result.stdout)
                return data
    except Exception as e:
        print(f"Erro ao obter informações do Windows: {e}")
    return None

def display_cpu_info():
    """
    Exibe informações completas da CPU
    """
    print("=" * 60)
    print("MONITOR DE CLOCK DA CPU POR NÚCLEO")
    print("=" * 60)
    
    # Informações gerais da CPU
    print(f"Sistema: {platform.system()} {platform.release()}")
    print(f"Processador: {platform.processor()}")
    print(f"Núcleos físicos: {psutil.cpu_count(logical=False)}")
    print(f"Núcleos lógicos: {psutil.cpu_count(logical=True)}")
    
    # Tentar método do Windows primeiro
    if platform.system() == "Windows":
        windows_info = get_windows_cpu_info()
        if windows_info:
            if isinstance(windows_info, list):
                windows_info = windows_info[0]
            print(f"Velocidade máxima: {windows_info.get('MaxClockSpeed', 'N/A')} MHz")
            print(f"Velocidade atual (base): {windows_info.get('CurrentClockSpeed', 'N/A')} MHz")
    
    print("\n" + "-" * 60)
    print(" MONITORAMENTO EM TEMPO REAL")
    print("-" * 60)
    
    try:
        while True:
            # Obter frequências por núcleo
            cores_freq = get_cpu_freq_per_core()
            cores_usage = get_cpu_usage_per_core()
            
            print(f"\n[{time.strftime('%H:%M:%S')}]")
            
            if cores_freq and cores_usage:
                print(f"{'Core':<6} {'Clock (MHz)':<12} {'Uso (%)':<8}")
                print("-" * 30)
                
                for i, (freq_info, usage) in enumerate(zip(cores_freq, cores_usage)):
                    core_num = freq_info['core']
                    current_freq = freq_info['current_mhz']
                    
                    print(f"{core_num:<6} {current_freq:<12} {usage:<8.1f}")
                
                # Estatísticas gerais
                if cores_freq:
                    avg_freq = sum(f['current_mhz'] for f in cores_freq) / len(cores_freq)
                    max_freq = max(f['current_mhz'] for f in cores_freq)
                    min_freq = min(f['current_mhz'] for f in cores_freq)
                    
                    print("-" * 30)
                    print(f"Média: {avg_freq:.1f} MHz | Max: {max_freq:.1f} MHz | Min: {min_freq:.1f} MHz")
                
                if cores_usage:
                    avg_usage = sum(cores_usage) / len(cores_usage)
                    print(f"Uso médio da CPU: {avg_usage:.1f}%")
            else:
                print("Não foi possível obter informações detalhadas dos núcleos")
                # Fallback para informações básicas
                try:
                    general_freq = psutil.cpu_freq()
                    general_usage = psutil.cpu_percent(interval=1)
                    
                    if general_freq:
                        print(f"Frequência geral: {general_freq.current:.1f} MHz")
                    print(f"Uso geral da CPU: {general_usage:.1f}%")
                except:
                    print("Erro ao obter informações básicas da CPU")
            
            time.sleep(2)  # Atualizar a cada 2 segundos
            
    except KeyboardInterrupt:
        print("\n\nMonitoramento interrompido pelo usuário.")
    except Exception as e:
        print(f"\nErro durante o monitoramento: {e}")

def main():
    """
    Função principal
    """
    try:
        display_cpu_info()
    except Exception as e:
        print(f"Erro fatal: {e}")

if __name__ == "__main__":
    main()
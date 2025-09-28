# Entidades de Banco de Dados - Melene

Este documento descreve as entidades de banco de dados criadas na pasta `db/` baseadas nos modelos de domínio da pasta `model/`.

## Arquitetura

A separação entre **Modelos de Domínio** (`model/`) e **Entidades de Banco** (`db/`) oferece:

- **Separação de responsabilidades**: Modelos focam na lógica de negócio, entidades focam na persistência
- **Flexibilidade**: Mudanças no banco não afetam a lógica de negócio
- **Mapeamento limpo**: Conversão bidirecional entre domínio e persistência
- **Testabilidade**: Modelos podem ser testados independentemente do banco

## Entidades Criadas

### 1. CpuEntity
- **Tabela**: `cpus`
- **Chave Primária**: `Name` (string)
- **Relacionamentos**: 
  - 1:N com `CpuMeasuresEntity`
  - 1:N com `CoreMeasuresEntity`

### 2. CpuMeasuresEntity
- **Tabela**: `cpu_measures`
- **Chave Primária**: `Timestamp` (DateTime)
- **Chave Estrangeira**: `CpuName` → `CpuEntity.Name`
- **Relacionamentos**:
  - N:1 com `CpuEntity`

### 3. CoreMeasuresEntity
- **Tabela**: `core_measures`
- **Chave Primária Composta**: `CoreID` + `Timestamp`
- **Chave Estrangeira**: `CpuName` → `CpuEntity.Name`
- **Relacionamentos**:
  - N:1 com `CpuEntity`
  - 1:N com `CoreThreadEntity`

### 4. CoreThreadEntity
- **Tabela**: `core_threads`
- **Chave Primária Composta**: `ThreadID` + `TimeStamp`
- **Chave Estrangeira Composta**: `CoreID` + `CoreTimestamp` → `CoreMeasuresEntity`
- **Relacionamentos**:
  - N:1 com `CoreMeasuresEntity`

### 5. MemoryMeasureEntity
- **Tabela**: `memory_measures`
- **Chave Primária**: `Timestamp` (DateTime)
- **Relacionamentos**: Nenhum (entidade independente)

### 6. GpuMeasureEntity
- **Tabela**: `gpu_measures`
- **Chave Primária**: `TimeStamp` (DateTime)
- **Relacionamentos**: Nenhum (entidade independente)

## Métodos de Conversão

Cada entidade possui:

- **Construtor parameterless**: Para Entity Framework
- **Construtor com modelo**: `new EntityName(ModelName model)`
- **Método ToModel()**: Converte entidade para modelo de domínio

## Exemplo de Uso

```csharp
// Converter modelo para entidade (para salvar no banco)
var cpu = new Cpu("AMD Ryzen 7");
var cpuEntity = new CpuEntity(cpu);
await context.Cpus.AddAsync(cpuEntity);

// Converter entidade para modelo (para lógica de negócio)
var cpuFromDb = await context.Cpus.FirstAsync();
var cpuModel = cpuFromDb.ToModel();
```

## Configuração no DbContext

O `MeleneDbContext` foi atualizado para:

- Usar `DbSet<EntityName>` ao invés de `DbSet<ModelName>`
- Configurar relacionamentos usando Fluent API
- Manter integridade referencial com cascading deletes

## Vantagens da Separação

1. **Evolução independente**: Modelos e entidades podem evoluir separadamente
2. **Performance**: Entidades podem ter otimizações específicas para o banco
3. **Versionamento**: Diferentes versões do modelo podem usar as mesmas entidades
4. **Manutenibilidade**: Código mais limpo e organizado
5. **Testabilidade**: Testes unitários não dependem do banco de dados

## Estrutura de Pastas

```
Melene/
├── model/           # Modelos de domínio (lógica de negócio)
│   ├── Cpu.cs
│   ├── CpuMeasures.cs
│   ├── CoreMeasures.cs
│   ├── CoreThread.cs
│   ├── MemoryMeasure.cs
│   └── GpuMeasures.cs
├── db/              # Entidades de banco de dados (persistência)
│   ├── CpuEntity.cs
│   ├── CpuMeasuresEntity.cs
│   ├── CoreMeasuresEntity.cs
│   ├── CoreThreadEntity.cs
│   ├── MemoryMeasureEntity.cs
│   └── GpuMeasureEntity.cs
└── MeleneDbContext.cs
```

Esta arquitetura garante que o sistema seja maintível, testável e flexível para futuras mudanças.
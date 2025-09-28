using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melene.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "core_threads",
                columns: table => new
                {
                    threadID = table.Column<int>(type: "integer", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    load = table.Column<float>(type: "real", nullable: false),
                    CoreMeasuresId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_threads", x => new { x.threadID, x.timeStamp });
                });

            migrationBuilder.CreateTable(
                name: "cpus",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    coreCount = table.Column<int>(type: "integer", nullable: false),
                    l1Size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    l1Instruction = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    l2Size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    l3Size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    socket = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    frequencyLimit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpus", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "gpu_measures",
                columns: table => new
                {
                    timeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gpuName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    memoryUsage = table.Column<float>(type: "real", nullable: false),
                    temperatureHotspot = table.Column<float>(type: "real", nullable: false),
                    memoryTemperature = table.Column<float>(type: "real", nullable: false),
                    gpuCoreTemperature = table.Column<float>(type: "real", nullable: false),
                    totalMemory = table.Column<float>(type: "real", nullable: false),
                    totalMemoryUsage = table.Column<float>(type: "real", nullable: false),
                    freeMemory = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gpu_measures", x => x.timeStamp);
                });

            migrationBuilder.CreateTable(
                name: "memory_measures",
                columns: table => new
                {
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    memoryID = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    used = table.Column<float>(type: "real", nullable: false),
                    loadInPercentage = table.Column<float>(type: "real", nullable: false),
                    available = table.Column<float>(type: "real", nullable: false),
                    total = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_memory_measures", x => x.timestamp);
                });

            migrationBuilder.CreateTable(
                name: "core_measures",
                columns: table => new
                {
                    coreID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    frequency = table.Column<float>(type: "real", nullable: false),
                    frequencyEff = table.Column<float>(type: "real", nullable: false),
                    c0Residency = table.Column<float>(type: "real", nullable: false),
                    temperature = table.Column<float>(type: "real", nullable: false),
                    CpuName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    load = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_measures", x => new { x.coreID, x.timestamp });
                    table.ForeignKey(
                        name: "FK_core_measures_cpus_CpuName",
                        column: x => x.CpuName,
                        principalTable: "cpus",
                        principalColumn: "name");
                });

            migrationBuilder.CreateTable(
                name: "cpu_measures",
                columns: table => new
                {
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CpuName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    pptCurrentValue = table.Column<float>(type: "real", nullable: false),
                    pptCurrentLimit = table.Column<float>(type: "real", nullable: false),
                    edcCurrentLimit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    edcCurrentValue = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tdcCurrentLimit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tdcCurrentValue = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    cHTCLimit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    VDDCRPower = table.Column<float>(type: "real", nullable: false),
                    VDDCRSOCPower = table.Column<float>(type: "real", nullable: false),
                    currentMode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    peakCoreSpeed = table.Column<float>(type: "real", nullable: false),
                    coreSpeedAverage = table.Column<float>(type: "real", nullable: false),
                    peakCoreVoltage = table.Column<float>(type: "real", nullable: false),
                    temperature = table.Column<float>(type: "real", nullable: false),
                    totalLoad = table.Column<float>(type: "real", nullable: false),
                    maxLoadOfOneCore = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpu_measures", x => x.timestamp);
                    table.ForeignKey(
                        name: "FK_cpu_measures_cpus_CpuName",
                        column: x => x.CpuName,
                        principalTable: "cpus",
                        principalColumn: "name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_measures_CpuName",
                table: "core_measures",
                column: "CpuName");

            migrationBuilder.CreateIndex(
                name: "IX_cpu_measures_CpuName",
                table: "cpu_measures",
                column: "CpuName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_measures");

            migrationBuilder.DropTable(
                name: "core_threads");

            migrationBuilder.DropTable(
                name: "cpu_measures");

            migrationBuilder.DropTable(
                name: "gpu_measures");

            migrationBuilder.DropTable(
                name: "memory_measures");

            migrationBuilder.DropTable(
                name: "cpus");
        }
    }
}

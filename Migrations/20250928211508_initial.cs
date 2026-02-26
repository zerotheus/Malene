using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melene.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cpus",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false),
                    core_count = table.Column<int>(type: "integer", nullable: false),
                    l1_size = table.Column<string>(type: "text", nullable: false),
                    l1_instruction = table.Column<string>(type: "text", nullable: false),
                    l2_size = table.Column<string>(type: "text", nullable: false),
                    l3_size = table.Column<string>(type: "text", nullable: false),
                    socket = table.Column<string>(type: "text", nullable: false),
                    frequency_limit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpus", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "gpu_measures",
                columns: table => new
                {
                    time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gpu_name = table.Column<string>(type: "text", nullable: false),
                    memory_usage = table.Column<float>(type: "real", nullable: false),
                    temperature_hotspot = table.Column<float>(type: "real", nullable: false),
                    memory_temperature = table.Column<float>(type: "real", nullable: false),
                    gpu_core_temperature = table.Column<float>(type: "real", nullable: false),
                    total_memory = table.Column<float>(type: "real", nullable: false),
                    total_memory_usage = table.Column<float>(type: "real", nullable: false),
                    free_memory = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gpu_measures", x => x.time_stamp);
                });

            migrationBuilder.CreateTable(
                name: "memory_measures",
                columns: table => new
                {
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    memory_id = table.Column<string>(type: "text", nullable: false),
                    used = table.Column<float>(type: "real", nullable: false),
                    load_in_percentage = table.Column<float>(type: "real", nullable: false),
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
                    core_id = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cpu_name = table.Column<string>(type: "text", nullable: false),
                    frequency = table.Column<float>(type: "real", nullable: false),
                    frequency_eff = table.Column<float>(type: "real", nullable: false),
                    c0_residency = table.Column<float>(type: "real", nullable: false),
                    temperature = table.Column<float>(type: "real", nullable: false),
                    load = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_measures", x => new { x.core_id, x.timestamp });
                    table.ForeignKey(
                        name: "FK_core_measures_cpus_cpu_name",
                        column: x => x.cpu_name,
                        principalTable: "cpus",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cpu_measures",
                columns: table => new
                {
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cpu_name = table.Column<string>(type: "text", nullable: false),
                    ppt_current_value = table.Column<float>(type: "real", nullable: false),
                    ppt_current_limit = table.Column<float>(type: "real", nullable: false),
                    edc_current_limit = table.Column<string>(type: "text", nullable: false),
                    edc_current_value = table.Column<string>(type: "text", nullable: false),
                    tdc_current_limit = table.Column<string>(type: "text", nullable: false),
                    tdc_current_value = table.Column<string>(type: "text", nullable: false),
                    chtc_limit = table.Column<string>(type: "text", nullable: false),
                    vddcr_power = table.Column<float>(type: "real", nullable: false),
                    vddcr_soc_power = table.Column<float>(type: "real", nullable: false),
                    current_mode = table.Column<string>(type: "text", nullable: false),
                    peak_core_speed = table.Column<float>(type: "real", nullable: false),
                    core_speed_average = table.Column<float>(type: "real", nullable: false),
                    peak_core_voltage = table.Column<float>(type: "real", nullable: false),
                    temperature = table.Column<float>(type: "real", nullable: false),
                    total_load = table.Column<float>(type: "real", nullable: false),
                    max_load_of_one_core = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cpu_measures", x => x.timestamp);
                    table.ForeignKey(
                        name: "FK_cpu_measures_cpus_cpu_name",
                        column: x => x.cpu_name,
                        principalTable: "cpus",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "core_threads",
                columns: table => new
                {
                    thread_id = table.Column<int>(type: "integer", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    load = table.Column<float>(type: "real", nullable: false),
                    core_id = table.Column<int>(type: "integer", nullable: false),
                    core_timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_threads", x => new { x.thread_id, x.time_stamp });
                    table.ForeignKey(
                        name: "FK_core_threads_core_measures_core_id_core_timestamp",
                        columns: x => new { x.core_id, x.core_timestamp },
                        principalTable: "core_measures",
                        principalColumns: new[] { "core_id", "timestamp" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_measures_cpu_name",
                table: "core_measures",
                column: "cpu_name");

            migrationBuilder.CreateIndex(
                name: "IX_core_threads_core_id_core_timestamp",
                table: "core_threads",
                columns: new[] { "core_id", "core_timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_cpu_measures_cpu_name",
                table: "cpu_measures",
                column: "cpu_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_threads");

            migrationBuilder.DropTable(
                name: "cpu_measures");

            migrationBuilder.DropTable(
                name: "gpu_measures");

            migrationBuilder.DropTable(
                name: "memory_measures");

            migrationBuilder.DropTable(
                name: "core_measures");

            migrationBuilder.DropTable(
                name: "cpus");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melene.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_core_measures_cpus_CpuName",
                table: "core_measures");

            migrationBuilder.DropForeignKey(
                name: "FK_cpu_measures_cpus_CpuName",
                table: "cpu_measures");

            migrationBuilder.DropColumn(
                name: "CoreMeasuresId",
                table: "core_threads");

            migrationBuilder.RenameColumn(
                name: "memoryID",
                table: "memory_measures",
                newName: "memory_id");

            migrationBuilder.RenameColumn(
                name: "loadInPercentage",
                table: "memory_measures",
                newName: "load_in_percentage");

            migrationBuilder.RenameColumn(
                name: "totalMemoryUsage",
                table: "gpu_measures",
                newName: "total_memory_usage");

            migrationBuilder.RenameColumn(
                name: "totalMemory",
                table: "gpu_measures",
                newName: "total_memory");

            migrationBuilder.RenameColumn(
                name: "temperatureHotspot",
                table: "gpu_measures",
                newName: "temperature_hotspot");

            migrationBuilder.RenameColumn(
                name: "memoryUsage",
                table: "gpu_measures",
                newName: "memory_usage");

            migrationBuilder.RenameColumn(
                name: "memoryTemperature",
                table: "gpu_measures",
                newName: "memory_temperature");

            migrationBuilder.RenameColumn(
                name: "gpuName",
                table: "gpu_measures",
                newName: "gpu_name");

            migrationBuilder.RenameColumn(
                name: "gpuCoreTemperature",
                table: "gpu_measures",
                newName: "gpu_core_temperature");

            migrationBuilder.RenameColumn(
                name: "freeMemory",
                table: "gpu_measures",
                newName: "free_memory");

            migrationBuilder.RenameColumn(
                name: "timeStamp",
                table: "gpu_measures",
                newName: "time_stamp");

            migrationBuilder.RenameColumn(
                name: "l3Size",
                table: "cpus",
                newName: "l3_size");

            migrationBuilder.RenameColumn(
                name: "l2Size",
                table: "cpus",
                newName: "l2_size");

            migrationBuilder.RenameColumn(
                name: "l1Size",
                table: "cpus",
                newName: "l1_size");

            migrationBuilder.RenameColumn(
                name: "l1Instruction",
                table: "cpus",
                newName: "l1_instruction");

            migrationBuilder.RenameColumn(
                name: "frequencyLimit",
                table: "cpus",
                newName: "frequency_limit");

            migrationBuilder.RenameColumn(
                name: "coreCount",
                table: "cpus",
                newName: "core_count");

            migrationBuilder.RenameColumn(
                name: "totalLoad",
                table: "cpu_measures",
                newName: "total_load");

            migrationBuilder.RenameColumn(
                name: "tdcCurrentValue",
                table: "cpu_measures",
                newName: "tdc_current_value");

            migrationBuilder.RenameColumn(
                name: "tdcCurrentLimit",
                table: "cpu_measures",
                newName: "tdc_current_limit");

            migrationBuilder.RenameColumn(
                name: "pptCurrentValue",
                table: "cpu_measures",
                newName: "ppt_current_value");

            migrationBuilder.RenameColumn(
                name: "pptCurrentLimit",
                table: "cpu_measures",
                newName: "ppt_current_limit");

            migrationBuilder.RenameColumn(
                name: "peakCoreVoltage",
                table: "cpu_measures",
                newName: "peak_core_voltage");

            migrationBuilder.RenameColumn(
                name: "peakCoreSpeed",
                table: "cpu_measures",
                newName: "peak_core_speed");

            migrationBuilder.RenameColumn(
                name: "maxLoadOfOneCore",
                table: "cpu_measures",
                newName: "max_load_of_one_core");

            migrationBuilder.RenameColumn(
                name: "edcCurrentValue",
                table: "cpu_measures",
                newName: "edc_current_value");

            migrationBuilder.RenameColumn(
                name: "edcCurrentLimit",
                table: "cpu_measures",
                newName: "edc_current_limit");

            migrationBuilder.RenameColumn(
                name: "currentMode",
                table: "cpu_measures",
                newName: "current_mode");

            migrationBuilder.RenameColumn(
                name: "coreSpeedAverage",
                table: "cpu_measures",
                newName: "core_speed_average");

            migrationBuilder.RenameColumn(
                name: "cHTCLimit",
                table: "cpu_measures",
                newName: "chtc_limit");

            migrationBuilder.RenameColumn(
                name: "VDDCRSOCPower",
                table: "cpu_measures",
                newName: "vddcr_soc_power");

            migrationBuilder.RenameColumn(
                name: "VDDCRPower",
                table: "cpu_measures",
                newName: "vddcr_power");

            migrationBuilder.RenameColumn(
                name: "CpuName",
                table: "cpu_measures",
                newName: "cpu_name");

            migrationBuilder.RenameIndex(
                name: "IX_cpu_measures_CpuName",
                table: "cpu_measures",
                newName: "IX_cpu_measures_cpu_name");

            migrationBuilder.RenameColumn(
                name: "timeStamp",
                table: "core_threads",
                newName: "time_stamp");

            migrationBuilder.RenameColumn(
                name: "threadID",
                table: "core_threads",
                newName: "thread_id");

            migrationBuilder.RenameColumn(
                name: "frequencyEff",
                table: "core_measures",
                newName: "frequency_eff");

            migrationBuilder.RenameColumn(
                name: "c0Residency",
                table: "core_measures",
                newName: "c0_residency");

            migrationBuilder.RenameColumn(
                name: "CpuName",
                table: "core_measures",
                newName: "cpu_name");

            migrationBuilder.RenameColumn(
                name: "coreID",
                table: "core_measures",
                newName: "core_id");

            migrationBuilder.RenameIndex(
                name: "IX_core_measures_CpuName",
                table: "core_measures",
                newName: "IX_core_measures_cpu_name");

            migrationBuilder.AlterColumn<string>(
                name: "memory_id",
                table: "memory_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "gpu_name",
                table: "gpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "socket",
                table: "cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cpus",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "l3_size",
                table: "cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "l2_size",
                table: "cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "l1_size",
                table: "cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "l1_instruction",
                table: "cpus",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tdc_current_value",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tdc_current_limit",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "edc_current_value",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "edc_current_limit",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "current_mode",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "chtc_limit",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cpu_name",
                table: "cpu_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "core_id",
                table: "core_threads",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "core_timestamp",
                table: "core_threads",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "cpu_name",
                table: "core_measures",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "core_id",
                table: "core_measures",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_core_threads_core_id_core_timestamp",
                table: "core_threads",
                columns: new[] { "core_id", "core_timestamp" });

            migrationBuilder.AddForeignKey(
                name: "FK_core_measures_cpus_cpu_name",
                table: "core_measures",
                column: "cpu_name",
                principalTable: "cpus",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_core_threads_core_measures_core_id_core_timestamp",
                table: "core_threads",
                columns: new[] { "core_id", "core_timestamp" },
                principalTable: "core_measures",
                principalColumns: new[] { "core_id", "timestamp" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cpu_measures_cpus_cpu_name",
                table: "cpu_measures",
                column: "cpu_name",
                principalTable: "cpus",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_core_measures_cpus_cpu_name",
                table: "core_measures");

            migrationBuilder.DropForeignKey(
                name: "FK_core_threads_core_measures_core_id_core_timestamp",
                table: "core_threads");

            migrationBuilder.DropForeignKey(
                name: "FK_cpu_measures_cpus_cpu_name",
                table: "cpu_measures");

            migrationBuilder.DropIndex(
                name: "IX_core_threads_core_id_core_timestamp",
                table: "core_threads");

            migrationBuilder.DropColumn(
                name: "core_id",
                table: "core_threads");

            migrationBuilder.DropColumn(
                name: "core_timestamp",
                table: "core_threads");

            migrationBuilder.RenameColumn(
                name: "memory_id",
                table: "memory_measures",
                newName: "memoryID");

            migrationBuilder.RenameColumn(
                name: "load_in_percentage",
                table: "memory_measures",
                newName: "loadInPercentage");

            migrationBuilder.RenameColumn(
                name: "total_memory_usage",
                table: "gpu_measures",
                newName: "totalMemoryUsage");

            migrationBuilder.RenameColumn(
                name: "total_memory",
                table: "gpu_measures",
                newName: "totalMemory");

            migrationBuilder.RenameColumn(
                name: "temperature_hotspot",
                table: "gpu_measures",
                newName: "temperatureHotspot");

            migrationBuilder.RenameColumn(
                name: "memory_usage",
                table: "gpu_measures",
                newName: "memoryUsage");

            migrationBuilder.RenameColumn(
                name: "memory_temperature",
                table: "gpu_measures",
                newName: "memoryTemperature");

            migrationBuilder.RenameColumn(
                name: "gpu_name",
                table: "gpu_measures",
                newName: "gpuName");

            migrationBuilder.RenameColumn(
                name: "gpu_core_temperature",
                table: "gpu_measures",
                newName: "gpuCoreTemperature");

            migrationBuilder.RenameColumn(
                name: "free_memory",
                table: "gpu_measures",
                newName: "freeMemory");

            migrationBuilder.RenameColumn(
                name: "time_stamp",
                table: "gpu_measures",
                newName: "timeStamp");

            migrationBuilder.RenameColumn(
                name: "l3_size",
                table: "cpus",
                newName: "l3Size");

            migrationBuilder.RenameColumn(
                name: "l2_size",
                table: "cpus",
                newName: "l2Size");

            migrationBuilder.RenameColumn(
                name: "l1_size",
                table: "cpus",
                newName: "l1Size");

            migrationBuilder.RenameColumn(
                name: "l1_instruction",
                table: "cpus",
                newName: "l1Instruction");

            migrationBuilder.RenameColumn(
                name: "frequency_limit",
                table: "cpus",
                newName: "frequencyLimit");

            migrationBuilder.RenameColumn(
                name: "core_count",
                table: "cpus",
                newName: "coreCount");

            migrationBuilder.RenameColumn(
                name: "vddcr_soc_power",
                table: "cpu_measures",
                newName: "VDDCRSOCPower");

            migrationBuilder.RenameColumn(
                name: "vddcr_power",
                table: "cpu_measures",
                newName: "VDDCRPower");

            migrationBuilder.RenameColumn(
                name: "total_load",
                table: "cpu_measures",
                newName: "totalLoad");

            migrationBuilder.RenameColumn(
                name: "tdc_current_value",
                table: "cpu_measures",
                newName: "tdcCurrentValue");

            migrationBuilder.RenameColumn(
                name: "tdc_current_limit",
                table: "cpu_measures",
                newName: "tdcCurrentLimit");

            migrationBuilder.RenameColumn(
                name: "ppt_current_value",
                table: "cpu_measures",
                newName: "pptCurrentValue");

            migrationBuilder.RenameColumn(
                name: "ppt_current_limit",
                table: "cpu_measures",
                newName: "pptCurrentLimit");

            migrationBuilder.RenameColumn(
                name: "peak_core_voltage",
                table: "cpu_measures",
                newName: "peakCoreVoltage");

            migrationBuilder.RenameColumn(
                name: "peak_core_speed",
                table: "cpu_measures",
                newName: "peakCoreSpeed");

            migrationBuilder.RenameColumn(
                name: "max_load_of_one_core",
                table: "cpu_measures",
                newName: "maxLoadOfOneCore");

            migrationBuilder.RenameColumn(
                name: "edc_current_value",
                table: "cpu_measures",
                newName: "edcCurrentValue");

            migrationBuilder.RenameColumn(
                name: "edc_current_limit",
                table: "cpu_measures",
                newName: "edcCurrentLimit");

            migrationBuilder.RenameColumn(
                name: "current_mode",
                table: "cpu_measures",
                newName: "currentMode");

            migrationBuilder.RenameColumn(
                name: "cpu_name",
                table: "cpu_measures",
                newName: "CpuName");

            migrationBuilder.RenameColumn(
                name: "core_speed_average",
                table: "cpu_measures",
                newName: "coreSpeedAverage");

            migrationBuilder.RenameColumn(
                name: "chtc_limit",
                table: "cpu_measures",
                newName: "cHTCLimit");

            migrationBuilder.RenameIndex(
                name: "IX_cpu_measures_cpu_name",
                table: "cpu_measures",
                newName: "IX_cpu_measures_CpuName");

            migrationBuilder.RenameColumn(
                name: "time_stamp",
                table: "core_threads",
                newName: "timeStamp");

            migrationBuilder.RenameColumn(
                name: "thread_id",
                table: "core_threads",
                newName: "threadID");

            migrationBuilder.RenameColumn(
                name: "frequency_eff",
                table: "core_measures",
                newName: "frequencyEff");

            migrationBuilder.RenameColumn(
                name: "cpu_name",
                table: "core_measures",
                newName: "CpuName");

            migrationBuilder.RenameColumn(
                name: "c0_residency",
                table: "core_measures",
                newName: "c0Residency");

            migrationBuilder.RenameColumn(
                name: "core_id",
                table: "core_measures",
                newName: "coreID");

            migrationBuilder.RenameIndex(
                name: "IX_core_measures_cpu_name",
                table: "core_measures",
                newName: "IX_core_measures_CpuName");

            migrationBuilder.AlterColumn<string>(
                name: "memoryID",
                table: "memory_measures",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "gpuName",
                table: "gpu_measures",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "socket",
                table: "cpus",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cpus",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "l3Size",
                table: "cpus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "l2Size",
                table: "cpus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "l1Size",
                table: "cpus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "l1Instruction",
                table: "cpus",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "tdcCurrentValue",
                table: "cpu_measures",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "tdcCurrentLimit",
                table: "cpu_measures",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "edcCurrentValue",
                table: "cpu_measures",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "edcCurrentLimit",
                table: "cpu_measures",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "currentMode",
                table: "cpu_measures",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CpuName",
                table: "cpu_measures",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "cHTCLimit",
                table: "cpu_measures",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CoreMeasuresId",
                table: "core_threads",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CpuName",
                table: "core_measures",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "coreID",
                table: "core_measures",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_core_measures_cpus_CpuName",
                table: "core_measures",
                column: "CpuName",
                principalTable: "cpus",
                principalColumn: "name");

            migrationBuilder.AddForeignKey(
                name: "FK_cpu_measures_cpus_CpuName",
                table: "cpu_measures",
                column: "CpuName",
                principalTable: "cpus",
                principalColumn: "name");
        }
    }
}

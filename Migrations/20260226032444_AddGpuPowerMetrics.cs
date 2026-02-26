using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melene.Migrations
{
    /// <inheritdoc />
    public partial class AddGpuPowerMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "gpu_chip_energy_joules",
                table: "gpu_measures",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "gpu_power_draw_watts",
                table: "gpu_measures",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gpu_chip_energy_joules",
                table: "gpu_measures");

            migrationBuilder.DropColumn(
                name: "gpu_power_draw_watts",
                table: "gpu_measures");
        }
    }
}

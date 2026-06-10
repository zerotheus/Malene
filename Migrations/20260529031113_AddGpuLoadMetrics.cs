using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melene.Migrations
{
    /// <inheritdoc />
    public partial class AddGpuLoadMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "gpu_load_percent",
                table: "gpu_measures",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gpu_load_percent",
                table: "gpu_measures");
        }
    }
}

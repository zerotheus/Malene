using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melene.Migrations
{
    /// <inheritdoc />
    public partial class AddGpuCoreClock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "gpu_core_clock",
                table: "gpu_measures",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gpu_core_clock",
                table: "gpu_measures");
        }
    }
}

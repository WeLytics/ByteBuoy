using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMetricGroupingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupBy",
                table: "MetricGroups",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetricInterval",
                table: "MetricGroups",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupBy",
                table: "MetricGroups");

            migrationBuilder.DropColumn(
                name: "MetricInterval",
                table: "MetricGroups");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedGroupByValueColumnMetricGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GroupByValue",
                table: "MetricGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupByValue",
                table: "MetricGroups");
        }
    }
}

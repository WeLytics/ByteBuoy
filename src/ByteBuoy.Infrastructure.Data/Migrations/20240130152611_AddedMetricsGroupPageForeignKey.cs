using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMetricsGroupPageForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_MetricGroups_MetricGroupId",
                table: "Metrics");

            migrationBuilder.AlterColumn<int>(
                name: "MetricGroupId",
                table: "Metrics",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "MetricGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MetricGroups_PageId",
                table: "MetricGroups",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MetricGroups_Pages_PageId",
                table: "MetricGroups",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_MetricGroups_MetricGroupId",
                table: "Metrics",
                column: "MetricGroupId",
                principalTable: "MetricGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetricGroups_Pages_PageId",
                table: "MetricGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_MetricGroups_MetricGroupId",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_MetricGroups_PageId",
                table: "MetricGroups");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "MetricGroups");

            migrationBuilder.AlterColumn<int>(
                name: "MetricGroupId",
                table: "Metrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_MetricGroups_MetricGroupId",
                table: "Metrics",
                column: "MetricGroupId",
                principalTable: "MetricGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

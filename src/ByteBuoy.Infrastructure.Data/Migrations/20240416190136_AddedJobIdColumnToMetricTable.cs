using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedJobIdColumnToMetricTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DELETE FROM Metrics");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Metrics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_JobId",
                table: "Metrics",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Metrics_Jobs_JobId",
                table: "Metrics",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metrics_Jobs_JobId",
                table: "Metrics");

            migrationBuilder.DropIndex(
                name: "IX_Metrics_JobId",
                table: "Metrics");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Metrics");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuoy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMetricHashSHA256 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashSHA256",
                table: "Metrics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobHistory_JobId",
                table: "JobHistory",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHistory_Jobs_JobId",
                table: "JobHistory",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHistory_Jobs_JobId",
                table: "JobHistory");

            migrationBuilder.DropIndex(
                name: "IX_JobHistory_JobId",
                table: "JobHistory");

            migrationBuilder.DropColumn(
                name: "HashSHA256",
                table: "Metrics");
        }
    }
}

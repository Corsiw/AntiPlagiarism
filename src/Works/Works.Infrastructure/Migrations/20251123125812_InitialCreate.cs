using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    WorkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    AssignmentId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FileId = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PlagiarismFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    AnalysisRequestedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AnalysisCompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.WorkId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_AssignmentId",
                table: "Works",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_Status",
                table: "Works",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Works_StudentId",
                table: "Works",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Works");
        }
    }
}

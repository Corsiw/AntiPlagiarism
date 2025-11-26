using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analysis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analysis",
                columns: table => new
                {
                    AnalysisId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WorkId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    FileId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ReportFileId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    PlagiarismFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysis", x => x.AnalysisId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_AnalysisId",
                table: "Analysis",
                column: "AnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_WorkId",
                table: "Analysis",
                column: "WorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analysis");
        }
    }
}

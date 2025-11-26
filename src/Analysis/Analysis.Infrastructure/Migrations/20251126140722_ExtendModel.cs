using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analysis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlagiarismFlag",
                table: "Analysis");

            migrationBuilder.RenameColumn(
                name: "ReportFileId",
                table: "Analysis",
                newName: "ReportId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Analysis",
                newName: "SubmittedAt");

            migrationBuilder.RenameColumn(
                name: "AnalysisId",
                table: "Analysis",
                newName: "AnalysisRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Analysis_AnalysisId",
                table: "Analysis",
                newName: "IX_Analysis_AnalysisRecordId");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentId",
                table: "Analysis",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Analysis",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Analysis",
                type: "TEXT",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnalysisRecordId = table.Column<Guid>(type: "TEXT", maxLength: 64, nullable: false),
                    FileId = table.Column<Guid>(type: "TEXT", maxLength: 64, nullable: false),
                    IsPlagiarism = table.Column<bool>(type: "INTEGER", nullable: false),
                    SimilarityPercentage = table.Column<double>(type: "REAL", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_AnalysisRecordId",
                table: "Report",
                column: "AnalysisRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReportId",
                table: "Report",
                column: "ReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Analysis");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Analysis");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Analysis");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Analysis",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Analysis",
                newName: "ReportFileId");

            migrationBuilder.RenameColumn(
                name: "AnalysisRecordId",
                table: "Analysis",
                newName: "AnalysisId");

            migrationBuilder.RenameIndex(
                name: "IX_Analysis_AnalysisRecordId",
                table: "Analysis",
                newName: "IX_Analysis_AnalysisId");

            migrationBuilder.AddColumn<bool>(
                name: "PlagiarismFlag",
                table: "Analysis",
                type: "INTEGER",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analysis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContentHashModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentHashEntry",
                columns: table => new
                {
                    HashEntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    AnalysisRecordId = table.Column<Guid>(type: "TEXT", maxLength: 64, nullable: false),
                    StudentId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentHashEntry", x => x.HashEntryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentHashEntry_AnalysisRecordId",
                table: "ContentHashEntry",
                column: "AnalysisRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentHashEntry_HashEntryId",
                table: "ContentHashEntry",
                column: "HashEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentHashEntry");
        }
    }
}

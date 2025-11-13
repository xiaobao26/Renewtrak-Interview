using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Part_B.Migrations
{
    /// <inheritdoc />
    public partial class InitSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlossaryTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Term = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Definition = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlossaryTerms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GlossaryTerms",
                columns: new[] { "Id", "Definition", "Term" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa9"), "The ocean floor offshore from the continental margin, usually very flat with a slight slope.", "abyssal plain" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb6"), "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.", "accrete" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afc7"), "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium.", "alkaline" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlossaryTerms_Term",
                table: "GlossaryTerms",
                column: "Term",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlossaryTerms");
        }
    }
}

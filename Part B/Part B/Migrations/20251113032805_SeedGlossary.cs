using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Part_B.Migrations
{
    /// <inheritdoc />
    public partial class SeedGlossary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GlossaryTerms",
                columns: new[] { "Id", "Definition", "Term" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa9"), "The ocean floor offshore from the continental margin, usually very flat with a slight slope.", "abyssal plain" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb6"), "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.", "accrete" },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afc7"), "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium.", "alkaline" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa9"));

            migrationBuilder.DeleteData(
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb6"));

            migrationBuilder.DeleteData(
                table: "GlossaryTerms",
                keyColumn: "Id",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afc7"));
        }
    }
}

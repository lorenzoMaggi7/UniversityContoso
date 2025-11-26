using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityContoso.Migrations
{
    /// <inheritdoc />
    public partial class addPostiandDurata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurataMesi",
                table: "Corsi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostiDisponibili",
                table: "Corsi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurataMesi",
                table: "Corsi");

            migrationBuilder.DropColumn(
                name: "PostiDisponibili",
                table: "Corsi");
        }
    }
}

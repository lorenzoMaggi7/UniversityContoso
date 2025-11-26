using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityContoso.Migrations
{
    /// <inheritdoc />
    public partial class corso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descrizione",
                table: "Corsi",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descrizione",
                table: "Corsi");
        }
    }
}

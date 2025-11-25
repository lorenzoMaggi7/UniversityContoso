using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityContoso.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Professori",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Professori",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Crediti",
                table: "Corsi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProfessoriID",
                table: "Corsi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corsi_ProfessoriID",
                table: "Corsi",
                column: "ProfessoriID");

            migrationBuilder.AddForeignKey(
                name: "FK_Corsi_Professori_ProfessoriID",
                table: "Corsi",
                column: "ProfessoriID",
                principalTable: "Professori",
                principalColumn: "ProfessoriID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corsi_Professori_ProfessoriID",
                table: "Corsi");

            migrationBuilder.DropIndex(
                name: "IX_Corsi_ProfessoriID",
                table: "Corsi");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Professori");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Professori");

            migrationBuilder.DropColumn(
                name: "ProfessoriID",
                table: "Corsi");

            migrationBuilder.AlterColumn<int>(
                name: "Crediti",
                table: "Corsi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityContoso.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corsi",
                columns: table => new
                {
                    CorsoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crediti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corsi", x => x.CorsoID);
                });

            migrationBuilder.CreateTable(
                name: "Professori",
                columns: table => new
                {
                    ProfessoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAssunzione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professori", x => x.ProfessoriID);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataIscrizione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenteID = table.Column<int>(type: "int", nullable: false),
                    CorsoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Corsi_CorsoID",
                        column: x => x.CorsoID,
                        principalTable: "Corsi",
                        principalColumn: "CorsoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Studenti_StudenteID",
                        column: x => x.StudenteID,
                        principalTable: "Studenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CorsoID",
                table: "Enrollments",
                column: "CorsoID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudenteID",
                table: "Enrollments",
                column: "StudenteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Professori");

            migrationBuilder.DropTable(
                name: "Corsi");

            migrationBuilder.DropTable(
                name: "Studenti");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTechnique.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emplois",
                columns: table => new
                {
                    EmploiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entreprise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonneEmploiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplois", x => x.EmploiID);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.PersonneId);
                });

            migrationBuilder.CreateTable(
                name: "PersonneEmploi",
                columns: table => new
                {
                    PersonneEmploiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmploiID = table.Column<int>(type: "int", nullable: false),
                    dateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonneId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonneEmploi", x => x.PersonneEmploiID);
                    table.ForeignKey(
                        name: "FK_PersonneEmploi_Emplois_EmploiID",
                        column: x => x.EmploiID,
                        principalTable: "Emplois",
                        principalColumn: "EmploiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonneEmploi_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonneEmploi_EmploiID",
                table: "PersonneEmploi",
                column: "EmploiID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonneEmploi_PersonneId",
                table: "PersonneEmploi",
                column: "PersonneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonneEmploi");

            migrationBuilder.DropTable(
                name: "Emplois");

            migrationBuilder.DropTable(
                name: "Personnes");
        }
    }
}

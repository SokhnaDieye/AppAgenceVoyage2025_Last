using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiAspNet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNIClient",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNIGestionnaire",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Agences",
                columns: table => new
                {
                    IdAgence = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdresseAgence = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: true),
                    Latitude = table.Column<float>(type: "real", nullable: true),
                    NineaGestionnaire = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RccmGestionnaire = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdGestionnaire = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agences", x => x.IdAgence);
                    table.ForeignKey(
                        name: "FK_Agences_Users_IdGestionnaire",
                        column: x => x.IdGestionnaire,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chauffeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chauffeurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offres",
                columns: table => new
                {
                    IdOffre = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescriptionOffre = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    PrixJourOffre = table.Column<float>(type: "real", nullable: false),
                    Disponibilite = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdAgence = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offres", x => x.IdOffre);
                    table.ForeignKey(
                        name: "FK_Offres_Agences_IdAgence",
                        column: x => x.IdAgence,
                        principalTable: "Agences",
                        principalColumn: "IdAgence",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agences_IdGestionnaire",
                table: "Agences",
                column: "IdGestionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_Offres_IdAgence",
                table: "Offres",
                column: "IdAgence");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chauffeurs");

            migrationBuilder.DropTable(
                name: "Offres");

            migrationBuilder.DropTable(
                name: "Agences");

            migrationBuilder.DropColumn(
                name: "CNIClient",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CNIGestionnaire",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");
        }
    }
}

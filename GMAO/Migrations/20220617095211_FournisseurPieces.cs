using Microsoft.EntityFrameworkCore.Migrations;

namespace GMAO.Migrations
{
    public partial class FournisseurPieces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FournisseurPièce",
                columns: table => new
                {
                    CodeFournisseur = table.Column<string>(nullable: false),
                    CodePiéce = table.Column<string>(nullable: true),
                    PAchat = table.Column<float>(nullable: false),
                    RefFrs = table.Column<string>(nullable: true),
                    Previent = table.Column<float>(nullable: false),
                    Devise = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FournisseurPiece", x => x.CodeFournisseur);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FournisseurPièce");
        }
    }
}

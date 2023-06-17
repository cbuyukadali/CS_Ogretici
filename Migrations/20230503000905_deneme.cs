using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS_Ogretici.Migrations
{
    /// <inheritdoc />
    public partial class deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basliklars",
                columns: table => new
                {
                    BaslikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaslikAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaslikYol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaslikParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basliklars", x => x.BaslikId);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilars",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciAd = table.Column<string>(type: "Varchar(20)", nullable: false),
                    KullaniciSoyad = table.Column<string>(type: "Varchar(20)", nullable: false),
                    KullaniciEposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilars", x => x.KullaniciId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basliklars");

            migrationBuilder.DropTable(
                name: "Kullanicilars");
        }
    }
}

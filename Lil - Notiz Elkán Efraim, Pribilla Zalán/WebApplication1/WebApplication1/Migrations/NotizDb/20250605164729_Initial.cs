using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations.NotizDb
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notiz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOTIZID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tulajdonos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feladat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allapot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MikorKezdje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HatarIdo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FOntossag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Komment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjektNev = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notiz", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notiz");
        }
    }
}

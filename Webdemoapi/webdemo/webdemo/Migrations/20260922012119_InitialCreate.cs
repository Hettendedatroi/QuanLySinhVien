using Microsoft.EntityFrameworkCore.Migrations;

namespace webdemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hanghoas",
                columns: table => new
                {
                    MaHangHoa = table.Column<string>(nullable: false),
                    TenHangHoa = table.Column<string>(nullable: true),
                    DonGia = table.Column<double>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hanghoas", x => x.MaHangHoa);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hanghoas");
        }
    }
}

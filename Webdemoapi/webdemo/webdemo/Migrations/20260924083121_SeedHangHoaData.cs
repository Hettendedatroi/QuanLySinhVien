using Microsoft.EntityFrameworkCore.Migrations;

namespace webdemo.Migrations
{
    public partial class SeedHangHoaData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hanghoas",
                columns: new[] { "MaHangHoa", "DonGia", "SoLuong", "TenHangHoa" },
                values: new object[] { "HH001", 32000.0, 100, "Sữa tươi Vinamilk" });

            migrationBuilder.InsertData(
                table: "Hanghoas",
                columns: new[] { "MaHangHoa", "DonGia", "SoLuong", "TenHangHoa" },
                values: new object[] { "HH002", 55000.0, 50, "Bánh Chocopie" });

            migrationBuilder.InsertData(
                table: "Hanghoas",
                columns: new[] { "MaHangHoa", "DonGia", "SoLuong", "TenHangHoa" },
                values: new object[] { "HH003", 4500.0, 500, "Mì Tôm Hảo Hảo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hanghoas",
                keyColumn: "MaHangHoa",
                keyValue: "HH001");

            migrationBuilder.DeleteData(
                table: "Hanghoas",
                keyColumn: "MaHangHoa",
                keyValue: "HH002");

            migrationBuilder.DeleteData(
                table: "Hanghoas",
                keyColumn: "MaHangHoa",
                keyValue: "HH003");
        }
    }
}

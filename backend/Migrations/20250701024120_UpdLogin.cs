using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a48cf44b-9195-469c-bf21-3bbb26eaaadb", "AQAAAAIAAYagAAAAEA7yR1I2/Lp0vjphUYI/lkgSdPSgrojoBo0RvR4o/SFj5hdtTDkLtHKoV4beeo1Djw==", "670c1012-8318-49ae-a487-82d2d948b221" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83dc0ddb-1512-481b-b678-6ffd556f95c8", "AQAAAAIAAYagAAAAEIhS23kmZMnBzfcBmD2RxI2abd1OrqxVkvLTiCVu+rPmo4guTyeDiTvS55GxBLoqaA==", "bf2e8f02-4659-41cb-bda3-c76ebf528395" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8763ca7-95ab-4872-a135-a167aede744c", null, "a4c04dbb-7efa-462a-8a30-ace62710f6a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3626a82b-256a-464c-8672-885b1cc31531", null, "a7a7d2c3-a85e-43f6-8c7d-7a901983f00e" });
        }
    }
}

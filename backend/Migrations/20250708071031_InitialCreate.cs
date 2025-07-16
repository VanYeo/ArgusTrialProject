using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc6dc425-f9b3-4386-9df9-f4c3ea8c2b81", "AQAAAAIAAYagAAAAENLifuNLc1nNwELByyB2j0SZjfPayY0jyLfdabjP2SQ8qVLJ9neJiN+4qvjMG4yBrg==", "ed6e1595-3120-44ef-86e4-1ecdbdf098cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a21ab95c-243c-4ae4-81be-b82937e94351", "AQAAAAIAAYagAAAAEBC7s/ZBmJbaMiQKBiFuvgVj3lUcRwHwC744Nfm9MQij08o9E2WhKER15ppn0dI3MA==", "79f64bb8-312b-49ce-a003-b86283943a20" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "220c53b7-12ba-4c82-8a43-6e00d38ab857", "AQAAAAIAAYagAAAAEOTmTX26rZckQ3M6N5arTVTAyusqp+TnhVUUP1wZCR1IRoFpEEGID9dPMam9V1N5xg==", "5ae1a0ce-e3c7-4667-b3a7-37db95a34a59" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "677c2f96-f389-4b7c-8a53-cbe2b09abe1a", "AQAAAAIAAYagAAAAEJQGqMDvHqSj73EtzDxaumO/GUb2oi5ba+ldA/IwId5qa3NqY3Euc+Dh16VqzpaB8A==", "1a021a77-b84a-4f60-823d-50f46cbd598e" });
        }
    }
}

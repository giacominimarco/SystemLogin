using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetoLogin.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "AQAAAAIAAYagAAAAEAm2nhrP6s8Dqy+JoDILJ69bk535Mgmmg+0/L+nmJrAkez/F0cFyI3kBsl8A9zjB2w==", "admin" },
                    { 2, "AQAAAAIAAYagAAAAELwCzsEBg9FX2iBQOeV1zcpQRRWMU1eU81gTNHOCXUhTVdnTRv2Iz2oF7ksuno/6XQ==", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

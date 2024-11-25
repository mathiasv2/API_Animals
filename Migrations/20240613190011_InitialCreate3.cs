using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalWorkshop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "email", "name", "password" },
                values: new object[] { 1, "admin@gmail.com", "admin", "Administrator1!" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}

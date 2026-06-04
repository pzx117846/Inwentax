using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inwentax.Migrations
{
    /// <inheritdoc />
    public partial class update_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_used",
                table: "Laptops");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Laptops",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Laptops");

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "Laptops",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}

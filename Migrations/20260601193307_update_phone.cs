using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inwentax.Migrations
{
    /// <inheritdoc />
    public partial class update_phone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_used",
                table: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Phone",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Phone");

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "Phone",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}

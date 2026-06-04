using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inwentax.Migrations
{
    /// <inheritdoc />
    public partial class updateassignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_used",
                table: "Assignments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}

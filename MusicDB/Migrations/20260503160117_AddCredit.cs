using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicDB.Migrations
{
    /// <inheritdoc />
    public partial class AddCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Credit",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Songs");
        }
    }
}

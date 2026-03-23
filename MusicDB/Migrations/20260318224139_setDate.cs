using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicDB.Migrations
{
    /// <inheritdoc />
    public partial class setDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "SongRequests",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "SongRequests");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicDB.Migrations
{
    /// <inheritdoc />
    public partial class AddedSongRequestSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chords_Scales_ScaleId",
                table: "Chords");

            migrationBuilder.DropForeignKey(
                name: "FK_Chords_Sequences_SequenceId",
                table: "Chords");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Scales_ScaleId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Scales");

            migrationBuilder.DropTable(
                name: "Sequences");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ScaleId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Chords_ScaleId",
                table: "Chords");

            migrationBuilder.DropIndex(
                name: "IX_Chords_SequenceId",
                table: "Chords");

            migrationBuilder.DropColumn(
                name: "ScaleId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ScaleId",
                table: "Chords");

            migrationBuilder.DropColumn(
                name: "SequenceId",
                table: "Chords");

            migrationBuilder.RenameColumn(
                name: "MajorOrMinor",
                table: "Songs",
                newName: "scale");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Songs",
                type: "date",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SongRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    FulfillerId = table.Column<int>(type: "int", nullable: true),
                    PriorityScore = table.Column<double>(type: "float", nullable: true),
                    IsFulfilled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongRequests_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SongRequests_Users_FulfillerId",
                        column: x => x.FulfillerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "songRequestVotes",
                columns: table => new
                {
                    SongRequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VotedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songRequestVotes", x => new { x.UserId, x.SongRequestId });
                    table.ForeignKey(
                        name: "FK_songRequestVotes_SongRequests_SongRequestId",
                        column: x => x.SongRequestId,
                        principalTable: "SongRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_songRequestVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongRequests_CreatorId",
                table: "SongRequests",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SongRequests_FulfillerId",
                table: "SongRequests",
                column: "FulfillerId");

            migrationBuilder.CreateIndex(
                name: "IX_songRequestVotes_SongRequestId",
                table: "songRequestVotes",
                column: "SongRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "songRequestVotes");

            migrationBuilder.DropTable(
                name: "SongRequests");

            migrationBuilder.RenameColumn(
                name: "scale",
                table: "Songs",
                newName: "MajorOrMinor");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Songs",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ScaleId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScaleId",
                table: "Chords",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SequenceId",
                table: "Chords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Scales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sequences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ScaleId",
                table: "Songs",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Chords_ScaleId",
                table: "Chords",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Chords_SequenceId",
                table: "Chords",
                column: "SequenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chords_Scales_ScaleId",
                table: "Chords",
                column: "ScaleId",
                principalTable: "Scales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chords_Sequences_SequenceId",
                table: "Chords",
                column: "SequenceId",
                principalTable: "Sequences",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Scales_ScaleId",
                table: "Songs",
                column: "ScaleId",
                principalTable: "Scales",
                principalColumn: "Id");
        }
    }
}

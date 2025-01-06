using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "cinemaHalls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cinemaHalls_CinemaId",
                table: "cinemaHalls",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_cinemaHalls_Cinemas_CinemaId",
                table: "cinemaHalls",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cinemaHalls_Cinemas_CinemaId",
                table: "cinemaHalls");

            migrationBuilder.DropIndex(
                name: "IX_cinemaHalls_CinemaId",
                table: "cinemaHalls");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "cinemaHalls");
        }
    }
}

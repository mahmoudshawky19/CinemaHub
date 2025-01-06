using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_bookingseats_CinemaHallId",
                table: "bookingseats",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_bookingseats_CinemaId",
                table: "bookingseats",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_bookingseats_MovieId",
                table: "bookingseats",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingseats_Cinemas_CinemaId",
                table: "bookingseats",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingseats_Movies_MovieId",
                table: "bookingseats",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingseats_cinemaHalls_CinemaHallId",
                table: "bookingseats",
                column: "CinemaHallId",
                principalTable: "cinemaHalls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingseats_Cinemas_CinemaId",
                table: "bookingseats");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingseats_Movies_MovieId",
                table: "bookingseats");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingseats_cinemaHalls_CinemaHallId",
                table: "bookingseats");

            migrationBuilder.DropIndex(
                name: "IX_bookingseats_CinemaHallId",
                table: "bookingseats");

            migrationBuilder.DropIndex(
                name: "IX_bookingseats_CinemaId",
                table: "bookingseats");

            migrationBuilder.DropIndex(
                name: "IX_bookingseats_MovieId",
                table: "bookingseats");
        }
    }
}

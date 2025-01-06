using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_movieRatings_MovieId",
                table: "movieRatings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_movieRatings_Movies_MovieId",
                table: "movieRatings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movieRatings_Movies_MovieId",
                table: "movieRatings");

            migrationBuilder.DropIndex(
                name: "IX_movieRatings_MovieId",
                table: "movieRatings");
        }
    }
}

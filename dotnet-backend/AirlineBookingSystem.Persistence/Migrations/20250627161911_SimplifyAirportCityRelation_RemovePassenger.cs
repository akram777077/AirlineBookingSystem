using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AirlineBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyAirportCityRelation_RemovePassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_countries_country_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_airports_countries_country_id",
                table: "airports");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_passengers_passenger_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_users_user_id",
                table: "bookings");

            migrationBuilder.DropTable(
                name: "passengers");

            migrationBuilder.DropIndex(
                name: "IX_bookings_user_id",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_airports_country_id",
                table: "airports");

            migrationBuilder.DropIndex(
                name: "IX_addresses_country_id",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "airports");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_users_passenger_id",
                table: "bookings",
                column: "passenger_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_users_passenger_id",
                table: "bookings");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "country_id",
                table: "airports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "country_id",
                table: "addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "passengers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passengers", x => x.id);
                    table.ForeignKey(
                        name: "FK_passengers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id",
                table: "bookings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_airports_country_id",
                table: "airports",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_country_id",
                table: "addresses",
                column: "country_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_passengers_user_id",
                table: "passengers",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_countries_country_id",
                table: "addresses",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_airports_countries_country_id",
                table: "airports",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_passengers_passenger_id",
                table: "bookings",
                column: "passenger_id",
                principalTable: "passengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_users_user_id",
                table: "bookings",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

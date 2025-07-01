using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AirlineBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Redesign_ScaledDomainSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_city_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_airports_cities_city_id",
                table: "airports");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_booking_status_booking_status_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_flights_flight_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_users_passenger_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_cities_countries_country_id",
                table: "cities");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_airports_from_airport_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_airports_to_airport_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_people_addresses_address_id",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_passenger_id",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "ix_bookings_seat_number",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_airports_city_id",
                table: "airports");

            migrationBuilder.DropIndex(
                name: "IX_addresses_city_id",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "people");

            migrationBuilder.DropColumn(
                name: "seat_number",
                table: "bookings");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "ix_users_person_id",
                table: "users",
                newName: "IX_users_person_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_username",
                table: "users",
                newName: "active_username");

            migrationBuilder.RenameIndex(
                name: "ix_people_last_name",
                table: "people",
                newName: "IX_people_last_name");

            migrationBuilder.RenameColumn(
                name: "to_airport_id",
                table: "flights",
                newName: "flight_status_id");

            migrationBuilder.RenameColumn(
                name: "from_airport_id",
                table: "flights",
                newName: "departure_gate_id");

            migrationBuilder.RenameIndex(
                name: "ix_flights_flight_number_departure_time",
                table: "flights",
                newName: "IX_flights_flight_number_departure_time");

            migrationBuilder.RenameIndex(
                name: "IX_flights_to_airport_id",
                table: "flights",
                newName: "IX_flights_flight_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_from_airport_id",
                table: "flights",
                newName: "IX_flights_departure_gate_id");

            migrationBuilder.RenameIndex(
                name: "ix_countries_code",
                table: "countries",
                newName: "IX_countries_code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "flight_id",
                table: "Bookings",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "booking_status_id",
                table: "Bookings",
                newName: "BookingStatusId");

            migrationBuilder.RenameColumn(
                name: "passenger_id",
                table: "Bookings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_flight_id",
                table: "Bookings",
                newName: "IX_Bookings_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_booking_status_id",
                table: "Bookings",
                newName: "IX_Bookings_BookingStatusId");

            migrationBuilder.RenameIndex(
                name: "ix_airports_airport_code",
                table: "airports",
                newName: "IX_airports_airport_code");

            migrationBuilder.RenameIndex(
                name: "ix_addresses_zip_code",
                table: "addresses",
                newName: "IX_addresses_zip_code");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "people",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mid_name",
                table: "people",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "people",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "image_path",
                table: "people",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "people",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "people",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "people",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "gender_id",
                table: "people",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "flight_number",
                table: "flights",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "airplane_id",
                table: "flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "arrival_gate_id",
                table: "flights",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "flights",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "flights",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "flights",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "countries",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "countries",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cities",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Bookings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketNumber",
                table: "Bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "airports",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "airport_code",
                table: "airports",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "timezone",
                table: "airports",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "zip_code",
                table: "addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "street",
                table: "addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "airplanes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model = table.Column<string>(type: "text", nullable: false),
                    manufacturer = table.Column<string>(type: "text", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airplanes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "flight_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<char>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookingId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TransactionId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "terminals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    airport_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terminals", x => x.id);
                    table.ForeignKey(
                        name: "FK_terminals_airports_airport_id",
                        column: x => x.airport_id,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAirports",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AirportId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAirports", x => new { x.UserId, x.AirportId });
                    table.ForeignKey(
                        name: "FK_UserAirports_airports_AirportId",
                        column: x => x.AirportId,
                        principalTable: "airports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAirports_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightId = table.Column<int>(type: "integer", nullable: false),
                    ClassTypeId = table.Column<int>(type: "integer", nullable: false),
                    SeatCapacity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightClasses_ClassTypes_ClassTypeId",
                        column: x => x.ClassTypeId,
                        principalTable: "ClassTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightClasses_flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gate_number = table.Column<string>(type: "text", nullable: false),
                    terminal_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gates", x => x.id);
                    table.ForeignKey(
                        name: "FK_gates_terminals_terminal_id",
                        column: x => x.terminal_id,
                        principalTable: "terminals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightClassId = table.Column<int>(type: "integer", nullable: false),
                    SeatNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    IsReserved = table.Column<bool>(type: "boolean", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_FlightClasses_FlightClassId",
                        column: x => x.FlightClassId,
                        principalTable: "FlightClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_people_gender_id",
                table: "people",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_airplane_id",
                table: "flights",
                column: "airplane_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_arrival_gate_id",
                table: "flights",
                column: "arrival_gate_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeatId",
                table: "Bookings",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TicketNumber",
                table: "Bookings",
                column: "TicketNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId_FlightId",
                table: "Bookings",
                columns: new[] { "UserId", "FlightId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_airports_city_id",
                table: "airports",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_city_id",
                table: "addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_airplanes_code",
                table: "airplanes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flight_status_status_name",
                table: "flight_status",
                column: "status_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightClasses_ClassTypeId",
                table: "FlightClasses",
                column: "ClassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightClasses_FlightId_ClassTypeId",
                table: "FlightClasses",
                columns: new[] { "FlightId", "ClassTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gates_terminal_id_gate_number",
                table: "gates",
                columns: new[] { "terminal_id", "gate_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genders_code",
                table: "genders",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightClassId_SeatNumber",
                table: "Seats",
                columns: new[] { "FlightClassId", "SeatNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_terminals_airport_id_name",
                table: "terminals",
                columns: new[] { "airport_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAirports_AirportId",
                table: "UserAirports",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAirports_UserId_AirportId",
                table: "UserAirports",
                columns: new[] { "UserId", "AirportId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_city_id",
                table: "addresses",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_airports_cities_city_id",
                table: "airports",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Seats_SeatId",
                table: "Bookings",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_booking_status_BookingStatusId",
                table: "Bookings",
                column: "BookingStatusId",
                principalTable: "booking_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_flights_FlightId",
                table: "Bookings",
                column: "FlightId",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cities_countries_country_id",
                table: "cities",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_airplanes_airplane_id",
                table: "flights",
                column: "airplane_id",
                principalTable: "airplanes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_flight_status_flight_status_id",
                table: "flights",
                column: "flight_status_id",
                principalTable: "flight_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_gates_arrival_gate_id",
                table: "flights",
                column: "arrival_gate_id",
                principalTable: "gates",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_gates_departure_gate_id",
                table: "flights",
                column: "departure_gate_id",
                principalTable: "gates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_addresses_address_id",
                table: "people",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_genders_gender_id",
                table: "people",
                column: "gender_id",
                principalTable: "genders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_city_id",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_airports_cities_city_id",
                table: "airports");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Seats_SeatId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_booking_status_BookingStatusId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_flights_FlightId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_users_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_cities_countries_country_id",
                table: "cities");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_airplanes_airplane_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_flight_status_flight_status_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_gates_arrival_gate_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_gates_departure_gate_id",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_people_addresses_address_id",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_people_genders_gender_id",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "airplanes");

            migrationBuilder.DropTable(
                name: "flight_status");

            migrationBuilder.DropTable(
                name: "gates");

            migrationBuilder.DropTable(
                name: "genders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "UserAirports");

            migrationBuilder.DropTable(
                name: "terminals");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "FlightClasses");

            migrationBuilder.DropTable(
                name: "ClassTypes");

            migrationBuilder.DropIndex(
                name: "IX_people_gender_id",
                table: "people");

            migrationBuilder.DropIndex(
                name: "IX_flights_airplane_id",
                table: "flights");

            migrationBuilder.DropIndex(
                name: "IX_flights_arrival_gate_id",
                table: "flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SeatId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TicketNumber",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId_FlightId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_airports_city_id",
                table: "airports");

            migrationBuilder.DropIndex(
                name: "IX_addresses_city_id",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "gender_id",
                table: "people");

            migrationBuilder.DropColumn(
                name: "airplane_id",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "arrival_gate_id",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "BookedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TicketNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "timezone",
                table: "airports");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "bookings");

            migrationBuilder.RenameIndex(
                name: "IX_users_person_id",
                table: "users",
                newName: "ix_users_person_id");

            migrationBuilder.RenameIndex(
                name: "active_username",
                table: "users",
                newName: "ix_users_username");

            migrationBuilder.RenameIndex(
                name: "IX_people_last_name",
                table: "people",
                newName: "ix_people_last_name");

            migrationBuilder.RenameColumn(
                name: "flight_status_id",
                table: "flights",
                newName: "to_airport_id");

            migrationBuilder.RenameColumn(
                name: "departure_gate_id",
                table: "flights",
                newName: "from_airport_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_flight_number_departure_time",
                table: "flights",
                newName: "ix_flights_flight_number_departure_time");

            migrationBuilder.RenameIndex(
                name: "IX_flights_flight_status_id",
                table: "flights",
                newName: "IX_flights_to_airport_id");

            migrationBuilder.RenameIndex(
                name: "IX_flights_departure_gate_id",
                table: "flights",
                newName: "IX_flights_from_airport_id");

            migrationBuilder.RenameIndex(
                name: "IX_countries_code",
                table: "countries",
                newName: "ix_countries_code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bookings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "bookings",
                newName: "flight_id");

            migrationBuilder.RenameColumn(
                name: "BookingStatusId",
                table: "bookings",
                newName: "booking_status_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "bookings",
                newName: "passenger_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_FlightId",
                table: "bookings",
                newName: "IX_bookings_flight_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "bookings",
                newName: "IX_bookings_booking_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_airports_airport_code",
                table: "airports",
                newName: "ix_airports_airport_code");

            migrationBuilder.RenameIndex(
                name: "IX_addresses_zip_code",
                table: "addresses",
                newName: "ix_addresses_zip_code");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "users",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "people",
                type: "varchar",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "mid_name",
                table: "people",
                type: "varchar",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "people",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "image_path",
                table: "people",
                type: "varchar",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "people",
                type: "varchar",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "people",
                type: "varchar",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "people",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<char>(
                name: "gender",
                table: "people",
                type: "char(1)",
                nullable: false,
                defaultValue: '\0');

            migrationBuilder.AlterColumn<string>(
                name: "flight_number",
                table: "flights",
                type: "varchar",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "countries",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "countries",
                type: "varchar",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cities",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "seat_number",
                table: "bookings",
                type: "varchar",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "airports",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "airport_code",
                table: "airports",
                type: "varchar",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "zip_code",
                table: "addresses",
                type: "varchar",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "street",
                table: "addresses",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_passenger_id",
                table: "bookings",
                column: "passenger_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_seat_number",
                table: "bookings",
                column: "seat_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_airports_city_id",
                table: "airports",
                column: "city_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_addresses_city_id",
                table: "addresses",
                column: "city_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_city_id",
                table: "addresses",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_airports_cities_city_id",
                table: "airports",
                column: "city_id",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_booking_status_booking_status_id",
                table: "bookings",
                column: "booking_status_id",
                principalTable: "booking_status",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_flights_flight_id",
                table: "bookings",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_users_passenger_id",
                table: "bookings",
                column: "passenger_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cities_countries_country_id",
                table: "cities",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_airports_from_airport_id",
                table: "flights",
                column: "from_airport_id",
                principalTable: "airports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_airports_to_airport_id",
                table: "flights",
                column: "to_airport_id",
                principalTable: "airports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_addresses_address_id",
                table: "people",
                column: "address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

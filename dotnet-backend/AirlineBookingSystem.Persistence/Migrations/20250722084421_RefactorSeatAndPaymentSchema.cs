using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSeatAndPaymentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_FlightClasses_ClassTypes_ClassTypeId",
                table: "FlightClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightClasses_flights_FlightId",
                table: "FlightClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_people_Genders_gender_id",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_FlightClasses_FlightClassId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAirports_airports_AirportId",
                table: "UserAirports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAirports_users_UserId",
                table: "UserAirports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAirports",
                table: "UserAirports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightClasses",
                table: "FlightClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassTypes",
                table: "ClassTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Seats");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "seats");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "genders");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "bookings");

            migrationBuilder.RenameTable(
                name: "UserAirports",
                newName: "user_airports");

            migrationBuilder.RenameTable(
                name: "FlightClasses",
                newName: "flight_classes");

            migrationBuilder.RenameTable(
                name: "ClassTypes",
                newName: "class_types");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "seats",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SeatNumber",
                table: "seats",
                newName: "seat_number");

            migrationBuilder.RenameColumn(
                name: "IsReserved",
                table: "seats",
                newName: "is_reserved");

            migrationBuilder.RenameColumn(
                name: "FlightClassId",
                table: "seats",
                newName: "class_types_id");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_FlightClassId_SeatNumber",
                table: "seats",
                newName: "IX_seats_class_types_id_seat_number");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "payments",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "payments",
                newName: "transaction_id");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "payments",
                newName: "method");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "payments",
                newName: "paid_at");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "payments",
                newName: "booking_id");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "genders",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "genders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Genders_Code",
                table: "genders",
                newName: "IX_genders_code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bookings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "bookings",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "bookings",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TicketNumber",
                table: "bookings",
                newName: "ticket_number");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "bookings",
                newName: "seat_id");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "bookings",
                newName: "payment_status");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "bookings",
                newName: "flight_id");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "bookings",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "bookings",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BookingStatusId",
                table: "bookings",
                newName: "booking_status_id");

            migrationBuilder.RenameColumn(
                name: "BookedAt",
                table: "bookings",
                newName: "booked_at");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId_FlightId",
                table: "bookings",
                newName: "IX_bookings_user_id_flight_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TicketNumber",
                table: "bookings",
                newName: "IX_bookings_ticket_number");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_SeatId",
                table: "bookings",
                newName: "IX_bookings_seat_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_FlightId",
                table: "bookings",
                newName: "IX_bookings_flight_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookingStatusId",
                table: "bookings",
                newName: "IX_bookings_booking_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_UserAirports_UserId_AirportId",
                table: "user_airports",
                newName: "IX_user_airports_UserId_AirportId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAirports_AirportId",
                table: "user_airports",
                newName: "IX_user_airports_AirportId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "flight_classes",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "flight_classes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SeatCapacity",
                table: "flight_classes",
                newName: "seat_capacity");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "flight_classes",
                newName: "flight_id");

            migrationBuilder.RenameColumn(
                name: "ClassTypeId",
                table: "flight_classes",
                newName: "class_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_FlightClasses_FlightId_ClassTypeId",
                table: "flight_classes",
                newName: "IX_flight_classes_flight_id_class_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_FlightClasses_ClassTypeId",
                table: "flight_classes",
                newName: "IX_flight_classes_class_type_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "class_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "class_types",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "seat_number",
                table: "seats",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "is_reserved",
                table: "seats",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "airplane_id",
                table: "seats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "transaction_id",
                table: "payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "method",
                table: "payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "booked_at",
                table: "bookings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "class_types",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_seats",
                table: "seats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_genders",
                table: "genders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_airports",
                table: "user_airports",
                columns: new[] { "UserId", "AirportId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_flight_classes",
                table: "flight_classes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_class_types",
                table: "class_types",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_seats_airplane_id",
                table: "seats",
                column: "airplane_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_booking_id",
                table: "payments",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_transaction_id",
                table: "payments",
                column: "transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_class_types_name",
                table: "class_types",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_booking_status_booking_status_id",
                table: "bookings",
                column: "booking_status_id",
                principalTable: "booking_status",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_flights_flight_id",
                table: "bookings",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_seats_seat_id",
                table: "bookings",
                column: "seat_id",
                principalTable: "seats",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_users_user_id",
                table: "bookings",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_classes_class_types_class_type_id",
                table: "flight_classes",
                column: "class_type_id",
                principalTable: "class_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flight_classes_flights_flight_id",
                table: "flight_classes",
                column: "flight_id",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_bookings_booking_id",
                table: "payments",
                column: "booking_id",
                principalTable: "bookings",
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
                name: "FK_seats_airplanes_airplane_id",
                table: "seats",
                column: "airplane_id",
                principalTable: "airplanes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seats_class_types_class_types_id",
                table: "seats",
                column: "class_types_id",
                principalTable: "class_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_airports_airports_AirportId",
                table: "user_airports",
                column: "AirportId",
                principalTable: "airports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_airports_users_UserId",
                table: "user_airports",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_booking_status_booking_status_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_flights_flight_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_seats_seat_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_users_user_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_classes_class_types_class_type_id",
                table: "flight_classes");

            migrationBuilder.DropForeignKey(
                name: "FK_flight_classes_flights_flight_id",
                table: "flight_classes");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_bookings_booking_id",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_people_genders_gender_id",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_airplanes_airplane_id",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_seats_class_types_class_types_id",
                table: "seats");

            migrationBuilder.DropForeignKey(
                name: "FK_user_airports_airports_AirportId",
                table: "user_airports");

            migrationBuilder.DropForeignKey(
                name: "FK_user_airports_users_UserId",
                table: "user_airports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seats",
                table: "seats");

            migrationBuilder.DropIndex(
                name: "IX_seats_airplane_id",
                table: "seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_booking_id",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_transaction_id",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_genders",
                table: "genders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_airports",
                table: "user_airports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_flight_classes",
                table: "flight_classes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_class_types",
                table: "class_types");

            migrationBuilder.DropIndex(
                name: "IX_class_types_name",
                table: "class_types");

            migrationBuilder.DropColumn(
                name: "airplane_id",
                table: "seats");

            migrationBuilder.RenameTable(
                name: "seats",
                newName: "Seats");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "genders",
                newName: "Genders");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "user_airports",
                newName: "UserAirports");

            migrationBuilder.RenameTable(
                name: "flight_classes",
                newName: "FlightClasses");

            migrationBuilder.RenameTable(
                name: "class_types",
                newName: "ClassTypes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Seats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "seat_number",
                table: "Seats",
                newName: "SeatNumber");

            migrationBuilder.RenameColumn(
                name: "is_reserved",
                table: "Seats",
                newName: "IsReserved");

            migrationBuilder.RenameColumn(
                name: "class_types_id",
                table: "Seats",
                newName: "FlightClassId");

            migrationBuilder.RenameIndex(
                name: "IX_seats_class_types_id_seat_number",
                table: "Seats",
                newName: "IX_Seats_FlightClassId_SeatNumber");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Payments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "transaction_id",
                table: "Payments",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "paid_at",
                table: "Payments",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "method",
                table: "Payments",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "booking_id",
                table: "Payments",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Genders",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Genders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_genders_code",
                table: "Genders",
                newName: "IX_Genders_Code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Bookings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Bookings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "ticket_number",
                table: "Bookings",
                newName: "TicketNumber");

            migrationBuilder.RenameColumn(
                name: "seat_id",
                table: "Bookings",
                newName: "SeatId");

            migrationBuilder.RenameColumn(
                name: "payment_status",
                table: "Bookings",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "flight_id",
                table: "Bookings",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Bookings",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Bookings",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "booking_status_id",
                table: "Bookings",
                newName: "BookingStatusId");

            migrationBuilder.RenameColumn(
                name: "booked_at",
                table: "Bookings",
                newName: "BookedAt");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_user_id_flight_id",
                table: "Bookings",
                newName: "IX_Bookings_UserId_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_ticket_number",
                table: "Bookings",
                newName: "IX_Bookings_TicketNumber");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_seat_id",
                table: "Bookings",
                newName: "IX_Bookings_SeatId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_flight_id",
                table: "Bookings",
                newName: "IX_Bookings_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_booking_status_id",
                table: "Bookings",
                newName: "IX_Bookings_BookingStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_user_airports_UserId_AirportId",
                table: "UserAirports",
                newName: "IX_UserAirports_UserId_AirportId");

            migrationBuilder.RenameIndex(
                name: "IX_user_airports_AirportId",
                table: "UserAirports",
                newName: "IX_UserAirports_AirportId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "FlightClasses",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FlightClasses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "seat_capacity",
                table: "FlightClasses",
                newName: "SeatCapacity");

            migrationBuilder.RenameColumn(
                name: "flight_id",
                table: "FlightClasses",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "class_type_id",
                table: "FlightClasses",
                newName: "ClassTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_flight_classes_flight_id_class_type_id",
                table: "FlightClasses",
                newName: "IX_FlightClasses_FlightId_ClassTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_flight_classes_class_type_id",
                table: "FlightClasses",
                newName: "IX_FlightClasses_ClassTypeId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ClassTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ClassTypes",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "SeatNumber",
                table: "Seats",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsReserved",
                table: "Seats",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Seats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Seats",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Seats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Seats",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Seats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Payments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Payments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookedAt",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClassTypes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAirports",
                table: "UserAirports",
                columns: new[] { "UserId", "AirportId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightClasses",
                table: "FlightClasses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassTypes",
                table: "ClassTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

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
                name: "FK_FlightClasses_ClassTypes_ClassTypeId",
                table: "FlightClasses",
                column: "ClassTypeId",
                principalTable: "ClassTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightClasses_flights_FlightId",
                table: "FlightClasses",
                column: "FlightId",
                principalTable: "flights",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_Genders_gender_id",
                table: "people",
                column: "gender_id",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_FlightClasses_FlightClassId",
                table: "Seats",
                column: "FlightClassId",
                principalTable: "FlightClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAirports_airports_AirportId",
                table: "UserAirports",
                column: "AirportId",
                principalTable: "airports",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAirports_users_UserId",
                table: "UserAirports",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeArrivalGateIdMandatoryAndUpdateFlightClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_gates_arrival_gate_id",
                table: "flights");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FlightClasses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FlightClasses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FlightClasses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FlightClasses");

            migrationBuilder.AlterColumn<int>(
                name: "arrival_gate_id",
                table: "flights",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_gates_arrival_gate_id",
                table: "flights",
                column: "arrival_gate_id",
                principalTable: "gates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_gates_arrival_gate_id",
                table: "flights");

            migrationBuilder.AlterColumn<int>(
                name: "arrival_gate_id",
                table: "flights",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FlightClasses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FlightClasses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FlightClasses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FlightClasses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_flights_gates_arrival_gate_id",
                table: "flights",
                column: "arrival_gate_id",
                principalTable: "gates",
                principalColumn: "id");
        }
    }
}

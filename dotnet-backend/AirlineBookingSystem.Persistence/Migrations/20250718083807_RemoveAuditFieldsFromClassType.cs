using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineBookingSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAuditFieldsFromClassType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_people_genders_gender_id",
                table: "people");

            migrationBuilder.DropPrimaryKey(
                name: "PK_genders",
                table: "genders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClassTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ClassTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClassTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ClassTypes");

            migrationBuilder.RenameTable(
                name: "genders",
                newName: "Genders");

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

            migrationBuilder.AlterColumn<char>(
                name: "Code",
                table: "Genders",
                type: "character(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(char),
                oldType: "char(1)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_people_Genders_gender_id",
                table: "people",
                column: "gender_id",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_people_Genders_gender_id",
                table: "people");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "genders");

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

            migrationBuilder.AlterColumn<char>(
                name: "code",
                table: "genders",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(char),
                oldType: "character(1)",
                oldMaxLength: 1);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ClassTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ClassTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClassTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ClassTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_genders",
                table: "genders",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_people_genders_gender_id",
                table: "people",
                column: "gender_id",
                principalTable: "genders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

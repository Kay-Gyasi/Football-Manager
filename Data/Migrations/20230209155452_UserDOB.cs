using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UserDOB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Players");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 296, DateTimeKind.Utc).AddTicks(1209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(5542));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 295, DateTimeKind.Utc).AddTicks(9080),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(2874));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Players",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 273, DateTimeKind.Utc).AddTicks(2596),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 6, DateTimeKind.Utc).AddTicks(6497));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Players",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 272, DateTimeKind.Utc).AddTicks(7606),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 4, DateTimeKind.Utc).AddTicks(6046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 222, DateTimeKind.Utc).AddTicks(8261),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(4579));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 222, DateTimeKind.Utc).AddTicks(6131),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(451));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(5542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 296, DateTimeKind.Utc).AddTicks(1209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(2874),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 295, DateTimeKind.Utc).AddTicks(9080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Players",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 6, DateTimeKind.Utc).AddTicks(6497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 273, DateTimeKind.Utc).AddTicks(2596));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Players",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 4, DateTimeKind.Utc).AddTicks(6046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 272, DateTimeKind.Utc).AddTicks(7606));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(4579),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 222, DateTimeKind.Utc).AddTicks(8261));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 15, 54, 51, 222, DateTimeKind.Utc).AddTicks(6131));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class PlayersTeamsNullId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Audit_UpdatedBy",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(5542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 571, DateTimeKind.Utc).AddTicks(9441));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_Status",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Audit_CreatedBy",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(2874),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 571, DateTimeKind.Utc).AddTicks(7553));

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Players",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Audit_UpdatedBy",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Players",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 6, DateTimeKind.Utc).AddTicks(6497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 556, DateTimeKind.Utc).AddTicks(3779));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_Status",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Audit_CreatedBy",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Players",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 4, DateTimeKind.Utc).AddTicks(6046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 555, DateTimeKind.Utc).AddTicks(9946));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_UpdatedBy",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(4579),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 537, DateTimeKind.Utc).AddTicks(8256));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_Status",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Audit_CreatedBy",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 537, DateTimeKind.Utc).AddTicks(6664));

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Audit_UpdatedBy",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 571, DateTimeKind.Utc).AddTicks(9441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(5542));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_Status",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Audit_CreatedBy",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 571, DateTimeKind.Utc).AddTicks(7553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 25, DateTimeKind.Utc).AddTicks(2874));

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Audit_UpdatedBy",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 556, DateTimeKind.Utc).AddTicks(3779),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 6, DateTimeKind.Utc).AddTicks(6497));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_Status",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Audit_CreatedBy",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 555, DateTimeKind.Utc).AddTicks(9946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 14, 4, DateTimeKind.Utc).AddTicks(6046));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_UpdatedBy",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 537, DateTimeKind.Utc).AddTicks(8256),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(4579));

            migrationBuilder.AlterColumn<string>(
                name: "Audit_Status",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Audit_CreatedBy",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "admin",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "admin");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 9, 12, 33, 30, 537, DateTimeKind.Utc).AddTicks(6664),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 9, 13, 34, 13, 959, DateTimeKind.Utc).AddTicks(451));

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

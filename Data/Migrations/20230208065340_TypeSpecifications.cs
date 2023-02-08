using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class TypeSpecifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StadiumName",
                table: "Teams",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 480, DateTimeKind.Utc).AddTicks(5196),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 337, DateTimeKind.Utc).AddTicks(4014));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 480, DateTimeKind.Utc).AddTicks(4080),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 337, DateTimeKind.Utc).AddTicks(3070));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 472, DateTimeKind.Utc).AddTicks(1435),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 330, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 471, DateTimeKind.Utc).AddTicks(9679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 330, DateTimeKind.Utc).AddTicks(6526));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 456, DateTimeKind.Utc).AddTicks(9990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 315, DateTimeKind.Utc).AddTicks(8207));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 456, DateTimeKind.Utc).AddTicks(8354),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 315, DateTimeKind.Utc).AddTicks(7143));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StadiumName",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 337, DateTimeKind.Utc).AddTicks(4014),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 480, DateTimeKind.Utc).AddTicks(5196));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 337, DateTimeKind.Utc).AddTicks(3070),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 480, DateTimeKind.Utc).AddTicks(4080));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 330, DateTimeKind.Utc).AddTicks(7677),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 472, DateTimeKind.Utc).AddTicks(1435));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 330, DateTimeKind.Utc).AddTicks(6526),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 471, DateTimeKind.Utc).AddTicks(9679));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 315, DateTimeKind.Utc).AddTicks(8207),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 456, DateTimeKind.Utc).AddTicks(9990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Coaches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 6, 50, 12, 315, DateTimeKind.Utc).AddTicks(7143),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 8, 6, 53, 40, 456, DateTimeKind.Utc).AddTicks(8354));
        }
    }
}

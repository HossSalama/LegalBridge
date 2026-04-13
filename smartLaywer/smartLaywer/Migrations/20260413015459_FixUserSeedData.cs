using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartLaywer.Migrations
{
    /// <inheritdoc />
    public partial class FixUserSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NextHearingDate",
                schema: "Legal",
                table: "Hearings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "HearingType",
                schema: "Legal",
                table: "Hearings",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Hearing");

            migrationBuilder.UpdateData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastLoginAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 13, 3, 54, 55, 848, DateTimeKind.Local).AddTicks(7750), "$2a$11$Fdny20UP3dmshlwtiIJwduLTW7N16vrqf/J42ElcFZbsRLamKAp4K" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NextHearingDate",
                schema: "Legal",
                table: "Hearings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HearingType",
                schema: "Legal",
                table: "Hearings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Hearing",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.UpdateData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastLoginAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 4, 9, 2, 43, 47, 504, DateTimeKind.Local).AddTicks(5003), "$2a$11$mC8769zS57X6A.Y4zS57X6A.Y4zS57X6A.Y4zS57X6A.Y4zS57X6A." });
        }
    }
}

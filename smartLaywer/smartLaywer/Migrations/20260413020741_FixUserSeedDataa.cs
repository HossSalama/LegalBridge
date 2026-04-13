using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartLaywer.Migrations
{
    /// <inheritdoc />
    public partial class FixUserSeedDataa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "LastLoginAt", "NationalId", "PasswordHash", "PhoneNumber", "RoleId", "SecondNumber" },
                values: new object[] { 2, "admin@lawyer.com", "أدمن النظام", true, new DateTime(2026, 4, 13, 4, 7, 37, 497, DateTimeKind.Local).AddTicks(6060), "29001011234567", "$2a$11$kaqHoyNMTwzuvqARUXSKjOLl.Mfwc6is2w4mVeJyaMCbGoFUsi/dS", "01012345678", 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "LastLoginAt", "NationalId", "PasswordHash", "PhoneNumber", "RoleId", "SecondNumber" },
                values: new object[] { 1, "admin@lawyer.com", "أدمن النظام", true, new DateTime(2026, 4, 13, 3, 54, 55, 848, DateTimeKind.Local).AddTicks(7750), "29001011234567", "$2a$11$Fdny20UP3dmshlwtiIJwduLTW7N16vrqf/J42ElcFZbsRLamKAp4K", "01012345678", 1, null });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartLaywer.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentType = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CaseId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Legal",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "LastLoginAt", "NationalId", "PasswordHash", "PhoneNumber", "RoleId", "SecondNumber" },
                values: new object[] { 2, "admin@lawyer.com", "أدمن النظام", true, new DateTime(2026, 4, 14, 2, 47, 0, 113, DateTimeKind.Local).AddTicks(6527), "29001011234567", "$2a$11$U.nJs.24VLr.B6cOc1hQ3OTqeEVlWLlkI9GtOBwsw03q4lpEusVq2", "01012345678", 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CaseId",
                schema: "Legal",
                table: "Appointments",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                schema: "Legal",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CreatedBy",
                schema: "Legal",
                table: "Appointments",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "Legal");

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

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smartLaywer.Migrations
{
    /// <inheritdoc />
    public partial class edits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "مريهان محمد", "$2a$11$Cx1Ki7y.nQwenO2xVDSRI.TAU49YZ3qX9D1S9iv5bppvKT8O78Kxe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Core",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "مريهان محمد ", "$2a$11$2UPMRrl.ORHly2.lygdU9OodtquBhvIocyCrCOardx6SciS0GoCoq" });
        }
    }
}

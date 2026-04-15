using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace smartLaywer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Finance");

            migrationBuilder.EnsureSchema(
                name: "Legal");

            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.EnsureSchema(
                name: "Docs");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "CaseStatuses",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Color = table.Column<string>(type: "varchar(7)", unicode: false, maxLength: 7, nullable: false, defaultValue: "#1D9E75")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CaseStat__3214EC07B5F2F383", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseTypes",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CaseType__3214EC075B6A89A7", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalId = table.Column<string>(type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: true),
                    CommercialReg = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    SecondaryPhone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ClientType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clients__3214EC072E76F1EE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courts__3214EC0774F0030F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opponents",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalId = table.Column<string>(type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: true),
                    Phone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OpponentLawyerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OpponentLawyerPhone = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Opponent__3214EC075B5771E3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC07B922EC91", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    JudgeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__3214EC07056CED17", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Courts",
                        column: x => x.CourtId,
                        principalSchema: "Lookup",
                        principalTable: "Courts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    SecondNumber = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    NationalId = table.Column<string>(type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC0763A419AA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleId,
                        principalSchema: "Core",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(CONVERT([date],getdate()))"),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ArchivedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    ArchivedBy = table.Column<int>(type: "int", nullable: true),
                    ArchiveNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CaseTypeId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    AssignedLawyerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cases__3214EC077FEDE386", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_ArchivedBy",
                        column: x => x.ArchivedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_CaseType",
                        column: x => x.CaseTypeId,
                        principalSchema: "Lookup",
                        principalTable: "CaseTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Client",
                        column: x => x.ClientId,
                        principalSchema: "Legal",
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Court",
                        column: x => x.CourtId,
                        principalSchema: "Lookup",
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Dept",
                        column: x => x.DeptId,
                        principalSchema: "Lookup",
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Lawyer",
                        column: x => x.AssignedLawyerId,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Status",
                        column: x => x.StatusId,
                        principalSchema: "Lookup",
                        principalTable: "CaseStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTemplates",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MimeType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__3214EC0780986D55", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTemplates_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LegalLibrary",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    MimeType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FilePath = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LegalLib__3214EC072F97D00B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalLibrary_AddedBy",
                        column: x => x.AddedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    RelatedTable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RelatedId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notes__3214EC07523701A4", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_User",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GeneratedBy = table.Column<int>(type: "int", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reports__3214EC0721CC3C25", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_GeneratedBy",
                        column: x => x.GeneratedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdminExpenses",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpenseDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(CONVERT([date],getdate()))"),
                    PaidBy = table.Column<int>(type: "int", nullable: false),
                    ReceiptPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AdminExp__3214EC0712846B0C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminExp_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdminExp_PaidBy",
                        column: x => x.PaidBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appeals",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    AppealNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    AppealType = table.Column<int>(type: "int", nullable: false),
                    AppealDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Grounds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedLawyerId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Appeals__3214EC0717FB0C75", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appeals_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appeals_Court",
                        column: x => x.CourtId,
                        principalSchema: "Lookup",
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appeals_Lawyer",
                        column: x => x.AssignedLawyerId,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appeals_Status",
                        column: x => x.StatusId,
                        principalSchema: "Lookup",
                        principalTable: "CaseStatuses",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "CaseLawyers",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    AssignedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    RemovedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CaseLawy__3214EC0729B90EEA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseLawyers_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseLawyers_User",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaseOpponents",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    OpponentId = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CaseOppo__3214EC0713244ACE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseOpponents_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseOpponents_Opponent",
                        column: x => x.OpponentId,
                        principalSchema: "Legal",
                        principalTable: "Opponents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "Docs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocType = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    MimeType = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    UploadedBy = table.Column<int>(type: "int", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ArchivedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchivedBy = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__3214EC07983567AD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_ArchivedBy",
                        column: x => x.ArchivedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_UploadedBy",
                        column: x => x.UploadedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fees__3214EC0761BC1105", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fees_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fees_Client",
                        column: x => x.ClientId,
                        principalSchema: "Legal",
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hearings",
                schema: "Legal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    HearingType = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    HearingDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    JudgeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(case when datepart(hour,[HearingDateTime])<(12) then 1 else 2 end)", stored: false),
                    AttendanceStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextHearingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextHearingPeriod = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Hearings__3214EC075601EDF7", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hearings_Case",
                        column: x => x.CaseId,
                        principalSchema: "Legal",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hearings_Court",
                        column: x => x.CourtId,
                        principalSchema: "Lookup",
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hearings_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hearings_Dept",
                        column: x => x.DeptId,
                        principalSchema: "Lookup",
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentSchedule",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeId = table.Column<int>(type: "int", nullable: false),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    PlannedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentS__3214EC070A799146", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaySched_Fee",
                        column: x => x.FeeId,
                        principalSchema: "Finance",
                        principalTable: "Fees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ActualPayments",
                schema: "Finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(CONVERT([date],getdate()))"),
                    Method = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReceivedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    InstallmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ActualPa__3214EC079243D2AE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActPay_Fee",
                        column: x => x.FeeId,
                        principalSchema: "Finance",
                        principalTable: "Fees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActPay_Installment",
                        column: x => x.InstallmentId,
                        principalSchema: "Finance",
                        principalTable: "PaymentSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActPay_RecBy",
                        column: x => x.ReceivedBy,
                        principalSchema: "Core",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "CaseStatuses",
                columns: new[] { "Id", "Color", "StatusName" },
                values: new object[,]
                {
                    { 1, "#1D9E75", 1 },
                    { 2, "#BA7517", 2 },
                    { 3, "#888780", 3 },
                    { 4, "#E24B4A", 4 },
                    { 5, "#639922", 5 },
                    { 6, "#534AB7", 6 }
                });

            migrationBuilder.InsertData(
                schema: "Lookup",
                table: "CaseTypes",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 7, 7 }
                });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                schema: "Core",
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "LastLoginAt", "NationalId", "PasswordHash", "PhoneNumber", "RoleId", "SecondNumber" },
                values: new object[] { 1, "admin@lawyer.com", "مدير النظام", true, null, "12345678901234", "$2a$11$2UPMRrl.ORHly2.lygdU9OodtquBhvIocyCrCOardx6SciS0GoCoq", "01000000000", 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_ActualPayments_FeeId",
                schema: "Finance",
                table: "ActualPayments",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActualPayments_InstallmentId",
                schema: "Finance",
                table: "ActualPayments",
                column: "InstallmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ActualPayments_ReceivedBy",
                schema: "Finance",
                table: "ActualPayments",
                column: "ReceivedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__ActualPa__C08AFDAB7C3F98C9",
                schema: "Finance",
                table: "ActualPayments",
                column: "ReceiptNumber",
                unique: true,
                filter: "[ReceiptNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdminExpenses_CaseId",
                schema: "Finance",
                table: "AdminExpenses",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminExpenses_PaidBy",
                schema: "Finance",
                table: "AdminExpenses",
                column: "PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_AssignedLawyerId",
                schema: "Legal",
                table: "Appeals",
                column: "AssignedLawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_CaseId",
                schema: "Legal",
                table: "Appeals",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_CourtId",
                schema: "Legal",
                table: "Appeals",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_StatusId",
                schema: "Legal",
                table: "Appeals",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "UQ__Appeals__9802DB2581E9FC9B",
                schema: "Legal",
                table: "Appeals",
                column: "AppealNumber",
                unique: true,
                filter: "[AppealNumber] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_CaseLawyers_UserId",
                schema: "Legal",
                table: "CaseLawyers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ_CaseLawyers",
                schema: "Legal",
                table: "CaseLawyers",
                columns: new[] { "CaseId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseOpponents_OpponentId",
                schema: "Legal",
                table: "CaseOpponents",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "UQ_CaseOpponents",
                schema: "Legal",
                table: "CaseOpponents",
                columns: new[] { "CaseId", "OpponentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ArchivedBy",
                schema: "Legal",
                table: "Cases",
                column: "ArchivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_AssignedLawyerId",
                schema: "Legal",
                table: "Cases",
                column: "AssignedLawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseTypeId",
                schema: "Legal",
                table: "Cases",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId",
                schema: "Legal",
                table: "Cases",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtId",
                schema: "Legal",
                table: "Cases",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DeptId",
                schema: "Legal",
                table: "Cases",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_StatusId",
                schema: "Legal",
                table: "Cases",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "UQ__CaseStat__05E7698A51AACFCC",
                schema: "Lookup",
                table: "CaseStatuses",
                column: "StatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__CaseType__D4E7DFA851ADBB57",
                schema: "Lookup",
                table: "CaseTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Clients__5553E82743175A53",
                schema: "Legal",
                table: "Clients",
                column: "CommercialReg",
                unique: true,
                filter: "[CommercialReg] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Clients__E9AA32FAC4ED8D46",
                schema: "Legal",
                table: "Clients",
                column: "NationalId",
                unique: true,
                filter: "[NationalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Courts__5750888E36409F76",
                schema: "Lookup",
                table: "Courts",
                column: "CourtName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CourtId",
                schema: "Lookup",
                table: "Departments",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "UQ_Departments",
                schema: "Lookup",
                table: "Departments",
                columns: new[] { "DeptName", "CourtId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ArchivedBy",
                schema: "Docs",
                table: "Documents",
                column: "ArchivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CaseId",
                schema: "Docs",
                table: "Documents",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UploadedBy",
                schema: "Docs",
                table: "Documents",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTemplates_AddedBy",
                schema: "Docs",
                table: "DocumentTemplates",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_CaseId",
                schema: "Finance",
                table: "Fees",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_ClientId",
                schema: "Finance",
                table: "Fees",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_CreatedBy",
                schema: "Finance",
                table: "Fees",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_CaseId",
                schema: "Legal",
                table: "Hearings",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_CourtId",
                schema: "Legal",
                table: "Hearings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_CreatedBy",
                schema: "Legal",
                table: "Hearings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_DeptId",
                schema: "Legal",
                table: "Hearings",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalLibrary_AddedBy",
                schema: "Docs",
                table: "LegalLibrary",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                schema: "Legal",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Opponent__E9AA32FA65B02F64",
                schema: "Legal",
                table: "Opponents",
                column: "NationalId",
                unique: true,
                filter: "[NationalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_PaySched_Inst",
                schema: "Finance",
                table: "PaymentSchedule",
                columns: new[] { "FeeId", "InstallmentNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_GeneratedBy",
                schema: "Core",
                table: "Reports",
                column: "GeneratedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B6160D0D90D89",
                schema: "Core",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "Core",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__85FB4E38776812BE",
                schema: "Core",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053418FB8C99",
                schema: "Core",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Users__E9AA32FA09CF1B85",
                schema: "Core",
                table: "Users",
                column: "NationalId",
                unique: true,
                filter: "[NationalId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActualPayments",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "AdminExpenses",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Appeals",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "CaseLawyers",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "CaseOpponents",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "DocumentTemplates",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "Hearings",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "LegalLibrary",
                schema: "Docs");

            migrationBuilder.DropTable(
                name: "Notes",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "PaymentSchedule",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Opponents",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "Fees",
                schema: "Finance");

            migrationBuilder.DropTable(
                name: "Cases",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "CaseTypes",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "Legal");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "CaseStatuses",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Courts",
                schema: "Lookup");
        }
    }
}

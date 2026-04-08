using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabLinkBackend.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientAccount",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClientAc__E67E1A245BD6D29B", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "ContainerType",
                columns: table => new
                {
                    ContainerTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecimenType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Containe__46FA6FD9D73872C8", x => x.ContainerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departme__B2079BED58D910D0", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    FlagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlagType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Flags__780D4593B1088662", x => x.FlagId);
                });

            migrationBuilder.CreateTable(
                name: "InterfaceType",
                columns: table => new
                {
                    InterfaceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Interfac__BF5D47AB1A5F52B1", x => x.InterfaceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LabReportPack",
                columns: table => new
                {
                    LabReportPackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scope = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Metrics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LabRepor__6A46AD253081057A", x => x.LabReportPackId);
                });

            migrationBuilder.CreateTable(
                name: "Panel",
                columns: table => new
                {
                    PanelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PanelName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Panel__49CA68064C2DDDA0", x => x.PanelId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMode",
                columns: table => new
                {
                    PaymentModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__F95995495A034130", x => x.PaymentModeId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE1A917EB82E", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SpecimenType",
                columns: table => new
                {
                    SpecimenTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Specimen__0218E7DB1A433393", x => x.SpecimenTypeId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CC4C1BF5332B", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    VisitTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Visit__9BF3CC522C1C3095", x => x.VisitTypeId);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentRef",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InterfaceTypeId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Instrume__430A5386B333FA88", x => x.InstrumentId);
                    table.ForeignKey(
                        name: "FK_Instrument_Interface",
                        column: x => x.InterfaceTypeId,
                        principalTable: "InterfaceType",
                        principalColumn: "InterfaceTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    MethodId = table.Column<int>(type: "int", nullable: true),
                    SpecimenTypeId = table.Column<int>(type: "int", nullable: true),
                    ContainerTypeId = table.Column<int>(type: "int", nullable: false),
                    VolumeReq = table.Column<double>(type: "float", nullable: true),
                    Units = table.Column<int>(type: "int", nullable: true),
                    MaxNormalValue = table.Column<int>(type: "int", nullable: false),
                    MinNormalValue = table.Column<int>(type: "int", nullable: false),
                    TATTargetMinutes = table.Column<int>(type: "int", nullable: true),
                    RefRangeJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test__8CC33160062D6592", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_Test_ContainerType",
                        column: x => x.ContainerTypeId,
                        principalTable: "ContainerType",
                        principalColumn: "ContainerTypeId");
                    table.ForeignKey(
                        name: "FK_Test_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Test_SpecimenType",
                        column: x => x.SpecimenTypeId,
                        principalTable: "SpecimenType",
                        principalColumn: "SpecimenTypeId");
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Resource = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AuditLog__A17F23989F516BA0", x => x.AuditId);
                    table.ForeignKey(
                        name: "FK_AuditLog_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "CompetencyLog",
                columns: table => new
                {
                    CompetencyLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffUserId = table.Column<int>(type: "int", nullable: false),
                    CompetencyType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Competen__E76A996DF8042AE6", x => x.CompetencyLogId);
                    table.ForeignKey(
                        name: "FK_CompetencyLog_Reviewer",
                        column: x => x.ReviewerId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_CompetencyLog_Staff",
                        column: x => x.StaffUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E12915FB6C3", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    ContactInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryPhysicianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient__970EC36608EDAFF3", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patient_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Patient_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Assigned_At = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__3D978A3546C5285A", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Worklist",
                columns: table => new
                {
                    WorklistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InstrumentId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Worklist__F44B6698671EB079", x => x.WorklistId);
                    table.ForeignKey(
                        name: "FK_Worklist_Instrument",
                        column: x => x.InstrumentId,
                        principalTable: "InstrumentRef",
                        principalColumn: "InstrumentId");
                });

            migrationBuilder.CreateTable(
                name: "PanelTest",
                columns: table => new
                {
                    PanelTestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    ComponentsJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PanelTes__E1CE350B09B50239", x => x.PanelTestId);
                    table.ForeignKey(
                        name: "FK_PanelTest_Panel",
                        column: x => x.PanelId,
                        principalTable: "Panel",
                        principalColumn: "PanelId");
                    table.ForeignKey(
                        name: "FK_PanelTest_Test",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "TestId");
                });

            migrationBuilder.CreateTable(
                name: "PriceRef",
                columns: table => new
                {
                    PriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    PanelId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime", nullable: true),
                    EffectiveTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PriceRef__49575BAFCCA63FBE", x => x.PriceId);
                    table.ForeignKey(
                        name: "FK_PriceRef_Panel",
                        column: x => x.PanelId,
                        principalTable: "Panel",
                        principalColumn: "PanelId");
                    table.ForeignKey(
                        name: "FK_PriceRef_Test",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "TestId");
                });

            migrationBuilder.CreateTable(
                name: "QCRecord",
                columns: table => new
                {
                    QcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    ControlLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResultValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Units = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RunDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    RuleFlags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QCRecord__F5ECB18C1371668F", x => x.QcId);
                    table.ForeignKey(
                        name: "FK_QCRecord_Instrument",
                        column: x => x.InstrumentId,
                        principalTable: "InstrumentRef",
                        principalColumn: "InstrumentId");
                    table.ForeignKey(
                        name: "FK_QCRecord_Test",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "TestId");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    BookedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    VisitTypeId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhlebotomistId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Appointm__8ECDFCC2B0F9F50F", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_Appointment_User",
                        column: x => x.PhlebotomistId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Appointment_Visit",
                        column: x => x.VisitTypeId,
                        principalTable: "Visit",
                        principalColumn: "VisitTypeId");
                });

            migrationBuilder.CreateTable(
                name: "LabOrder",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    OrderedByUserId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LabOrder__C3905BCFE8228F02", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_LabOrder_Client",
                        column: x => x.ClientId,
                        principalTable: "ClientAccount",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_LabOrder_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK_LabOrder_User",
                        column: x => x.OrderedByUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentItem",
                columns: table => new
                {
                    AppItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    PanelId = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Appointm__2AB731087A1FB9A1", x => x.AppItemId);
                    table.ForeignKey(
                        name: "FK_AppItem_Appointment",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "AppointmentId");
                    table.ForeignKey(
                        name: "FK_AppItem_Panel",
                        column: x => x.PanelId,
                        principalTable: "Panel",
                        principalColumn: "PanelId");
                    table.ForeignKey(
                        name: "FK_AppItem_Test",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "TestId");
                });

            migrationBuilder.CreateTable(
                name: "Accession",
                columns: table => new
                {
                    AccessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    AccessionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccessionDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accessio__B4B2533D91A55B9E", x => x.AccessionId);
                    table.ForeignKey(
                        name: "FK_Accession_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "Addendum",
                columns: table => new
                {
                    AddendumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddendumText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Addendum__5F1020D068A42B79", x => x.AddendumId);
                    table.ForeignKey(
                        name: "FK_Addendum_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Addendum_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRef",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InvoiceR__D796AAB55F37DD7D", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Client",
                        column: x => x.ClientId,
                        principalTable: "ClientAccount",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Invoice_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Invoice_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "LabReport",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    ReportURI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AuthorizedBy = table.Column<int>(type: "int", nullable: true),
                    AuthorizedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LabRepor__D5BD4805FD806413", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_LabReport_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_LabReport_User",
                        column: x => x.AuthorizedBy,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: true),
                    PanelId = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__57ED0681F61DDCD8", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_OrderItem_Panel",
                        column: x => x.PanelId,
                        principalTable: "Panel",
                        principalColumn: "PanelId");
                    table.ForeignKey(
                        name: "FK_OrderItem_Test",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "TestId");
                });

            migrationBuilder.CreateTable(
                name: "PathologyReview",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patholog__74BC79CE155427F1", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_PathologyReview_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_PathologyReview_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentRef",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModeId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentR__9B556A38E358416C", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "InvoiceRef",
                        principalColumn: "InvoiceId");
                    table.ForeignKey(
                        name: "FK_Payment_Mode",
                        column: x => x.ModeId,
                        principalTable: "PaymentMode",
                        principalColumn: "PaymentModeId");
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    RecipientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    RecipientType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecipientRefId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recipien__F0A6024D9FC08155", x => x.RecipientId);
                    table.ForeignKey(
                        name: "FK_Recipient_Report",
                        column: x => x.ReportId,
                        principalTable: "LabReport",
                        principalColumn: "ReportId");
                });

            migrationBuilder.CreateTable(
                name: "ReportDelivery",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipientType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecipientId = table.Column<int>(type: "int", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReportDe__626D8FCE67F33EC6", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_ReportDelivery_Report",
                        column: x => x.ReportId,
                        principalTable: "LabReport",
                        principalColumn: "ReportId");
                });

            migrationBuilder.CreateTable(
                name: "ResultEntry",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    Analyte = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Units = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FlagId = table.Column<int>(type: "int", nullable: false),
                    EnteredBy = table.Column<int>(type: "int", nullable: true),
                    EnteredDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ResultEn__976902088E748652", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_ResultEntry_Flag",
                        column: x => x.FlagId,
                        principalTable: "Flags",
                        principalColumn: "FlagId");
                    table.ForeignKey(
                        name: "FK_ResultEntry_OrderItem",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "OrderItemId");
                    table.ForeignKey(
                        name: "FK_ResultEntry_Test",
                        column: x => x.TestId,
                        principalTable: "Test",
                        principalColumn: "TestId");
                    table.ForeignKey(
                        name: "FK_ResultEntry_User",
                        column: x => x.EnteredBy,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Specimen",
                columns: table => new
                {
                    SpecimenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    SpecimenTypeId = table.Column<int>(type: "int", nullable: true),
                    ContainerTypeId = table.Column<int>(type: "int", nullable: true),
                    CollectedBy = table.Column<int>(type: "int", nullable: true),
                    CollectedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Specimen__719B2271ECD3FE50", x => x.SpecimenId);
                    table.ForeignKey(
                        name: "FK_Specimen_ContainerType",
                        column: x => x.ContainerTypeId,
                        principalTable: "ContainerType",
                        principalColumn: "ContainerTypeId");
                    table.ForeignKey(
                        name: "FK_Specimen_Order",
                        column: x => x.OrderId,
                        principalTable: "LabOrder",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_Specimen_OrderItem",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "OrderItemId");
                    table.ForeignKey(
                        name: "FK_Specimen_SpecimenType",
                        column: x => x.SpecimenTypeId,
                        principalTable: "SpecimenType",
                        principalColumn: "SpecimenTypeId");
                    table.ForeignKey(
                        name: "FK_Specimen_User",
                        column: x => x.CollectedBy,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TechValidation",
                columns: table => new
                {
                    TvId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeltaCheckJSON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TechVali__F97248AE2536D887", x => x.TvId);
                    table.ForeignKey(
                        name: "FK_TechValidation_Result",
                        column: x => x.ResultId,
                        principalTable: "ResultEntry",
                        principalColumn: "ResultId");
                    table.ForeignKey(
                        name: "FK_TechValidation_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accession_OrderId",
                table: "Accession",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "UQ__Accessio__AE5A718E0F85B240",
                table: "Accession",
                column: "AccessionNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addendum_OrderId",
                table: "Addendum",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Addendum_UserId",
                table: "Addendum",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PatientId",
                table: "Appointment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PhlebotomistId",
                table: "Appointment",
                column: "PhlebotomistId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_VisitTypeId",
                table: "Appointment",
                column: "VisitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentItem_AppointmentId",
                table: "AppointmentItem",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentItem_PanelId",
                table: "AppointmentItem",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentItem_TestId",
                table: "AppointmentItem",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId",
                table: "AuditLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyLog_ReviewerId",
                table: "CompetencyLog",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyLog_StaffUserId",
                table: "CompetencyLog",
                column: "StaffUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentRef_InterfaceTypeId",
                table: "InstrumentRef",
                column: "InterfaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRef_ClientId",
                table: "InvoiceRef",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRef_OrderId",
                table: "InvoiceRef",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRef_PatientId",
                table: "InvoiceRef",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrder_ClientId",
                table: "LabOrder",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrder_OrderedByUserId",
                table: "LabOrder",
                column: "OrderedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabOrder_PatientId",
                table: "LabOrder",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LabReport_AuthorizedBy",
                table: "LabReport",
                column: "AuthorizedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LabReport_OrderId",
                table: "LabReport",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_PanelId",
                table: "OrderItem",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_TestId",
                table: "OrderItem",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "UQ__Panel__75B0AFB7AC7DF449",
                table: "Panel",
                column: "PanelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PanelTest_PanelId",
                table: "PanelTest",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_PanelTest_TestId",
                table: "PanelTest",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_PathologyReview_OrderId",
                table: "PathologyReview",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PathologyReview_UserId",
                table: "PathologyReview",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserId1",
                table: "Patient",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "UQ__Patient__1788CC4D85CB9787",
                table: "Patient",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRef_InvoiceId",
                table: "PaymentRef",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRef_ModeId",
                table: "PaymentRef",
                column: "ModeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRef_PanelId",
                table: "PriceRef",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRef_TestId",
                table: "PriceRef",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_QCRecord_InstrumentId",
                table: "QCRecord",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_QCRecord_TestId",
                table: "QCRecord",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_ReportId",
                table: "Recipient",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDelivery_ReportId",
                table: "ReportDelivery",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultEntry_EnteredBy",
                table: "ResultEntry",
                column: "EnteredBy");

            migrationBuilder.CreateIndex(
                name: "IX_ResultEntry_FlagId",
                table: "ResultEntry",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultEntry_OrderItemId",
                table: "ResultEntry",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultEntry_TestId",
                table: "ResultEntry",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Specimen_CollectedBy",
                table: "Specimen",
                column: "CollectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Specimen_ContainerTypeId",
                table: "Specimen",
                column: "ContainerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Specimen_OrderId",
                table: "Specimen",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Specimen_OrderItemId",
                table: "Specimen",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Specimen_SpecimenTypeId",
                table: "Specimen",
                column: "SpecimenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TechValidation_ResultId",
                table: "TechValidation",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TechValidation_UserId",
                table: "TechValidation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_ContainerTypeId",
                table: "Test",
                column: "ContainerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_DepartmentId",
                table: "Test",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_SpecimenTypeId",
                table: "Test",
                column: "SpecimenTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ__Test__A25C5AA711F98565",
                table: "Test",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User__A9D105349E858773",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklist_InstrumentId",
                table: "Worklist",
                column: "InstrumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accession");

            migrationBuilder.DropTable(
                name: "Addendum");

            migrationBuilder.DropTable(
                name: "AppointmentItem");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "CompetencyLog");

            migrationBuilder.DropTable(
                name: "LabReportPack");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PanelTest");

            migrationBuilder.DropTable(
                name: "PathologyReview");

            migrationBuilder.DropTable(
                name: "PaymentRef");

            migrationBuilder.DropTable(
                name: "PriceRef");

            migrationBuilder.DropTable(
                name: "QCRecord");

            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "ReportDelivery");

            migrationBuilder.DropTable(
                name: "Specimen");

            migrationBuilder.DropTable(
                name: "TechValidation");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Worklist");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "InvoiceRef");

            migrationBuilder.DropTable(
                name: "PaymentMode");

            migrationBuilder.DropTable(
                name: "LabReport");

            migrationBuilder.DropTable(
                name: "ResultEntry");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "InstrumentRef");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Flags");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "InterfaceType");

            migrationBuilder.DropTable(
                name: "LabOrder");

            migrationBuilder.DropTable(
                name: "Panel");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "ClientAccount");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "ContainerType");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "SpecimenType");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

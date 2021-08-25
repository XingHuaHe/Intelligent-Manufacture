using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntelligentManufactureApplication.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<string>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    ChildenID = table.Column<bool>(nullable: false),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentID = table.Column<string>(nullable: false),
                    ComponentName = table.Column<string>(nullable: true),
                    ParentID = table.Column<string>(nullable: true),
                    ChildenID = table.Column<bool>(nullable: false),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentID);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    ComponentTypeID = table.Column<string>(nullable: false),
                    ComponentTypeName = table.Column<string>(nullable: true),
                    ParentID = table.Column<string>(nullable: true),
                    ChildenID = table.Column<bool>(nullable: false),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.ComponentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Crafts",
                columns: table => new
                {
                    CraftID = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CategoryID = table.Column<string>(nullable: true),
                    ComponentTypeID = table.Column<string>(nullable: true),
                    ComponentID = table.Column<string>(nullable: true),
                    PartTypeID = table.Column<string>(nullable: true),
                    PartID = table.Column<string>(nullable: true),
                    CraftName = table.Column<string>(nullable: true),
                    FileVersion = table.Column<string>(nullable: true),
                    CheckState = table.Column<bool>(nullable: false),
                    CheckTime = table.Column<string>(nullable: true),
                    CheckedUser = table.Column<string>(nullable: true),
                    ModifiedTimee = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crafts", x => x.CraftID);
                });

            migrationBuilder.CreateTable(
                name: "Drawings",
                columns: table => new
                {
                    DrawingID = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CategoryID = table.Column<string>(nullable: true),
                    ComponentTypeID = table.Column<string>(nullable: true),
                    ComponentID = table.Column<string>(nullable: true),
                    PartTypeID = table.Column<string>(nullable: true),
                    PartID = table.Column<string>(nullable: true),
                    DrawingName = table.Column<string>(nullable: true),
                    FileVersion = table.Column<string>(nullable: true),
                    CheckState = table.Column<bool>(nullable: false),
                    CheckTime = table.Column<string>(nullable: true),
                    CheckedUser = table.Column<string>(nullable: true),
                    ModifiedTimee = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drawings", x => x.DrawingID);
                });

            migrationBuilder.CreateTable(
                name: "NCPrograms",
                columns: table => new
                {
                    NCProgramID = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    ProcessID = table.Column<string>(nullable: true),
                    CategoryID = table.Column<string>(nullable: true),
                    ComponentTypeID = table.Column<string>(nullable: true),
                    ComponentID = table.Column<string>(nullable: true),
                    PartTypeID = table.Column<string>(nullable: true),
                    PartID = table.Column<string>(nullable: true),
                    NCProgramName = table.Column<string>(nullable: true),
                    FileVersion = table.Column<string>(nullable: true),
                    CheckState = table.Column<bool>(nullable: false),
                    CheckTime = table.Column<string>(nullable: true),
                    CheckedUser = table.Column<string>(nullable: true),
                    ModifiedTimee = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCPrograms", x => x.NCProgramID);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartID = table.Column<string>(nullable: false),
                    PartName = table.Column<string>(nullable: true),
                    ParentID = table.Column<string>(nullable: true),
                    ChildenID = table.Column<bool>(nullable: false),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartID);
                });

            migrationBuilder.CreateTable(
                name: "PartTypes",
                columns: table => new
                {
                    PartTypeID = table.Column<string>(nullable: false),
                    PartTypeName = table.Column<string>(nullable: true),
                    ParentID = table.Column<string>(nullable: true),
                    ChildenID = table.Column<bool>(nullable: false),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTypes", x => x.PartTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    ProcessID = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    ProcessNumber = table.Column<string>(nullable: true),
                    ProcessName = table.Column<string>(nullable: true),
                    Applicability = table.Column<string>(nullable: true),
                    Hours = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<string>(nullable: true),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.ProcessID);
                });

            migrationBuilder.CreateTable(
                name: "ProductLists",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PartID = table.Column<string>(nullable: true),
                    MaterialsNumber = table.Column<string>(nullable: true),
                    DrawingID = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Makings = table.Column<string>(nullable: true),
                    MakingsNumber = table.Column<string>(nullable: true),
                    Standard = table.Column<string>(nullable: true),
                    HeatTreatmentCode = table.Column<string>(nullable: true),
                    MaterialStrength = table.Column<string>(nullable: true),
                    CheckGroup = table.Column<string>(nullable: true),
                    HeattreatmentStrength = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    BlankSize = table.Column<string>(nullable: true),
                    CuttingSize = table.Column<string>(nullable: true),
                    StelliteAndNitriding = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SavePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserPasswd = table.Column<string>(maxLength: 20, nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    HeadPortrait = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    OnlineState = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "ComponentTypes");

            migrationBuilder.DropTable(
                name: "Crafts");

            migrationBuilder.DropTable(
                name: "Drawings");

            migrationBuilder.DropTable(
                name: "NCPrograms");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "PartTypes");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "ProductLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

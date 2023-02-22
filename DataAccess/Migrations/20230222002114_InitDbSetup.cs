using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<double>(type: "float(10)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PremiseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EquipmentCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(type: "int", precision: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Equipment_EquipmentCode",
                        column: x => x.EquipmentCode,
                        principalTable: "Equipment",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Premises_PremiseCode",
                        column: x => x.PremiseCode,
                        principalTable: "Premises",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Code", "Area", "Name" },
                values: new object[,]
                {
                    { "EQ00000001", 50.0, "Equipment A" },
                    { "EQ00000002", 10.0, "Equipment B" },
                    { "EQ00000003", 25.0, "Equipment C" }
                });

            migrationBuilder.InsertData(
                table: "Premises",
                columns: new[] { "Code", "Area", "Name" },
                values: new object[,]
                {
                    { "ABCDE00001", 500.0, "Premise A" },
                    { "ABCDE00002", 400.0, "Premise B" },
                    { "ABCDE00003", 1000.0, "Premise C" },
                    { "ABCDE00004", 700.0, "Premise D" },
                    { "ABCDE00005", 800.0, "Premise E" },
                    { "ABCDE00006", 100.0, "Premise F" },
                    { "ABCDE00007", 300.0, "Premise G" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EquipmentCode",
                table: "Contracts",
                column: "EquipmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PremiseCode",
                table: "Contracts",
                column: "PremiseCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Premises");
        }
    }
}

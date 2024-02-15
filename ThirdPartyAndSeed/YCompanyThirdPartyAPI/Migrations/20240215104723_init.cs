using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YCompanyThirdPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policy_Coverage");

            migrationBuilder.DropTable(
                name: "Vehicle_Coverage");

            migrationBuilder.DropTable(
                name: "Vehicle_Driver");

            migrationBuilder.CreateTable(
                name: "PolicyCoverage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CoverageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCoverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyCoverage_Coverage_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PolicyCoverage_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleCoverage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CoverageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCoverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleCoverage_Coverage_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleCoverage_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleDriver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriveForBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrimaryDriver = table.Column<bool>(type: "bit", nullable: false),
                    EverydayMileage = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleDriver_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VehicleDriver_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCoverage_CoverageId",
                table: "PolicyCoverage",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCoverage_PolicyId",
                table: "PolicyCoverage",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleCoverage_CoverageId",
                table: "VehicleCoverage",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleCoverage_VehicleId",
                table: "VehicleCoverage",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDriver_DriverId",
                table: "VehicleDriver",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDriver_VehicleId",
                table: "VehicleDriver",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyCoverage");

            migrationBuilder.DropTable(
                name: "VehicleCoverage");

            migrationBuilder.DropTable(
                name: "VehicleDriver");

            migrationBuilder.CreateTable(
                name: "Policy_Coverage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy_Coverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policy_Coverage_Coverage_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Policy_Coverage_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicle_Coverage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle_Coverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Coverage_Coverage_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicle_Coverage_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicle_Driver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriveForBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverydayMileage = table.Column<float>(type: "real", nullable: false),
                    IsPrimaryDriver = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Driver_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vehicle_Driver_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policy_Coverage_CoverageId",
                table: "Policy_Coverage",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_Coverage_PolicyId",
                table: "Policy_Coverage",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Coverage_CoverageId",
                table: "Vehicle_Coverage",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Coverage_VehicleId",
                table: "Vehicle_Coverage",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Driver_DriverId",
                table: "Vehicle_Driver",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Driver_VehicleId",
                table: "Vehicle_Driver",
                column: "VehicleId");
        }
    }
}

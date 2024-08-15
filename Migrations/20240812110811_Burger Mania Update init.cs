using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_BurgerManiaBackend.Migrations
{
    /// <inheritdoc />
    public partial class BurgerManiaUpdateinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BurgerDatas",
                columns: table => new
                {
                    BurgerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BurgerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BurgerPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurgerImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurgerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurgerDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BurgerAvailableTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurgerCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerDatas", x => x.BurgerId);
                });

            migrationBuilder.CreateTable(
                name: "BurgerOrderDatas",
                columns: table => new
                {
                    BurgerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BurgerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BurgerPrice = table.Column<int>(type: "int", nullable: false),
                    BurgerImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurgerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurgerDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BurgerCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerOrderDatas", x => x.BurgerId);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDatas",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalBillPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDatas", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurgerDatas");

            migrationBuilder.DropTable(
                name: "BurgerOrderDatas");

            migrationBuilder.DropTable(
                name: "OrdersDatas");

            migrationBuilder.DropTable(
                name: "UserDatas");
        }
    }
}

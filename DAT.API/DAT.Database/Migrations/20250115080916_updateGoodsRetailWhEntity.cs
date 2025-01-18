using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAT.Database.Migrations
{
    /// <inheritdoc />
    public partial class updateGoodsRetailWhEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "WH",
                table: "GoodsRetail");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "WH",
                table: "GoodsRetail");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "WH",
                table: "GoodsRetail",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "GoodsCode",
                schema: "WH",
                table: "GoodsRetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GoodsName",
                schema: "WH",
                table: "GoodsRetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsCode",
                schema: "WH",
                table: "GoodsRetail");

            migrationBuilder.DropColumn(
                name: "GoodsName",
                schema: "WH",
                table: "GoodsRetail");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "WH",
                table: "GoodsRetail",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "WH",
                table: "GoodsRetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "WH",
                table: "GoodsRetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

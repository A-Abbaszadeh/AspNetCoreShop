using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Add_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 777, DateTimeKind.Local).AddTicks(5846),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(9481));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 777, DateTimeKind.Local).AddTicks(3648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(7279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(7156),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 777, DateTimeKind.Local).AddTicks(1723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(5336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(9882),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(3451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(4867),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 868, DateTimeKind.Local).AddTicks(8167));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 762, DateTimeKind.Local).AddTicks(9453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 854, DateTimeKind.Local).AddTicks(6394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(1814),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 868, DateTimeKind.Local).AddTicks(2841));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ReciverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogItemId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(9481),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 777, DateTimeKind.Local).AddTicks(5846));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(7279),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 777, DateTimeKind.Local).AddTicks(3648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(396),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(7156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(5336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 777, DateTimeKind.Local).AddTicks(1723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 869, DateTimeKind.Local).AddTicks(3451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(9882));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 868, DateTimeKind.Local).AddTicks(8167),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(4867));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 854, DateTimeKind.Local).AddTicks(6394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 762, DateTimeKind.Local).AddTicks(9453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 11, 23, 48, 21, 868, DateTimeKind.Local).AddTicks(2841),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 12, 23, 14, 48, 776, DateTimeKind.Local).AddTicks(1814));
        }
    }
}

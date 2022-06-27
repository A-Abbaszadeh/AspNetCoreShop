using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class add_Banner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 584, DateTimeKind.Local).AddTicks(1886),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 956, DateTimeKind.Local).AddTicks(1749));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 583, DateTimeKind.Local).AddTicks(9995),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 955, DateTimeKind.Local).AddTicks(9024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 583, DateTimeKind.Local).AddTicks(3728),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 955, DateTimeKind.Local).AddTicks(784));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 583, DateTimeKind.Local).AddTicks(7706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 955, DateTimeKind.Local).AddTicks(5773));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(9855),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 954, DateTimeKind.Local).AddTicks(5340));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(7129),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 954, DateTimeKind.Local).AddTicks(1740));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 581, DateTimeKind.Local).AddTicks(7494),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 952, DateTimeKind.Local).AddTicks(9805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(5218),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 953, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(3044),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 953, DateTimeKind.Local).AddTicks(6902));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFavorites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(1332),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 953, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 581, DateTimeKind.Local).AddTicks(4817),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 952, DateTimeKind.Local).AddTicks(5988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 572, DateTimeKind.Local).AddTicks(6418),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 943, DateTimeKind.Local).AddTicks(7561));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 581, DateTimeKind.Local).AddTicks(2249),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 952, DateTimeKind.Local).AddTicks(3573));

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 956, DateTimeKind.Local).AddTicks(1749),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 584, DateTimeKind.Local).AddTicks(1886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 955, DateTimeKind.Local).AddTicks(9024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 583, DateTimeKind.Local).AddTicks(9995));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 955, DateTimeKind.Local).AddTicks(784),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 583, DateTimeKind.Local).AddTicks(3728));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 955, DateTimeKind.Local).AddTicks(5773),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 583, DateTimeKind.Local).AddTicks(7706));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 954, DateTimeKind.Local).AddTicks(5340),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(9855));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 954, DateTimeKind.Local).AddTicks(1740),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(7129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 952, DateTimeKind.Local).AddTicks(9805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 581, DateTimeKind.Local).AddTicks(7494));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 953, DateTimeKind.Local).AddTicks(9181),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(5218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 953, DateTimeKind.Local).AddTicks(6902),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(3044));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFavorites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 953, DateTimeKind.Local).AddTicks(4528),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 582, DateTimeKind.Local).AddTicks(1332));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 952, DateTimeKind.Local).AddTicks(5988),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 581, DateTimeKind.Local).AddTicks(4817));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 943, DateTimeKind.Local).AddTicks(7561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 572, DateTimeKind.Local).AddTicks(6418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 24, 21, 15, 0, 952, DateTimeKind.Local).AddTicks(3573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 26, 20, 15, 58, 581, DateTimeKind.Local).AddTicks(2249));
        }
    }
}

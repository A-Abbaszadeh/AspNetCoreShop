using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CatalogItemComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 408, DateTimeKind.Local).AddTicks(2384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 149, DateTimeKind.Local).AddTicks(3802));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 408, DateTimeKind.Local).AddTicks(559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 149, DateTimeKind.Local).AddTicks(2209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 407, DateTimeKind.Local).AddTicks(4594),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 148, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 407, DateTimeKind.Local).AddTicks(8168),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 149, DateTimeKind.Local).AddTicks(425));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 407, DateTimeKind.Local).AddTicks(1147),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 148, DateTimeKind.Local).AddTicks(4173));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(8043),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 148, DateTimeKind.Local).AddTicks(1866));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 405, DateTimeKind.Local).AddTicks(6933),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(6731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(6124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 147, DateTimeKind.Local).AddTicks(9985));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(4263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 147, DateTimeKind.Local).AddTicks(8288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFavorites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(2651),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(9960));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 405, DateTimeKind.Local).AddTicks(4424),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(4116));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 398, DateTimeKind.Local).AddTicks(5283),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 129, DateTimeKind.Local).AddTicks(9791));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 405, DateTimeKind.Local).AddTicks(2356),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(2084));

            migrationBuilder.CreateTable(
                name: "CatalogItemComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogItemId = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(812)),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItemComments_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemComments_CatalogItemId",
                table: "CatalogItemComments",
                column: "CatalogItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItemComments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 149, DateTimeKind.Local).AddTicks(3802),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 408, DateTimeKind.Local).AddTicks(2384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 149, DateTimeKind.Local).AddTicks(2209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 408, DateTimeKind.Local).AddTicks(559));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 148, DateTimeKind.Local).AddTicks(7570),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 407, DateTimeKind.Local).AddTicks(4594));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 149, DateTimeKind.Local).AddTicks(425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 407, DateTimeKind.Local).AddTicks(8168));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 148, DateTimeKind.Local).AddTicks(4173),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 407, DateTimeKind.Local).AddTicks(1147));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 148, DateTimeKind.Local).AddTicks(1866),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(8043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(6731),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 405, DateTimeKind.Local).AddTicks(6933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 147, DateTimeKind.Local).AddTicks(9985),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(6124));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 147, DateTimeKind.Local).AddTicks(8288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(4263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFavorites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(9960),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 406, DateTimeKind.Local).AddTicks(2651));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(4116),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 405, DateTimeKind.Local).AddTicks(4424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 129, DateTimeKind.Local).AddTicks(9791),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 398, DateTimeKind.Local).AddTicks(5283));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 12, 17, 52, 20, 138, DateTimeKind.Local).AddTicks(2084),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 12, 23, 31, 22, 405, DateTimeKind.Local).AddTicks(2356));
        }
    }
}

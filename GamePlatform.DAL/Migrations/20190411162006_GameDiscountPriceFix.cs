using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlatform.DAL.Migrations
{
    public partial class GameDiscountPriceFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Games",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Games",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_DataTransfer_DbContext_Fiorella.DataContext.Migrations
{
    public partial class SaleModelAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "SalesProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "SalesProducts");
        }
    }
}

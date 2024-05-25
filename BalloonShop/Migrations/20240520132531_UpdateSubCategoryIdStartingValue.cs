using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalloonShop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubCategoryIdStartingValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT ('SubCategories', RESEED, 1);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT ('SubCategories', RESEED, 0);");
        }
    }

}

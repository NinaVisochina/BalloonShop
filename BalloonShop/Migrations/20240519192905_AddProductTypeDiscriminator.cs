using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalloonShop.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTypeDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Products",
                newName: "ProductType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Products",
                newName: "Discriminator");
        }
    }
}

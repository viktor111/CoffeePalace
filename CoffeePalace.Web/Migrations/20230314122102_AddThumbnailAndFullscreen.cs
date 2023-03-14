using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeePalace.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailAndFullscreen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FullScreen",
                table: "ImageDatas",
                type: "longblob",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "ImageDatas",
                type: "longblob",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullScreen",
                table: "ImageDatas");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "ImageDatas");
        }
    }
}

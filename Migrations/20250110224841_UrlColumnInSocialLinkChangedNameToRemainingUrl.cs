using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPulse.Migrations
{
    /// <inheritdoc />
    public partial class UrlColumnInSocialLinkChangedNameToRemainingUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "SocialLinks");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "SocialLinks",
                newName: "RemainingUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingUrl",
                table: "SocialLinks",
                newName: "Url");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "SocialLinks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

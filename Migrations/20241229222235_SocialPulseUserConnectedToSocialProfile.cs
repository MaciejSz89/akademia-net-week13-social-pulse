using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPulse.Migrations
{
    /// <inheritdoc />
    public partial class SocialPulseUserConnectedToSocialProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SocialProfiles");

            migrationBuilder.AddColumn<string>(
                name: "SocialPulseUserId",
                table: "SocialProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SocialProfiles_SocialPulseUserId",
                table: "SocialProfiles",
                column: "SocialPulseUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialProfiles_AspNetUsers_SocialPulseUserId",
                table: "SocialProfiles",
                column: "SocialPulseUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialProfiles_AspNetUsers_SocialPulseUserId",
                table: "SocialProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SocialProfiles_SocialPulseUserId",
                table: "SocialProfiles");

            migrationBuilder.DropColumn(
                name: "SocialPulseUserId",
                table: "SocialProfiles");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SocialProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

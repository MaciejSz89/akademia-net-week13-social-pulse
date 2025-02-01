using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPulse.Migrations
{
    /// <inheritdoc />
    public partial class UserLinkStyleMergedWithSocialProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLinkStyles");

            migrationBuilder.DropColumn(
                name: "UserLinkStyleId",
                table: "SocialProfiles");

            migrationBuilder.AddColumn<string>(
                name: "UserLinkStyle",
                table: "SocialProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserLinkStyle",
                table: "SocialProfiles");

            migrationBuilder.AddColumn<int>(
                name: "UserLinkStyleId",
                table: "SocialProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserLinkStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialProfileId = table.Column<int>(type: "int", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLinkStyles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLinkStyles_SocialProfiles_SocialProfileId",
                        column: x => x.SocialProfileId,
                        principalTable: "SocialProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLinkStyles_SocialProfileId",
                table: "UserLinkStyles",
                column: "SocialProfileId",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPulse.Migrations
{
    /// <inheritdoc />
    public partial class IdentityColumnAutoInrementOff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_SocialLinks_SocialNetworks_SocialNetworkId", 
                table: "SocialLinks"               
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks"
            );

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SocialNetworks"
            );

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SocialNetworks",
                nullable: false
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks",
                column: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_SocialLinks_SocialNetworks_SocialNetworkId",
                table: "SocialLinks",
                column: "SocialNetworkId",
                principalTable: "SocialNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialLinks_SocialNetworks_SocialNetworkId",
                table: "SocialLinks"
            );
            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks"
            );

            migrationBuilder.AddColumn<int>(
                name: "Id_temp",
                table: "SocialNetworks",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.Sql("UPDATE SocialNetworks SET Id_temp = Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SocialNetworks"
            );

            migrationBuilder.RenameColumn(
                name: "Id_temp",
                newName: "Id",
                table: "SocialNetworks"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworks",
                table: "SocialNetworks",
                column: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_SocialLinks_SocialNetworks_SocialNetworkId",
                table: "SocialLinks",
                column: "SocialNetworkId",
                principalTable: "SocialNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}

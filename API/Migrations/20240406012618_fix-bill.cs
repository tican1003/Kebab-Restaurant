using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class fixbill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSucces",
                table: "Bills",
                newName: "IsSuccess");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_RoleId",
                table: "Bills",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Roles_RoleId",
                table: "Bills",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Roles_RoleId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_RoleId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "IsSuccess",
                table: "Bills",
                newName: "IsSucces");
        }
    }
}

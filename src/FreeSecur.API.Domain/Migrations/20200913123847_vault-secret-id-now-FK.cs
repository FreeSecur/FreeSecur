using Microsoft.EntityFrameworkCore.Migrations;

namespace FreeSecur.API.Domain.Migrations
{
    public partial class vaultsecretidnowFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaultSecrets_VaultItems_VaultItemId",
                table: "VaultSecrets");

            migrationBuilder.DropIndex(
                name: "IX_VaultSecrets_VaultItemId",
                table: "VaultSecrets");

            migrationBuilder.DropColumn(
                name: "VaultItemId",
                table: "VaultSecrets");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "VaultSecrets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_VaultSecrets_VaultItems_Id",
                table: "VaultSecrets",
                column: "Id",
                principalTable: "VaultItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaultSecrets_VaultItems_Id",
                table: "VaultSecrets");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "VaultSecrets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "VaultItemId",
                table: "VaultSecrets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_VaultSecrets_VaultItemId",
                table: "VaultSecrets",
                column: "VaultItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaultSecrets_VaultItems_VaultItemId",
                table: "VaultSecrets",
                column: "VaultItemId",
                principalTable: "VaultItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

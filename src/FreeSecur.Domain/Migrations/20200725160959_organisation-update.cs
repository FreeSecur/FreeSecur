using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreeSecur.Domain.Migrations
{
    public partial class organisationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VaultItems_VaultId",
                table: "VaultItems");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vaults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organisation_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organisation_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrganistationId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Organisation_OrganistationId",
                        column: x => x.OrganistationId,
                        principalTable: "Organisation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganisationUserRight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationUserId = table.Column<int>(type: "int", nullable: false),
                    AccessRight = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    ModifiedById = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationUserRight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganisationUserRight_OrganisationUser_OrganisationUserId",
                        column: x => x.OrganisationUserId,
                        principalTable: "OrganisationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUserRight_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationUserRight_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "uk_vault_vaultid_key",
                table: "VaultItems",
                columns: new[] { "VaultId", "Key" },
                unique: true,
                filter: "[Key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OrganisationId",
                table: "Teams",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_CreatedById",
                table: "Organisation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_ModifiedById",
                table: "Organisation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_CreatedById",
                table: "OrganisationUser",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_ModifiedById",
                table: "OrganisationUser",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_OrganistationId",
                table: "OrganisationUser",
                column: "OrganistationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_OwnerId",
                table: "OrganisationUser",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUser_UserId",
                table: "OrganisationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUserRight_CreatedById",
                table: "OrganisationUserRight",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUserRight_ModifiedById",
                table: "OrganisationUserRight",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationUserRight_OrganisationUserId",
                table: "OrganisationUserRight",
                column: "OrganisationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organisation_OrganisationId",
                table: "Teams",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organisation_OrganisationId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "OrganisationUserRight");

            migrationBuilder.DropTable(
                name: "OrganisationUser");

            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropIndex(
                name: "uk_vault_vaultid_key",
                table: "VaultItems");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OrganisationId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vaults");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_VaultItems_VaultId",
                table: "VaultItems",
                column: "VaultId");
        }
    }
}

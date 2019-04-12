using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlatform.DAL.Migrations
{
    public partial class SupportTicketAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportTicketCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: true),
                    ColorHex = table.Column<string>(maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicketCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportTicket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Content = table.Column<string>(maxLength: 2500, nullable: true),
                    IsSolved = table.Column<bool>(nullable: true),
                    IssuedUserId = table.Column<string>(maxLength: 450, nullable: true),
                    FixedUserId = table.Column<string>(maxLength: 450, nullable: true),
                    SupportTicketCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportTicket_AspNetUsers_FixedUserId",
                        column: x => x.FixedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportTicket_AspNetUsers_IssuedUserId",
                        column: x => x.IssuedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportTicket_SupportTicketCategories_SupportTicketCategoryId",
                        column: x => x.SupportTicketCategoryId,
                        principalTable: "SupportTicketCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_FixedUserId",
                table: "SupportTicket",
                column: "FixedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_IssuedUserId",
                table: "SupportTicket",
                column: "IssuedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicket_SupportTicketCategoryId",
                table: "SupportTicket",
                column: "SupportTicketCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportTicket");

            migrationBuilder.DropTable(
                name: "SupportTicketCategories");
        }
    }
}

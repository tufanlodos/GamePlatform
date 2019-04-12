using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamePlatform.DAL.Migrations
{
    public partial class SupportTicketFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportTicketSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Content = table.Column<string>(maxLength: 1000, nullable: true),
                    SupportTicketId = table.Column<int>(nullable: false),
                    AnsweredUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTicketSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportTicketSteps_AspNetUsers_AnsweredUserId",
                        column: x => x.AnsweredUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportTicketSteps_SupportTicket_SupportTicketId",
                        column: x => x.SupportTicketId,
                        principalTable: "SupportTicket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicketSteps_AnsweredUserId",
                table: "SupportTicketSteps",
                column: "AnsweredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportTicketSteps_SupportTicketId",
                table: "SupportTicketSteps",
                column: "SupportTicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportTicketSteps");
        }
    }
}

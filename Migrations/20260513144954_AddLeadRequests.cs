using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Msa3edcomAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Budget = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    AttachmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadRequests_CreatedAt",
                table: "LeadRequests",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_LeadRequests_Status",
                table: "LeadRequests",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadRequests");
        }
    }
}

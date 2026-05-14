using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Msa3edcomAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddCmsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "SiteContents",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HeroImageUrl",
                table: "SiteContents",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutImageUrl",
                table: "SiteContents",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerButtonTextAr",
                table: "SiteContents",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerButtonTextEn",
                table: "SiteContents",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerButtonUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerImageUrl",
                table: "SiteContents",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerSubtitleAr",
                table: "SiteContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerSubtitleEn",
                table: "SiteContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerTitleAr",
                table: "SiteContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CtaBannerTitleEn",
                table: "SiteContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaviconUrl",
                table: "SiteContents",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroEyebrowAr",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroEyebrowEn",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroSecondaryCtaTextAr",
                table: "SiteContents",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroSecondaryCtaTextEn",
                table: "SiteContents",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeroSecondaryCtaUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoDarkUrl",
                table: "SiteContents",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioLeadAr",
                table: "SiteContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioLeadEn",
                table: "SiteContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioTitleAr",
                table: "SiteContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioTitleEn",
                table: "SiteContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesLeadAr",
                table: "SiteContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesLeadEn",
                table: "SiteContents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesTitleAr",
                table: "SiteContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicesTitleEn",
                table: "SiteContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FaqItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionEn = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    QuestionAr = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AnswerEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    AnswerAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Technologies = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ProjectUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetaTitleEn = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaTitleAr = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
                    MetaDescriptionEn = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    MetaDescriptionAr = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    MetaKeywordsEn = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    MetaKeywordsAr = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    OpenGraphImage = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    FaviconUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    GoogleAnalyticsCode = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    PrimaryColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SecondaryColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelEn = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LabelAr = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechStackItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechStackItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    RoleEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RoleAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CompanyEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CompanyAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    QuoteEn = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    QuoteAr = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaqItems_DisplayOrder",
                table: "FaqItems",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_Category",
                table: "MediaItems",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_CreatedAt",
                table: "MediaItems",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioItems_DisplayOrder",
                table: "PortfolioItems",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessSteps_DisplayOrder",
                table: "ProcessSteps",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_DisplayOrder",
                table: "ServiceItems",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_StatItems_DisplayOrder",
                table: "StatItems",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_TechStackItems_DisplayOrder",
                table: "TechStackItems",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_DisplayOrder",
                table: "Testimonials",
                column: "DisplayOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaqItems");

            migrationBuilder.DropTable(
                name: "MediaItems");

            migrationBuilder.DropTable(
                name: "PortfolioItems");

            migrationBuilder.DropTable(
                name: "ProcessSteps");

            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "StatItems");

            migrationBuilder.DropTable(
                name: "TechStackItems");

            migrationBuilder.DropTable(
                name: "Testimonials");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerButtonTextAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerButtonTextEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerButtonUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerImageUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerSubtitleAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerSubtitleEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerTitleAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "CtaBannerTitleEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "FaviconUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "HeroEyebrowAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "HeroEyebrowEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "HeroSecondaryCtaTextAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "HeroSecondaryCtaTextEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "HeroSecondaryCtaUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "LogoDarkUrl",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "PortfolioLeadAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "PortfolioLeadEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "PortfolioTitleAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "PortfolioTitleEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "ServicesLeadAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "ServicesLeadEn",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "ServicesTitleAr",
                table: "SiteContents");

            migrationBuilder.DropColumn(
                name: "ServicesTitleEn",
                table: "SiteContents");

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HeroImageUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AboutImageUrl",
                table: "SiteContents",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);
        }
    }
}

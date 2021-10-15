using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PublicWorkflow.Infrastructure.ApplicationDb
{
    public partial class addRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUser_Organization_OrganizationId",
                table: "OrganizationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessConfig_Organization_OrganizationId",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "AttachApprovalPdf",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "FeedBackUrl",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "IncludeApproverDetails",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "NotifyAllApproverOnApproval",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "NotifyInitiatorOnApproval",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "PublishType",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "SingleRejection",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "CanCreateConfig",
                table: "OrganizationUser");

            migrationBuilder.DropColumn(
                name: "CanCreateUser",
                table: "OrganizationUser");

            migrationBuilder.DropColumn(
                name: "CanManageConfig",
                table: "OrganizationUser");

            migrationBuilder.DropColumn(
                name: "CanManageUser",
                table: "OrganizationUser");

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "ProcessConfig",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ProcessConfig",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "OrganizationUser",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "PublishOption",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Publish = table.Column<int>(type: "integer", nullable: false),
                    Url_Topic = table.Column<string>(type: "text", nullable: true),
                    PostObjectKeyNames = table.Column<string>(type: "text", nullable: true),
                    ProcessConfigId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishOption_ProcessConfig_ProcessConfigId",
                        column: x => x.ProcessConfigId,
                        principalTable: "ProcessConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Requirement = table.Column<int>(type: "integer", nullable: false),
                    ProcessConfigId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirement_ProcessConfig_ProcessConfigId",
                        column: x => x.ProcessConfigId,
                        principalTable: "ProcessConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublishOption_ProcessConfigId",
                table: "PublishOption",
                column: "ProcessConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_ProcessConfigId",
                table: "Requirement",
                column: "ProcessConfigId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUser_Organization_OrganizationId",
                table: "OrganizationUser",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessConfig_Organization_OrganizationId",
                table: "ProcessConfig",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUser_Organization_OrganizationId",
                table: "OrganizationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessConfig_Organization_OrganizationId",
                table: "ProcessConfig");

            migrationBuilder.DropTable(
                name: "PublishOption");

            migrationBuilder.DropTable(
                name: "Requirement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProcessConfig");

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "ProcessConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AttachApprovalPdf",
                table: "ProcessConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FeedBackUrl",
                table: "ProcessConfig",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeApproverDetails",
                table: "ProcessConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyAllApproverOnApproval",
                table: "ProcessConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyInitiatorOnApproval",
                table: "ProcessConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PublishType",
                table: "ProcessConfig",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SingleRejection",
                table: "ProcessConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "OrganizationUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanCreateConfig",
                table: "OrganizationUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanCreateUser",
                table: "OrganizationUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanManageConfig",
                table: "OrganizationUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanManageUser",
                table: "OrganizationUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUser_Organization_OrganizationId",
                table: "OrganizationUser",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessConfig_Organization_OrganizationId",
                table: "ProcessConfig",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

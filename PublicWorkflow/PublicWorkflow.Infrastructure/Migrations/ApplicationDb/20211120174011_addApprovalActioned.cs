using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicWorkflow.Infrastructure.Migrations.ApplicationDb
{
    public partial class addApprovalActioned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ApprovalConfig_ApprovalConfigId",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessRule_ProcessConfig_ProcessConfigId",
                table: "ProcessRule");

            migrationBuilder.AlterColumn<long>(
                name: "ProcessConfigId",
                table: "ProcessRule",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProcessLevelsConcurrently",
                table: "ProcessConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "ApprovalConfigId",
                table: "ApprovalRule",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlreadyActioned",
                table: "Approval",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ApprovalConfig_ApprovalConfigId",
                table: "ApprovalRule",
                column: "ApprovalConfigId",
                principalTable: "ApprovalConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessRule_ProcessConfig_ProcessConfigId",
                table: "ProcessRule",
                column: "ProcessConfigId",
                principalTable: "ProcessConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRule_ApprovalConfig_ApprovalConfigId",
                table: "ApprovalRule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessRule_ProcessConfig_ProcessConfigId",
                table: "ProcessRule");

            migrationBuilder.DropColumn(
                name: "ProcessLevelsConcurrently",
                table: "ProcessConfig");

            migrationBuilder.DropColumn(
                name: "AlreadyActioned",
                table: "Approval");

            migrationBuilder.AlterColumn<long>(
                name: "ProcessConfigId",
                table: "ProcessRule",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ApprovalConfigId",
                table: "ApprovalRule",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRule_ApprovalConfig_ApprovalConfigId",
                table: "ApprovalRule",
                column: "ApprovalConfigId",
                principalTable: "ApprovalConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessRule_ProcessConfig_ProcessConfigId",
                table: "ProcessRule",
                column: "ProcessConfigId",
                principalTable: "ProcessConfig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

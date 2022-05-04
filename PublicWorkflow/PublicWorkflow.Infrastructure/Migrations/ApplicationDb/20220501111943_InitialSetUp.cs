using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PublicWorkflow.Infrastructure.Migrations.ApplicationDb
{
    public partial class InitialSetUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "application");

            migrationBuilder.CreateTable(
                name: "audit_logs",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    table_name = table.Column<string>(type: "text", nullable: true),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    old_values = table.Column<string>(type: "text", nullable: true),
                    new_values = table.Column<string>(type: "text", nullable: true),
                    affected_columns = table.Column<string>(type: "text", nullable: true),
                    primary_key = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "history",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    process_id = table.Column<long>(type: "bigint", nullable: true),
                    approval_id = table.Column<long>(type: "bigint", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    action = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    motto = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    logo = table.Column<string>(type: "text", nullable: true),
                    contact_email = table.Column<string>(type: "text", nullable: true),
                    address1 = table.Column<string>(type: "text", nullable: true),
                    address2 = table.Column<string>(type: "text", nullable: true),
                    province = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    land_mark = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization_user",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: true),
                    organization_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_organization_user_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "application",
                        principalTable: "organization",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "process_config",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    required_approval_levels = table.Column<int>(type: "integer", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    process_levels_concurrently = table.Column<bool>(type: "boolean", nullable: false),
                    organization_id = table.Column<long>(type: "bigint", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_process_config", x => x.id);
                    table.ForeignKey(
                        name: "fk_process_config_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "application",
                        principalTable: "organization",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "approval_config",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    process_config_id = table.Column<long>(type: "bigint", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    required_approvers = table.Column<int>(type: "integer", nullable: false),
                    approvers = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approval_config", x => x.id);
                    table.ForeignKey(
                        name: "fk_approval_config_process_config_process_config_id",
                        column: x => x.process_config_id,
                        principalSchema: "application",
                        principalTable: "process_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "process",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    process_config_id = table.Column<long>(type: "bigint", nullable: false),
                    attachements = table.Column<string>(type: "text", nullable: true),
                    job_reference_id = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<string>(type: "text", nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    completed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_process", x => x.id);
                    table.ForeignKey(
                        name: "fk_process_process_config_process_config_id",
                        column: x => x.process_config_id,
                        principalSchema: "application",
                        principalTable: "process_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "process_rule",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    values = table.Column<string[]>(type: "text[]", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    condition = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    process_config_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_process_rule", x => x.id);
                    table.ForeignKey(
                        name: "fk_process_rule_process_config_process_config_id",
                        column: x => x.process_config_id,
                        principalSchema: "application",
                        principalTable: "process_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publish_option",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publish = table.Column<int>(type: "integer", nullable: false),
                    url_topic = table.Column<string>(type: "text", nullable: true),
                    post_object_key_names = table.Column<string>(type: "text", nullable: true),
                    process_config_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publish_option", x => x.id);
                    table.ForeignKey(
                        name: "fk_publish_option_process_config_process_config_id",
                        column: x => x.process_config_id,
                        principalSchema: "application",
                        principalTable: "process_config",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "requirement",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    requirement = table.Column<int>(type: "integer", nullable: false),
                    process_config_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requirement", x => x.id);
                    table.ForeignKey(
                        name: "fk_requirement_process_config_process_config_id",
                        column: x => x.process_config_id,
                        principalSchema: "application",
                        principalTable: "process_config",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "approval_rule",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    values = table.Column<string[]>(type: "text[]", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    condition = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<int>(type: "integer", nullable: false),
                    approval_config_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approval_rule", x => x.id);
                    table.ForeignKey(
                        name: "fk_approval_rule_approval_config_approval_config_id",
                        column: x => x.approval_config_id,
                        principalSchema: "application",
                        principalTable: "approval_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approval",
                schema: "application",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    treated = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    review_status = table.Column<int>(type: "integer", nullable: false),
                    review_started = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    actioned = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    process_id = table.Column<long>(type: "bigint", nullable: false),
                    approvalconfig_id = table.Column<long>(type: "bigint", nullable: false),
                    already_approved = table.Column<string>(type: "text", nullable: true),
                    already_actioned = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_approval", x => x.id);
                    table.ForeignKey(
                        name: "fk_approval_approval_config_approvalconfig_id",
                        column: x => x.approvalconfig_id,
                        principalSchema: "application",
                        principalTable: "approval_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_approval_process_process_id",
                        column: x => x.process_id,
                        principalSchema: "application",
                        principalTable: "process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_approval_approvalconfig_id",
                schema: "application",
                table: "approval",
                column: "approvalconfig_id");

            migrationBuilder.CreateIndex(
                name: "ix_approval_process_id",
                schema: "application",
                table: "approval",
                column: "process_id");

            migrationBuilder.CreateIndex(
                name: "ix_approval_config_process_config_id",
                schema: "application",
                table: "approval_config",
                column: "process_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_approval_rule_approval_config_id",
                schema: "application",
                table: "approval_rule",
                column: "approval_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_organization_user_organization_id",
                schema: "application",
                table: "organization_user",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_process_process_config_id",
                schema: "application",
                table: "process",
                column: "process_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_process_config_organization_id",
                schema: "application",
                table: "process_config",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_process_rule_process_config_id",
                schema: "application",
                table: "process_rule",
                column: "process_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_publish_option_process_config_id",
                schema: "application",
                table: "publish_option",
                column: "process_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_requirement_process_config_id",
                schema: "application",
                table: "requirement",
                column: "process_config_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approval",
                schema: "application");

            migrationBuilder.DropTable(
                name: "approval_rule",
                schema: "application");

            migrationBuilder.DropTable(
                name: "audit_logs",
                schema: "application");

            migrationBuilder.DropTable(
                name: "history",
                schema: "application");

            migrationBuilder.DropTable(
                name: "organization_user",
                schema: "application");

            migrationBuilder.DropTable(
                name: "process_rule",
                schema: "application");

            migrationBuilder.DropTable(
                name: "publish_option",
                schema: "application");

            migrationBuilder.DropTable(
                name: "requirement",
                schema: "application");

            migrationBuilder.DropTable(
                name: "process",
                schema: "application");

            migrationBuilder.DropTable(
                name: "approval_config",
                schema: "application");

            migrationBuilder.DropTable(
                name: "process_config",
                schema: "application");

            migrationBuilder.DropTable(
                name: "organization",
                schema: "application");
        }
    }
}

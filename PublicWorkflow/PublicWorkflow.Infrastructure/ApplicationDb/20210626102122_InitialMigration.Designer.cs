﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PublicWorkflow.Infrastructure.DbContexts;

namespace PublicWorkflow.Infrastructure.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210626102122_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NewValues")
                        .HasColumnType("text");

                    b.Property<string>("OldValues")
                        .HasColumnType("text");

                    b.Property<string>("PrimaryKey")
                        .HasColumnType("text");

                    b.Property<string>("TableName")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.Approval", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("Actioned")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("AlreadyApproved")
                        .HasColumnType("text");

                    b.Property<long>("ApprovalconfigId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("ProcessId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ReviewStarted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ReviewStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<bool>("Treated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalconfigId");

                    b.HasIndex("ProcessId");

                    b.ToTable("Approval");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.ApprovalConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Approvers")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("ProcessConfigId")
                        .HasColumnType("bigint");

                    b.Property<int>("RequiredApprovers")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProcessConfigId");

                    b.ToTable("ApprovalConfig");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.History", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Action")
                        .HasColumnType("text");

                    b.Property<long?>("ApprovalId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long?>("ProcessId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("History");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Address1")
                        .HasColumnType("text");

                    b.Property<string>("Address2")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LandMark")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Motto")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Province")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.OrganizationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("CanCreateConfig")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanCreateUser")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanManageConfig")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanManageUser")
                        .HasColumnType("boolean");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationUser");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.Process", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Attachements")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Completed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("JobReferenceId")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("ProcessConfigId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProcessConfigId");

                    b.ToTable("Process");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.ProcessConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("AttachApprovalPdf")
                        .HasColumnType("boolean");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FeedBackUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IncludeApproverDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("NotifyAllApproverOnApproval")
                        .HasColumnType("boolean");

                    b.Property<bool>("NotifyInitiatorOnApproval")
                        .HasColumnType("boolean");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<int>("PublishType")
                        .HasColumnType("integer");

                    b.Property<int>("RequiredApprovalLevels")
                        .HasColumnType("integer");

                    b.Property<bool>("SingleRejection")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("ProcessConfig");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.Approval", b =>
                {
                    b.HasOne("PublicWorkflow.Domain.Entities.Catalog.ApprovalConfig", "ApprovalConfig")
                        .WithMany("Approvals")
                        .HasForeignKey("ApprovalconfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublicWorkflow.Domain.Entities.Catalog.Process", "Process")
                        .WithMany("Actions")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovalConfig");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.ApprovalConfig", b =>
                {
                    b.HasOne("PublicWorkflow.Domain.Entities.Catalog.ProcessConfig", "ProcessConfig")
                        .WithMany("ApprovalConfigs")
                        .HasForeignKey("ProcessConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessConfig");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.OrganizationUser", b =>
                {
                    b.HasOne("PublicWorkflow.Domain.Entities.Catalog.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.Process", b =>
                {
                    b.HasOne("PublicWorkflow.Domain.Entities.Catalog.ProcessConfig", "ProcessConfig")
                        .WithMany()
                        .HasForeignKey("ProcessConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessConfig");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.ProcessConfig", b =>
                {
                    b.HasOne("PublicWorkflow.Domain.Entities.Catalog.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.ApprovalConfig", b =>
                {
                    b.Navigation("Approvals");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.Process", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("PublicWorkflow.Domain.Entities.Catalog.ProcessConfig", b =>
                {
                    b.Navigation("ApprovalConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}

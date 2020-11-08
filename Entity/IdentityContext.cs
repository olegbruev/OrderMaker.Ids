using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mtd.OrderMaker.Ids.Entity
{
    public class IdentityContext : IdentityDbContext<WebAppUser, WebAppRole, Guid>
    {

        public virtual DbSet<MtdGroup> MtdGroups { get; set; }
        public virtual DbSet<MtdStoreOwner> MtdStoreOwners { get; set; }
        public virtual DbSet<MtdFilterOwner> MtdFilterOwners { get; set; }
        public virtual DbSet<MtdLogDocument> MtdLogDocuments { get; set; }
        public virtual DbSet<MtdPolicy> MtdPolicies { get; set; }
        public virtual DbSet<MtdPolicyForm> MtdPolicyForms { get; set; }
        public virtual DbSet<MtdPolicyPart> MtdPolicyParts { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MtdGroup>(entity =>
            {
                entity.ToTable("mtd_group");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(255)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("action_time")
                    .HasColumnType("nvarchar(1024)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("uniqueidentifier");

            });

            modelBuilder.Entity<MtdStoreOwner>(entity =>
            {
                entity.ToTable("mtd_store_owner");

                entity.HasIndex(e => e.StoreId)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StoreId)
                    .HasColumnName("store_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("uniqueidentifier");

            });

            modelBuilder.Entity<MtdFilterOwner>(entity =>
            {
                entity.ToTable("mtd_filter_owner");

                entity.HasIndex(e => e.FilterId)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.FilterId)
                    .HasColumnName("filter_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("uniqueidentifier");

            });

            modelBuilder.Entity<MtdLogDocument>(entity =>
            {
                entity.ToTable("mtd_log_document");

                entity.HasIndex(e => e.TimeCh)
                    .HasName("ix_date");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StoreId)
                    .HasName("fk_log_document_store_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.TimeCh)
                    .HasColumnName("timech")
                    .HasColumnType("datetime");

                entity.Property(e => e.StoreId)
                    .IsRequired()
                    .HasColumnName("store_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("uniqueidentifier");
            });

            modelBuilder.Entity<MtdPolicy>(entity =>
            {
                entity.ToTable("mtd_policy");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(1024)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(255)");
            });

            modelBuilder.Entity<MtdPolicyForm>(entity =>
            {
                entity.ToTable("mtd_policy_form");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.FormId)
                    .HasName("policy_forms_form_idx");

                entity.HasIndex(e => e.PolicyId)
                    .HasName("fk_policy_forms_policy_idx");

                entity.HasIndex(e => new { e.PolicyId, e.FormId })
                    .HasName("UNIQUE_FORM")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.ChangeDate)
                    .HasColumnName("change_date")
                    .HasColumnType("bit");

                entity.Property(e => e.ChangeOwner)
                    .HasColumnName("change_owner")
                    .HasColumnType("bit");

                entity.Property(e => e.Create)
                    .HasColumnName("create")
                    .HasColumnType("bit");

                entity.Property(e => e.DeleteAll)
                    .HasColumnName("delete_all")
                    .HasColumnType("bit");

                entity.Property(e => e.DeleteGroup)
                    .HasColumnName("delete_group")
                    .HasColumnType("bit");

                entity.Property(e => e.DeleteOwn)
                    .HasColumnName("delete_own")
                    .HasColumnType("bit");

                entity.Property(e => e.EditAll)
                    .HasColumnName("edit_all")
                    .HasColumnType("bit");

                entity.Property(e => e.EditGroup)
                    .HasColumnName("edit_group")
                    .HasColumnType("bit");

                entity.Property(e => e.EditOwn)
                    .HasColumnName("edit_own")
                    .HasColumnType("bit");

                entity.Property(e => e.FormId)
                    .IsRequired()
                    .HasColumnName("form_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.PolicyId)
                    .IsRequired()
                    .HasColumnName("policy_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.ViewAll)
                    .HasColumnName("view_all")
                    .HasColumnType("bit");

                entity.Property(e => e.ViewGroup)
                    .HasColumnName("view_group")
                    .HasColumnType("bit");

                entity.Property(e => e.ViewOwn)
                    .HasColumnName("view_own")
                    .HasColumnType("bit");

                entity.Property(e => e.ExportToExcel)
                    .HasColumnName("export_to_excel")
                    .HasColumnType("bit");                    

                entity.HasOne(d => d.MtdPolicy)
                    .WithMany(p => p.MtdPolicyForms)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("fk_policy_forms_policy");
            });

            modelBuilder.Entity<MtdPolicyPart>(entity =>
            {
                entity.ToTable("mtd_policy_part");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => new { e.PolicyId, e.PartId })
                    .HasName("UNIQUE_PART")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Create)
                    .HasColumnName("create")
                    .HasColumnType("bit");

                entity.Property(e => e.Edit)
                    .HasColumnName("edit")
                    .HasColumnType("bit");                    

                entity.Property(e => e.PartId)
                    .IsRequired()
                    .HasColumnName("part_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.PolicyId)
                    .IsRequired()
                    .HasColumnName("policy_id")
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.View)
                    .HasColumnName("view")
                    .HasColumnType("bit");

                entity.HasOne(d => d.MtdPolicy)
                    .WithMany(p => p.MtdPolicyParts)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("fk_policy_part_policy");
            });
        }
    }
}

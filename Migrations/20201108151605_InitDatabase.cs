using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mtd.OrderMaker.Ids.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Seq = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_filter_owner",
                columns: table => new
                {
                    filter_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_filter_owner", x => x.filter_id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_group",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    action_time = table.Column<string>(type: "nvarchar(1024)", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_log_document",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    timech = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_log_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_policy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_policy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtd_store_owner",
                columns: table => new
                {
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_store_owner", x => x.store_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtd_policy_form",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    policy_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    form_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create = table.Column<bool>(type: "bit", nullable: false),
                    edit_all = table.Column<bool>(type: "bit", nullable: false),
                    edit_group = table.Column<bool>(type: "bit", nullable: false),
                    edit_own = table.Column<bool>(type: "bit", nullable: false),
                    view_all = table.Column<bool>(type: "bit", nullable: false),
                    view_group = table.Column<bool>(type: "bit", nullable: false),
                    view_own = table.Column<bool>(type: "bit", nullable: false),
                    delete_all = table.Column<bool>(type: "bit", nullable: false),
                    delete_group = table.Column<bool>(type: "bit", nullable: false),
                    delete_own = table.Column<bool>(type: "bit", nullable: false),
                    change_owner = table.Column<bool>(type: "bit", nullable: false),
                    change_date = table.Column<bool>(type: "bit", nullable: false),
                    export_to_excel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_policy_form", x => x.id);
                    table.ForeignKey(
                        name: "fk_policy_forms_policy",
                        column: x => x.policy_id,
                        principalTable: "mtd_policy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtd_policy_part",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    policy_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    part_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    create = table.Column<bool>(type: "bit", nullable: false),
                    edit = table.Column<bool>(type: "bit", nullable: false),
                    view = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mtd_policy_part", x => x.id);
                    table.ForeignKey(
                        name: "fk_policy_part_policy",
                        column: x => x.policy_id,
                        principalTable: "mtd_policy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_filter_owner",
                column: "filter_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_group",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_log_document",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_log_document_store_idx",
                table: "mtd_log_document",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_date",
                table: "mtd_log_document",
                column: "timech");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_policy",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "policy_forms_form_idx",
                table: "mtd_policy_form",
                column: "form_id");

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_policy_form",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_policy_forms_policy_idx",
                table: "mtd_policy_form",
                column: "policy_id");

            migrationBuilder.CreateIndex(
                name: "UNIQUE_FORM",
                table: "mtd_policy_form",
                columns: new[] { "policy_id", "form_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_policy_part",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UNIQUE_PART",
                table: "mtd_policy_part",
                columns: new[] { "policy_id", "part_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "mtd_store_owner",
                column: "store_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "mtd_filter_owner");

            migrationBuilder.DropTable(
                name: "mtd_group");

            migrationBuilder.DropTable(
                name: "mtd_log_document");

            migrationBuilder.DropTable(
                name: "mtd_policy_form");

            migrationBuilder.DropTable(
                name: "mtd_policy_part");

            migrationBuilder.DropTable(
                name: "mtd_store_owner");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "mtd_policy");
        }
    }
}

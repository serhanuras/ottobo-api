using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ottobo.Infrastructure.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "EntityFrameworkHiLoSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    map_id = table.Column<long>(nullable: false),
                    x_coordinate = table.Column<string>(nullable: true),
                    y_coordinate = table.Column<string>(nullable: true),
                    theate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_types",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_types",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "robot",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_robot", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_types",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    city_id = table.Column<int>(nullable: false),
                    town_id = table.Column<int>(nullable: false),
                    order_type_id = table.Column<Guid>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_order_types_order_type_id",
                        column: x => x.order_type_id,
                        principalTable: "order_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "master_data",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    sku_code = table.Column<string>(nullable: true),
                    sku_name = table.Column<string>(nullable: true),
                    barcode = table.Column<string>(nullable: true),
                    unit_pack = table.Column<int>(nullable: false),
                    unit_case = table.Column<int>(nullable: false),
                    unit_palet = table.Column<int>(nullable: false),
                    is_packaged = table.Column<bool>(nullable: false),
                    is_cased = table.Column<bool>(nullable: false),
                    case_width = table.Column<decimal>(nullable: false),
                    case_height = table.Column<decimal>(nullable: false),
                    case_depth = table.Column<decimal>(nullable: false),
                    case_m3 = table.Column<decimal>(nullable: false),
                    is_signed_on = table.Column<bool>(nullable: false),
                    image_url = table.Column<string>(nullable: true),
                    package_height = table.Column<decimal>(nullable: false),
                    purchase_type_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_master_data_purchase_types_purchase_type_id",
                        column: x => x.purchase_type_id,
                        principalTable: "purchase_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "robot_task",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    robot_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_robot_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_robot_task_robot_robot_id",
                        column: x => x.robot_id,
                        principalTable: "robot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roles_claim",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    role_id = table.Column<long>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_claim", x => x.id);
                    table.ForeignKey(
                        name: "FK_roles_claim_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_claim",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo),
                    user_id = table.Column<long>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_claim", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_claim_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_login",
                columns: table => new
                {
                    login_provider = table.Column<string>(nullable: false),
                    provider_key = table.Column<string>(nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_login", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_users_login_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_role",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false),
                    role_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_role", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_users_role_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_role_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_token",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false),
                    login_provider = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_token", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "FK_users_token_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    last_movement_date = table.Column<DateTime>(nullable: false),
                    location_level = table.Column<string>(nullable: true),
                    master_data_id = table.Column<Guid>(nullable: false),
                    stock_type_id = table.Column<Guid>(nullable: false),
                    location_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_stocks_location_location_id",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_master_data_master_data_id",
                        column: x => x.master_data_id,
                        principalTable: "master_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_stock_types_stock_type_id",
                        column: x => x.stock_type_id,
                        principalTable: "stock_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    picked_quantity = table.Column<int>(nullable: false),
                    basket_id = table.Column<int>(nullable: false),
                    stock_id = table.Column<Guid>(nullable: false),
                    order_id = table.Column<Guid>(nullable: false),
                    robot_task_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_details_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_details_robot_task_robot_task_id",
                        column: x => x.robot_task_id,
                        principalTable: "robot_task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_details_stocks_stock_id",
                        column: x => x.stock_id,
                        principalTable: "stocks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_master_data_purchase_type_id",
                table: "master_data",
                column: "purchase_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_order_id",
                table: "order_details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_robot_task_id",
                table: "order_details",
                column: "robot_task_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_stock_id",
                table: "order_details",
                column: "stock_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_order_type_id",
                table: "orders",
                column: "order_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_robot_task_robot_id",
                table: "robot_task",
                column: "robot_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_claim_role_id",
                table: "roles_claim",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_location_id",
                table: "stocks",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_master_data_id",
                table: "stocks",
                column: "master_data_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stocks_stock_type_id",
                table: "stocks",
                column: "stock_type_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_claim_user_id",
                table: "users_claim",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_login_user_id",
                table: "users_login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_role_id",
                table: "users_role",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "roles_claim");

            migrationBuilder.DropTable(
                name: "users_claim");

            migrationBuilder.DropTable(
                name: "users_login");

            migrationBuilder.DropTable(
                name: "users_role");

            migrationBuilder.DropTable(
                name: "users_token");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "robot_task");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "order_types");

            migrationBuilder.DropTable(
                name: "robot");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "master_data");

            migrationBuilder.DropTable(
                name: "stock_types");

            migrationBuilder.DropTable(
                name: "purchase_types");

            migrationBuilder.DropSequence(
                name: "EntityFrameworkHiLoSequence");
        }
    }
}

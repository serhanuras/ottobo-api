using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ottobo.Infrastructure.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_logs");
            
            
            migrationBuilder.CreateTable(
                name: "api_logs",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    action_by = table.Column<string>(nullable: true),
                    controller_name = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true),
                    exception = table.Column<string>(nullable: true),
                    last_accessed = table.Column<DateTime>(nullable: true),
                    log_type = table.Column<string>(nullable: true),
                    method_name = table.Column<string>(nullable: true),
                    request_body = table.Column<string>(nullable: true),
                    response_body = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable: true),
                    updated_on = table.Column<DateTime>(nullable: true),
                },
                constraints: table => { table.PrimaryKey("PK_api_logs", x => x.id); });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

           
        }
    }
}

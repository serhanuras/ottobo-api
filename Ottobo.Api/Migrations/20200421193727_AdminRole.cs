using Microsoft.EntityFrameworkCore.Migrations;

namespace Ottobo.Migrations
{
    public partial class AdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            Insert into roles (id, name, normalized_name)
            values (1, 'Admin', 'Admin')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql(@"delete roles
             where id = 1");
        }
    }
}

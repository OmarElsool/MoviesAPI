using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Security;

#nullable disable

namespace Movies.Migrations
{
    public partial class addAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES(N'8aa6bfbe-7d62-4ed3-a495-33df61fcb669', N'Omar', N'Elsool', N'Admin', N'ADMIN', N'Admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAEAACcQAAAAEE/EJMCg9hv8pYpANs17FpzUc+gNKK5MDYufW0BbCHCtvgezDY2I7cN/8OQMP1Yeqw==', N'YGLKTM2432EHONTDZFQ74YBB2HNHIYUP', N'1adf28b5-9e8e-4cbd-82a7-fb065bbdcaa9', NULL, 0, 0, NULL, 1, 0)");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [dbo].[AspNetUsers] WHERE Id = '8aa6bfbe-7d62-4ed3-a495-33df61fcb669'");
        }
    }
}

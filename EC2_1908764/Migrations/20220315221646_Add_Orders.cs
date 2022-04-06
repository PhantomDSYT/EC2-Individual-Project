using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EC2_1908764.Migrations
{
    public partial class Add_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Item = table.Column<string>(nullable: false),
                    ItemPic = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    DateOrderd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}

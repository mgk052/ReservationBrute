using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationBrute.Migrations.ReservationBruteTableMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableBooking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    location = table.Column<string>(nullable: true),
                    booked_seats = table.Column<int>(nullable: false),
                    booking_no = table.Column<int>(nullable: false),
                    date_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableBooking", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableBooking");
        }
    }
}

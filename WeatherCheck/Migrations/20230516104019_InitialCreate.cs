using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherCheck.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<float>(nullable: true),
                    RelativeHumidity = table.Column<float>(nullable: true),
                    DewPoint = table.Column<float>(nullable: true),
                    AtmosphericPressure = table.Column<short>(nullable: true),
                    WindDirection = table.Column<string>(maxLength: 50, nullable: true),
                    WindSpeed = table.Column<byte>(nullable: true),
                    Cloudiness = table.Column<byte>(nullable: true),
                    CloudCeiling = table.Column<short>(nullable: true),
                    HorizontalVisibility = table.Column<string>(maxLength: 50, nullable: true),
                    WeatherEvents = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}

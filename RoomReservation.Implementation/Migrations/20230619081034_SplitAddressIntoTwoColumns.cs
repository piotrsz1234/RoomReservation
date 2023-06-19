using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomReservation.Implementation.Migrations
{
    public partial class SplitAddressIntoTwoColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Building",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "BuildingNumber",
                table: "Building",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "Building");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Building",
                newName: "Address");
        }
    }
}

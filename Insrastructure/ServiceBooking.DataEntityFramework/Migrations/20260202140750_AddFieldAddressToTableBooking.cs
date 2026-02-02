using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceBooking.DataEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldAddressToTableBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bookings");
        }
    }
}

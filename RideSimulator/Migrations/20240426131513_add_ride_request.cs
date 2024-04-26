using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideSimulator.Migrations
{
    public partial class add_ride_request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideRequest_Drivers_RequestedDriverId",
                table: "RideRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RideRequest_Riders_RiderId",
                table: "RideRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RideRequest",
                table: "RideRequest");

            migrationBuilder.RenameTable(
                name: "RideRequest",
                newName: "RideRequests");

            migrationBuilder.RenameIndex(
                name: "IX_RideRequest_RiderId",
                table: "RideRequests",
                newName: "IX_RideRequests_RiderId");

            migrationBuilder.RenameIndex(
                name: "IX_RideRequest_RequestedDriverId",
                table: "RideRequests",
                newName: "IX_RideRequests_RequestedDriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RideRequests",
                table: "RideRequests",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RideRequests_Drivers_RequestedDriverId",
                table: "RideRequests",
                column: "RequestedDriverId",
                principalTable: "Drivers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RideRequests_Riders_RiderId",
                table: "RideRequests",
                column: "RiderId",
                principalTable: "Riders",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideRequests_Drivers_RequestedDriverId",
                table: "RideRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RideRequests_Riders_RiderId",
                table: "RideRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RideRequests",
                table: "RideRequests");

            migrationBuilder.RenameTable(
                name: "RideRequests",
                newName: "RideRequest");

            migrationBuilder.RenameIndex(
                name: "IX_RideRequests_RiderId",
                table: "RideRequest",
                newName: "IX_RideRequest_RiderId");

            migrationBuilder.RenameIndex(
                name: "IX_RideRequests_RequestedDriverId",
                table: "RideRequest",
                newName: "IX_RideRequest_RequestedDriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RideRequest",
                table: "RideRequest",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RideRequest_Drivers_RequestedDriverId",
                table: "RideRequest",
                column: "RequestedDriverId",
                principalTable: "Drivers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RideRequest_Riders_RiderId",
                table: "RideRequest",
                column: "RiderId",
                principalTable: "Riders",
                principalColumn: "ID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Identity.Migrations
{
    public partial class AddModelUserRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "12c9ee64-da76-415f-9692-c30b9a707187");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                column: "ConcurrencyStamp",
                value: "6a1bf1ef-d8d6-49c6-aa64-65aca5e6145e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f30f7584-3577-4548-b93d-46c3630e3b01", "AQAAAAEAACcQAAAAEF83BPj1W5GjJS/QTt2Q9kb8Vxc748I+R+ptucOHKhIn8p+CEeBBlXKcHCqK7t+Prw==", "0700aa49-034f-4c12-b70b-c4efd901a724" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8798cf84-80ff-43ae-b7e0-4a3da18d0e58", "AQAAAAEAACcQAAAAEFBFC2+LOl9BvzzswOdeXFquMSXZsnNAYJUblmXqGxeV+OB28Qij+nT8drZVqd1z4Q==", "f569bb45-0505-448d-97a2-16b1c067b42a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "68bb5593-dfd4-4951-a3b0-5fe5788ebde4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                column: "ConcurrencyStamp",
                value: "d11f9641-113b-4d94-9505-35d91cc7f298");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2cedf01-cee0-430a-b820-0c377b586c0a", "AQAAAAEAACcQAAAAEN/+QPuA+uuzTXrdYR7YF0/+JPxiPmjUY/ukh+lBiXnLyMcVGwVaQDSrcginM2gbtA==", "b0bc8cba-dbe5-49c1-b77c-108c1dcccacc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49ff2dd4-d01a-42bb-afb8-83202db83269", "AQAAAAEAACcQAAAAEEnH3am4iXtp/eOTCv2+/TfcX6lIU9iYzudCAZkuGPuPyHjO8/6+fUwUX0kZfjZj3A==", "a039aaef-c811-4cc7-a4e3-42092c879d08" });
        }
    }
}

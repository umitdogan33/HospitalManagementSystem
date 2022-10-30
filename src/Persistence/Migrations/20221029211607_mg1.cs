using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationClaim",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshTokenValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaim",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperationClaimId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaim_OperationClaim_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaim_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaim",
                columns: new[] { "Id", "CreateDate", "Name", "UpdateDate" },
                values: new object[] { "0460d8c9-75a6-4d68-abc3-f249523f3e93", null, "admin", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status", "UpdateDate" },
                values: new object[] { "ce1bf1ec-4854-49b0-a7cc-6c8a902e0aa9", null, "admin@admin.com", "admin", "admin", new byte[] { 78, 166, 230, 143, 233, 78, 30, 98, 160, 139, 181, 233, 7, 234, 54, 7, 83, 23, 81, 189, 66, 178, 177, 125, 139, 14, 151, 251, 234, 30, 27, 21, 106, 98, 47, 42, 251, 34, 188, 105, 76, 194, 28, 43, 252, 211, 101, 181, 83, 50, 153, 170, 240, 215, 65, 226, 126, 220, 53, 8, 130, 242, 195, 106 }, new byte[] { 24, 122, 238, 43, 236, 20, 106, 232, 235, 238, 9, 92, 104, 126, 2, 100, 53, 126, 182, 222, 253, 5, 151, 61, 190, 84, 95, 19, 225, 16, 176, 35, 231, 186, 74, 237, 214, 180, 199, 198, 239, 173, 243, 187, 75, 35, 126, 31, 13, 163, 132, 138, 31, 82, 148, 3, 78, 253, 215, 79, 229, 31, 147, 205, 80, 21, 228, 230, 121, 54, 75, 222, 53, 195, 62, 96, 249, 192, 193, 202, 109, 35, 161, 65, 120, 233, 49, 143, 43, 173, 10, 159, 27, 3, 171, 112, 200, 226, 47, 0, 216, 93, 223, 230, 49, 38, 195, 234, 224, 223, 169, 154, 80, 183, 80, 152, 163, 70, 36, 224, 56, 248, 2, 89, 106, 190, 238, 38 }, true, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaim",
                columns: new[] { "Id", "CreateDate", "OperationClaimId", "UpdateDate", "UserId" },
                values: new object[] { "5565e2ed-b81d-42b6-a26c-0d08f383ce5f", null, "0460d8c9-75a6-4d68-abc3-f249523f3e93", null, "ce1bf1ec-4854-49b0-a7cc-6c8a902e0aa9" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaim_OperationClaimId",
                table: "UserOperationClaim",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaim_UserId",
                table: "UserOperationClaim",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UserOperationClaim");

            migrationBuilder.DropTable(
                name: "OperationClaim");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

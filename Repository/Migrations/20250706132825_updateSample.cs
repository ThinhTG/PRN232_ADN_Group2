using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateSample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleKits");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Samples",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Samples_PersonId",
                table: "Samples",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_TestPersons_PersonId",
                table: "Samples",
                column: "PersonId",
                principalTable: "TestPersons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samples_TestPersons_PersonId",
                table: "Samples");

            migrationBuilder.DropIndex(
                name: "IX_Samples_PersonId",
                table: "Samples");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Samples");

            migrationBuilder.CreateTable(
                name: "SampleKits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleKits_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SampleKits_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "FeedbackId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SampleKits_ApplicationUserId",
                table: "SampleKits",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SampleKits_FeedbackId",
                table: "SampleKits",
                column: "FeedbackId");
        }
    }
}

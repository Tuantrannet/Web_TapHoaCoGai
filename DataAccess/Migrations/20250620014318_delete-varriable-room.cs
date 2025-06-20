using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class deletevarriableroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_Roomid",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_Roomid",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Messages_Roomid",
                table: "Messages",
                column: "Roomid");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_Roomid",
                table: "Messages",
                column: "Roomid",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

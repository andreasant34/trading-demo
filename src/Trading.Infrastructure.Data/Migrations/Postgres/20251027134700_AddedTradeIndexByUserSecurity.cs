using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trading.Infrastructure.Data.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class AddedTradeIndexByUserSecurity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserId_SecurityId",
                table: "Trades",
                columns: new[] { "UserId", "SecurityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trades_UserId_SecurityId",
                table: "Trades");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WegaisApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class ProducerTestMigrationColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestMigrationColumnAdded",
                table: "Producers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMigrationColumnAdded",
                table: "Producers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscontinued = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsDiscontinued", "LastUpdated", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "High performance laptop", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptop", 1500.99m },
                    { 2, "Latest model smartphone", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartphone", 899.99m },
                    { 3, "Noise cancelling", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Headphones", 349.99m },
                    { 4, "Next-gen gaming console", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gaming Console", 499.99m },
                    { 5, "Ultra HD Smart TV", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4K TV", 1200.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}

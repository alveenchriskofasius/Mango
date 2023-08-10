using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "ID", "Code", "DiscountAmount", "MinAmount" },
                values: new object[,]
                {
                    { 1, "10FF", 10.0, 20 },
                    { 2, "20FF", 20.0, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}

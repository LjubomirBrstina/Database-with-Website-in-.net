using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StartingPrice = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BidDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bidder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TimeSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidDbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidDbs_AuctionDbs_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "AuctionDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AuctionDbs",
                columns: new[] { "Id", "AuctionName", "Description", "ExpireDate", "Seller", "StartingPrice" },
                values: new object[] { -1, "Bok", "Lär dig läsa", new DateTime(2022, 11, 10, 1, 56, 17, 489, DateTimeKind.Local).AddTicks(8793), "ljubomir@kth.se", 100 });

            migrationBuilder.InsertData(
                table: "BidDbs",
                columns: new[] { "Id", "Amount", "AuctionId", "Bidder", "TimeSubmitted" },
                values: new object[] { -2, 125, -1, "Ljubo@kth.se", new DateTime(2022, 11, 9, 20, 56, 17, 489, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.InsertData(
                table: "BidDbs",
                columns: new[] { "Id", "Amount", "AuctionId", "Bidder", "TimeSubmitted" },
                values: new object[] { -1, 120, -1, "Joel@kth.se", new DateTime(2022, 11, 9, 20, 56, 17, 489, DateTimeKind.Local).AddTicks(9014) });

            migrationBuilder.CreateIndex(
                name: "IX_BidDbs_AuctionId",
                table: "BidDbs",
                column: "AuctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidDbs");

            migrationBuilder.DropTable(
                name: "AuctionDbs");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CitiesApi.Migrations
{
    /// <inheritdoc />
    public partial class added_city_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "PointOfInterests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The one with that big park.", "New York City" },
                    { 2, "The one with the cathedral that was never really finished.", "Antwerp" },
                    { 3, "The one with that big tower.", "Paris" }
                });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "PointOfInterestId", "CityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "A large city park in the heart of Manhattan.", "Central Park" },
                    { 2, 1, "A 102-story skyscraper with amazing views.", "Empire State Building" },
                    { 3, 2, "A stunning Gothic cathedral.", "Cathedral of Our Lady" },
                    { 4, 2, "One of the oldest zoos in the world.", "Antwerp Zoo" },
                    { 5, 3, "The iconic iron tower.", "Eiffel Tower" },
                    { 6, 3, "The world’s largest art museum.", "Louvre Museum" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests");

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "PointOfInterestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "PointOfInterestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "PointOfInterestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "PointOfInterestId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "PointOfInterestId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "PointOfInterestId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "PointOfInterests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PointOfInterests_Cities_CityId",
                table: "PointOfInterests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId");
        }
    }
}

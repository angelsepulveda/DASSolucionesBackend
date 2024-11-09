using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DASSolucionesBackend.General.Data.Migrations
{
    /// <inheritdoc />
    public partial class LocalityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localities",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_localities_countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_localities_regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "General",
                        principalTable: "regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_localities_CountryId",
                schema: "General",
                table: "localities",
                column: "CountryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_localities_RegionId",
                schema: "General",
                table: "localities",
                column: "RegionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localities",
                schema: "General");
        }
    }
}

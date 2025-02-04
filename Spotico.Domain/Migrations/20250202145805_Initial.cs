using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spotico.Core.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(320)", nullable: false),
                    Bio = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CoverPath = table.Column<string>(type: "text", nullable: true),
                    TrackIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(600)", nullable: true),
                    TrackIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TrackPath = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tracks_Users_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("6aaf1267-83d8-4823-a9d0-677e917067b9"), "This is the admin user.", "admin@gmail.com", "admin", "Admin", "admin" },
                    { new Guid("7697442a-eb32-4f87-a447-ac535bdb97f8"), "This is the default user.", "user@gmail.com", "user", "User", "user" },
                    { new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"), "This is the artist.", "artist@gmail.com", "artist", "Author", "artist" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "CoverPath", "CreatedById", "Title", "TrackIds" },
                values: new object[] { new Guid("7c97edb9-32ee-41fd-abfe-99e83f29ca78"), "C:\\Programming\\ASP.NET+Angular\\Spotico\\Spotico\\Spotico.Server\\wwwroot/images/default.png", new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"), "Through The Disaster", new List<Guid> { new Guid("fa33f69f-190c-4f00-9009-99f1fe4a07a2"), new Guid("d535ae45-e567-4cf5-96a3-a2f6ca95ed62") } });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "CreatedById", "Description", "IsPublic", "Title", "TrackIds" },
                values: new object[] { new Guid("b9bbf213-5030-4de0-b65d-045fbc1df85d"), new Guid("6aaf1267-83d8-4823-a9d0-677e917067b9"), "The best of Nirvana", false, "Kurtka Cobain", new List<Guid> { new Guid("fa33f69f-190c-4f00-9009-99f1fe4a07a2"), new Guid("d535ae45-e567-4cf5-96a3-a2f6ca95ed62") } });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AlbumId", "ArtistId", "Duration", "Title", "TrackPath", "Views" },
                values: new object[,]
                {
                    { new Guid("d535ae45-e567-4cf5-96a3-a2f6ca95ed62"), new Guid("7c97edb9-32ee-41fd-abfe-99e83f29ca78"), new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"), 141.5, "welcome and goodbye", "C:\\Programming\\ASP.NET+Angular\\Spotico\\Spotico\\Spotico.Server\\wwwroot/tracks/2.mp3", 1425272 },
                    { new Guid("fa33f69f-190c-4f00-9009-99f1fe4a07a2"), new Guid("7c97edb9-32ee-41fd-abfe-99e83f29ca78"), new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"), 196.30000000000001, "Through The Disaster", "C:\\Programming\\ASP.NET+Angular\\Spotico\\Spotico\\Spotico.Server\\wwwroot/tracks/1.mp3", 51445 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_CreatedById",
                table: "Albums",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_CreatedById",
                table: "Playlists",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

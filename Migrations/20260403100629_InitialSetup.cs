using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolDBCoreWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "School");

            migrationBuilder.CreateTable(
                name: "grades",
                schema: "School",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "School",
                columns: table => new
                {
                    UsertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User", x => x.UsertId);
                });

            migrationBuilder.CreateTable(
                name: "students",
                schema: "School",
                columns: table => new
                {
                    Studentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RollNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student", x => x.Studentid);
                    table.ForeignKey(
                        name: "FK__grades__students",
                        column: x => x.GradeId,
                        principalSchema: "School",
                        principalTable: "grades",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "School",
                table: "users",
                columns: new[] { "UsertId", "Email", "FullName", "PassWordHash", "Role" },
                values: new object[,]
                {
                    { 1, "pranaya.rout@teksystems.com", "Pranaya Rout", "ayRIdQoJLhXyufbY11KD1fXqmpf4aHDEXLWsdYmLZek=", "Adminstrator,Manager" },
                    { 2, "john.doe@teksystems.com", "John Doe", "ZlKyJQ6uc/cQ0FZNsH/AqjoLvXeATZrliDoFdEMm6pk=", "Administrator" },
                    { 3, "jane.smith@teksystems.com", "Jane Smith", "sKmBLz4SePwSKvTys+H4Zs9IpVJ7uaVmTJfqWPV5OzE=", "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_GradeId",
                schema: "School",
                table: "students",
                column: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students",
                schema: "School");

            migrationBuilder.DropTable(
                name: "users",
                schema: "School");

            migrationBuilder.DropTable(
                name: "grades",
                schema: "School");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beltek.Project.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Classid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Quota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Classid);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Studentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Classid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Studentid);
                    table.ForeignKey(
                        name: "FK_Students_Classes_Classid",
                        column: x => x.Classid,
                        principalTable: "Classes",
                        principalColumn: "Classid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Classid",
                table: "Students",
                column: "Classid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}

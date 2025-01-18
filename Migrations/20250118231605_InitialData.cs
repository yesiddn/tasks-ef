using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tasks_ef.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Difficulty", "Name" },
                values: new object[,]
                {
                    { new Guid("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"), "Tareas personales", 0, "Personal" },
                    { new Guid("f5950436-1c72-4d10-a211-9752045e2947"), "Tareas de trabajo", 1, "Work" }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "CategoryId", "Description", "Priority", "Title" },
                values: new object[,]
                {
                    { new Guid("3161ea46-b39e-434f-8893-bb8a1f8932dc"), new Guid("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"), "Ir al supermercado y comprar leche", "Low", "Comprar leche" },
                    { new Guid("98326563-8e88-4402-b12e-4bd824967291"), new Guid("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"), null, "Low", "Sacar la basura" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("f5950436-1c72-4d10-a211-9752045e2947"));

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: new Guid("3161ea46-b39e-434f-8893-bb8a1f8932dc"));

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: new Guid("98326563-8e88-4402-b12e-4bd824967291"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("13d3c9ba-5d63-4397-84b1-58b2fcdd6202"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}

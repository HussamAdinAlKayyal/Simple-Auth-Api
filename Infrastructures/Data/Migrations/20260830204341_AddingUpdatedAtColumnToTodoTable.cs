using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicAuthApi.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingUpdatedAtColumnToTodoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Todos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Todos");
        }
    }
}

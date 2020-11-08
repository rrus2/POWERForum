﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace POWERForum.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (1, 'Admin', 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (2, 'User', 'USER')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Name == 'Admin'");
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Name == 'User'");
        }
    }
}

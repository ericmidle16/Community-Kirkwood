﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPresentation.Data.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        LogicLayer.UserManager userManager = new LogicLayer.UserManager();

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { "152558a1-7dfd-4f8a-ac27-a0694e6ff19e", "Admin", "ADMIN", DateTime.Now.ToString() },
                    { Guid.NewGuid().ToString(), "User", "USER", DateTime.Now.ToString() },
                    { Guid.NewGuid().ToString(), "No Access", "NO ACCESS", DateTime.Now.ToString() }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
                    "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "AccessFailedCount",
                    "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled"
                    },
                values: new object[,]
                {
                    { "4f4af597-4b7a-463b-8e43-bd18c8eee952", "admin@company.com", "ADMIN@COMPANY.COM",
                        "admin@company.com", "ADMIN@COMPANY.COM", true,
                        new PasswordHasher<IdentityUser>().HashPassword(null, "P@ssw0rd"),
                        "BIXSQ7WWCLLC6KADKKKLLOYLY672DD7T", DateTime.Now.ToString(), 0, false, false, true
                    }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "4f4af597-4b7a-463b-8e43-bd18c8eee952", "152558a1-7dfd-4f8a-ac27-a0694e6ff19e" }
                });

            userManager.InsertUser("Admin", "User", "1234567890", "admin@company.com", "P@ssw0rd", null, null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
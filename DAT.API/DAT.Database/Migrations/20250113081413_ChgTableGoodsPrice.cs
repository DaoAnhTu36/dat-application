﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAT.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChgTableGoodsPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TransactionDetailId",
                schema: "WH",
                table: "GoodsPrice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDetailId",
                schema: "WH",
                table: "GoodsPrice");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Ticket_TicketId1",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario");

            migrationBuilder.DropIndex(
                name: "IX_Comentario_TicketId1",
                table: "Comentario");

            migrationBuilder.RenameColumn(
                name: "TicketId1",
                table: "Comentario",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "Comentario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comentario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_TicketId",
                table: "Comentario",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Ticket_TicketId",
                table: "Comentario",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Ticket_TicketId",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario");

            migrationBuilder.DropIndex(
                name: "IX_Comentario_TicketId",
                table: "Comentario");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comentario",
                newName: "TicketId1");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "Comentario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TicketId1",
                table: "Comentario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_TicketId1",
                table: "Comentario",
                column: "TicketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Ticket_TicketId1",
                table: "Comentario",
                column: "TicketId1",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

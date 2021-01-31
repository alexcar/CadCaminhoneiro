using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSL.CadCaminhoneiro.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcaCaminhao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaCaminhao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorista",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cpf = table.Column<string>(type: "char(11)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    NomePai = table.Column<string>(type: "varchar(100)", nullable: false),
                    NomeMae = table.Column<string>(type: "varchar(100)", nullable: false),
                    Naturalidade = table.Column<string>(type: "varchar(19)", nullable: false),
                    NumeroRegistroGeral = table.Column<string>(type: "varchar(20)", nullable: false),
                    OrgaoExpedicaoRegistroGeral = table.Column<string>(type: "varchar(20)", nullable: false),
                    DataExpedicaoRegistroGeral = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeloCaminhao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false),
                    Ano = table.Column<string>(type: "varchar(4)", nullable: false),
                    MarcaCaminhaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloCaminhao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloCaminhao_MarcaCaminhao_MarcaCaminhaoId",
                        column: x => x.MarcaCaminhaoId,
                        principalTable: "MarcaCaminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(30)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: false),
                    Municipio = table.Column<string>(type: "varchar(50)", nullable: false),
                    Uf = table.Column<string>(type: "char(2)", nullable: false),
                    Cep = table.Column<string>(type: "char(8)", nullable: false),
                    MotoristaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Motorista_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Habilitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroRegistro = table.Column<string>(type: "varchar(20)", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(7)", nullable: false),
                    DataPrimeiraHabilitacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(50)", nullable: true),
                    MotoristaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habilitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habilitacao_Motorista_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Caminhao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "varchar(8)", nullable: false),
                    Eixo = table.Column<byte>(type: "tinyint", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(30)", nullable: true),
                    MotoristaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarcaCaminhaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModeloCaminhaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caminhao_MarcaCaminhao_MarcaCaminhaoId",
                        column: x => x.MarcaCaminhaoId,
                        principalTable: "MarcaCaminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Caminhao_ModeloCaminhao_ModeloCaminhaoId",
                        column: x => x.ModeloCaminhaoId,
                        principalTable: "ModeloCaminhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Caminhao_Motorista_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_MarcaCaminhaoId",
                table: "Caminhao",
                column: "MarcaCaminhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_ModeloCaminhaoId",
                table: "Caminhao",
                column: "ModeloCaminhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Caminhao_MotoristaId",
                table: "Caminhao",
                column: "MotoristaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_MotoristaId",
                table: "Endereco",
                column: "MotoristaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Habilitacao_MotoristaId",
                table: "Habilitacao",
                column: "MotoristaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloCaminhao_MarcaCaminhaoId",
                table: "ModeloCaminhao",
                column: "MarcaCaminhaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhao");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Habilitacao");

            migrationBuilder.DropTable(
                name: "ModeloCaminhao");

            migrationBuilder.DropTable(
                name: "Motorista");

            migrationBuilder.DropTable(
                name: "MarcaCaminhao");
        }
    }
}

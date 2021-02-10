using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JSL.CadCaminhoneiro.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Uf = table.Column<string>(type: "char(2)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Uf);
                });

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
                    MarcaCaminhaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Estado",
                columns: new[] { "Uf", "Descricao" },
                values: new object[,]
                {
                    { "AC", "Acre" },
                    { "TO", "Tocantins" },
                    { "SP", "São Paulo" },
                    { "SE", "Sergipe" },
                    { "SC", "Santa Catarina" },
                    { "RS", "Rio Grande do Sul" },
                    { "RR", "Roraima" },
                    { "RO", "Rondônia" },
                    { "RN", "Rio Grande do Norte" },
                    { "RJ", "Rio de Janeiro" },
                    { "PR", "Paraná" },
                    { "PI", "Piauí" },
                    { "PE", "Pernambuco" },
                    { "PB", "Paraíba" },
                    { "MT", "Mato Grosso" },
                    { "MS", "Mato Grosso do Sul" },
                    { "MG", "Minas Gerais" },
                    { "MA", "Maranhão" },
                    { "GO", "Goiás" },
                    { "ES", "Espírito Santo" },
                    { "DF", "Distrito Federal" },
                    { "CE", "Ceará" },
                    { "BA", "Bahia" },
                    { "AP", "Amapá" },
                    { "AM", "Amazonas" },
                    { "AL", "Alagoas" },
                    { "PA", "Pará" }
                });

            migrationBuilder.InsertData(
                table: "MarcaCaminhao",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Descricao" },
                values: new object[] { new Guid("468d7d0d-93cb-4cbc-8b6a-430a93a4a639"), null, new DateTime(2021, 2, 9, 21, 29, 7, 306, DateTimeKind.Local).AddTicks(8823), "Ford" });

            migrationBuilder.InsertData(
                table: "Motorista",
                columns: new[] { "Id", "Cpf", "DataAlteracao", "DataCriacao", "DataExpedicaoRegistroGeral", "DataNascimento", "Naturalidade", "Nome", "NomeMae", "NomePai", "NumeroRegistroGeral", "OrgaoExpedicaoRegistroGeral" },
                values: new object[] { new Guid("a7772991-4e68-4403-b323-2be7709cef6b"), "22627804804", null, new DateTime(2021, 2, 9, 21, 29, 7, 310, DateTimeKind.Local).AddTicks(1512), new DateTime(2006, 2, 9, 21, 29, 7, 309, DateTimeKind.Local).AddTicks(7085), new DateTime(1991, 2, 9, 21, 29, 7, 309, DateTimeKind.Local).AddTicks(7005), "São Paulo", "João da Silva Junior", "Maria da Silva", "João da Silva", "083943218", "SSP-SP" });

            migrationBuilder.InsertData(
                table: "Endereco",
                columns: new[] { "Id", "Bairro", "Cep", "Complemento", "DataAlteracao", "DataCriacao", "Logradouro", "MotoristaId", "Municipio", "Numero", "Uf" },
                values: new object[] { new Guid("ed70e7f9-bb3a-4af5-b17b-5894399b5c22"), "São Judas", "07500000", "centro", null, new DateTime(2021, 2, 9, 21, 29, 7, 310, DateTimeKind.Local).AddTicks(5972), "Rua Central", new Guid("a7772991-4e68-4403-b323-2be7709cef6b"), "São Paulo", "100", "SP" });

            migrationBuilder.InsertData(
                table: "Habilitacao",
                columns: new[] { "Id", "Categoria", "DataAlteracao", "DataCriacao", "DataEmissao", "DataPrimeiraHabilitacao", "DataValidade", "MotoristaId", "NumeroRegistro", "Observacao" },
                values: new object[] { new Guid("8bc2cef5-86ef-45d3-bc17-baf49e5339df"), "E", null, new DateTime(2021, 2, 9, 21, 29, 7, 311, DateTimeKind.Local).AddTicks(194), new DateTime(2011, 2, 9, 21, 29, 7, 310, DateTimeKind.Local).AddTicks(6474), new DateTime(2011, 2, 9, 21, 29, 7, 310, DateTimeKind.Local).AddTicks(6465), new DateTime(2031, 2, 9, 21, 29, 7, 310, DateTimeKind.Local).AddTicks(6472), new Guid("a7772991-4e68-4403-b323-2be7709cef6b"), "7834738", "sem restrições" });

            migrationBuilder.InsertData(
                table: "ModeloCaminhao",
                columns: new[] { "Id", "Ano", "DataAlteracao", "DataCriacao", "Descricao", "MarcaCaminhaoId" },
                values: new object[,]
                {
                    { new Guid("0a18d8e0-3d35-4e36-ba4d-38ecc9b91d66"), "1990", null, new DateTime(2021, 2, 9, 21, 29, 7, 309, DateTimeKind.Local).AddTicks(4606), "Cargo 1113", new Guid("468d7d0d-93cb-4cbc-8b6a-430a93a4a639") },
                    { new Guid("e3067d73-1f00-4078-9433-d839383c8599"), "2000", null, new DateTime(2021, 2, 9, 21, 29, 7, 309, DateTimeKind.Local).AddTicks(5158), "Cargo 1114", new Guid("468d7d0d-93cb-4cbc-8b6a-430a93a4a639") },
                    { new Guid("328a0492-3076-40fa-95bc-e2d61abb5aab"), "2001", null, new DateTime(2021, 2, 9, 21, 29, 7, 309, DateTimeKind.Local).AddTicks(5171), "Cargo 3031", new Guid("468d7d0d-93cb-4cbc-8b6a-430a93a4a639") }
                });

            migrationBuilder.InsertData(
                table: "Caminhao",
                columns: new[] { "Id", "DataAlteracao", "DataCriacao", "Eixo", "MarcaCaminhaoId", "ModeloCaminhaoId", "MotoristaId", "Observacao", "Placa" },
                values: new object[] { new Guid("1f510b91-b8b5-43c7-9e68-abb51560ddba"), null, new DateTime(2021, 2, 9, 21, 29, 7, 311, DateTimeKind.Local).AddTicks(3998), (byte)5, new Guid("468d7d0d-93cb-4cbc-8b6a-430a93a4a639"), new Guid("0a18d8e0-3d35-4e36-ba4d-38ecc9b91d66"), new Guid("a7772991-4e68-4403-b323-2be7709cef6b"), "cor azul", "MKT9090" });

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
                name: "Estado");

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

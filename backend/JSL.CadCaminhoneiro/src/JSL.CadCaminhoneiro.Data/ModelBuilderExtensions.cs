using JSL.CadCaminhoneiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace JSL.CadCaminhoneiro.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var marcaCaminhao = new MarcaCaminhao("Ford");
            marcaCaminhao.Incluir(DateTime.Now);
            modelBuilder.Entity<MarcaCaminhao>().HasData(marcaCaminhao);

            var modeloCaminhao1 = new ModeloCaminhao("Cargo 1113", "1990", marcaCaminhao.Id); modeloCaminhao1.Incluir(DateTime.Now);
            var modeloCaminhao2 = new ModeloCaminhao("Cargo 1114", "2000", marcaCaminhao.Id); modeloCaminhao2.Incluir(DateTime.Now);
            var modeloCaminhao3 = new ModeloCaminhao("Cargo 3031", "2001", marcaCaminhao.Id); modeloCaminhao3.Incluir(DateTime.Now);

            modelBuilder.Entity<ModeloCaminhao>().HasData(
                modeloCaminhao1,
                modeloCaminhao2,
                modeloCaminhao3
                );

            modelBuilder.Entity<Estado>().HasData(
                new Estado() { Uf = "AC", Descricao = "Acre" },
                new Estado() { Uf = "AL", Descricao = "Alagoas" },
                new Estado() { Uf = "AM", Descricao = "Amazonas" },
                new Estado() { Uf = "AP", Descricao = "Amapá" },
                new Estado() { Uf = "BA", Descricao = "Bahia" },
                new Estado() { Uf = "CE", Descricao = "Ceará" },
                new Estado() { Uf = "DF", Descricao = "Distrito Federal" },
                new Estado() { Uf = "ES", Descricao = "Espírito Santo" },
                new Estado() { Uf = "GO", Descricao = "Goiás" },
                new Estado() { Uf = "MA", Descricao = "Maranhão" },
                new Estado() { Uf = "MG", Descricao = "Minas Gerais" },
                new Estado() { Uf = "MS", Descricao = "Mato Grosso do Sul" },
                new Estado() { Uf = "MT", Descricao = "Mato Grosso" },
                new Estado() { Uf = "PA", Descricao = "Pará" },
                new Estado() { Uf = "PB", Descricao = "Paraíba" },
                new Estado() { Uf = "PE", Descricao = "Pernambuco" },
                new Estado() { Uf = "PI", Descricao = "Piauí" },
                new Estado() { Uf = "PR", Descricao = "Paraná" },
                new Estado() { Uf = "RJ", Descricao = "Rio de Janeiro" },
                new Estado() { Uf = "RN", Descricao = "Rio Grande do Norte" },
                new Estado() { Uf = "RO", Descricao = "Rondônia" },
                new Estado() { Uf = "RR", Descricao = "Roraima" },
                new Estado() { Uf = "RS", Descricao = "Rio Grande do Sul" },
                new Estado() { Uf = "SC", Descricao = "Santa Catarina" },
                new Estado() { Uf = "SE", Descricao = "Sergipe" },
                new Estado() { Uf = "SP", Descricao = "São Paulo" },
                new Estado() { Uf = "TO", Descricao = "Tocantins" }
                );            

            var motorista = new Motorista(
                    "João da Silva Junior",
                    "22627804804",
                    DateTime.Now.AddYears(-30),
                    "João da Silva",
                    "Maria da Silva",
                    "São Paulo",
                    "083943218",
                    "SSP-SP",
                    DateTime.Now.AddYears(-15));
            motorista.Incluir(DateTime.Now);
            

            var endereco = new Endereco(
                "Rua Central", "100", "centro",
                "São Judas", "São Paulo", "SP", "07500000", motorista.Id);
            endereco.Incluir(DateTime.Now);
            // endereco.IncluirMotorista(motorista);
            // motorista.IncluirEndereco(endereco);
            


            var habilitacao = new Habilitacao(
                "7834738", "E",
                DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10),
                DateTime.Now.AddYears(-10), "sem restrições", motorista.Id);
            habilitacao.Incluir(DateTime.Now);
            // habilitacao.IncluirMotorista(motorista);
            // motorista.IncluirHabilitacao(habilitacao);


            var caminhao = new Caminhao(
                "MKT9090", 5, "cor azul",
                marcaCaminhao.Id, modeloCaminhao1.Id, motorista.Id);
            caminhao.Incluir(DateTime.Now);
            // caminhao.IncluirMotorista(motorista);
            // motorista.IncluirCaminhao(caminhao);

            modelBuilder.Entity<Motorista>().HasData(motorista);
            modelBuilder.Entity<Endereco>().HasData(endereco);
            modelBuilder.Entity<Habilitacao>().HasData(habilitacao);
            modelBuilder.Entity<Caminhao>().HasData(caminhao);

            //modelBuilder.Entity<Motorista>().HasData(motorista, caminhao, habilitacao, endereco);
            //modelBuilder.Entity<Caminhao>().HasData(caminhao);
            //modelBuilder.Entity<Habilitacao>().HasData(habilitacao);
            //modelBuilder.Entity<Endereco>().HasData(endereco);
        }
    }
}

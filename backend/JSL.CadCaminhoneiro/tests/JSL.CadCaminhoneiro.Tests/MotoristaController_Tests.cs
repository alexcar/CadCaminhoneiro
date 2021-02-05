using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Xunit;
using JSL.CadCaminhoneiro.Data;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Api.Controllers.v1;
using JSL.CadCaminhoneiro.Api.Infrastructure.Installers.Pagination;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Api.Services;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using JSL.CadCaminhoneiro.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using JSL.CadCaminhoneiro.Api.Dto;
using System.Linq;
using JSL.CadCaminhoneiro.Tests.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JSL.CadCaminhoneiro.Tests
{
    public class MotoristaController_Tests
    {
        private readonly IUriService uriService;
        private readonly NotificationContext notificationContext;
        private readonly IMotoristaRepository repository;
        private readonly IMotoristaService service;
        private readonly IMarcaCaminhaoRepository marcaRepository;
        private readonly IMarcaCaminhaoService marcaService;
        private readonly IModeloCaminhaoRepository modeloRepository;
        private readonly IModeloCaminhaoService modeloService;
        // private MotoristaTeste motoristaTeste;

        public MotoristaController_Tests()
        {
            uriService = new UriService("https://localhost:5050");
            notificationContext = new NotificationContext();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<CadCaminhoneiroContext>()
                .UseSqlServer(configuration["ConnectionStrings:SQLDBConnectionString"])
                .Options;

            var cadCaminhoneiroContext = new CadCaminhoneiroContext(options);

            marcaRepository = new MarcaCaminhaoRepository(cadCaminhoneiroContext);
            marcaService = new MarcaCaminhaoService(notificationContext, marcaRepository);

            modeloRepository = new ModeloCaminhaoRepository(cadCaminhoneiroContext);
            modeloService = new ModeloCaminhaoService(notificationContext, modeloRepository, marcaRepository);

            repository = new MotoristaRepository(cadCaminhoneiroContext);
            service = new MotoristaService(repository, marcaRepository, modeloRepository, notificationContext);

            //motoristaTeste = new MotoristaTeste
            //{
            //    Id = Guid.Empty,
            //    Nome = GerarStringaleatoria(30),
            //    Cpf = "71914249828",
            //    DataNascimento = DateTime.Now.AddYears(-30),
            //    NomePai = GerarStringaleatoria(30),
            //    NomeMae = GerarStringaleatoria(30),
            //    Naturalidade = GerarStringaleatoria(10),
            //    NumeroRegistroGeral = GerarStringaleatoria(10),
            //    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
            //    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
            //    EnderecoId = Guid.Empty,
            //    Logradouro = GerarStringaleatoria(30),
            //    Numero = "100",
            //    Complemento = GerarStringaleatoria(10),
            //    Bairro = GerarStringaleatoria(10),
            //    Municipio = GerarStringaleatoria(10),
            //    Uf = GerarStringaleatoria(2),
            //    Cep = GerarStringaleatoria(8),
            //    HabilitacaoId = Guid.Empty,
            //    NumeroRegistroHabilitacao = GerarStringaleatoria(5),
            //    CategoriaHabilitacao = "E",
            //    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
            //    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
            //    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
            //    ObservacaoHabilitacao = GerarStringaleatoria(10),
            //    CaminhaoId = Guid.Empty,
            //    Placa = GerarStringaleatoria(7),
            //    Eixo = 3,
            //    CaminhaoObservacao = GerarStringaleatoria(10),
            //    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
            //    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
            //};
        }

        /// <summary>
        /// Obter um motorista aleatório
        /// </summary>
        [Fact]
        public async void Obter_um_motorista()
        {
            #region Arrange

            MotoristaListDto motoristaExiste = null;
            MotoristaListDto motoristaNaoExiste = null;

            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var motorista = ObterMotoristaJaCadastrada().Result;

                motoristaExiste = await controller.ObterPorId(motorista.Id);
                motoristaNaoExiste = await controller.ObterPorId(Guid.NewGuid());
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(motoristaExiste != null && motoristaNaoExiste == null);

            #endregion
        }

        /// <summary>
        /// Obter uma lista de motiristas
        /// </summary>
        [Fact]
        public async void Obter_lista_de_motiristas()
        {
            #region Arrange

            PagedResponse<IEnumerable<MotoristaListDto>> motoristas = null;

            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                motoristas = await controller.ListarTodos(new SortFilterPageRequest());
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(motoristas != null);

            #endregion
        }

        /// <summary>
        /// Incluir um modelo de caminhão
        /// </summary>
        [Fact]
        public async void Incluir_um_motorista()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var novoCpf = GerarCpf();

                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    Cpf = novoCpf,
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    NumeroRegistroGeral = GerarStringaleatoria(10),
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    NumeroRegistroHabilitacao = GerarStringaleatoria(5),
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista != null && novoMotorista.StatusCode == 200);

            #endregion
        }

        /// <summary>
        /// Incluir um motorista sem informar o CPF
        /// </summary>
        [Fact]
        public async void Incuir_motorista_sem_cpf()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    // Cpf = "71914249828",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    NumeroRegistroGeral = GerarStringaleatoria(10),
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    NumeroRegistroHabilitacao = GerarStringaleatoria(5),
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista == null);

            #endregion
        }

        /// <summary>
        /// Incluir um motorista sem informar o RG
        /// </summary>
        [Fact]
        public async void Incuir_motorista_sem_RG()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    Cpf = GerarCpf(),
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    // NumeroRegistroGeral = GerarStringaleatoria(10),
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    NumeroRegistroHabilitacao = GerarStringaleatoria(5),
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista == null);

            #endregion
        }

        /// <summary>
        /// Incluir um motorista sem informar o número da habilitação
        /// </summary>
        [Fact]
        public async void Incuir_motorista_sem_CNH()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    Cpf = GerarCpf(),
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    NumeroRegistroGeral = GerarStringaleatoria(10),
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    // NumeroRegistroHabilitacao = GerarStringaleatoria(5),
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista == null);

            #endregion
        }

        /// <summary>
        /// Incluir um motorista com um CPF já cadastrado
        /// </summary>
        [Fact]
        public async void Incuir_motorista_com_cpf_ja_cadastrado()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    Cpf = ObterMotoristaJaCadastrada().Result.Cpf,
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    NumeroRegistroGeral = GerarStringaleatoria(10),
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    NumeroRegistroHabilitacao = GerarStringaleatoria(5),
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista == null);

            #endregion
        }

        /// <summary>
        /// Incluir um motorista com um RG já cadastrado
        /// </summary>
        [Fact]
        public async void Incuir_motorista_com_RG_ja_cadastrado()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    Cpf = "71914249828",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    NumeroRegistroGeral = ObterMotoristaJaCadastrada().Result.NumeroRegistroGeral,
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    NumeroRegistroHabilitacao = GerarStringaleatoria(5),
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista == null);

            #endregion
        }

        /// <summary>
        /// Incluir um motorista com o número da habilitação já cadastrado
        /// </summary>
        [Fact]
        public async void Incuir_motorista_com_CNH_ja_cadastrado()
        {
            #region Arrange

            OkObjectResult novoMotorista = null;
            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MotoristaIncluirRequest
                {
                    Nome = GerarStringaleatoria(30),
                    Cpf = "71914249828",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    NomePai = GerarStringaleatoria(30),
                    NomeMae = GerarStringaleatoria(30),
                    Naturalidade = GerarStringaleatoria(10),
                    NumeroRegistroGeral = GerarStringaleatoria(10),
                    OrgaoExpedicaoRegistroGeral = GerarStringaleatoria(3),
                    DataExpedicaoRegistroGeral = DateTime.Now.AddYears(-15),
                    Logradouro = GerarStringaleatoria(30),
                    Numero = "100",
                    Complemento = GerarStringaleatoria(10),
                    Bairro = GerarStringaleatoria(10),
                    Municipio = GerarStringaleatoria(10),
                    Uf = GerarStringaleatoria(2),
                    Cep = GerarStringaleatoria(8),
                    NumeroRegistroHabilitacao = ObterMotoristaJaCadastrada().Result.HabilitacaoDto.NumeroRegistro,
                    CategoriaHabilitacao = "E",
                    DataPrimeiraHabilitacao = DateTime.Now.AddYears(-10),
                    DataValidadeHabilitacao = DateTime.Now.AddYears(10),
                    DataEmissaoHabilitacao = DateTime.Now.AddYears(-10),
                    ObservacaoHabilitacao = GerarStringaleatoria(10),
                    Placa = GerarStringaleatoria(7),
                    Eixo = 3,
                    CaminhaoObservacao = GerarStringaleatoria(10),
                    MarcaCaminhaoId = ObterModeloJaCadastrada().Result.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = ObterModeloJaCadastrada().Result.Id
                };

                novoMotorista = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoMotorista == null);

            #endregion
        }

        /// <summary>
        /// Alterar um motorista
        /// </summary>
        [Fact]
        public async void Alterar_um_motorista()
        {
            #region Arrange

            OkObjectResult motoristaAlterado = null;
            MotoristaListDto motoristaJaCadastrado = null;
            MotoristaListDto motoristaAlteradoBancoDados = null;
            // MotoristaAlterarRequest novoMotorista = null;

            #endregion

            #region Act

            var controller = new MotoristaController(uriService, notificationContext, repository, service);

            try
            {
                // Obter um motorista já cadastrado
                motoristaJaCadastrado = ObterMotoristaJaCadastrada().Result;                

                var novoMotorista = new MotoristaAlterarRequest
                {
                    Id = motoristaJaCadastrado.Id,
                    Nome = motoristaJaCadastrado.Nome,
                    Cpf = motoristaJaCadastrado.Cpf,
                    DataNascimento = motoristaJaCadastrado.DataNascimento,
                    NomePai = GerarStringaleatoria(15),
                    NomeMae = motoristaJaCadastrado.NomeMae,
                    Naturalidade = motoristaJaCadastrado.Naturalidade,
                    NumeroRegistroGeral = motoristaJaCadastrado.NumeroRegistroGeral,
                    OrgaoExpedicaoRegistroGeral = motoristaJaCadastrado.OrgaoExpedicaoRegistroGeral,
                    DataExpedicaoRegistroGeral = motoristaJaCadastrado.DataExpedicaoRegistroGeral,
                    EnderecoId = motoristaJaCadastrado.EnderecoDto.Id,
                    Logradouro = motoristaJaCadastrado.EnderecoDto.Logradouro,
                    Numero = motoristaJaCadastrado.EnderecoDto.Numero,
                    Complemento = motoristaJaCadastrado.EnderecoDto.Complemento,
                    Bairro = motoristaJaCadastrado.EnderecoDto.Bairro,
                    Municipio = motoristaJaCadastrado.EnderecoDto.Municipio,
                    Uf = motoristaJaCadastrado.EnderecoDto.Uf,
                    Cep = motoristaJaCadastrado.EnderecoDto.Cep,
                    HabilitacaoId = motoristaJaCadastrado.HabilitacaoDto.Id,
                    NumeroRegistroHabilitacao = motoristaJaCadastrado.HabilitacaoDto.NumeroRegistro,
                    CategoriaHabilitacao = motoristaJaCadastrado.HabilitacaoDto.Categoria,
                    DataPrimeiraHabilitacao = motoristaJaCadastrado.HabilitacaoDto.DataPrimeiraHabilitacao,
                    DataValidadeHabilitacao = motoristaJaCadastrado.HabilitacaoDto.DataValidade,
                    DataEmissaoHabilitacao = motoristaJaCadastrado.HabilitacaoDto.DataEmissao,
                    ObservacaoHabilitacao = motoristaJaCadastrado.HabilitacaoDto.Observacao,
                    CaminhaoId = motoristaJaCadastrado.CaminhaoDto.Id,
                    Placa = motoristaJaCadastrado.CaminhaoDto.Placa,
                    Eixo = motoristaJaCadastrado.CaminhaoDto.Eixo,
                    CaminhaoObservacao = motoristaJaCadastrado.CaminhaoDto.Observacao,
                    MarcaCaminhaoId = motoristaJaCadastrado.CaminhaoDto.MarcaCaminhaoListDto.Id,
                    ModeloCaminhaoId = motoristaJaCadastrado.CaminhaoDto.ModeloCaminhaoListDto.Id
                };

                // Realiza a alteração
                motoristaAlterado = (OkObjectResult)await controller
                    .Alterar(motoristaJaCadastrado.Id, novoMotorista);

                // Recupera o novo modelo no banco de dados
                motoristaAlteradoBancoDados = await controller.ObterPorId(novoMotorista.Id);

            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(
                motoristaAlterado != null && motoristaAlterado.StatusCode == 200 &&
                motoristaJaCadastrado.NomePai != motoristaAlteradoBancoDados.NomePai);

            #endregion
        }


        /// <summary>
        /// Gera uma marca de forma aleatória
        /// </summary>
        /// <param name="tamanho"></param>
        /// <returns></returns>
        private static string GerarStringaleatoria(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        /// <summary>
        /// Obter um motorista já cadastrado
        /// </summary>
        /// <returns></returns>
        private async Task<MotoristaListDto> ObterMotoristaJaCadastrada()
        {
            var controller = new MotoristaController(uriService, notificationContext, repository, service);
            var motorista = await controller.ListarTodos(new SortFilterPageRequest());

            return motorista.Data.FirstOrDefault();
        }

        /// <summary>
        /// Obter uma marca já cadastrada
        /// </summary>
        /// <returns></returns>
        private async Task<MarcaCaminhaoListDto> ObterMarcaJaCadastrada()
        {
            var controller = new MarcaCaminhaoController(uriService, notificationContext, marcaRepository, marcaService);
            var marca = await controller.ListarTodos(new SortFilterPageRequest());

            return marca.Data.FirstOrDefault();
        }

        /// <summary>
        /// Obter um modelo já cadastrado
        /// </summary>
        /// <returns></returns>
        private async Task<ModeloCaminhaoListDto> ObterModeloJaCadastrada()
        {
            var controller = new ModeloCaminhaoController(uriService, notificationContext, modeloRepository, modeloService);
            var modelo = await controller.ListarTodos(new SortFilterPageRequest());

            return modelo.Data.FirstOrDefault();
        }

        private static string GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }
    }
}

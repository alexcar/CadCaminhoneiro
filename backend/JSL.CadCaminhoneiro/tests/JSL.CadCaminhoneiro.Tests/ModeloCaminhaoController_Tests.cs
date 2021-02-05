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
    public class ModeloCaminhaoController_Tests
    {
        private readonly IUriService uriService;
        private readonly NotificationContext notificationContext;
        private readonly IModeloCaminhaoRepository repository;
        private readonly IModeloCaminhaoService service;
        private readonly IMarcaCaminhaoRepository marcaRepository;
        private readonly IMarcaCaminhaoService marcaService;
        private ModeloCaminhaoTeste modeloCaminhaoTeste;

        public ModeloCaminhaoController_Tests()
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

            repository = new ModeloCaminhaoRepository(cadCaminhoneiroContext);
            service = new ModeloCaminhaoService(notificationContext, repository, marcaRepository);

            modeloCaminhaoTeste = new ModeloCaminhaoTeste
            {
                Id = Guid.Empty,
                Descricao = GerarNomeModelo(5),
                Ano = "1989"
            };
        }

        /// <summary>
        /// Obter um modelo de caminhão aleatório
        /// </summary>
        [Fact]
        public async void Obter_um_modelo()
        {
            #region Arrange

            ModeloCaminhaoListDto modeloCaminhaoExiste = null;
            ModeloCaminhaoListDto modeloCaminhaoNaoExiste = null;

            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                var modelo = await ObterModelosJaCadastrados();
                var modeloCaminhaoAleatoria = modelo.FirstOrDefault();

                modeloCaminhaoExiste = await controller.ObterPorId(Guid.Parse(modeloCaminhaoAleatoria.Id.ToString()));
                modeloCaminhaoNaoExiste = await controller.ObterPorId(Guid.NewGuid());
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(modeloCaminhaoExiste != null && modeloCaminhaoNaoExiste == null);

            #endregion
        }

        /// <summary>
        /// Obter uma lista de modelos de caminhão
        /// </summary>
        [Fact]
        public async void Obter_lista_de_modelos()
        {
            #region Arrange

            PagedResponse<IEnumerable<ModeloCaminhaoListDto>> modeloCaminhaoExiste = null;

            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                modeloCaminhaoExiste = await controller.ListarTodos(new SortFilterPageRequest());
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(modeloCaminhaoExiste != null);

            #endregion
        }

        /// <summary>
        /// Incluir um modelo de caminhão
        /// </summary>
        [Fact]
        public async void Incluir_um_modelo()
        {
            #region Arrange

            OkObjectResult novoModeloCaminhao = null;
            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new ModeloCaminhaoIncluirRequest
                {
                    Descricao = modeloCaminhaoTeste.Descricao,
                    Ano = "1989",
                    MarcaCaminhaoId = ObterMarcaJaCadastrada().Result.Id

                };

                novoModeloCaminhao = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoModeloCaminhao != null && novoModeloCaminhao.StatusCode == 200);

            #endregion
        }

        /// <summary>
        /// Incluir um modelo de camonhão sem informar a descrição
        /// </summary>
        [Fact]
        public async void Incuir_modelo_sem_descricao()
        {
            #region Arrange

            OkObjectResult novoModeloCaminhao = null;
            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new ModeloCaminhaoIncluirRequest
                {
                    Descricao = string.Empty,
                    Ano = "1989",
                    MarcaCaminhaoId = ObterMarcaJaCadastrada().Result.Id
                };

                novoModeloCaminhao = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novoModeloCaminhao != null);

            #endregion
        }

        /// <summary>
        /// Incluir um modelo de caminhão já existente
        /// </summary>
        [Fact]
        public async void Incluir_modelo_ja_existente()
        {
            #region Arrange

            IActionResult modeloCaminhao = null;

            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                // Obter um modelo já cadastrada
                var modelos = await ObterModelosJaCadastrados();
                var modeloCaminhaoJaCadastrado = modelos.FirstOrDefault();

                var incluirRequest = new ModeloCaminhaoIncluirRequest
                {
                    Descricao = modeloCaminhaoJaCadastrado.Descricao
                };

                modeloCaminhao = await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(modeloCaminhao is null);

            #endregion
        }

        /// <summary>
        /// Alterar um modelo de caminhão
        /// </summary>
        [Fact]
        public async void Alterar_um_modelo()
        {
            #region Arrange

            OkObjectResult modeloCaminhao = null;
            ModeloCaminhaoListDto modeloCaminhaoJaCadastrado = null;
            ModeloCaminhaoListDto modeloCaminhaoAlterado = null;
            ModeloCaminhaoAlterarRequest novoModeloCaminhao = null;

            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                // Obter um modelo já cadastrado
                var modelos = await ObterModelosJaCadastrados();
                modeloCaminhaoJaCadastrado = modelos.FirstOrDefault();

                novoModeloCaminhao = new ModeloCaminhaoAlterarRequest
                {
                    Id = modeloCaminhaoJaCadastrado.Id,
                    Ano = modeloCaminhaoJaCadastrado.Ano,
                    MarcaCaminhaoId = modeloCaminhaoJaCadastrado.MarcaCaminhaoListDto.Id,
                    Descricao = GerarNomeModelo(5)
                };

                // Realiza a alteração
                modeloCaminhao = (OkObjectResult)await controller
                    .Alterar(modeloCaminhaoJaCadastrado.Id, novoModeloCaminhao);

                // Recupera o novo modelo no banco de dados
                modeloCaminhaoAlterado = await controller.ObterPorId(novoModeloCaminhao.Id);

            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(
                modeloCaminhao != null && modeloCaminhao.StatusCode == 200 &&
                modeloCaminhaoJaCadastrado.Descricao != modeloCaminhaoAlterado.Descricao);

            #endregion
        }

        /// <summary>
        /// Alterar um modelo de caminhão cujo a nova marca já existe.
        /// Este teste só pode ser executado se existir mais de um modelo
        /// </summary>
        [Fact]
        public async void Alterar_modelo_para_uma_ja_existente()
        {
            #region Arrange

            OkObjectResult modeloCaminhao = null;
            ModeloCaminhaoListDto modeloCaminhaoJaCadastrado = null;
            ModeloCaminhaoListDto modeloCaminhaoAlterado = null;
            ModeloCaminhaoAlterarRequest novoModeloCaminhao = null;

            #endregion

            #region Act

            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                // obter alguns modelos
                var modelos = await ObterModelosJaCadastrados();

                // Obter um modelo já cadastrado para ser alterado
                modeloCaminhaoJaCadastrado = modelos.FirstOrDefault();

                // Obter um outro modelo
                var outroModeloJaCadastrado = modelos.LastOrDefault();

                novoModeloCaminhao = new ModeloCaminhaoAlterarRequest
                {
                    Id = modeloCaminhaoJaCadastrado.Id,
                    Ano = modeloCaminhaoJaCadastrado.Ano,
                    MarcaCaminhaoId = modeloCaminhaoJaCadastrado.MarcaCaminhaoListDto.Id,
                    Descricao = outroModeloJaCadastrado.Descricao
                };

                // Realiza a alteração
                modeloCaminhao = (OkObjectResult)await controller
                    .Alterar(modeloCaminhaoJaCadastrado.Id, novoModeloCaminhao);

                // Recupera o novo modelo no banco de dados
                modeloCaminhaoAlterado = await controller.ObterPorId(novoModeloCaminhao.Id);

            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(modeloCaminhao is null);

            #endregion
        }

        /// <summary>
        /// Gera um modelo de forma aleatória
        /// </summary>
        /// <param name="tamanho"></param>
        /// <returns></returns>
        private static string GerarNomeModelo(int tamanho)
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
        /// Obtem alguns modelos
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<ModeloCaminhaoListDto>> ObterModelosJaCadastrados()
        {
            var controller = new ModeloCaminhaoController(uriService, notificationContext, repository, service);
            var modelos = await controller.ListarTodos(new SortFilterPageRequest());

            return modelos.Data;
        }

        /// <summary>
        /// Obter uma marca já cadastrada
        /// </summary>
        /// <returns></returns>
        private async Task<MarcaCaminhaoListDto> ObterMarcaJaCadastrada()
        {
            var controller = new MarcaCaminhaoController(uriService, notificationContext, marcaRepository, marcaService);
            var marcas = await controller.ListarTodos(new SortFilterPageRequest());

            return marcas.Data.FirstOrDefault();
        }
    }
}

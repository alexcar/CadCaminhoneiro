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
    public class MarcaCaminhaoController_Tests
    {
        private readonly IUriService uriService;
        private readonly NotificationContext notificationContext;
        private readonly IMarcaCaminhaoRepository repository;
        private readonly IMarcaCaminhaoService service;
        private MarcaCaminhaoTeste marcaCaminhaoTeste;

        public MarcaCaminhaoController_Tests()
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
            repository = new MarcaCaminhaoRepository(cadCaminhoneiroContext);
            service = new MarcaCaminhaoService(notificationContext, repository);

            marcaCaminhaoTeste = new MarcaCaminhaoTeste
            {
                Id = Guid.Empty,
                Descricao = GerarNomeMarca(5)
            };
        }
        
        /// <summary>
        /// Obter uma maraca de caminhão aleatória
        /// </summary>
        [Fact]
        public async void Obter_uma_marca()
        {
            #region Arrange

            MarcaCaminhaoListDto marcaCaminhaoExiste = null;
            MarcaCaminhaoListDto marcaCaminhaoNaoExiste = null;            

            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                var marca = await ObterMarcasJaCadastradas();
                var marcaCaminhaoAleatoria = marca.FirstOrDefault();

                marcaCaminhaoExiste = await controller.ObterPorId(Guid.Parse(marcaCaminhaoAleatoria.Id.ToString()));
                marcaCaminhaoNaoExiste = await controller.ObterPorId(Guid.NewGuid());
            }
            catch (Exception)
            {
            }            

            #endregion

            #region Assert

            Assert.True(marcaCaminhaoExiste != null && marcaCaminhaoNaoExiste == null);

            #endregion
        }

        /// <summary>
        /// Obter uma lista de marcas de caminhão
        /// </summary>
        [Fact]
        public async void Obter_lista_de_marcas()
        {
            #region Arrange

            PagedResponse<IEnumerable<MarcaCaminhaoListDto>> marcaCaminhaoExiste = null;

            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);
            
            try
            {
                marcaCaminhaoExiste = await controller.ListarTodos(new SortFilterPageRequest());
            }
            catch (Exception)
            {                
            }

            #endregion

            #region Assert

            Assert.True(marcaCaminhaoExiste != null);

            #endregion
        }

        /// <summary>
        /// Incluir uma marca de caminhão
        /// </summary>
        [Fact]
        public async void Incluir_uma_marca()
        {
            #region Arrange

            OkObjectResult novaMarcaCaminhao = null;
            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MarcaCaminhaoIncluirRequest
                {
                    Descricao = marcaCaminhaoTeste.Descricao
                };

                novaMarcaCaminhao = (OkObjectResult) await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novaMarcaCaminhao != null && novaMarcaCaminhao.StatusCode == 200);

            #endregion
        }

        /// <summary>
        /// Incluir uma marca de camonhão sem informar a descrição
        /// </summary>
        [Fact]
        public async void Incuir_marca_sem_descricao()
        {
            #region Arrange

            OkObjectResult novaMarcaCaminhao = null;
            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                var incluirRequest = new MarcaCaminhaoIncluirRequest
                {
                    Descricao = string.Empty
                };

                novaMarcaCaminhao = (OkObjectResult)await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(novaMarcaCaminhao != null);

            #endregion
        }

        /// <summary>
        /// Incluir uma marca de caminhão já existente
        /// </summary>
        [Fact]
        public async void Incluir_marca_ja_existente()
        {
            #region Arrange

            IActionResult marcaCaminhao = null;

            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                // Obter uma marca já cadastrada
                var marcas = await ObterMarcasJaCadastradas();
                var marcaCaminhaoJaCadastrada = marcas.FirstOrDefault();
                
                var incluirRequest = new MarcaCaminhaoIncluirRequest
                {
                    Descricao = marcaCaminhaoJaCadastrada.Descricao
                };

                marcaCaminhao = await controller.Incluir(incluirRequest);
            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(marcaCaminhao is null);

            #endregion
        }

        /// <summary>
        /// Alterar uma marca de caminhão
        /// </summary>
        [Fact]
        public async void Alterar_uma_marca()
        {
            #region Arrange

            OkObjectResult marcaCaminhao = null;
            MarcaCaminhaoListDto marcaCaminhaoJaCadastrada = null;
            MarcaCaminhaoListDto marcaCaminhaoAlterada = null;
            MarcaCaminhaoAlterarRequest novaMarcaCaminhao = null;

            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);
            
            try
            {
                // Obter uma marca já cadastrada
                var marcas = await ObterMarcasJaCadastradas();
                marcaCaminhaoJaCadastrada = marcas.FirstOrDefault();

                novaMarcaCaminhao = new MarcaCaminhaoAlterarRequest
                {
                    Id = marcaCaminhaoJaCadastrada.Id,
                    Descricao = GerarNomeMarca(5)
                };

                // Realiza a alteração
                marcaCaminhao = (OkObjectResult) await controller
                    .Alterar(marcaCaminhaoJaCadastrada.Id, novaMarcaCaminhao);

                // Recupera a nova marca no banco de dados
                marcaCaminhaoAlterada = await controller.ObterPorId(novaMarcaCaminhao.Id);

            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(
                marcaCaminhao != null && marcaCaminhao.StatusCode == 200 &&
                marcaCaminhaoJaCadastrada.Descricao != marcaCaminhaoAlterada.Descricao);

            #endregion
        }

        /// <summary>
        /// Alterar uma marca de caminhão cujo a nova marca já existe.
        /// Este teste só pode ser executado se existir mais de uma marca
        /// </summary>
        [Fact]
        public async void Alterar_marca_para_uma_ja_existente()
        {
            #region Arrange

            OkObjectResult marcaCaminhao = null;
            MarcaCaminhaoListDto marcaCaminhaoJaCadastrada = null;
            MarcaCaminhaoListDto marcaCaminhaoAlterada = null;
            MarcaCaminhaoAlterarRequest novaMarcaCaminhao = null;

            #endregion

            #region Act

            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);

            try
            {
                
                // obter algumas marcas
                var marcas = await ObterMarcasJaCadastradas();

                // Obter uma marca já cadastrada para ser alterada
                marcaCaminhaoJaCadastrada = marcas.FirstOrDefault();

                // Obter uma outra marca
                var outraMarcaJaCadastrada = marcas.LastOrDefault();

                novaMarcaCaminhao = new MarcaCaminhaoAlterarRequest
                {
                    Id = marcaCaminhaoJaCadastrada.Id,
                    Descricao = outraMarcaJaCadastrada.Descricao
                };

                // Realiza a alteração
                marcaCaminhao = (OkObjectResult)await controller
                    .Alterar(marcaCaminhaoJaCadastrada.Id, novaMarcaCaminhao);

                // Recupera a nova marca no banco de dados
                marcaCaminhaoAlterada = await controller.ObterPorId(novaMarcaCaminhao.Id);

            }
            catch (Exception)
            {
            }

            #endregion

            #region Assert

            Assert.True(marcaCaminhao is null);

            #endregion
        }

        /// <summary>
        /// Gera uma marca de forma aleatória
        /// </summary>
        /// <param name="tamanho"></param>
        /// <returns></returns>
        private static string GerarNomeMarca(int tamanho)
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
        /// Obtem algumas masrcas
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<MarcaCaminhaoListDto>> ObterMarcasJaCadastradas()
        {
            var controller = new MarcaCaminhaoController(uriService, notificationContext, repository, service);
            var marcas = await controller.ListarTodos(new SortFilterPageRequest());
                   
            return marcas.Data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;
using JSL.CadCaminhoneiro.Api.Infrastructure.Installers.Pagination;
using JSL.CadCaminhoneiro.Api.Infrastructure.Notifications;
using JSL.CadCaminhoneiro.Domain.Interfaces;
using JSL.CadCaminhoneiro.Api.Services;
using JSL.CadCaminhoneiro.Domain.Entities;
using JSL.CadCaminhoneiro.Api.Dto;

namespace JSL.CadCaminhoneiro.Api.Controllers.v1
{
    [Route("api/v1/modelo-caminhao")]
    [ApiController]
    public class ModeloCaminhaoController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly NotificationContext _notificationContext;
        private readonly IModeloCaminhaoRepository _repository;
        private readonly IModeloCaminhaoService _service;

        public ModeloCaminhaoController(
            IUriService uriService,
            NotificationContext notificationContext,
            IModeloCaminhaoRepository repository,
            IModeloCaminhaoService service
            )
        {
            _uriService = uriService;
            _notificationContext = notificationContext;
            _repository = repository;
            _service = service;
        }        

        // GET: api/v1/modelo-caminhao/listar-todos
        [HttpGet]
        [Route("listar-todos")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ModeloCaminhaoListDto>>), Status200OK)]
        public async Task<PagedResponse<IEnumerable<ModeloCaminhaoListDto>>> ListarTodos([FromQuery] SortFilterPageRequest sortFilterPageRequest)
        {
            var route = string.Empty;

            if (Request != null)
                route = Request.Path.Value;
            
            var validFilter = new PaginationFilter(sortFilterPageRequest.PageNumber, sortFilterPageRequest.PageSize);
            var totalRegistros = await _repository.ObterTotalRegistrosAsync(sortFilterPageRequest.Filter);

            var modelosCaminhao = await _repository.ListarTodosQueryResponseAsync(
                sortFilterPageRequest.Sort, sortFilterPageRequest.Filter,
                validFilter.PageNumber, validFilter.PageSize);

            var pagedResponse =
                PaginationHelper.CreatePagedReponse<ModeloCaminhaoListDto>(
                    modelosCaminhao, validFilter, totalRegistros, _uriService, route);

            return pagedResponse;
        }

        // GET: api/v1/modelo-caminhao/listar-todos-sem-paginacao
        [HttpGet]
        [Route("listar-todos-sem-paginacao")]
        [ProducesResponseType(typeof(IEnumerable<ModeloCaminhaoListDto>), Status200OK)]
        public async Task<IEnumerable<ModeloCaminhaoListDto>> ListarTodosSemPaginacao()
        {
            var modelosCaminhao = await _repository.ListarTodosSemPaginacaoAsync();

            return modelosCaminhao;
        }

        // GET: api/v1/modelo-caminhao/982ea2dd-d8c1-4660-a0f6-ed3a491b2b9e
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ModeloCaminhaoListDto), Status200OK)]
        [ProducesResponseType(typeof(ModeloCaminhaoListDto), Status404NotFound)]
        public async Task<ModeloCaminhaoListDto> ObterPorId(Guid id)
        {
            var modeloCaminhao = await _repository.ObterPorIdQueryResponseAsync(id);

            if (modeloCaminhao == null)
                throw new ApiProblemDetailsException($"Modelo caminhão com id: {id} não existe.", Status404NotFound);

            return modeloCaminhao;
        }

        // POST: api/v1/modelo-caminhao
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<IActionResult> Incluir([FromBody] ModeloCaminhaoIncluirRequest incluirRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var modeloCaminhaoId = await _service.IncluirAsync(incluirRequest);

            if (_notificationContext.HasNotifications)
            {
                var notification =
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                throw new ApiProblemDetailsException(notification, Status400BadRequest);
            }

            return Ok($"O modelo de caminhão: {incluirRequest.Descricao} foi inserido com sucesso.");
        }

        // PUT: api/v1/modelo-caminhao/982ea2dd-d8c1-4660-a0f6-ed3a491b2b9e
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] ModeloCaminhaoAlterarRequest alterarRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            if (id != alterarRequest.Id || !await _repository.ExisteAsync(id))
                throw new ApiProblemDetailsException($"Modelo de caminhão com o Id: {id} não existe.", Status404NotFound);

            await _service.AlterarAsync(alterarRequest);

            if (_notificationContext.HasNotifications)
            {
                var notification =
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                throw new ApiProblemDetailsException(notification, Status400BadRequest);
            }

            return Ok($"O modelo de caminhão: {alterarRequest.Descricao} foi alterado com sucesso.");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var modeloCaminhao = await _repository.ObterPorIdAsync(id);

            if (modeloCaminhao is null)
            {
                throw new ApiProblemDetailsException($"Modelo de caminhão com o Id: {id} não existe.", Status404NotFound);
            }

            await _service.ExcluirAsync(modeloCaminhao);

            if (_notificationContext.HasNotifications)
            {
                var notification =
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                throw new ApiProblemDetailsException(notification, Status400BadRequest);
            }

            return Ok($"O modelo de caminhão: {modeloCaminhao.Descricao} foi excluído com sucesso.");
        }
    }
}

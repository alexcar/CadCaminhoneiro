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
    [Route("api/v1/marca-caminhao")]
    [ApiController]
    public class MarcaCaminhaoController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly NotificationContext _notificationContext;
        private readonly IMarcaCaminhaoRepository _repository;
        private readonly IMarcaCaminhaoService _service;

        public MarcaCaminhaoController(
            IUriService uriService, 
            NotificationContext notificationContext,
            IMarcaCaminhaoRepository repository,
            IMarcaCaminhaoService service
            )
        {
            _uriService = uriService;
            _notificationContext = notificationContext;
            _repository = repository;
            _service = service;
        }

        // GET: api/v1/marca-caminhao/listar-todos
        [HttpGet]
        [Route("listar-todos")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<MarcaCaminhaoListDto>>), Status200OK)]
        public async Task<PagedResponse<IEnumerable<MarcaCaminhaoListDto>>> ListarTodos([FromQuery] SortFilterPageRequest sortFilterPageRequest)
        {
            var route = string.Empty;

            if (Request != null)
                route = Request.Path.Value;

            var validFilter = new PaginationFilter(sortFilterPageRequest.PageNumber, sortFilterPageRequest.PageSize);
            var totalRegistros = await _repository.ObterTotalRegistrosAsync(sortFilterPageRequest.Filter);

            var marcasCaminhao = await _repository.ListarTodosQueryResponseAsync(
                sortFilterPageRequest.Sort, sortFilterPageRequest.Filter,
                validFilter.PageNumber, validFilter.PageSize);

            var pagedResponse =
                PaginationHelper.CreatePagedReponse<MarcaCaminhaoListDto>(
                    marcasCaminhao, validFilter, totalRegistros, _uriService, route);

            return pagedResponse;
        }

        // GET: api/v1/marca-caminhao/982ea2dd-d8c1-4660-a0f6-ed3a491b2b9e
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(MarcaCaminhaoListDto), Status200OK)]
        [ProducesResponseType(typeof(MarcaCaminhaoListDto), Status404NotFound)]
        public async Task<MarcaCaminhaoListDto> ObterPorId(Guid id)
        {
            var marcaCaminhao = await _repository.ObterPorIdQueryResponseAsync(id);

            if (marcaCaminhao == null)
                throw new ApiProblemDetailsException($"Marca caminhão com id: {id} não existe.", Status404NotFound);

            return marcaCaminhao;
        }

        // POST: api/v1/marca-caminhao
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<IActionResult> Incluir([FromBody] MarcaCaminhaoIncluirRequest incluirRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var marcaCaminhaoId = await _service.IncluirAsync(incluirRequest);

            if (_notificationContext.HasNotifications)
            {
                var notification = 
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                throw new ApiProblemDetailsException(notification, Status400BadRequest);
            }

            return Ok(marcaCaminhaoId.ToString());
        }

        // PUT: api/v1/marca-caminhao/982ea2dd-d8c1-4660-a0f6-ed3a491b2b9e
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] MarcaCaminhaoAlterarRequest alterarRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            if (id != alterarRequest.Id || !await _repository.ExisteAsync(id))
                throw new ApiProblemDetailsException($"Marca de caminhão com o Id: {id} não existe.", Status404NotFound);

            await _service.AlterarAsync(alterarRequest);

            if (_notificationContext.HasNotifications)
            {
                var notification =
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                throw new ApiProblemDetailsException(notification, Status400BadRequest);
            }

            return Ok($"Registro com o Id: {id} alterado com sucesso");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var marcaCaminhao = await _repository.ObterPorIdAsync(id);

            if (marcaCaminhao is null)
            {
                throw new ApiProblemDetailsException($"Marca de caminhão com o Id: {id} não existe.", Status404NotFound);
            }

            await _service.ExcluirAsync(marcaCaminhao);

            return Ok($"Registro com o Id: {id} excluído com sucesso");
        }
    }
}

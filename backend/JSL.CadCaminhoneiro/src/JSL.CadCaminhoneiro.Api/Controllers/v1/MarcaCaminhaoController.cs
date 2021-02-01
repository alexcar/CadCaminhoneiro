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

        // GET: api/v1/marca-caminhao
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var marcaCaminhao = await _repository
                .ListarTodosQueryResponseAsync(null, null, filter.PageNumber, filter.PageSize);

            var pageData = marcaCaminhao
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = marcaCaminhao.Count();
            var pagedResponse =
                PaginationHelper.CreatePagedReponse<MarcaCaminhaoListDto>(
                    pageData, validFilter, totalRecords, _uriService,
                    route);

            return Ok(pagedResponse);
        }

        // GET: api/v1/marca-caminhao/listar-todos
        [HttpGet]
        [Route("listar-todos")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<MarcaCaminhaoListDto>>), Status200OK)]
        public async Task<PagedResponse<IEnumerable<MarcaCaminhaoListDto>>> ListarTodos([FromQuery] SortFilterPageRequest sortFilterPageRequest)
        {
            var route = Request.Path.Value;
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
        public async Task<MarcaCaminhaoListDto> Get(Guid id)
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
        public async Task<IActionResult> Post([FromBody] MarcaCaminhaoIncluirRequest incluirRequest)
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
                // TODO: Retornar vários erros para testar o retorno de erro como uma lista                
                var notification =
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                //var notification =
                //    _notificationContext.Notifications.Select(p => p.Message).ToList();

                //return BadRequest(notification);
                // return BadRequest(new ApiException(notification, Status400BadRequest));
                // return BadRequest(new ApiProblemDetailsException(notification, Status400BadRequest));
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
                // return NotFound($"Marca de caminhão com o Id: {id} não existe.");
                throw new ApiProblemDetailsException($"Marca de caminhão com o Id: {id} não existe.", Status404NotFound);
            }

            await _service.ExcluirAsync(marcaCaminhao);

            return Ok($"Registro com o Id: {id} excluído com sucesso");
        }
    }
}

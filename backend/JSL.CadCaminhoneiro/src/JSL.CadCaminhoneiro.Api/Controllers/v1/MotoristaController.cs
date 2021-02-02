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
    [Route("api/v1/motorista")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        private readonly IUriService _uriService;
        private readonly NotificationContext _notificationContext;
        private readonly IMotoristaRepository _repository;
        private readonly IMotoristaService _service;

        public MotoristaController(
            IUriService uriService,
            NotificationContext notificationContext,
            IMotoristaRepository repository,
            IMotoristaService service
            )
        {
            _uriService = uriService;
            _notificationContext = notificationContext;
            _repository = repository;
            _service = service;
        }

        // GET: api/v1/motorista/listar-todos
        [HttpGet]
        [Route("listar-todos")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<MotoristaListDto>>), Status200OK)]
        public async Task<PagedResponse<IEnumerable<MotoristaListDto>>> ListarTodos([FromQuery] SortFilterPageRequest sortFilterPageRequest)
        {
            try
            {
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(sortFilterPageRequest.PageNumber, sortFilterPageRequest.PageSize);
                var totalRegistros = await _repository.ObterTotalRegistrosAsync(sortFilterPageRequest.Filter);

                var motoristas = await _repository.ListarTodosQueryResponseAsync(
                    sortFilterPageRequest.Sort, sortFilterPageRequest.Filter,
                    validFilter.PageNumber, validFilter.PageSize);

                var pagedResponse =
                    PaginationHelper.CreatePagedReponse<MotoristaListDto>(
                        motoristas, validFilter, totalRegistros, _uriService, route);

                return pagedResponse;
            }
            catch (Exception e)
            {

                var msg = e.Message;
                throw;
            }            
        }

        // GET: api/v1/motorista/982ea2dd-d8c1-4660-a0f6-ed3a491b2b9e
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ModeloCaminhaoListDto), Status200OK)]
        [ProducesResponseType(typeof(ModeloCaminhaoListDto), Status404NotFound)]
        public async Task<MotoristaListDto> Get(Guid id)
        {
            var motorista = await _repository.ObterPorIdQueryResponseAsync(id);

            if (motorista is null)
                throw new ApiProblemDetailsException($"Motorista com id: {id} não existe.", Status404NotFound);

            return motorista;
        }

        // POST: api/v1/motorista
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] MotoristaIncluirRequest incluirRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var motoristaId = await _service.IncluirAsync(incluirRequest);

            if (_notificationContext.HasNotifications)
            {
                var notification =
                    _notificationContext.Notifications.Select(p => p.Message).FirstOrDefault();

                throw new ApiProblemDetailsException(notification, Status400BadRequest);
            }

            return Ok(motoristaId.ToString());
        }

        // PUT: api/v1/motorista/982ea2dd-d8c1-4660-a0f6-ed3a491b2b9e
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] MotoristaAlterarRequest alterarRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            if (id != alterarRequest.Id || !await _repository.ExisteAsync(id))
                throw new ApiProblemDetailsException($"Motorista com o Id: {id} não existe.", Status404NotFound);

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
            var motorista = await _repository.ObterPorIdAsync(id);

            if (motorista is null)
            {
                throw new ApiProblemDetailsException($"Motorista com o Id: {id} não existe.", Status404NotFound);
            }

            await _service.ExcluirAsync(motorista);

            return Ok($"Registro com o Id: {id} excluído com sucesso");
        }
    }
}

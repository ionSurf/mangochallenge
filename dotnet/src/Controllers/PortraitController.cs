using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Repositories;
using Entities.Models;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using LoggerService;
using Filters.ActionFilters;
using Filters.ExceptionFilters;

namespace Controllers
{
    [Route("api/portrait")]
    [ApiController]
    public class PortraitController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PortraitController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //[HttpGet]
        [HttpGet,Authorize]
        [EnableCors("MangoPolicy")]
        [ApiExceptionFilter]
        public async Task<IActionResult> GetAllPortraits()
        {
            //var portraits = _repository.Portrait.GetAllPortraits();
            var portraits = await _repository.Portrait.GetAllPortraitsAsync();
            _logger.LogInfo($"Returned all portraits from database.");
            //return Ok(portraits);

            var portraitsResult = _mapper.Map<IEnumerable<PortraitDto>>(portraits);
            return Ok(portraitsResult);
            
        }

        [EnableCors("MangoPolicy")]
        [HttpGet("{id}", Name = "PortraitById"), Authorize]
        [ServiceFilter(typeof(PortraitValidateEntityExistsAttribute))]
        [ApiExceptionFilter]
        public IActionResult GetPortraitById(Guid id)
        {
            _logger.LogInfo($"Returned Portrait with id: {id}");
            var portraitEntity = HttpContext.Items["entity"] as Portrait;
            var result = _mapper.Map<PortraitDto>(portraitEntity);
            return Ok(result);
        }

        [HttpPost, Authorize]
        [EnableCors("MangoPolicy")]
        [ServiceFilter(typeof(PortraitValidationFilterAttribute<PortraitForCreationDto>))]
        [ApiExceptionFilter]
        public async Task<IActionResult> CreatePortrait([FromBody] PortraitForCreationDto Portrait)
        {
            var portraitEntity = _mapper.Map<Portrait>(Portrait);
            _repository.Portrait.CreatePortrait(portraitEntity);

            await _repository.SaveAsync();

            _logger.LogInfo($"Created Portrait with id: {portraitEntity.Id}");
            var createdPortrait = _mapper.Map<PortraitDto>(portraitEntity);
            return CreatedAtRoute("PortraitById", new { id = createdPortrait.Id }, createdPortrait);
        }

        [HttpPut("{id}"), Authorize]
        [EnableCors("MangoPolicy")]
        [ServiceFilter(typeof(PortraitValidationFilterAttribute<PortraitForUpdateDto>))]
        [ServiceFilter(typeof(PortraitValidateEntityExistsAttribute))]
        [ApiExceptionFilter]
        public async Task<IActionResult> UpdatePortrait(Guid id, [FromBody] PortraitForUpdateDto Portrait)
        {
            var portraitEntity = HttpContext.Items["entity"] as Portrait;
            _mapper.Map(Portrait, portraitEntity);
            _repository.Portrait.UpdatePortrait(portraitEntity);

            await _repository.SaveAsync();
            
            _logger.LogInfo($"Updated Portrait with id: {portraitEntity.Id}");
            var createdPortrait = _mapper.Map<PortraitDto>(portraitEntity);
            return CreatedAtRoute("PortraitById", new { id = createdPortrait.Id }, createdPortrait);
        }

        [EnableCors("MangoPolicy")]
        [HttpDelete("{id}"), Authorize]
        [ServiceFilter(typeof(PortraitValidateEntityExistsAttribute))]
        [ApiExceptionFilter]
        public async Task<IActionResult> DeletePortrait(Guid id)
        {
            var portraitEntity = HttpContext.Items["entity"] as Portrait;
            _repository.Portrait.DeletePortrait(portraitEntity);
            
            await _repository.SaveAsync();

            var result = _mapper.Map<PortraitDto>(portraitEntity);
            return Ok(result);
        }
    }
}
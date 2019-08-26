using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamoDBApi.Models;
using DynamoDBApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DynamoDBApi.Controllers
{
    [Route("api/[controller]/organization/{organizationId}")]
    [ApiController]
    public class WidgetStatesController : ControllerBase
    {
        private readonly WidgetStateRepository _widgetStateRepository;

        public WidgetStatesController(WidgetStateRepository widgetStateRepository)
        {
            _widgetStateRepository = widgetStateRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WidgetState>>> GetByOrganization(int organizationId)
        {
            var widgetStates = await _widgetStateRepository.FindBy(organizationId);
            return Ok(widgetStates);
        }
        
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WidgetState>>> Get(int organizationId, int userId)
        {
            var widgetStates = await _widgetStateRepository.FindBy(organizationId, userId);
            return Ok(widgetStates);
        }
        
        [HttpPut("user/{userId}/widget/{widgetName}")]
        public async Task<ActionResult> UpdateWidgetStateForUser(int organizationId, int userId, string widgetName, [FromBody]dynamic states)
        {
            await _widgetStateRepository.Update(organizationId, userId, widgetName, states);
            return Ok();
        }
        
        [HttpPut("widget/{widgetName}")]
        public async Task<ActionResult> UpdateWidgetStateForOrganization(int organizationId, string widgetName, [FromBody]dynamic states)
        {
            await _widgetStateRepository.Update(organizationId, null, widgetName, states);
            return Ok();
        }
    }
}
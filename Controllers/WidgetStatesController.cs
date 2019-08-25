using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamoDBApi.Models;
using DynamoDBApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DynamoDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetStatesController : ControllerBase
    {
        private readonly WidgetStateRepository _widgetStateRepository;

        public WidgetStatesController(WidgetStateRepository widgetStateRepository)
        {
            _widgetStateRepository = widgetStateRepository;
        }
        
        // GET api/values/5
        [HttpGet("organization/{organizationId}/user/{userId}")]
        public async Task<ActionResult<WidgetState>> Get(int organizationId, int userId)
        {
            return await _widgetStateRepository.FindBy(userId, organizationId);
        }
    }
}
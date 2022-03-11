using bnbAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private FacilityService facility = new FacilityService();

        [HttpGet]
        public IActionResult GetFacility()
        {
            return Ok(facility.GetAllFacilities());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZWalkAPI.Data;

namespace NewZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;
        public RegionsController(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ou RECUPERER TOUTES LES REGIONS
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            return Ok(regions);
        }

        // GET REGIONS AVEC SON ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // var region = dbContext.Regions.Find(id);
            // ou utiliser LINQ
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(region == null)
            {
                return NotFound();
            }

            return Ok(region);

        }
    }
}

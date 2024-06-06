using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZWalkAPI.Data;
using NewZWalkAPI.Models.DTO;

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
            // Avoir les données provenant de SQL SERVER - Domains models
            var regionsDomain = dbContext.Regions.ToList();
            // Mapper le domain models à DTO
            var regionsDto = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
                
            }

            // Retourner le DTO
            return Ok(regionsDto);
        }

        // GET REGIONS AVEC SON ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // var region = dbContext.Regions.Find(id);
            // ou utiliser LINQ
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(regionDomain == null)
            {
                return NotFound();
            }

            // Si ce n'est pas NULL, on va mapper/Conversion Region Domain Model en Region DTO
            var regionsDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //Retourner le DTO à nos clients

            return Ok(regionsDto);

        }

        // POST pour créer une nouvelle région
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {

        }
    }
}

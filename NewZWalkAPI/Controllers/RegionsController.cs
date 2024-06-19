using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZWalkAPI.Data;
using NewZWalkAPI.Models.Domain;
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
        public async Task<IActionResult> GetAll()
        {
            // Avoir les données provenant de SQL SERVER - Domains models
            var regionsDomain = await dbContext.Regions.ToArrayAsync();
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // var region = dbContext.Regions.Find(id);
            // ou utiliser LINQ
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

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

        //----------------- POST pour créer une nouvelle région------------------//
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            // Utiliser le Domain Model pour créer une région
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync(); // Pour enregistrer dans la BDD

            // Map Domain model retourne to DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //-----------------Update une region en fonction de son ID------------------//
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDto)
        {
            // Verifier si la région existe
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to DomainModel
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            // Conversion de Domain Model à DTO
            var regionDto = new Region
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // Supprimer une région
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Supprimer la région
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            // Retour des regions 
            // Map Domain Model à DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }
    }
}

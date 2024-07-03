using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZWalkAPI.Data;
using NewZWalkAPI.Models.Domain;
using NewZWalkAPI.Models.DTO;
using NewZWalkAPI.Repositories;

namespace NewZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalkDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ou RECUPERER TOUTES LES REGIONS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Avoir les données provenant de SQL SERVER - comme c'est la responsabilité de RegionRepository, on l'interroge
            var regionsDomain = await regionRepository.GetAllAsync();
            // Mapper le domain models à DTO
            /*var regionsDto = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
                
            }*/
            
            // Map Domain Model to DTO
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);

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
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if(regionDomain == null)
            {
                return NotFound();
            }


            //Retourner le DTO à nos clients

            return Ok(mapper.Map<RegionDTO>(regionDomain));

        }

        //----------------- POST pour créer une nouvelle région------------------//
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            // A la place d'utiliser le Domain Model, on utilisera le Repository
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model retourne to DTO
            var regionDto = mapper.Map<RegionDTO> (regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //-----------------Update une region en fonction de son ID------------------//
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDto)
        {
            // Convertir DTO en Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Pour certifier de prendre ce qui vont venir de repository

            regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Conversion de Domain Model à DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);
        }

        // Supprimer une région
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // On appelle le repository pour interroger les données de la BDD
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }        

            // Retour des regions 
            // Map Domain Model à DTO
            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDto);

        }
    }
}

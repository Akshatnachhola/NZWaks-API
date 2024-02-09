using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO_s;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;
        
        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
      //  [Authorize(Roles = "Reader")]



        
        //Get All Regions

        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain Models
            var regions = await regionRepository.GetAllAsync();


            //Map Domain Models to DTO's
            //var regionsDto = new List<RegionDto>();  
            // foreach(var region in regions)
            // {
            // regionsDto.Add(new RegionDto()
            // {
            //Id = region.Id,
            //Code = region.Code,
            //  Name = region.Name,
            // RegionImageUrl = region.RegionImageUrl,


            // });

            // }

           var regionsDto =  mapper.Map<List<RegionDto>>(regions);


            //return DTO's
            return Ok(regionsDto);  
                
        }

        //GET Region by Id(Get Sible Region)

        [HttpGet]
      //  [Authorize(Roles = "Reader")]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) 
        {


            var region = await regionRepository.GetByIdAsync(id);

            if (region==null)
            {
                return NotFound();


                
            }

            //Map conversion


            // var regionDto = new RegionDto()
            // {
            // Id = region.Id,
            // Code = region.Code,
            // Name = region.Name,
            // RegionImageUrl = region.RegionImageUrl,


            // };

            //return Dto back to client

            return Ok(mapper.Map<RegionDto>(region));

        }

       //Post to create single Region

        [HttpPost]
        [ValidateModel]
      //  [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            if (ModelState.IsValid)
            {
                //Map or Convert DTO to DOmain Model

                //var regionDomainModel = new Region
                // {
                //Code = addRegionRequestDto.Code,
                //Name = addRegionRequestDto.Name,
                //RegionImageUrl = addRegionRequestDto.RegionImageUrl


                // };

                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                //Use Domain Model to Create Region 
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain model back to Dto
                // var regionDto = new RegionDto
                // {

                // Id = regionDomainModel.Id,
                //Code = regionDomainModel.Code,
                // Name = regionDomainModel.Name,

                // };

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
            }
            else
            {
                return BadRequest();
            }
        }

        //Update region
        //Put : https://localhost:portnumber/api/region/{id}

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
      //  [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto) 
        {
            if (ModelState.IsValid)
            {

                //Map Dto to Domain Model

                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //  var regionDomainModel = new Region
                // {
                //  Code = updateRegionRequestDto.Code,
                //  Name = updateRegionRequestDto.Name,
                // RegionImageUrl = updateRegionRequestDto.RegionImageUrl


                // };
                //Check if Region Exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Convert Domain Model to Dto

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                // var regionDto = new RegionDto
                //// {
                // Id = regionDomainModel.Id,
                // Code = regionDomainModel.Code,
                // Name = regionDomainModel.Name,
                // RegionImageUrl = regionDomainModel.RegionImageUrl

                //  };

                return Ok(regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }


        
        }


        //Delete the Region
        //DELETE: 
        [HttpDelete]

        [Route("{id.Guid}")]
       // [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {

                return NotFound();

            
            }



            //return Deleted Region Back
            //Map Domain to Dto

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //var regionDto = new RegionDto
           // {
              //  Id = regionDomainModel.Id,
               // Code = regionDomainModel.Code,
               // Name = regionDomainModel.Name,
                //RegionImageUrl = regionDomainModel.RegionImageUrl

            //};

            return Ok(regionDto);

        }
    }
}

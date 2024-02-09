using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Models.DTO_s;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository IWalkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.IWalkRepository = walkRepository;
        }

        //Create Walk 
        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map Dto to Domain Model

                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                walkDomainModel = await IWalkRepository.CreateAsync(walkDomainModel);

                //Map Domain Model to Dto

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //Get Walks

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var walksDomainModel = await IWalkRepository.GetAllAsync();

            //Map Domain Model to Dto

            return Ok(mapper.Map<List<Walk>>(walksDomainModel));
        }


        //Get Walk By Id
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await IWalkRepository.GetByIdAsync(id);

            if (walkDomainModel == null) 
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Update by Id 

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto )

        {
            if (ModelState.IsValid)
            {
                //MaP DTO 
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                await IWalkRepository.updateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {

                    return NotFound();


                }

                //Map Domain Model to Dto 

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id )
      
        {

           var deletedWalkDomainModel =  await IWalkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            return Ok (mapper.Map<WalkDto>(deletedWalkDomainModel));



            
        }
    }


}

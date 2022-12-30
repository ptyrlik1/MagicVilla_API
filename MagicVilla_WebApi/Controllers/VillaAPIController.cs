using AutoMapper;
using MagicVilla_WebApi.Data;

using MagicVilla_WebApi.Models;
using MagicVilla_WebApi.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace MagicVilla_WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper; 
        public VillaAPIController(ApplicationDbContext db, IMapper mapper) 
        {
            _db = db;
            _mapper = mapper;
        }
        //GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok( _mapper.Map<VillaDTO>(villaList));
        }
        //GET ID
        [HttpGet("{id:int}",Name="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public  async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {

            var villa = await  _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (id ==0)
            {
               
                return BadRequest();
            }
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa)); 
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaDTO)
        {
         
            if( await _db.Villas.FirstOrDefaultAsync(u=>u.Name.ToLower()==villaDTO.Name.ToLower())!=null)
            {
                ModelState.AddModelError("", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if(villaDTO== null)
            {
                return BadRequest(villaDTO);
            }
            //if(villaDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            Villa model = new Villa()
            {
             
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetVilla", new {id = model.Id},model);
        }

        [HttpDelete("{id:int}", Name = "DelteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id==id);
            if(villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            //var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;
            Villa model = new Villa()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}", Name ="UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa =  await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

         

            VillaUpdateDTO villaDTO = new ()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = new Villa()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Update(model);
            await _db.SaveChangesAsync();
      

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

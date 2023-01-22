using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrebolAcademy.Models;

namespace TrebolAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrimoryController : ControllerBase
    {
        private readonly TrebolAcademyContext _dbContext;

        public GrimoryController(TrebolAcademyContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET GRIMORY
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grimorio>>> GetGrimorys()
        {
            if (_dbContext.Grimory == null)
            {
                return NotFound();
            }
            return await _dbContext.Grimory.ToListAsync();
        }

        //GET GRIMORY ID
        [HttpGet("{Id}")]
        public async Task<ActionResult<Grimorio>> GetGrimory(int Id)
        {
            if (_dbContext.Grimory == null)
            {
                return NotFound();
            }
            var gremory = await _dbContext.Grimory.FindAsync(Id);
            if (gremory == null)
            {
                return NotFound();
            }
            return gremory;
        }

        //POST GRIMORY 
        [HttpPost]
        public async Task<ActionResult<Grimorio>> PostGrimory(Grimorio grimorio)
        {
            _dbContext.Grimory.Add(grimorio);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGrimory), new { id = grimorio.Id }, grimorio);
        }

        //PUT GRIMORY
        [HttpPut("{Id}")]
        public async Task<ActionResult> PutGrimory(int Id, Grimorio grimorio)
        {
            if (Id != grimorio.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(grimorio).State= EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            { 
                if(!GrimoryExist(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool GrimoryExist(long id)
        {
            return (_dbContext.Grimory?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Delete Grimory
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGrimory(int id)
        {
            if(_dbContext.Grimory == null)
            {
                return NotFound("No se encontro registros");
            }
            var grimory = await _dbContext.Grimory.FindAsync(id);

            if (grimory == null)
            {
                return NotFound("No existe registro con el id proporcionado");
            }

            _dbContext.Grimory.Remove(grimory);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

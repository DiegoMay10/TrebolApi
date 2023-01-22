using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TrebolAcademy.Models;
using TrebolAcademy.Service;

namespace TrebolAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly TrebolAcademyContext _dbContext;

        public SolicitudController(TrebolAcademyContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET Solicitud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solicitud>>> GetSolicitudes()
        {
            if (_dbContext.Solicitude == null)
            {
                return NotFound();
            }
            return await _dbContext.Solicitude.ToListAsync();
        }

        //GET Solicitud ID
        [HttpGet("{Id}")]
        public async Task<ActionResult<Solicitud>> GetSolicitude(int Id)
        {
            if (_dbContext.Solicitude == null)
            {
                return NotFound();
            }
            var solicitud = await _dbContext.Solicitude.FindAsync(Id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return solicitud;
        }

        //POST Solicitud 
        [HttpPost]
        public async Task<ActionResult<Solicitud>> PostSolicitude(SolicitudeRequestDTO solicitud)
        {
            if (solicitud == null)
            {
                return NotFound();
            }
            if (solicitud.name.Length > 20 || !Regex.IsMatch(solicitud.name.ToString(), "[^a-zA-Z]*$"))
            {
                return NotFound("El nombre no puede contener mas de 20 caracteres y solo se permiten letras.");
            }
            if (solicitud.surname.Length > 20 || !Regex.IsMatch(solicitud.surname.ToString(), "[^a-zA-Z]*$"))
            {
                return NotFound("El apellido no puede contener mas de 20 caracteres y solo se permiten letras.");
            }
            if (solicitud.identifier.Length > 10)
            {
                return NotFound("La identificación no puede contener mas de 10 caracteres.");
            }
            if (!Regex.IsMatch(solicitud.age.ToString(), "^[0-9]*${2,2}") || solicitud.age > 99 )
            {
                return NotFound("La edad no puede ser menor o mayor a 2 digitos.");
            }

            Solicitud solicitude = new Solicitud();
            solicitude.name = solicitud.name;
            solicitude.surname = solicitud.surname;
            solicitude.identifier = solicitud.identifier;
            solicitude.age = solicitud.age;
            solicitude.afinity = solicitud.afinity;
            solicitude.idGrimorio = 0;
            solicitude.Grimorio = "Pendiente";
            solicitude.idStatus = 1;
            solicitude.status = "Pendiente";
            solicitude.title = "";
            _dbContext.Solicitude.Add(solicitude);
            await _dbContext.SaveChangesAsync();
            return Ok(solicitud);
        }

        //PUT Solicitud
        [HttpPut("{Id}")]
        public async Task<ActionResult> PutSolicitude(int Id, SolicitudeRequestDTO solicitud)
        {
            if (solicitud == null)
            {
                return BadRequest("No se encontro registro");
            }
            var solicitudes = await _dbContext.Solicitude.FindAsync(Id);
            if(solicitudes == null)
            {
                return BadRequest("No se encontro registro");
            }
            if (solicitud.name.Length > 20 || !Regex.IsMatch(solicitud.name.ToString(), "[^a-zA-Z]*$"))
            {
                return NotFound("El nombre no puede contener mas de 20 caracteres y solo se permiten letras.");
            }
            if (solicitud.surname.Length > 20 || !Regex.IsMatch(solicitud.surname.ToString(), "[^a-zA-Z]*$"))
            {
                return NotFound("El apellido no puede contener mas de 20 caracteres y solo se permiten letras.");
            }
            if (solicitud.identifier.Length > 10)
            {
                return NotFound("La identificación no puede contener mas de 10 caracteres.");
            }
            if (!Regex.IsMatch(solicitud.age.ToString(), "^[0-9]*${2,2}") || solicitud.age > 99)
            {
                return NotFound("La edad no puede ser menor o mayor a 2 digitos.");
            }
            solicitudes.name = solicitud.name;
            solicitudes.surname = solicitud.surname;
            solicitudes.identifier = solicitud.identifier;
            solicitudes.age = solicitud.age;
            solicitudes.afinity = solicitud.afinity;

            _dbContext.Solicitude.Update(solicitudes);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudeExist(Id))
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

        //PUT Solicitud
       
        private bool SolicitudeExist(long id)
        {
            return (_dbContext.Solicitude?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Delete Grimory
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSolicitude(int id)
        {
            if (_dbContext.Solicitude == null)
            {
                return NotFound("No se encontro registros");
            }
            var solicitude = await _dbContext.Solicitude.FindAsync(id);

            if (solicitude == null)
            {
                return NotFound("No existe registro con el id proporcionado");
            }

            _dbContext.Solicitude.Remove(solicitude);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

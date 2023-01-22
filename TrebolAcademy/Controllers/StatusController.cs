using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrebolAcademy.Models;

namespace TrebolAcademy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly TrebolAcademyContext _dbContext;

        public StatusController(TrebolAcademyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> PutStatutsSolicitude(int Id, int idStatus)
        {
            var todo = await _dbContext.Solicitude.FindAsync(Id);
            if (todo == null)
            {
                return NotFound("No se encontro registro");
            }

            switch (idStatus)
            {
                case 1:
                    todo.idStatus = idStatus;
                    todo.status = "Pendiente";
                    break;
                case 2:
                    todo.idStatus = idStatus;
                    todo.status = "Aprobado";

                    Random r = new Random();
                    int portada = r.Next(1, 5);
                    var grimorio = await _dbContext.Grimory.FindAsync(portada);
                    switch (portada)
                    {
                        case 1:
                            todo.title = "Trébol de 1 hoja";
                            todo.idGrimorio = grimorio.Id;
                            todo.Grimorio = grimorio.nameGrimoire;
                            break;
                        case 2:
                            todo.title = "Trébol de 2 hojas";
                            todo.idGrimorio = grimorio.Id;
                            todo.Grimorio = grimorio.nameGrimoire;
                            break;
                        case 3:
                            todo.title = "Trébol de 3 hojas";
                            todo.idGrimorio = grimorio.Id;
                            todo.Grimorio = grimorio.nameGrimoire;
                            break;
                        case 4:
                            todo.title = "Trébol de 4 hojas";
                            todo.idGrimorio = grimorio.Id;
                            todo.Grimorio = grimorio.nameGrimoire;
                            break;
                        case 5:
                            todo.title = "Trébol de 5 hojas";
                            todo.idGrimorio = grimorio.Id;
                            todo.Grimorio = grimorio.nameGrimoire;
                            break;
                        default:
                            return BadRequest("No se encontro grimorio con dicha portada.");
                            break;
                    }
                    break;
                case 3:
                    todo.idStatus = idStatus;
                    todo.status = "Rechazado";
                    break;
            }

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

        private bool SolicitudeExist(long id)
        {
            return (_dbContext.Solicitude?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}

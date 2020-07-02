using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly IproAgilRepository _palestrante;

        public PalestranteController(IproAgilRepository palestrante)
        {
            _palestrante = palestrante;
        }
        
        [HttpGet("getByNome/{Name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var results = await _palestrante.GetAllPalestranteAsyncByName(name, true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                var results = await _palestrante.GetPalestranteAsync(PalestranteId, true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _palestrante.Add(model);

                if(await _palestrante.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }     

            return BadRequest();       
        }

        [HttpPut("{PalestranteId}")]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {
            try
            {
                var palestrante = await _palestrante.GetPalestranteAsync(PalestranteId, false);
                if(palestrante == null) return NotFound();

                _palestrante.Update(model);

                if(await _palestrante.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }     

            return BadRequest();       
        }
        
        [HttpDelete("{PalestranteId}")]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var palestrante = await _palestrante.GetPalestranteAsync(PalestranteId, false);
                if(palestrante == null) return NotFound();

                _palestrante.Delete(palestrante);

                if(await _palestrante.SaveChangesAsync())
                {
                    return Ok("" );
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }     

            return BadRequest();       
        }
        
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroFuncionarios.Classes;
using CadastroFuncionarios.Context;

namespace CadastroFuncionarios.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly Db _context;

        public FuncionariosController(Db context)
        {
            _context = context;
        }

        // GET: api/Funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionarios>>> GetFuncionarios()
        {
          if (_context.Funcionarios == null)
          {
              return NotFound();
          }
            return await _context.Funcionarios.ToListAsync();
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionarios>> GetFuncionarios(int id)
        {
          if (_context.Funcionarios == null)
          {
              return NotFound();
          }
            var funcionarios = await _context.Funcionarios.FindAsync(id);

            if (funcionarios == null)
            {
                return NotFound();
            }

            return funcionarios;
        }

        // PUT: api/Funcionarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionarios(int id, Funcionarios funcionarios)
        {
            if (id != funcionarios.ID)
            {
                return BadRequest();
            }

            _context.Entry(funcionarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionariosExists(id))
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

        // POST: api/Funcionarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Funcionarios>> PostFuncionarios(Funcionarios funcionarios)
        {
          if (_context.Funcionarios == null)
          {
              return Problem("Entity set 'Db.Funcionarios'  is null.");
          }
            _context.Funcionarios.Add(funcionarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionarios", new { id = funcionarios.ID }, funcionarios);
        }

        // DELETE: api/Funcionarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionarios(int id)
        {
            if (_context.Funcionarios == null)
            {
                return NotFound();
            }
            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuncionariosExists(int id)
        {
            return (_context.Funcionarios?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

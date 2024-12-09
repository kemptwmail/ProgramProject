using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakfastController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor that initializes the context
        public BreakfastController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Breakfast (Returns the first 5 breakfast items if no id is provided)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Breakfast>>> GetBreakfasts(int? id = 0)
        {
            if (id == null || id == 0)
            {
                return await _context.FavoriteBreakfastFoods.Take(5).ToListAsync();
            }

            var breakfast = await _context.FavoriteBreakfastFoods.FindAsync(id);

            if (breakfast == null)
            {
                return NotFound();
            }

            return new List<Breakfast> { breakfast };
        }

        // GET: api/Breakfast/5 (Get a single breakfast item by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Breakfast>> GetBreakfast(int id)
        {
            var breakfast = await _context.FavoriteBreakfastFoods.FindAsync(id);

            if (breakfast == null)
            {
                return NotFound();
            }

            return breakfast;
        }

        // POST: api/Breakfast (Create a new breakfast item)
        [HttpPost]
        public async Task<ActionResult<Breakfast>> PostBreakfast(Breakfast breakfast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            breakfast.Id = 0;

            _context.FavoriteBreakfastFoods.Add(breakfast);
            await _context.SaveChangesAsync();

            // Return the newly created breakfast with its location
            return CreatedAtAction(nameof(GetBreakfast), new { id = breakfast.Id }, breakfast);
        }

        // PUT: api/Breakfast/5 (Update an existing breakfast item)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreakfast(int id, Breakfast breakfast)
        {
            if (id != breakfast.Id)
            {
                return BadRequest();
            }

            _context.Entry(breakfast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreakfastExists(id))
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

        // DELETE: api/Breakfast/5 (Delete a breakfast item by id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreakfast(int id)
        {
            var breakfast = await _context.FavoriteBreakfastFoods.FindAsync(id);
            if (breakfast == null)
            {
                return NotFound();
            }

            _context.FavoriteBreakfastFoods.Remove(breakfast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a breakfast item exists by id
        private bool BreakfastExists(int id)
        {
            return _context.FavoriteBreakfastFoods.Any(e => e.Id == id);
        }
    }
}

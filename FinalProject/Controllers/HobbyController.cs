using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor that initializes the context
        public HobbyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Hobby (Returns the first 5 hobbies if no id is provided)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hobby>>> GetHobbies(int? id = 0)
        {
            if (id == null || id == 0)
            {
                return await _context.Hobbies.Take(5).ToListAsync();
            }

            var hobby = await _context.Hobbies.FindAsync(id);

            if (hobby == null)
            {
                return NotFound();
            }

            return new List<Hobby> { hobby };
        }

        // GET: api/Hobby/5 (Get a single hobby by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Hobby>> GetHobby(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);

            if (hobby == null)
            {
                return NotFound();
            }

            return hobby;
        }

        // POST: api/Hobby (Create a new hobby)
        [HttpPost]
        public async Task<ActionResult<Hobby>> PostHobby(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            hobby.Id = 0;

            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();

            // Return the newly created hobby with its location
            return CreatedAtAction(nameof(GetHobby), new { id = hobby.Id }, hobby);
        }

        // PUT: api/Hobby/5 (Update an existing hobby)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobby(int id, Hobby hobby)
        {
            if (id != hobby.Id)
            {
                return BadRequest();
            }

            _context.Entry(hobby).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyExists(id))
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

        // DELETE: api/Hobby/5 (Delete a hobby by id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }

            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a hobby exists by id
        private bool HobbyExists(int id)
        {
            return _context.Hobbies.Any(e => e.Id == id);
        }
    }
}
    


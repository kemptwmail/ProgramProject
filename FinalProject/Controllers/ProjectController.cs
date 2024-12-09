using Microsoft.AspNetCore.Mvc;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor that initializes the context
        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Project (Returns the first 5 projects if no id is provided)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(int? id = 0)
        {
            if (id == null || id == 0)
            {
                return await _context.Projects.Take(5).ToListAsync();
            }

            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return new List<Project> { project };
        }

        // GET: api/Project/5 (Get a single project by id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/Project (Create a new project)
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            project.Id = 0;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Return the newly created project with its location
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT: api/Project/5 (Update an existing project)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // DELETE: api/Project/5 (Delete a project by id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a project exists by id
        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}

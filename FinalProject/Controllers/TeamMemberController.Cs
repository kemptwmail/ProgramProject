using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamMemberController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TeamMember
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers(int? id = 0)
        {
            if (id == null || id == 0)
            
                return await _context.TeamMembers.Take(5).ToListAsync();
            

            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            
                return NotFound();
            

            return new List<TeamMember> { teamMember };
        }

        // POST: api/TeamMember
        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            teamMember.Id = 0;
            _context.TeamMembers.Add(teamMember);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeamMembers), new { id = teamMember.Id }, teamMember);
        }

        // PUT: api/TeamMember/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            if (id != teamMember.Id)
                return BadRequest();

            _context.Entry(teamMember).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TeamMember/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
                return NotFound();

            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

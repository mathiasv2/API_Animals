using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalWorkshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : Controller
    {
        private readonly BookingAnimalsDbContext Db;

        public SpeciesController(BookingAnimalsDbContext db)
        {
            Db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Species>>> GetSpecies()
        {
            var species = await Db.Species.ToListAsync();
            return species;
        }

        [HttpPost]
        public async Task<ActionResult<Species>> CreateSpecies(Species specie)
        {
            Species species = new Species
            {
                name = specie.name
            };

            Db.Species.Add(species);
            await Db.SaveChangesAsync();

            return CreatedAtAction("CreateSpecies", new { specie.id }, specie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSpecies(int id, Species species)
        {

            if (id != species.id)
            {
                return BadRequest();
            }

            Db.Entry(species).State = EntityState.Modified;

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecies(int id)
        {
            var species = await Db.Species.FindAsync(id);
            if (species == null)
                return NotFound();

            Db.Species.Remove(species);
            await Db.SaveChangesAsync();

            return NoContent();
        }
    }
}


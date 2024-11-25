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
    public class AnimalsController : Controller
    {
        static HttpClient client = new HttpClient();
        private readonly BookingAnimalsDbContext Db;

        public AnimalsController(BookingAnimalsDbContext db)
        {
            Db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animals>>> GetAnimals()
        {
            var animals = await Db.Animals.Include(x => x.Species).ToListAsync();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<Animals> GetAnimalsById(int id)
        {
            var animal = await Db.Animals.Where(x => x.id == id).Include(x => x.Species).FirstOrDefaultAsync();


            if (animal == null)
                return null;

            return animal;

        }

        [HttpGet("{name}")]
        public async Task<Animals> GetAnimalsByName(string name)
        {
            var animal = await Db.Animals.Where(x => x.name == name).Include(x => x.Species).FirstOrDefaultAsync();

            if (animal == null)
                return null;

            return animal;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnimal(int id, Animals animal)
        {

            if (id != animal.id)
            {
                return BadRequest();
            }

            Db.Entry(animal).State = EntityState.Modified;

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


        [HttpPost]
        public async Task<ActionResult<Animals>> CreateAnimals(Animals animals)
        {
            Animals animal = new Animals
            {
                id = animals.id,
                name = animals.name,
                SpeciesId = animals.SpeciesId
                
            };

            Db.Animals.Add(animal);
            await Db.SaveChangesAsync();

            return CreatedAtAction("CreateAnimals", new { animals.id }, animals);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await Db.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            Db.Animals.Remove(animal);
            await Db.SaveChangesAsync();

            return NoContent();
        }
    }
}


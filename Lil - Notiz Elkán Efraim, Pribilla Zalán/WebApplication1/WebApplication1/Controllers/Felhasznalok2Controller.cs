using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommonLibrary;
using WebApplication1.CONTEXT;
using System.Xml.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Felhasznalok2Controller : ControllerBase
    {
        private readonly BejelentkezesDbContext _context;

        public Felhasznalok2Controller(BejelentkezesDbContext context)
        {
            _context = context;
        }

        // GET: api/Felhasznalok2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Felhasznalok2>>> Getfelhasznalok2()
        {
            return await _context.felhasznalok2.ToListAsync();
        }

        // GET: api/Felhasznalok2/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Felhasznalok2>> GetFelhasznalok2(int id)
        {
            var felhasznalok2 = await _context.felhasznalok2.FindAsync(id);

            if (felhasznalok2 == null)
            {
                return NotFound();
            }

            return felhasznalok2;
        }

        // PUT: api/Felhasznalok2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFelhasznalok2(int id, Felhasznalok2 felhasznalok2)
        {
            if (id != felhasznalok2.ID)
            {
                return BadRequest();
            }

            _context.Entry(felhasznalok2).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Felhasznalok2Exists(id))
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

        // POST: api/Felhasznalok2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Felhasznalok2>> PostFelhasznalok2(Felhasznalok2 felhasznalok2)
        {
            _context.felhasznalok2.Add(felhasznalok2);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFelhasznalok2", new { id = felhasznalok2.ID }, felhasznalok2);
        }


        [HttpPut("notiz/{felhasznaloNev}")]
        public async Task<IActionResult> UpdateFelhasznaloNotizByNev(string felhasznaloNev, [FromBody] FelhasznaloNotizDto notizDto)
        {
            var felhasznalo = await _context.felhasznalok2
                .FirstOrDefaultAsync(f => f.felhasznaloNev == felhasznaloNev);

            if (felhasznalo == null)
            {
                return NotFound(new { message = "Felhasználó nem található" });
            }

            felhasznalo.FELHASZNALONOTIZID = notizDto.FELHASZNALONOTIZID;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        public class FelhasznaloNotizDto
        {
            public string FELHASZNALONOTIZID { get; set; }
        }


        // DELETE: api/Felhasznalok2/5
        [HttpPut("removePart/{felhasznaloNev}")]
        public async Task<IActionResult> RemoveNotizPart(string felhasznaloNev, [FromBody] string partToRemove)
        {
            var felhasznalo = await _context.felhasznalok2.FirstOrDefaultAsync(f => f.felhasznaloNev == felhasznaloNev);

            if (felhasznalo == null || string.IsNullOrEmpty(felhasznalo.FELHASZNALONOTIZID))
            {
                return NotFound("Felhasználó nem található vagy nincs NotizID.");
            }

            if (string.IsNullOrEmpty(partToRemove) || partToRemove.Length != 5)
            {
                return BadRequest("A törlendő rész pontosan 5 karakter hosszú kell legyen.");
            }

            // Az ID-t 5 karakteres blokkokra osztjuk
            var blokkok = new List<string>();
            for (int i = 0; i <= felhasznalo.FELHASZNALONOTIZID.Length - 5; i += 5)
            {
                blokkok.Add(felhasznalo.FELHASZNALONOTIZID.Substring(i, 5));
            }

            // Ha a törlendő rész szerepel a blokkok között, eltávolítjuk
            if (blokkok.Remove(partToRemove))
            {
                felhasznalo.FELHASZNALONOTIZID = string.Join("", blokkok);
                await _context.SaveChangesAsync();
                return Ok(new { message = "NotizID frissítve.", updatedNotiz = felhasznalo.FELHASZNALONOTIZID });
            }

            return BadRequest("A megadott NotizID rész nem található.");
        }

        private bool Felhasznalok2Exists(int id)
        {
            return _context.felhasznalok2.Any(e => e.ID == id);
        }
    }
}

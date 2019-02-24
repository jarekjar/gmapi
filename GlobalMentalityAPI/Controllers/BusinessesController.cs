using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalMentalityAPI.Data;
using GlobalMentalityAPI.Models;

namespace GlobalMentalityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly BusinessContext _context;

        public BusinessesController(BusinessContext context)
        {
            _context = context;
        }

        // GET: api/Businesses
        [HttpGet]
        public IEnumerable<Business> GetBusinesses()
        {
            return _context.Businesses.Include(x => x.Patients).Include(x => x.OfficeAdmins).Include(x => x.Doctors);
        }

        // GET: api/Businesses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusiness([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var business = await _context.Businesses.FindAsync(id);

            if (business == null)
            {
                return NotFound();
            }

            return Ok(business);
        }

        // PUT: api/Businesses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusiness([FromRoute] int id, [FromBody] Business business)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != business.ID)
            {
                return BadRequest();
            }

            _context.Entry(business).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessExists(id))
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

        // POST: api/Businesses
        [HttpPost]
        public async Task<IActionResult> PostBusiness([FromBody] Business business)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusiness", new { id = business.ID }, business);
        }

        // DELETE: api/Businesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var business = await _context.Businesses.FindAsync(id);
            if (business == null)
            {
                return NotFound();
            }

            _context.Businesses.Remove(business);
            await _context.SaveChangesAsync();

            return Ok(business);
        }

        private bool BusinessExists(int id)
        {
            return _context.Businesses.Any(e => e.ID == id);
        }
    }
}
﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vou.API.Data;
using Vou.API.Helper;
using Vou.Shared.Entities;
using Vou.Shared.Pagination;

namespace Vou.API.Controllers.Entities
{
    [Route("api/softplan")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    public class SoftPlansController : ControllerBase
    {
        private readonly DataContext _context;

        public SoftPlansController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SoftPlans
        [HttpGet]
        public async Task<ActionResult<List<SoftPlan>>> GetSoftPlans([FromQuery] PaginationDTO pagination)
        {
            if (string.IsNullOrWhiteSpace(pagination.Filter))
            {
                var queryable = _context.SoftPlans.AsQueryable();

                await HttpContext.InsertParameterPaginationResponse(queryable, pagination.RecordsNumber);
                return Ok(await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync());
            }
            else 
            {
                var queryable = _context.SoftPlans.Where(x=> x.Name.ToLower().Contains(pagination.Filter.ToLower())).AsQueryable();

                await HttpContext.InsertParameterPaginationResponse(queryable, pagination.RecordsNumber);
                return Ok(await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync());
            }
        }

        // GET: api/SoftPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoftPlan>> GetSoftPlan(int id)
        {
            var softPlan = await _context.SoftPlans.FindAsync(id);

            if (softPlan == null)
            {
                return NotFound();
            }

            return softPlan;
        }

        // PUT: api/SoftPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftPlan(int id, SoftPlan softPlan)
        {
            if (id != softPlan.SoftPlanId)
            {
                return BadRequest();
            }

            _context.Entry(softPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftPlanExists(id))
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

        // POST: api/SoftPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SoftPlan>> PostSoftPlan(SoftPlan softPlan)
        {
            _context.SoftPlans.Add(softPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSoftPlan", new { id = softPlan.SoftPlanId }, softPlan);
        }

        // DELETE: api/SoftPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoftPlan(int id)
        {
            var softPlan = await _context.SoftPlans.FindAsync(id);
            if (softPlan == null)
            {
                return NotFound();
            }

            _context.SoftPlans.Remove(softPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SoftPlanExists(int id)
        {
            return _context.SoftPlans.Any(e => e.SoftPlanId == id);
        }
    }
}

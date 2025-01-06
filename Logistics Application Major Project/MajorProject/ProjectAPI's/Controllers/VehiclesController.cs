using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_s.Data;
using ProjectAPI_s.Models;

namespace ProjectAPI_s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly MajorLogisticsDataBaseContext _context;

        public VehiclesController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutVehicle(int id, [FromBody] Vehicle vehicle)
        {
            Console.WriteLine(id + "," + vehicle.VehicleId);
            string result = "Error Occured while Updating :-";
            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                result = "Vehicle Updated Successfully";
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostVehicle(Vehicle vehicle)
        {
            string result = "Error Occured while Inserted:-";
            try
            {
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
                result = "Vechile Added Successfully";
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }
            return result;
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public string DeleteVehicle(int id)
        {

            string result = "Error Occured while Deleting";
            try
            {
                var vehicle = _context.Vehicles.Find(id);
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
                result = "Vehicle Deleted Successfully";
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;


        }
    }

}
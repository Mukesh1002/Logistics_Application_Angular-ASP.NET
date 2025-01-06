using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_s.Data;
using ProjectAPI_s.Models;

namespace ProjectAPI_s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly MajorLogisticsDataBaseContext _context;

        public ManagersController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Managers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetManagers()
        {
            return await _context.Managers.ToListAsync();
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);

            if (manager == null)
            {
                return NotFound();
            }

            return manager;
        }

        // PUT: api/Managers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public string PutManager(int id, [FromBody]Manager manager)
        {
            string result = "Error Occurred while Updating Item:";
            try
            {
                SqlParameter itemIdParam = new SqlParameter("@ItemId", id);
                SqlParameter itemNameParam = new SqlParameter("@ItemName", manager.ManagerName);
                SqlParameter itemImgParam = new SqlParameter("@ItemImg", manager.ManagerPassword);
                SqlParameter quantityParam = new SqlParameter("@Quantity", manager.ManagerPhonoNo);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC UpdateManager @ItemId,@ItemName, @ItemImg, @Quantity",
                    itemIdParam, itemNameParam, itemImgParam, quantityParam).Result;

                if (res > 0)
                {
                    result = "Successfully Updated";
                }

            }
            catch (Exception ex)
            {
                result += ex.Message;

            }

            return result;
        }

        // POST: api/Managers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public string PostManager(Manager manager)
        {
            string result = "Error occurred while adding manager:";
            try
            {
                SqlParameter nameParam = new SqlParameter("@name", manager.ManagerName);
                SqlParameter passwordParam = new SqlParameter("@password", manager.ManagerPassword);
                SqlParameter phoneParam =  new SqlParameter("@phone", manager.ManagerPhonoNo);
                var res = _context.Database.ExecuteSqlRawAsync("EXEC AddManager @name, @password, @phone",
                    nameParam, passwordParam, phoneParam).Result;

                if (res > 0)
                {
                    result = "Successfully Added";
                }
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }
        [HttpPost("AddAssign")]
        public string AddAssignment([FromBody] Assign assign)
        {
            string result = "Error occurred while adding assignment:";
            try
            {
                SqlParameter driverDetailsParam = new SqlParameter("@DriverDetails", assign.DriverDetails);
                SqlParameter orderIdParam = new SqlParameter("@OrderID", assign.OrderId);
                SqlParameter destinationParam = new SqlParameter("@Destination", assign.Destination);
                SqlParameter VehicleParam = new SqlParameter("@vehiclenum", assign.VehicleNum);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC AddAssignment @DriverDetails, @OrderID, @Destination,@vehiclenum",
                    driverDetailsParam, orderIdParam, destinationParam,VehicleParam).Result;

                if (res > 0)
                {
                    result = "Successfully Assigned";
                }
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }




        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public string DeleteManager(int id)
        {
            string result = "Error Occured while Deleting";
            try
            {
                var manager =  _context.Managers.Find(id);
                _context.Managers.Remove(manager);
                _context.SaveChangesAsync();
                result = "Manager Deleted Successfully";
            }
            catch(Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

        private bool ManagerExists(int id)
        {
            return _context.Managers.Any(e => e.ManagerId == id);
        }
    }
}

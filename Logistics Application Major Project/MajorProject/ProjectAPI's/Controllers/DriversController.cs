using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_s.Data;
using ProjectAPI_s.Models;

namespace ProjectAPI_s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly MajorLogisticsDataBaseContext _context;

        public DriversController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers.ToListAsync();
        }

        // GET: api/Drivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }

        // PUT: api/Drivers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public string PutDriver(int id, [FromBody] Driver driver)
        {
            string result = "Error Occurred while Updating Item:";
            try
            {
                SqlParameter itemIdParam = new SqlParameter("@ItemId", id);
                SqlParameter itemNameParam = new SqlParameter("@ItemName", driver.DriverName);
                SqlParameter itemImgParam = new SqlParameter("@ItemImg", driver.DriverPassword);

                SqlParameter quantityParam = new SqlParameter("@Quantity", driver.DriverPhoneNo);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC UpdateDriver @ItemId,@ItemName, @ItemImg, @Quantity",
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

        // POST: api/Drivers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public string PostDriver(Driver driver)
        {
            string result = "Error occurred while adding Driver:";
            try
            {
                SqlParameter nameParam = new SqlParameter("@name", driver.DriverName);
                SqlParameter passwordParam = new SqlParameter("@password", driver.DriverPassword);
                SqlParameter phoneParam = new SqlParameter("@phone", driver.DriverPhoneNo);
                var res = _context.Database.ExecuteSqlRawAsync("EXEC AddDriver @name, @password, @phone",
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

        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public string DeleteDriver(int id)
        {
            string result = "Error occurred while deleting driver:";
            Resource resObj = new Resource();
           
                SqlParameter itemIdParam = new SqlParameter("@ItemId", id);
                SqlParameter messageParam = new SqlParameter("@message", SqlDbType.VarChar, 200);
                messageParam.Direction = ParameterDirection.Output;

                var rec = _context.Resources.Where(rec => rec.DriverId == id);
                foreach (var item in rec)
                {
                    resObj.ResourceId = item.ResourceId;
                    resObj.DriverAvailabilityStatus = item.DriverAvailabilityStatus;
                    resObj.CurrentAssignment = item.CurrentAssignment;
                    resObj.VehicleAllocated = item.VehicleAllocated;
                }

                if (resObj.CurrentAssignment == null)
                {


                        try
                        {

                            var res = _context.Database.ExecuteSqlRawAsync("EXEC DeleteDriver @ItemId, @message OUTPUT",
                                itemIdParam, messageParam).Result;

                            string message = messageParam.Value.ToString();

                            if (message.StartsWith("Driver deleted successfully"))
                            {
                                result = "Driver deleted successfully";
                            }
                            else
                            {
                                result = message;
                            }
                        }

                        catch (Exception ex)
                        {
                            result += ex.Message;
                        }

                  }
            else
            {
                result = "Driver is Currently Delivering A order.Cant Be deleted";
            }

            return result;
        }


        [HttpGet("AssignedOrders/{id}")]

        public IEnumerable<Assign> getassignedorders(int id)
        {
            return _context.Assigns.Where(rec => rec.DriverDetails == id).ToList();
        }

        [HttpGet("OrderdetailsView/{id}")]
        public IEnumerable<OrderDetailsView> GetOrderDetails(int id)
        {
            return _context.OrderDetailsViews.Where(rec => rec.OrderId == id).ToList();
        }


        [HttpPost("UpdateStatus")]
        public string UpdateStatus([FromBody] ShipmentView item,[FromQuery] string status)
        {
            string result = "Error Occurred while Updating Item:";

            int Shipmentid = 0;
            try
            {
                var Shipment_id = from rec in _context.Shipments where rec.OrderDetailsId == item.OrderDetailsId && rec.OrderId == item.OrderId select rec.ShipmentId;

         
                foreach(var id in Shipment_id)
                {
                     Shipmentid = id;
                }

                SqlParameter shipmentIdParam = new SqlParameter("@Shipmentid", Shipmentid);
                SqlParameter msgParam = new SqlParameter("@msg", status);
                SqlParameter orderIdParam = new SqlParameter("@orderid", item.OrderId);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC StatusUpdation @Shipmentid, @msg, @orderid",
                    shipmentIdParam, msgParam, orderIdParam).Result;

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

        [HttpGet("GetAllDriverOrders/{id}")]

        public IEnumerable<Order> GetAllDriverOrders(int id)
        {
            // Join Assigns and Orders tables to get orders assigned to the driver
            var orders = from assign in _context.Assigns
                         join order in _context.Orders on assign.OrderId equals order.OrderId
                         where assign.DriverDetails == id
                         select new Order
                         {
                             OrderId = order.OrderId,
                             OrderDetails = order.OrderDetails,
                             OrderPlacedDate = order.OrderPlacedDate,
                             OrderStatus = order.OrderStatus
                             // Add more properties as needed
                         };

            return orders.ToList();
        }



    }



}

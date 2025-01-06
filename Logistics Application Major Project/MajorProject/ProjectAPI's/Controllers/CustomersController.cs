using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class CustomersController : ControllerBase
    {
        private readonly MajorLogisticsDataBaseContext _context;

        public CustomersController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public string PutCustomer(int id,[FromBody] Customer customer)
        {
            string result = "Error Occurred While Updating: ";

            try
            {
                // Retrieve the existing customer entity from the database
                var existingCustomer = _context.Customers.Find(id);

                if (existingCustomer != null)
                {
                    // Update the properties of the existing customer entity
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.CustomerAddress = customer.CustomerAddress;
                    existingCustomer.CustomerPhoneNo = customer.CustomerPhoneNo;
                    existingCustomer.CustomerPassword=customer.CustomerPassword;

                    // Mark the entity as modified
                    _context.Entry(existingCustomer).State = EntityState.Modified;

                    // Save changes to the database
                    _context.SaveChanges();

                    result = "Successfully Updated";
                }
                else
                {
                    result = "Customer not found";
                }
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

        [HttpPut("{id}/wallet")]
        public string PutCustomerWallet(int id, [FromBody] Customer customer)
        {
            string result = "Error Occurred While Updating: ";

            try
            {
                // Retrieve the existing customer entity from the database
                var existingCustomer = _context.Customers.Find(id);

                if (existingCustomer != null)
                {
                    // Update the properties of the existing customer entity
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.CustomerAddress = customer.CustomerAddress;
                    existingCustomer.CustomerPhoneNo = customer.CustomerPhoneNo;
                    existingCustomer.CustomerPassword = customer.CustomerPassword;
                    existingCustomer.CustomerWallet= customer.CustomerWallet;

                    // Mark the entity as modified
                    _context.Entry(existingCustomer).State = EntityState.Modified;

                    // Save changes to the database
                    _context.SaveChanges();

                    result = "Wallet Successfully Updated";
                }
                else
                {
                    result = "Wallet Not Updated";
                }
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }

            return result;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            try
            {
                var userNameExists = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == customer.CustomerName);
                if (userNameExists != null)
                {
                    return BadRequest(new { message = "Username already exists" });
                }

                var mobileNumberExists = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerPhoneNo == customer.CustomerPhoneNo);
                if (mobileNumberExists != null)
                {
                    return BadRequest(new { message = "This account is registered with the Mobile Number" });
                }

                var parameters = new SqlParameter[]
                {
            new SqlParameter("@CustomerName", customer.CustomerName),
            new SqlParameter("@CustomerPassword", customer.CustomerPassword),
            new SqlParameter("@CustomerPhoneNo", customer.CustomerPhoneNo),
            new SqlParameter("@CustomerAddress", customer.CustomerAddress),
            new SqlParameter("@CustomerWallet", customer.CustomerWallet)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC addcustomer @CustomerName, @CustomerPassword, @CustomerPhoneNo, @CustomerAddress, @CustomerWallet", parameters);

                return Ok(new { message = "Registered Successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred while registering the customer: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing the request" });
            }
        }





        [HttpGet("GetCustomerOrders/{id}")]
       // [Authorize]
        public IEnumerable<Order> getOders(int id) {
            return _context.Orders.Where(res=>res.CustomerId==id);
                }


        [HttpGet("GetAllOrders")]

        public IEnumerable<Order> getAllOders()
        {
            return _context.Orders.ToList();
        }



        [HttpGet("GetCustomerOrdersDetails/{id}")]

        public IEnumerable<OrderDetailsView> getOderDetails(int id)
        {
            return _context.OrderDetailsViews.Where(res => res.CustomerId == id);
        }



        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }


        [HttpPost("insertorder")]
        public int InsertOrder(Order orders)
        {
            int orderId = 0;
            try
            {
                SqlParameter orderIdParam = new SqlParameter("@OrderId", SqlDbType.Int);
                orderIdParam.Direction = ParameterDirection.Output;

                // Get today's date without time component
                DateTime today = DateTime.Today;
                SqlParameter orderPlacedDateParam = new SqlParameter("@OrderPlacedDate", today);

                SqlParameter customerIdParam = new SqlParameter("@Customer_Id", orders.CustomerId);
                SqlParameter destinationParam = new SqlParameter("@Destination", orders.Destination);

                SqlParameter billparam = new SqlParameter("@BillAmount", orders.TotalBillAmount); // Changed parameter name to @BillAmount

                var res = _context.Database.ExecuteSqlRawAsync("EXEC InsertOrder @Customer_Id, @Destination, @OrderPlacedDate, @BillAmount, @OrderId OUTPUT", // Added comma between @OrderPlacedDate and @BillAmount
                    customerIdParam, destinationParam, orderPlacedDateParam, billparam, orderIdParam).Result;

                if (res <= 0)
                {
                    throw new Exception("Failed to insert order");
                }

                orderId = Convert.ToInt32(orderIdParam.Value);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log the error)
            }

            return orderId;
        }

        [HttpGet("GetShipments")]

        public IEnumerable<Shipment> GetShipment()
        {
            return _context.Shipments.ToList();
        }

        [HttpPost("shipments")]
        public string InsertShipment([FromBody] Shipment shipment)
        {
            string result = "Error occurred while inserting shipment";
            try
            {
                DateTime today = DateTime.Today;

                Random random = new Random();
                int randomNumber = random.Next(3, 8);
                DateTime threeDaysFromNow = today.AddDays(randomNumber);

                SqlParameter orderIdParam = new SqlParameter("@OrderId", shipment.OrderId);
                SqlParameter orderetailsIdParam = new SqlParameter("@orderdetailsId", shipment.OrderDetailsId);
                SqlParameter destinationParam = new SqlParameter("@Destination", shipment.Destination);
                SqlParameter orderPlacedDateParam = new SqlParameter("@OrderPlacedDate", today);
                SqlParameter expectedDeliveryParam = new SqlParameter("@Expected_Delivery", threeDaysFromNow);

                var resShipment = _context.Database.ExecuteSqlRawAsync("EXEC InsertShipment @OrderId,@orderdetailsId, @Destination, @OrderPlacedDate, @Expected_Delivery",
                    orderIdParam, orderetailsIdParam, destinationParam, orderPlacedDateParam, expectedDeliveryParam).Result;

                if (resShipment <= 0)
                {
                    result = "Failed to insert shipment";
                }
                else
                {
                    result = "Shipment inserted successfully";
                }
            }
            catch (Exception ex)
            {
                result += ". " + ex.Message; 
            }
            return result;
        }


        [HttpPost("insertorderDetail")]
        public int InsertOrderDetail([FromBody] OrderDetail orderdetails)
        {
            string result = "Error Occurred while Placing OrderDetail :";
            int orderDetailId = 0;
            try
            {
                // Generate a random number between 3 and 8 for expected delivery days
                Random random = new Random();
                int randomNumber = random.Next(3, 8);
                DateTime today = DateTime.Today;
                DateTime expectedDeliveryDate = today.AddDays(randomNumber);

                SqlParameter orderIdParam = new SqlParameter("@OrderId", orderdetails.OrderId);
                SqlParameter itemIdParam = new SqlParameter("@Item_Id", orderdetails.ItemId);
                SqlParameter itemQuantityParam = new SqlParameter("@Item_Quantity", orderdetails.ItemQuantity);
                SqlParameter itemPriceParam = new SqlParameter("@ItemPrice", orderdetails.ItemPrice);
                SqlParameter expectedDeliveryParam = new SqlParameter("@expectedDelivery", expectedDeliveryDate);

                SqlParameter orderDetailIdParam = new SqlParameter("@OrderDetailId", SqlDbType.Int);
                orderDetailIdParam.Direction = ParameterDirection.Output;

                var res = _context.Database.ExecuteSqlRawAsync("EXEC InsertOrderDetail @OrderId, @Item_Id, @Item_Quantity, @ItemPrice, @expectedDelivery, @OrderDetailId OUTPUT",
                    orderIdParam, itemIdParam, itemQuantityParam, itemPriceParam, expectedDeliveryParam, orderDetailIdParam).Result;

                if (res <= 0)
                {
                    throw new Exception("Failed to place order");
                }

                 orderDetailId = Convert.ToInt32(orderDetailIdParam.Value);
                result = $"Order detail placed successfully with ID: {orderDetailId}";
            }
            catch (Exception ex)
            {
                result += ex.Message;
                // Log the exception for debugging purposes
            }
            return orderDetailId;
        }






    }
}

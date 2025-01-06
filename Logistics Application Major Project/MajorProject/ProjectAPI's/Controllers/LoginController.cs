using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_s.Data;
using ProjectAPI_s.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectAPI_s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        //private readonly MajorLogisticsDataBaseContext _context;

        //public LoginController(MajorLogisticsDataBaseContext context)
        //{
        //    _context = context;
        //}

        private readonly JwtService _jwtService;
        private readonly MajorLogisticsDataBaseContext _context;

        public LoginController(JwtService jwtService, MajorLogisticsDataBaseContext dbContext)
        {
            _jwtService = jwtService;
            _context = dbContext;
        }




        //[HttpPost("Admin")]
        //public int PostAdmin([FromBody] Credentials admin)
        //{
        //    int res = 0;
        //    var details= from item in _context.Admins
        //                 where item.AdminName== admin.UserName && item.AdminPassword== admin.UserPassword
        //                 select item.AdminId;
        //    foreach (var data in details)
        //    {
        //        res = data;
        //    }

        //    return res;
        //}
        [HttpPost("Admin")]
        public IActionResult LoginAdmin([FromBody] Credentials model)
        {
            var user = _context.Admins.FirstOrDefault(u => u.AdminName == model.UserName && u.AdminPassword == model.UserPassword);
            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateTokenAdmin(user);

            // Return both token and id in a single response
            return Ok(new { Token = token, AdminId = user.AdminId });
        }


        //[HttpPost("Customer")]
        //public int PostCustomer([FromBody] Credentials customer)
        //{
        //    int res = 0;
        //    var details = from item in _context.Customers
        //                  where item.CustomerName == customer.UserName && item.CustomerPassword == customer.UserPassword
        //                  select item.CustomerId;
        //    foreach (var data in details)
        //    {
        //        res = data;
        //    }

        //    return res;
        //}



        [HttpPost("Customer")]
        public IActionResult LoginCustomer([FromBody] Credentials model)
        {
            var user = _context.Customers.FirstOrDefault(u => u.CustomerName == model.UserName && u.CustomerPassword == model.UserPassword);
            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateTokenCustomer(user);

            // Return both token and id in a single response
            return Ok(new { Token = token, UserId = user.CustomerId });
        }



        //[HttpPost("Manager")]
        //public int PostManager([FromBody] Credentials manager)
        //{
        //    int res = 0;
        //    var details = from item in _context.Managers
        //                  where item.ManagerName == manager.UserName && item.ManagerPassword == manager.UserPassword
        //                  select item.ManagerId;


        //    foreach (var data in details)
        //    {
        //        res = data;
        //    }

        //    return res;
        //}

        [HttpPost("Manager")]
        public IActionResult LoginManager([FromBody] Credentials model)
        {
            var user = _context.Managers.FirstOrDefault(u => u.ManagerName == model.UserName && u.ManagerPassword == model.UserPassword);
            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateTokenManager(user);

            // Return both token and id in a single response
            return Ok(new { Token = token, ManagerId = user.ManagerId });
        }

        // DELETE api/<LoginController>/5
        //[HttpPost("Driver")]
        //public int PostDriver([FromBody] Credentials driver)
        //{
        //    int res = 0;
        //    var details = from item in _context.Drivers
        //                   where item.DriverName == driver.UserName && item.DriverPassword == driver.UserPassword
        //                   select item.DriverId;

        //    foreach (var data in details)
        //    {
        //        res = data;
        //    }

        //    return res;
        //}

        [HttpPost("Driver")]
        public IActionResult LoginDriver([FromBody] Credentials model)
        {
            var user = _context.Drivers.FirstOrDefault(u => u.DriverName == model.UserName && u.DriverPassword == model.UserPassword);
            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateTokenDriver(user);

            // Return both token and id in a single response
            return Ok(new { Token = token,DriverId = user.DriverId });
        }
    }
}

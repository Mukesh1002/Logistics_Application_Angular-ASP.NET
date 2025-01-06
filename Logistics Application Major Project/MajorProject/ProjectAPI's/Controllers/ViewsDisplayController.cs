using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI_s.Data;
using ProjectAPI_s.Models;

namespace ProjectAPI_s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsDisplayController : ControllerBase
    {
        private readonly MajorLogisticsDataBaseContext _context;

        public ViewsDisplayController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }



        [HttpGet("Driverdetailsview/{id}")]
        public IEnumerable<DriverDetailsView> GetDriverDetails(int id)
        {
            return _context.DriverDetailsViews.Where(rec=>rec.DriverId==id).ToList();
        }


        [HttpGet("OrderdetailsView")]
        public IEnumerable<OrderDetailsView> GetOrderDetails()
        {
            return _context.OrderDetailsViews.ToList();
        }

        [HttpGet("ResurcesView")]
        public IEnumerable<ResourcesView> GetResourcesDetails()
        {
            return _context.ResourcesViews.ToList();
        }

        [HttpGet("ShipmentsView")]
        public IEnumerable<ShipmentView> GetShipmentsDetails()
        {
            return _context.ShipmentViews.ToList();
        }

        [HttpGet("CartView")]
        public IEnumerable<CartView> GetCartDetails()
        {
            return _context.CartViews.ToList();
        }
    }
}

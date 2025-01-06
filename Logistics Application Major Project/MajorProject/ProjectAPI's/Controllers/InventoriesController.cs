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
    public class InventoriesController : ControllerBase
    {

        private readonly MajorLogisticsDataBaseContext _context;

        public InventoriesController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }


        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public string PutInventory(int id, [FromBody] Inventory item)
        {
            string result = "Error Occurred while Updating Item:";
            try
            {
                SqlParameter itemIdParam = new SqlParameter("@ItemId", id);
                SqlParameter itemNameParam = new SqlParameter("@ItemName", item.ItemName);
                SqlParameter itemImgParam = new SqlParameter("@ItemImg", item.ItemImg);
                int quantityValue = Convert.ToInt32(item.Quantity);
                SqlParameter quantityParam = new SqlParameter("@Quantity", quantityValue);
                SqlParameter priceParam = new SqlParameter("@Price", item.Price); // Add price parameter

                var res = _context.Database.ExecuteSqlRawAsync("EXEC UpdateInventory @ItemId, @ItemName, @ItemImg, @Quantity, @Price",
                    itemIdParam, itemNameParam, itemImgParam, quantityParam, priceParam).Result;

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


        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public string PostInventory([FromBody] Inventory item)
        {
            string result = "Error Occurred while Adding Item:";
            try
            {
                SqlParameter itemNameParam = new SqlParameter("@ItemName", item.ItemName);
                SqlParameter itemImgParam = new SqlParameter("@ItemImg", item.ItemImg);
                SqlParameter quantityParam = new SqlParameter("@Quantity", Convert.ToInt32(item.Quantity));
                SqlParameter itemPriceParam = new SqlParameter("@Price", item.Price);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC AddInventory @ItemName, @ItemImg, @Quantity, @Price",
                    itemNameParam, itemImgParam, quantityParam, itemPriceParam).Result;

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



        // DELETE: api/Inventories/5
        [HttpDelete("deleteItem/{id}")]
        public string DeleteInventory(int id)
        {
            string result = "Error occurred while deleting the item";

            try
            {

                SqlParameter itemIdParam = new SqlParameter("@ItemId", id);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC DeleteInventory @ItemId", itemIdParam).Result;

                if (res > 0)
                {
                    result = "Inventory item deleted successfully";
                }
            }
            catch (Exception e)
            {
                result += ". Failed to delete inventory item: " + e.Message;
            }

            return result;
        }


    }
}


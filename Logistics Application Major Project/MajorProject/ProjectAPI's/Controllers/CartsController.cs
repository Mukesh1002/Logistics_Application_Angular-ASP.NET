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
    public class CartsController : ControllerBase
    {
        private readonly MajorLogisticsDataBaseContext _context;

        public CartsController(MajorLogisticsDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostCart(Cart cart)
        {
            string result = "Error Occured while adding to cart";
            try
            {
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
                result = "Item Added To Cart Successfully";
            }
            catch (Exception ex)
            {
                result += ex.Message;
            }
            

           return result;
        }

        // DELETE: api/Carts/5
        [HttpDelete("deletecart/{id}")]
        public string DeleteCart(int id)
        {
            string result = "Error occurred while deleting the item";
            try
            {
                SqlParameter cartIdParam = new SqlParameter("@CartId", id);

                var res = _context.Database.ExecuteSqlRawAsync("EXEC DeleteCartItemById @CartId", cartIdParam).Result;

                if (res > 0)
                {
                    result = "Removed From Cart successfully";
                }
            }
            catch (Exception e)
            {
                result += ". Failed to Remove From cart: " + e.Message;
            }

            return result;
        }

        [HttpPut("updatecartquantity/{id}/{newQuantity}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int id, int newQuantity)
        {
            try
            {
                // Find the cart item by id
                var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);

                if (cartItem != null)
                {
                    // Update the quantity
                    cartItem.ItemQuantity = newQuantity;
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Cart item quantity updated successfully" });
                }
                else
                {
                    return NotFound(new { message = "Cart item not found" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "Error occurred while updating cart item quantity: " + e.Message });
            }
        }




        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}

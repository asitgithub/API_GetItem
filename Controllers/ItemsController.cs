using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoryAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using CategoryAPI.Filter;

namespace CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly CategoryDbContext _context;

        public ItemsController(CategoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Items/
        [HttpGet]
        public async Task<ActionResult> GetItem([FromQuery] PaginationFilter filter)
        {
            if (ModelState.IsValid)
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
                var pageddata = await _context.Item
                               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                               .Take(validFilter.PageSize)
                               .ToListAsync();

                var data = (from item in _context.Item
                            join subCategory in _context.SubCategory on item.SubCategoryId equals subCategory.Id
                            join category in _context.Category on subCategory.CategoryId equals category.Id
                            select new
                            {
                                CategoryName = category.Name,
                                SubCategoryName = subCategory.Name,
                                ItemName = item.Name,
                                ItemDescription = item.Description
                            }).ToListAsync();

                return Ok(new PagedResponse<List<Item>>(pageddata, validFilter.PageNumber, validFilter.PageSize));
            }
            return BadRequest("Something went wrong");
        }

        // GET: api/Items/byname?name=curd
        [HttpGet("byname")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemByName([FromQuery] QueryParameters queryParameters,
            [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (ModelState.IsValid)
            {
                var data = (from item in _context.Item
                            join subCategory in _context.SubCategory on item.SubCategoryId equals subCategory.Id
                            join category in _context.Category on subCategory.CategoryId equals category.Id
                            where item.Name == queryParameters.name
                            select new
                            {
                                CategoryName = category.Name,
                                SubCategoryName = subCategory.Name,
                                ItemName = item.Name,
                                ItemDescription = item.Description
                            }).ToListAsync();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    var response = await data;
                    return Ok(response);
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }

    public class QueryParameters
    {
        [BindRequired]
        [StringLength(13, ErrorMessage = "Item name length should be between 3 to 13", MinimumLength = 3)]
        public string name { get; set; }
    }


}

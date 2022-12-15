using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem2022.Middleware;
using ReservationSystem2022.Models;
using ReservationSystem2022.Services;

namespace ReservationSystem2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ReservationContext _context;
        private readonly IItemService _service;
        private readonly IUserAuthenticationService _authenticationService;

        public ItemsController(ReservationContext context, IItemService service, IUserAuthenticationService authenticationService)
        {
            _context = context;
            _service = service;
            _authenticationService = authenticationService;
        }

        // GET: api/Items
        /// <summary>
        /// Get all items from database
        /// </summary>
        /// <returns>list of items</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems() // haetaan tuotteet
        {
            return Ok(await _service.GetItemsAsync());
        }

        // GET: api/Items/user/username
        /// Get all items from database
        [HttpGet("user/{username}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems(String username) //haetaan tuotteet tietyltä käyttäjältä
        {
            return Ok(await _service.GetItemsAsync(username));
        }

        // GET: api/Items/query
        /// <summary>
        /// Get all items from database matching given query
        /// </summary>
        /// <returns>list of items</returns>
        [HttpGet("{query}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> QueryItems(String query) //suoritetaan kysely
        {
            return Ok(await _service.QueryItemsAsync(query));
        }

        // GET: api/Items/5
        /// <summary>
        /// Get a single item
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Data for single item</returns>
        /// <response code="200">Returns the item</response>
        /// <response code="404">Item not found from database</response>

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemDTO>> GetItem(long id) // haetaan tuote ID:n perusteella
        {
            var item = await _service.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutItem(long id, ItemDTO item) // muokataan tuotetta ID:n perusteella
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            //Tarkista, onko oikeus muokata
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (!isAllowed)
            {
                return Unauthorized();
            }

            ItemDTO updatedItem = await _service.UpdateItemAsync(item);

            if (updatedItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            //Tarkista, onko oikeus muokata
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (!isAllowed)
            {
                return Unauthorized();
            }

            ItemDTO newItem = await _service.CreateItemAsync(item);
            if (newItem == null)
            {
                return Problem();
            }

            return CreatedAtAction("GetItem", new { id = newItem.Id }, newItem);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteItem(long id)
        {
            //Tarkista, onko oikeus muokata
            ItemDTO item = new ItemDTO();
            item.Id = id;
            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, item);

            if (!isAllowed)
            {
                return Unauthorized();
            }

            if (await _service.DeleteItemAsync(id))
            {
                return Ok();
            }
            return NotFound();
            }   

        }
    }


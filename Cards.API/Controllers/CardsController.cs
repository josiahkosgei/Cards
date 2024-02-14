using AutoMapper;
using Azure;
using Cards.Core.Models;
using Cards.Core.Services.Interfaces;
using Cards.Data.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cards.API.Controllers
{
    /// <summary>
    /// Exposes RESTFul Endpoints for Card Operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    // Requires Authorization for JWT Bearer Authentication Scheme
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardsController : ControllerBase
    {
        protected readonly ILogger<CardsController> _logger;
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardsController(ILogger<CardsController> logger, ICardService cardService, IMapper mapper)
        {
            _logger = logger;
            _cardService = cardService;
            _mapper = mapper;
        }

        /// <summary>
        /// Filters include name, color, status and date of creation <br></br>
        /// Optionally limit results using <paramref name="page"/> and <paramref name="size"/>  <br></br>
        /// Results may be sorted by name, color, status, date of creation <br></br>
        /// </summary>
        /// <param name="name"> Optionally filter by Name</param>
        /// <param name="color">or filter by Color</param>
        /// <param name="status">or filter by Status</param>
        /// <param name="createdDate">or filter by CreatedDate</param>
        /// <param name="sortBy">Optionally sort by Name,Color,Status or CreatedDate </param>
        /// <param name="orderBy"> and order by DESC/ASC</param>
        /// <param name="page">limit results using page</param>
        /// <param name="size"> and size options</param>
        /// <returns></returns>


        [HttpGet]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCards([FromQuery] string? name,[FromQuery] string? color,[FromQuery] CardStatus? status,[FromQuery] DateTime? createdDate,
                                                  [FromQuery] string? sortBy,[FromQuery] string? orderBy,
                                                  [FromQuery] int? page,[FromQuery] int? size)
        {
            try
            {
                var response = await _cardService.GetAllAsync(name, color, status, createdDate, sortBy, orderBy,page,size);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new CardDto { Message = ex.Message };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// Get a single card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCard(Guid id)
        {
            try
            {
                var response = await _cardService.GetAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new CardDto { Message = ex.Message };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// Create a card
        /// </summary>
        /// <param name="cardDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCard(CreateCardDto cardDto)
        {
            try
            {
                var response = await _cardService.AddAsync(cardDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new CardDto { Message = ex.Message };

                return BadRequest(response);
            }
        }
        /// <summary>
        /// Update a card
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedCardDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCard(Guid id, UpdateCardDto updatedCardDto)
        {
            try
            {
                var response = await _cardService.UpdateAsync(id, updatedCardDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new CardDto { Message = ex.Message };

                return BadRequest(response);
            }
        }

        /// <summary>
        /// Delete a card
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            try
            {
                var response = await _cardService.DeleteAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new CardDto { Message = ex.Message };

                return BadRequest(response);
            }
        }
    }
}

using AutoMapper;
using Azure;
using Cards.Core.Models;
using Cards.Core.Services.Interfaces;
using Cards.Data.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net;

namespace Cards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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


        // get all cards
        [HttpGet]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCards([FromQuery] string? name, [FromQuery] string? color, [FromQuery] CardStatus? status, [FromQuery] DateTime? createdDate)
        {
            try
            {
                var response = await _cardService.GetAllAsync(name, color, status, createdDate);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new CardDto { Message = ex.Message };

                return BadRequest(response);
            }
        }

        // Get a single card
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

        // Create a card
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
        // Update a card
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

        // Delete a card
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

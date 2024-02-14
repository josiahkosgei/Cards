using AutoMapper;
using Cards.Core.Enums;
using Cards.Core.Models;
using Cards.Core.Services.Interfaces;
using Cards.Data.Entities;
using Cards.Data.Enums;
using Cards.Data.IRepository;
using FluentValidation;

namespace Cards.Core.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IValidator<CreateCardDto> _createCardValidator;
        private readonly IValidator<UpdateCardDto> _updateCardValidator;
        private readonly IMapper _mapper;
        public CardService(ICardRepository cardRepository, IValidator<CreateCardDto> validator, IMapper mapper, IValidator<UpdateCardDto> updateCardValidator)
        {
            _cardRepository = cardRepository;
            _createCardValidator = validator;
            _mapper = mapper;
            _updateCardValidator = updateCardValidator;
        }

        public async Task<IList<CardDto>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate, string? sortBy, string? orderBy, int? page, int? size)
        {

            var entities = await _cardRepository.GetAllAsync(name, color, status, createdDate, sortBy, orderBy, page, size);
            var cardDto = _mapper.Map<List<CardDto>>(entities);
            return cardDto;
        }

        public async Task<CardDto> GetAsync(Guid id)
        {
            var entity = await _cardRepository.GetAsync(id);
            var cardDto = _mapper.Map<CardDto>(entity);
            return cardDto;
        }
        public async Task<CardDto> AddAsync(CreateCardDto cardDto)
        {
            var validatorResult = await _createCardValidator.ValidateAsync(cardDto);
            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new CardDto { Message = string.Join(", ", errors) };
            }


            var entity = _mapper.Map<Card>(cardDto);
            entity = await _cardRepository.AddAsync(entity);
            var createdCardDto = _mapper.Map<CardDto>(entity);
            return createdCardDto;
        }
        public async Task<CardDto> UpdateAsync(Guid id, UpdateCardDto cardDto)
        {
            var validatorResult = await _updateCardValidator.ValidateAsync(cardDto);
            if (!validatorResult.IsValid)
            {
                var errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new CardDto { Message = string.Join(", ", errors) };
            }
            var entity = _mapper.Map<Card>(cardDto);
            entity.Id = id;
            entity = await _cardRepository.UpdateAsync(id, entity);
            var updatedCardDto = _mapper.Map<CardDto>(entity);
            return updatedCardDto;
        }

        public async Task<CardDto> DeleteAsync(Guid id)
        {            
            var response= await _cardRepository.DeleteAsync(id);
            CardDto cardDto = new CardDto() { Message= response ?"Card Deleted": "Delete Failed" };
            return cardDto;
        }

    }
}

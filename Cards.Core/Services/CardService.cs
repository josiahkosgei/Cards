using AutoMapper;
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
        private readonly IValidator<CardDto> _validator;
        private readonly IMapper _mapper;
        public CardService(ICardRepository cardRepository, IValidator<CardDto> validator, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<IList<CardDto>> GetAllAsync(string? name, string? color, CardStatus? status, DateTime? createdDate)
        {

            var entities = await _cardRepository.GetAllAsync(name, color, status, createdDate);
            var cardDto = _mapper.Map<List<CardDto>>(entities);
            return cardDto;
        }

        public async Task<CardDto> GetAsync(Guid id)
        {
            var entity = await _cardRepository.GetAsync(id);
            var cardDto = _mapper.Map<CardDto>(entity);
            return cardDto;
        }
        public async Task<CardDto> AddAsync(CardDto cardDto)
        {
            var entity = _mapper.Map<Card>(cardDto);
            entity = await _cardRepository.AddAsync(entity);
            cardDto = _mapper.Map<CardDto>(entity);
            return cardDto;
        }
        public async Task<CardDto> UpdateAsync(Guid id, CardDto cardDto)
        {
            var entity = _mapper.Map<Card>(cardDto);
            entity.Id = id;
            entity = await _cardRepository.UpdateAsync(id, entity);
            cardDto = _mapper.Map<CardDto>(entity);
            return cardDto;
        }

        public async Task<bool> DeleteAsync(CardDto cardDto)
        {
            var entity = _mapper.Map<Card>(cardDto);
            return await _cardRepository.DeleteAsync(entity);
        }

    }
}

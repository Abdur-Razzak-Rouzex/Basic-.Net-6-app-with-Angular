using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var character = _mapper.Map<Character>(newCharacter);

            await _context.Characters.AddAsync(character);
            _context.SaveChanges();

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null)
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' is not found");
                }

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                _context.Characters.Update(character);
                _context.SaveChanges();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                serviceResponse.Message = $"Character with id {updatedCharacter.Id} is updated successfully";

            }
            catch (Exception exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exception.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> RemoveCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character is null)
                {
                    throw new Exception($"Character with id {id} is not found");
                }

                _context.Characters.Remove(character);
                _context.SaveChanges();
                serviceResponse.Message = $"Character with id {id} is removed successfully";
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }
    }
}
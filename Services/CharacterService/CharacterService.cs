using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using role_play_proj01.Data;
using role_play_proj01.Dtos.Character;
using role_play_proj01.Models;

namespace role_play_proj01.Services.CharacterService;

public class CharacterServices : ICharacterService
{

    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character {Id = 1, Name = "Sam"}
    };

    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CharacterServices(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        List<Character> dbCharacters = await _context.Characters.Where(c=>c.User.Id == getUserId()).ToListAsync(); 
        serviceResponse.Date = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
        Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == getUserId());
        serviceResponse.Date = _mapper.Map<GetCharacterDto>(dbCharacter);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        Character character = _mapper.Map<Character>(newCharacter);

        character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == getUserId());
        //character.User = await _context.Users.FirstOrDefaultAsync(c => c.Id == newCharacter.UserId);  //
        
        await _context.Characters.AddAsync(character);
        await _context.SaveChangesAsync();
       
        serviceResponse.Date = (_context.Characters.Where(c=>c.Id == getUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
        
        try
        {
            Character character = await _context.Characters.Include(c=>c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if (character.User.Id == getUserId())
            {
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;    

                _context.Characters.Update(character);
                await _context.SaveChangesAsync();

                serviceResponse.Date = _mapper.Map<GetCharacterDto>(character);
                
            }
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

        try
        {
            Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == getUserId());
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                serviceResponse.Date = _context.Characters.Where(c=>c.User.Id == getUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found";
            }
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }

        return serviceResponse;
    }

    private int getUserId() =>
        int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
}
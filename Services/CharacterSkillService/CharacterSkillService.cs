using System.Net.Mime;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using role_play_proj01.Data;
using role_play_proj01.Dtos.Character;
using role_play_proj01.Dtos.CharacterSkill;
using role_play_proj01.Models;
using role_play_proj01.Services.CharacterService;

namespace role_play_proj01.Services.CharacterSkillService;

public class CharacterSkillService : ICharacterSkillService
{

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    
    public CharacterSkillService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;  
    }
    
    public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
        try
        {
            Character character = await _context.Characters
                .Include(c=>c.Weapon)
                .Include(c=>c.CharacterSkills).ThenInclude(cs=>cs.Skill)
                .FirstOrDefaultAsync(c =>
                c.Id == newCharacterSkill.CharacterId && c.User.Id ==
                int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.skillId);

            if (skill == null)
            {
                response.Success = false;
                response.Message = "Skill not found";
                return response;
            }

            CharacterSkill characterSkill = new CharacterSkill
            {
                Character = character,
                Skill = skill
            };

            await _context.AddAsync(characterSkill);
            await _context.SaveChangesAsync();

            response.Date = _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }
}
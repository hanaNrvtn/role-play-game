using role_play_proj01.Dtos.Character;
using role_play_proj01.Dtos.CharacterSkill;
using role_play_proj01.Dtos.Skill;
using role_play_proj01.Models;

namespace role_play_proj01.Services.CharacterSkillService;

public interface ICharacterSkillService
{
    Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
}
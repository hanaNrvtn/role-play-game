using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using role_play_proj01.Dtos.CharacterSkill;
using role_play_proj01.Services.CharacterSkillService;

namespace role_play_proj01.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterSkillController : ControllerBase
{

    private readonly ICharacterSkillService _characterSkillService;

    public CharacterSkillController(ICharacterSkillService characterSkillService)
    {
        _characterSkillService = characterSkillService;
    }

    public async Task<ActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        return Ok(await _characterSkillService.AddCharacterSkill(newCharacterSkill));
    }
}
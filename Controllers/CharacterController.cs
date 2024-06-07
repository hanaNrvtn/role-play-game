using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using role_play_proj01.Dtos.Character;
using role_play_proj01.Models;
using role_play_proj01.Services.CharacterService;

namespace role_play_proj01.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    
    private readonly ICharacterService _characterService;
    //public ClaimsPrincipal User { get; }

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    
    private static Character knight = new();

    // [Route("GetAll")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        
        return Ok(await _characterService.GetAllCharacters());
    }

    /*
    // default 
    public IActionResult GetSingle()
    {
        return Ok(characters[0]);
    }
    */
    
    [HttpGet("{id}")]     // The parameter in the route has to match the parameter name in the function itself.
    public async Task<IActionResult> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }
    
    //[AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AddCharacter(AddCharacterDto character) 
    {
        return Ok(await _characterService.AddCharacter(character));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updatedCharacter);
        if (response.Date == null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);

        if (response.Date == null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}
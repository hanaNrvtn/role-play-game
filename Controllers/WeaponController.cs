using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using role_play_proj01.Dtos.Weapon;
using role_play_proj01.Services.WeaponService;

namespace role_play_proj01.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeaponController : ControllerBase
{
    private readonly IWeaponService _weaponService;

    public WeaponController(IWeaponService weaponService)
    {
        _weaponService = weaponService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon)
    {
        return Ok(await _weaponService.AddWeapon(newWeapon));
    }
}
using role_play_proj01.Dtos.Character;
using role_play_proj01.Dtos.Weapon;
using role_play_proj01.Models;

namespace role_play_proj01.Services.WeaponService;

public interface IWeaponService
{
    Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
}
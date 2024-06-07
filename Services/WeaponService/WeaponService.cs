using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using role_play_proj01.Data;
using role_play_proj01.Dtos.Character;
using role_play_proj01.Dtos.Weapon;
using role_play_proj01.Models;

namespace role_play_proj01.Services.WeaponService;

public class WeaponService : IWeaponService
{

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
        ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

        try
        {
            Character character = await _context.Characters.FirstOrDefaultAsync(c =>
                c.Id == newWeapon.CharacterId && c.User.Id ==
                int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

             // Weapon weapon = _mapper.Map<Weapon>(newWeapon);

            Weapon weapon = new Weapon
            {
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };

            await _context.weapons.AddAsync(weapon);
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
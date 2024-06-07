using AutoMapper;
using role_play_proj01.Dtos.Character;
using role_play_proj01.Dtos.Weapon;
using role_play_proj01.Models;

namespace role_play_proj01.Profiles;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>()
            .ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
        CreateMap<AddCharacterDto, Character>();
        CreateMap<Weapon, GetWeaponDto>();
        CreateMap<Skill, GetCharacterDto>();
    }
}
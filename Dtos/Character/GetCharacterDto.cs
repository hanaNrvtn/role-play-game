using role_play_proj01.Dtos.Skill;
using role_play_proj01.Dtos.Weapon;
using role_play_proj01.Models;

namespace role_play_proj01.Dtos.Character;

public class GetCharacterDto
{
    public int Id {get; set;  }
    public string Name {get; set; } = "Frodo";
    public int HitPoints {get; set; } = 100;
    public int Strength {get; set; } = 10;
    public int Defense {get; set; } = 10;
    public int Intelligence {get; set; } = 10;
    public RPGClass Class {get; set; } = RPGClass.Knight;
    public GetWeaponDto Weapon { get; set; }
    public List<GetSkillDto> Skills { get; set; }
}
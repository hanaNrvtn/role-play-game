namespace role_play_proj01.Models;

public class Weapon
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Damage {get; set; }
    public Character Character { get; set; }
    public int CharacterId { get; set; }
}
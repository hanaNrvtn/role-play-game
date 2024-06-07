using Microsoft.EntityFrameworkCore;
using role_play_proj01.Models;

namespace role_play_proj01.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set;  }
    public DbSet<Weapon> weapons { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CharacterSkill> CharacterSkills { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterSkill>().HasKey(cs => new {cs.CharacterId, cs.SkillId});
    }
        
        
        
        
        
}
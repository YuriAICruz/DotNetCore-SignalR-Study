using System.ComponentModel.DataAnnotations;

namespace WebServerStudy.Models
{
    public class Character
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public float ColorR { get; set; }
        [Required] public float ColorG { get; set; }
        [Required] public float ColorB { get; set; }
        [Required] public float ColorA { get; set; }

        public Character(int id, string name, float colorR, float colorG, float colorB, float colorA)
        {
            Id = id;
            Name = name;
            ColorR = colorR;
            ColorG = colorG;
            ColorB = colorB;
            ColorA = colorA;
        }
    }
}
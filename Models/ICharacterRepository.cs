using System.Collections.Generic;

namespace WebServerStudy.Models
{
    public interface ICharacterRepository
    {
        Character Get(int Id);
        IEnumerable<Character> GetAll();
        Character Create(Character character);
        Character Remove(Character character);
        Character Update(Character changes);
        bool Exists(int characterId);
    }
}
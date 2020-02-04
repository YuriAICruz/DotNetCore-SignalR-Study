using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebServerStudy.Models
{
    public class SqLiteCharacterRepository : ICharacterRepository
    {
        private readonly AppDbContext _context;

        public SqLiteCharacterRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public Character Get(int Id)
        {
            return _context.Characters.Find(Id);
        }

        public IEnumerable<Character> GetAll()
        {
            return _context.Characters;
        }

        public Character Create(Character character)
        {
            _context.Add(character);
            _context.SaveChanges();
            return character;
        }

        public Character Remove(Character character)
        {
            _context.Remove(character);
            _context.SaveChanges();
            return character;
        }

        public Character Update(Character changes)
        {
            var ch = _context.Attach(changes);
            ch.State = EntityState.Modified;
            _context.SaveChanges();
            return ch.Entity;
        }

        public bool Exists(int characterId)
        {
            return _context.Characters.Any(x=>x.Id == characterId);
        }
    }
}
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebServerStudy.Models
{
    public class SqlLitePlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public SqlLitePlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Player GetPlayer(int id)
        {
            return _context.Players.Find(id);
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players;
        }

        public Player Add(Player player)
        {
            _context.Add(player);
            _context.SaveChanges();
            return player;
        }

        public Player Remove(Player player)
        {
            _context.Remove(player);
            _context.SaveChanges();
            return player;
        }

        public Player Update(Player playerChanges)
        {
            var player = _context.Attach(playerChanges);
            player.State = EntityState.Modified;
            _context.SaveChanges();
            return playerChanges;
        }
    }
}
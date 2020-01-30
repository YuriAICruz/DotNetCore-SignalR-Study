using System.Collections.Generic;

namespace WebServerStudy.Models
{
    public class MockPlayerRepository : IPlayerRepository
    {
        public Player GetPlayer(int id)
        {
            return new Player()
            {
                Id = id,
                Name = "Player Mock"
            };
        }

        public IEnumerable<Player> GetPlayers()
        {
            throw new System.NotImplementedException();
        }

        public Player Add(Player player)
        {
            return player;
        }

        public Player Remove(Player player)
        {
            throw new System.NotImplementedException();
        }

        public Player Update(Player playerChanges)
        {
            throw new System.NotImplementedException();
        }
    }
}
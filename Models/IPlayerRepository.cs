using System.Collections.Generic;

namespace WebServerStudy.Models
{
    public interface IPlayerRepository
    {
        Player GetPlayer(int id);
        IEnumerable<Player> GetPlayers();
        Player Add(Player player);
        Player Remove(Player player);
        Player Update(Player playerChanges);
    }
}
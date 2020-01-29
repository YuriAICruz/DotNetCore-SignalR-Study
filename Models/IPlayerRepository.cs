namespace WebServerStudy.Models
{
    public interface IPlayerRepository
    {
        Player GetPlayer(int id);
    }
}
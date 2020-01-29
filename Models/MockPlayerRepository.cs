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
    }
}
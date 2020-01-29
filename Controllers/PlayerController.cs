using Microsoft.AspNetCore.Mvc;
using WebServerStudy.Models;

namespace WebServerStudy.Controllers
{
    public class PlayerController : Controller
    {
        private IPlayerRepository _repository;

        public PlayerController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index()
        {
            return View(_repository.GetPlayer(1));
        }
    }
}
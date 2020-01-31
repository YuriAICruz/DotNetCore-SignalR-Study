using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebServerStudy.Models;

namespace WebServerStudy.Controllers
{
    [Route("[controller]")]
    public class PlayerController : Controller
    {
        //Todo: check if id exists on post
        //Todo: 404 on not found id
        //Todo: check if exists on update
        private IPlayerRepository _repository;

        public PlayerController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        //[Route("{id}")]
        public ViewResult GetPlayerFront(int id)
        {
            return View("Index", _repository.GetPlayer(id));
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult GetPlayer(int id)
        {
            return Json(_repository.GetPlayer(id));
        }
        
        [HttpPost]
        //[Authorize(Roles = "RoleName")]
        [Authorize]
        public JsonResult SignUpPlayer([FromBody] Player player)
        {
            Console.WriteLine(player.Id);
            
            var p = _repository.Add(player);

            return Json(p);
        }
    }
}
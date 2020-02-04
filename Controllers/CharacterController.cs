using System;
using System.Diagnostics;
using System.Linq;
using Graphene.SharedModels.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerStudy.Models;

namespace WebServerStudy.Controllers
{
    [Route("Characters")]
    public class CharacterController : Controller
    {
        private readonly ICharacterRepository _repository;

        public CharacterController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(_repository.GetAll().Select(x=>new CharactersModelView(x)));
        }
        
        [HttpPost]
        [Route("add")]
        [Authorize]
        public JsonResult Add([FromBody] CharactersModelView character)
        {
            if (CheckColors(character.Color, HttpContext.Response, out var result))
            {
                return result;
            }

            var check = _repository.Exists(character.Id);
            if (check)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return Json(new ErrorMessage("already exists"));
            }
            var res = _repository.Create(new Character(
                character.Id,
                character.Name,
                character.Color[0],
                character.Color[1],
                character.Color[2],
                character.Color[3]
                ));
            
            return Json(new CharactersModelView(res));
        }

        [HttpPut]
        [Authorize]
        public JsonResult Modify([FromBody] CharactersModelView character)
        {
            if (CheckColors(character.Color, HttpContext.Response, out var result))
            {
                return result;
            }
            
            var check = _repository.Exists(character.Id);
            
            if (!check)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                return Json(new ErrorMessage("Not Found"));
            }

            var res = _repository.Update(new Character(
                character.Id,
                character.Name,
                character.Color[0],
                character.Color[1],
                character.Color[2],
                character.Color[3]
            ));
            
            return Json(new CharactersModelView(res));
        }

        private bool CheckColors(float[] color, HttpResponse responseStatusCode, out JsonResult result)
        {
            result = null;
            
            if (color.Length < 4)
            {
                responseStatusCode.StatusCode = StatusCodes.Status400BadRequest;
                result = Json(new ErrorMessage("color array size missmatch"));
                return true;
            }

            return false;
        }
    }
}
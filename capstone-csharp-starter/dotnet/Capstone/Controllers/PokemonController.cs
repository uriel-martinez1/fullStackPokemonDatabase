using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using PokemonDB.DAO;
using Microsoft.AspNetCore.Authorization;
using PokemonDB.Models;
using System.Collections.Generic;
using Capstone.Exceptions;

namespace Capstone.Controllers
{
    [Route("pokemon")]
    [ApiController]
    [Authorize]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonDao pokemonDao;
        private readonly IUserDao userDao;

        public PokemonController(IPokemonDao pokemonDao, IUserDao userDao)
        {
            this.pokemonDao = pokemonDao;
            this.userDao = userDao;
        }

        [HttpPost()]
        public ActionResult<PokemonDetail> SaveFavorite(PokemonDetail pokemonDetail)
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                PokemonDetail createdPokemon = pokemonDao.SaveFavorites(pokemonDetail, user.UserId);

                bool linkSuccess = pokemonDao.LinkPokemonToUser(createdPokemon.id, user.UserId);

                if (linkSuccess)
                {
                    return Created($"/pokemon/{createdPokemon.id}", createdPokemon);
                }
                else
                {
                    return BadRequest("Failed to link Pokemon to User.");
                }
            }
            catch (DaoException e)
            {
                throw new ResponseStatusException(HttpContext.Response, "Pokemon already in the favorite list");
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet()]
        public ActionResult<List<PokemonDetail>> GetAllFavoritePokemon()
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                List<PokemonDetail> listOfFavorites = pokemonDao.GetAllFavorites(user.UserId);
                return Ok(listOfFavorites);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}

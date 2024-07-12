using PokemonDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonDB.DAO
{
    public interface IPokemonDao
    {
        // abstract method -- no bodies, just the header

        // CRUD!!

        // create
        PokemonDetail SaveFavorites(PokemonDetail detail, int userId);
        // read
        List<PokemonDetail> GetAllFavorites(int userId);
        PokemonDetail GetPokemonById(int pokemonId);
        bool LinkPokemonToUser(int pokemonId, int userId);
        PokemonDetail getFavoriteByName(string name);
    }
}

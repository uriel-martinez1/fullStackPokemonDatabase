using PokemonDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System.Text;

namespace PokemonDB.DAO
{
    public class PokemonSqlDao : IPokemonDao
    {
        private readonly string connectionString;
        
        public PokemonSqlDao(string connString)
        {
            this.connectionString = connString;
        }
        public List<PokemonDetail> GetAllFavorites(int userId)
        {
            List<PokemonDetail> list = new List<PokemonDetail>();

            string sql = "SELECT p.id, api_id, name, base_experience, height, weight, front_url, back_url " +
                    "FROM pokemon p JOIN users_pokemon up ON p.id = up.pokemon_id WHERE users_id = @users_id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@users_id", userId);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PokemonDetail detail = MapRowToPokemonDetail(reader);
                        list.Add(detail);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return list;
        }

        public PokemonDetail GetPokemonById(int pokemonId)
        {
            PokemonDetail pokemon = new PokemonDetail();

            string sql = "SELECT id, api_id, name, base_experience, height, weight, front_url, back_url FROM pokemon WHERE id = @pokemonId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pokemonId", pokemonId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        pokemon = MapRowToPokemonDetail(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return pokemon;
        }

        public PokemonDetail SaveFavorites(PokemonDetail detail, int userId)
        {
            PokemonDetail newPokemon = new PokemonDetail();

            string sql = "INSERT INTO pokemon(api_id, name, base_experience, height, weight, back_url, front_url) " +
                    "OUTPUT INSERTED.id " +
                    "VALUES(@api_id, @name, @base_experience, @height, @weight, @back_url, @front_url);";

            try
            {
                int newPokemonId;
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@api_id", detail.apiId);
                    cmd.Parameters.AddWithValue("@name", detail.species["name"]);
                    cmd.Parameters.AddWithValue("@base_experience", detail.baseExperience);
                    cmd.Parameters.AddWithValue("@height", detail.height);
                    cmd.Parameters.AddWithValue("@weight", detail.weight);
                    cmd.Parameters.AddWithValue("@back_url", detail.sprites["back_default"]);
                    cmd.Parameters.AddWithValue("@front_url", detail.sprites["front_default"]);
                    newPokemonId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                newPokemon = GetPokemonById(newPokemonId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception occured", ex);
            }
            return newPokemon;
        }

        public bool LinkPokemonToUser(int pokemonId, int userId)
        {
            string sql = "INSERT INTO users_pokemon(pokemon_id, users_id) VALUES (@pokemon_id, @users_id);";

            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pokemon_id", pokemonId);
                    cmd.Parameters.AddWithValue("@users_id", userId);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception has ocurred: ", ex);
            }
            return rowsAffected > 0;
        }

        private PokemonDetail MapRowToPokemonDetail(SqlDataReader reader)
        {
            PokemonDetail temp = new PokemonDetail();

            // map each database attribute to the PD attribute
            temp.apiId = Convert.ToInt32(reader["api_id"]);
            temp.baseExperience = Convert.ToInt32(reader["base_experience"]);
            temp.height = Convert.ToInt32(reader["height"]);
            temp.weight = Convert.ToInt32(reader["weight"]);
            temp.id = Convert.ToInt32(reader["id"]);
            string n = Convert.ToString(reader["name"]);
            Dictionary<string, string> species = new Dictionary<string, string>();
            species["name"] = n;
            temp.species = species;

            string back = Convert.ToString(reader["back_url"]);
            string front = Convert.ToString(reader["front_url"]);
            Dictionary<string, string> sprites = new Dictionary<string, string>();
            sprites["back_default"] = back;
            sprites["front_default"] = front;
            temp.sprites = sprites;
            return temp;
        }

        public PokemonDetail getFavoriteByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

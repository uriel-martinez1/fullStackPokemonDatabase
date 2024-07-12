using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonDB.Models
{
    public class PokemonDetail
    {
        public int apiId { get; set; }
        
        public int id { get; set; }
        public int baseExperience { get; set; }
        public int height { get; set; }
        public int weight { get; set; }

        public Dictionary<string, string> species { get; set; }

        public Dictionary<string, string> sprites { get; set; }

        public override string ToString()
        {
            string backDefault = sprites["back_default"];
            string frontDefault = sprites["front_default"];

            return $"name={species["name"]}  base experience={baseExperience} height={height} weight={weight}\n\tback picture={backDefault}\n\tfront picture={frontDefault}";
        }
    }
}

using SMPage.Models;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMPage.Services
{


    public static class CsvLoader
    {
        public static async Task<List<VersionCSV>> LoadVersions(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return csvData.Split('\n')
                         .Skip(1)
                         .Where(line => !string.IsNullOrEmpty(line))
                         .Select(line => line.Split(','))
                         .Select(tokens => new VersionCSV
                         {
                             Id = int.Parse(tokens[0]),
                             VersionGroupId = int.Parse(tokens[1]),
                             Identifier = tokens[2]
                         })
                         .ToList();
        }

        public static async Task<List<VersionNameCSV>> LoadVersionNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return csvData.Split('\n')
                         .Skip(1)
                         .Where(line => !string.IsNullOrEmpty(line))
                         .Select(line => line.Split(','))
                         .Select(tokens => new VersionNameCSV
                         {
                             VersionId = int.Parse(tokens[0]),
                             LocalLanguageId = int.Parse(tokens[1]),
                             Name = tokens[2]
                         })
                         .ToList();
        }

        public static async Task<List<PokemonSpeciesNameCSV>> LoadPokemonNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return csvData.Split('\n')
                       .Skip(1) // 헤더 줄 건너뛰기
                       .Where(line => !string.IsNullOrEmpty(line))

                       .Select(line => line.Split(','))
                       .Select(tokens => new PokemonSpeciesNameCSV
                       {
                           SpeciesId = int.Parse(tokens[0]),
                           LocalLanguageId = int.Parse(tokens[1]),
                           Name = tokens[2]
                       })
                       .ToList();
        }

        public static List<PokemonCSV> LoadPokemons(string path)
        {
            return File.ReadAllLines(path)
                       .Skip(1) // 헤더 줄 건너뛰기
                       .Select(line => line.Split(','))
                       .Select(tokens => new PokemonCSV
                       {
                           id = int.Parse(tokens[0]),
                           identifier = tokens[1],
                           SpeciesId = int.Parse(tokens[2]),
                           order = int.Parse(tokens[3]),
                           isDefault = bool.Parse(tokens[4])
                       })
                       .ToList();
        }

        public static List<PokemonMoveCSV> LoadPokemonMoves(string path)
        {
            return File.ReadAllLines(path)
                       .Skip(1) // 헤더 줄 건너뛰기
                       .Select(line => line.Split(','))
                       .Select(tokens => new PokemonMoveCSV
                       {
                           PokemonId = int.Parse(tokens[0]),
                           VersionGroupId = int.Parse(tokens[1]),
                           MoveId = int.Parse(tokens[2]),
                           PokemonMoveMethodId = int.Parse(tokens[3]),
                           Level = int.Parse(tokens[4]),
                           Order = int.TryParse(tokens[5], out var orderValue) ? orderValue : 0
                       })
                       .ToList();
        }

    }

}

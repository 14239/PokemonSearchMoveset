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
            return ParseCsvData(csvData, tokens => new VersionCSV
            {
                Id = int.Parse(tokens[0]),
                VersionGroupId = int.Parse(tokens[1]),
                Identifier = tokens[2]
            });
        }

        public static async Task<List<VersionGroupCSV>> LoadVersionGroups(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new VersionGroupCSV
            {
                Id = int.Parse(tokens[0]),
                Identifier = tokens[1],
                GenerationId = int.Parse(tokens[2]),
                Order = int.Parse(tokens[3]),
            });
        }

        public static async Task<List<VersionNameCSV>> LoadVersionNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new VersionNameCSV
            {
                VersionId = int.Parse(tokens[0]),
                LocalLanguageId = int.Parse(tokens[1]),
                Name = tokens[2]
            });
        }

        public static async Task<List<PokemonCSV>> LoadPokemons(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonCSV
            {
                Id = int.Parse(tokens[0]),
                identifier = tokens[1],
                SpeciesId = int.Parse(tokens[2]),
                order = string.IsNullOrWhiteSpace(tokens[6]) ? 0 : int.Parse(tokens[6]),
                isDefault = tokens[7] == "1"
            });
        }

        public static async Task<List<PokemonSpeciesNameCSV>> LoadPokemonNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonSpeciesNameCSV
            {
                SpeciesId = int.Parse(tokens[0]),
                LocalLanguageId = int.Parse(tokens[1]),
                Name = tokens[2]
            });
        }

        public static async Task<List<PokemonFormNameCSV>> LoadPokemonFormNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonFormNameCSV
            {
                PokemonFormId = int.Parse(tokens[0]),
                LocalLanguageId = int.Parse(tokens[1]),
                FormName = tokens[2]
            });
        }


        public static async Task<List<PokemonFormCSV>> LoadPokemonForms(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonFormCSV
            {
                Id = int.Parse(tokens[0]),
                PokemonFormId = int.TryParse(tokens[2], out var orderValue) ? orderValue : 0,
                PokemonId = int.Parse(tokens[3]),
                IsDefault = tokens[5] == "1",
                IsBattleOnly = tokens[6] == "1",
            });
        }



        public static async Task<List<PokemonMoveCSV>> LoadPokemonMoves(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonMoveCSV
            {
                PokemonId = int.Parse(tokens[0]),
                VersionGroupId = int.Parse(tokens[1]),
                MoveId = int.Parse(tokens[2]),
                PokemonMoveMethodId = int.Parse(tokens[3]),
                Level = int.Parse(tokens[4]),
                Order = int.TryParse(tokens[5], out var orderValue) ? orderValue : 0
            });
        }

        public static async Task<List<MoveCSV>> LoadMoves(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new MoveCSV
            {
                Id = int.Parse(tokens[0]),
                Identifier = tokens[1],
                GenerationId = int.Parse(tokens[2]),
                TypeId = int.Parse(tokens[3]),
                Power = string.IsNullOrEmpty(tokens[4]) ? (int?)null : int.Parse(tokens[4]),
                Pp = int.Parse(tokens[5]),
                Accuracy = string.IsNullOrEmpty(tokens[6]) ? (int?)null : int.Parse(tokens[6]),
                Priority = int.Parse(tokens[7]),
                TargetId = int.Parse(tokens[8]),
                DamageClassId = int.Parse(tokens[9]),
                EffectId = int.Parse(tokens[10]),
                EffectChance = string.IsNullOrEmpty(tokens[11]) ? (int?)null : int.Parse(tokens[11]),
                ContestTypeId = int.Parse(tokens[12]),
                ContestEffectId = int.Parse(tokens[13]),
                SuperContestEffectId = int.Parse(tokens[14])
            });
        }

        public static async Task<List<MoveNameCSV>> LoadMoveNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new MoveNameCSV
            {
                MoveId = int.Parse(tokens[0]),
                LocalLanguageId = int.Parse(tokens[1]),
                Name = tokens[2]
            });
        }


        public static async Task<List<PokemonTypeCSV>> LoadPokemonTypes(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonTypeCSV
            {
                PokemonId = int.Parse(tokens[0]),
                TypeId = int.Parse(tokens[1]),
                Slot = int.Parse(tokens[2]),

            });
        }

        public static async Task<List<PokemonTypePastCSV>> LoadPokemonTypesPast(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new PokemonTypePastCSV
            {
                PokemonId = int.Parse(tokens[0]),
                GenerationId = int.Parse(tokens[1]),
                TypeId = int.Parse(tokens[2]),
                Slot = int.Parse(tokens[3]),
            });
        }

        public static async Task<List<TypeNameCSV>> LoadTypeNames(HttpClient http, string path)
        {
            var csvData = await http.GetStringAsync(path);
            return ParseCsvData(csvData, tokens => new TypeNameCSV
            {
                TypeId = int.Parse(tokens[0]),
                LocalLanguageId = int.Parse(tokens[1]),
                Name = tokens[2],
            });
        }



        private static List<T> ParseCsvData<T>(string csvData, Func<string[], T> parseFunc)
        {
            return csvData.Split('\n')
                         .Skip(1)
                         .Where(line => !string.IsNullOrEmpty(line))
                         .Select(line => line.Split(','))
                         .Select(parseFunc)
                         .ToList();
        }
    }


}

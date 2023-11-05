using PokemonSearchMoveset.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PokemonSearchMoveset.Services
{
	public class DataService
	{
		private readonly HttpClient _http;
        
        // 로딩 관련
		public int LoadedItemsCount { get; private set; } = 0;
        public int TotalItemsCount { get; private set; } = 17; // CSV 파일의 수 (예: 11)
        public int LoadingProgress => (int)((float)LoadedItemsCount / TotalItemsCount * 100);
        public event Action OnDataLoadProgress;



        // 전체
        public List<PokemonAbilityCSV> AllPokemonAbilities { get; set; } = new List<PokemonAbilityCSV>();
        public List<AbilityNameCSV> AbilityNames { get; set; } = new List<AbilityNameCSV>();

        public List<PokemonMoveCSV> AllPokemonMoves { get; set; } = new List<PokemonMoveCSV>();
		public List<PokemonCSV> Pokemons { get; set; } = new List<PokemonCSV>();
		public List<PokemonSpeciesNameCSV> PokemonNames { get; set; } = new List<PokemonSpeciesNameCSV>();
		public List<PokemonStatCSV> PokemonStats { get; set; } = new List<PokemonStatCSV>();
		public List<PokemonFormCSV> PokemonForms { get; set; } = new List<PokemonFormCSV>();
		public List<PokemonFormNameCSV> PokemonFormNames = new List<PokemonFormNameCSV>();
		public List<PokemonTypeCSV> PokemonTypes = new List<PokemonTypeCSV>();
		public List<PokemonTypePastCSV> PokemonTypesPast = new List<PokemonTypePastCSV>();
		public List<TypeNameCSV> TypeNames = new List<TypeNameCSV>();

        // 기술검색

        public List<VersionGroupCSV> VersionGroups = new List<VersionGroupCSV>();
        public List<VersionCSV> Versions = new List<VersionCSV>();
        public List<VersionNameCSV> VersionNames = new List<VersionNameCSV>();
        public List<MoveNameCSV> MoveNames = new List<MoveNameCSV>();
        public List<MoveCSV> Moves = new List<MoveCSV>();


		public DataService(HttpClient http)
		{
			_http = http;
		}
        public bool IsDataLoaded { get; private set; } = false;
        public async Task LoadAllDataAsync()
		{
            await Console.Out.WriteLineAsync("데이터 불러오기 시작");
            // Load Version Names

            AllPokemonAbilities = await CsvLoader.LoadPokemonAbilities(_http, "database/pokemon_abilities.csv") ?? new List<PokemonAbilityCSV>();
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            AbilityNames = await CsvLoader.LoadAbilityNames(_http, "database/ability_names.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            Pokemons = await CsvLoader.LoadPokemons(http: _http, path: "database/pokemon.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

			PokemonStats = await CsvLoader.LoadPokemonStats(http: _http, path: "database/pokemon_stats.csv");
			LoadedItemsCount++;
			OnDataLoadProgress?.Invoke(); // 이벤트 발생

			PokemonNames = await CsvLoader.LoadPokemonNames(http: _http, path: "database/pokemon_species_names.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            PokemonForms = await CsvLoader.LoadPokemonForms(http: _http, path: "database/pokemon_forms.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            PokemonFormNames = await CsvLoader.LoadPokemonFormNames(http: _http, path: "database/pokemon_form_names.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            TypeNames = await CsvLoader.LoadTypeNames(http: _http, path: "database/type_names.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            PokemonTypes = await CsvLoader.LoadPokemonTypes(http: _http, path: "database/pokemon_types.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            PokemonTypesPast = await CsvLoader.LoadPokemonTypesPast(http: _http, path: "database/pokemon_types_past.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            Versions = await CsvLoader.LoadVersions(_http, "database/versions.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            VersionNames = await CsvLoader.LoadVersionNames(_http, "database/version_names.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            VersionGroups = await CsvLoader.LoadVersionGroups(_http, "database/version_groups.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            MoveNames = await CsvLoader.LoadMoveNames(_http, "database/move_names.csv");
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

			AllPokemonMoves = await CsvLoader.LoadPokemonMoves(_http, "database/pokemon_moves.csv") ?? new List<PokemonMoveCSV>();
			LoadedItemsCount++;
			OnDataLoadProgress?.Invoke(); // 이벤트 발생

            Versions = await CsvLoader.LoadVersions(_http, "database/versions.csv") ?? new List<VersionCSV>();
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생
            
            VersionNames = await CsvLoader.LoadVersionNames(_http, "database/version_names.csv") ?? new List<VersionNameCSV>();
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            VersionGroups = await CsvLoader.LoadVersionGroups(_http, "database/version_groups.csv") ?? new List<VersionGroupCSV>();
            LoadedItemsCount++;
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

			Moves = await CsvLoader.LoadMoves(_http, "database/moves.csv") ?? new List<MoveCSV>();
			LoadedItemsCount++;
			OnDataLoadProgress?.Invoke(); // 이벤트 발생




			await Console.Out.WriteLineAsync("데이터 불러오기 종료");
            OnDataLoadProgress?.Invoke(); // 이벤트 발생

            IsDataLoaded = true;  // 데이터 로딩 완료 후 true로 설정

        }
    }
}

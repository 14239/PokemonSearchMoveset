namespace SMPage.Models
{
    // 언어, 국가 세팅

    public class VersionCSV // 개별 버전 (versions.csv)
    {
        public int Id { get; set; }
        public int VersionGroupId { get; set; }
        public string Identifier { get; set; }
    }
    public class VersionGroupCSV // 같은 버전끼리 묶어서 셈(ex 레드,그린) (version_groups.csv)
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public int GenerationId { get; set; }
        public int Order { get; set; }
    }

    public class VersionNameCSV // (version_names.csv)
    {
        public int VersionId { get; set; }
        public int LocalLanguageId { get; set; }
        public string Name { get; set; }
    }



    // 포켓몬 세팅 
    public class PokemonCSV // (pokemon.csv)
    {
        //id,identifier,species_id,height,weight,base_experience,order,is_default

        public int id { get; set; }                 // 데이터 ID. 폼 체인지는 10000부터 시작 
        public string identifier { get; set; }      // 영문 명칭

        public int SpeciesId { get; set; }

        public int order { get; set;}               // 모든 폼 포함 순서. 검색할 때 보여줄 순서임

        public bool isDefault { get; set; }         // 기본 폼인지 여부
        

    }

    public class PokemonSpeciesNameCSV // (pokemon_species_names.csv)
    {
        public int SpeciesId { get; set; }                 // 포켓몬 종족 번호
        public int LocalLanguageId { get; set; }    // 언어 ID. 1:일본어 3:한국어 9:영어
        public string Name { get; set; }            // 언어에 따른 포켓몬 이름

    }

    public class PokemonMoveCSV // (pokemon_moves.csv)
    {
        public int PokemonId { get; set; }
        public int VersionGroupId { get; set; }
        public int MoveId { get; set; }
        public int PokemonMoveMethodId { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
    }

}

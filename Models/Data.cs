namespace PokemonSearchMoveset.Models
{
    // 언어, 국가 세팅

    public class VersionCSV // 개별 버전 (versions.csv)
    {
        public int Id { get; set; }
        public int VersionGroupId { get; set; }
        public string? Identifier { get; set; }
    }
    public class VersionGroupCSV // 같은 버전끼리 묶어서 셈(ex 레드,그린) (version_groups.csv)
    {
        public int Id { get; set; }
        public string Identifier { get; set; } = "";
        public int GenerationId { get; set; }
        public int Order { get; set; }
    }

    public class VersionNameCSV // (version_names.csv)
    {
        public int VersionId { get; set; }
        public int LocalLanguageId { get; set; }
        public string Name { get; set; } = "";
    }



    // 포켓몬 세팅 
    public class PokemonCSV // (pokemon.csv)
    {
        //id,identifier,species_id,height,weight,base_experience,order,is_default

        public int Id { get; set; }                 // 데이터 ID. 폼 체인지는 10000부터 시작 
        public string identifier { get; set; } = "";      // 영문 명칭

        public int SpeciesId { get; set; }

        public int order { get; set;}               // 모든 폼 포함 순서. 검색할 때 보여줄 순서임

        public bool isDefault { get; set; }         // 기본 폼인지 여부
        

    }

    public class PokemonSpeciesNameCSV // (pokemon_species_names.csv)
    {
        public int SpeciesId { get; set; }                 // 포켓몬 종족 번호
        public int LocalLanguageId { get; set; }    // 언어 ID. 1:일본어 3:한국어 9:영어
        public string? Name { get; set; }            // 언어에 따른 포켓몬 이름

    }

    public class PokemonStatCSV
    {
        public int PokemonId { get; set;}
        public int StatId { get; set;}
        public int BaseStat { get; set; }
        public int StatEffort { get; set; }
	}


    public class PokemonFormNameCSV // (pokemon_species_names.csv)
    {
        public int PokemonFormId { get; set; }                 // 폼 번호 (안농ABC)
        public int LocalLanguageId { get; set; }    // 언어 ID. 1:일본어 3:한국어 9:영어
        public string? FormName { get; set; }            // 언어에 따른 포켓몬 이름

    }

    public class PokemonFormCSV // (pokemon_species_names.csv)
    {
        // id,identifier,form_identifier,pokemon_id,introduced_in_version_group_id,is_default,is_battle_only,is_mega,form_order,order

        public int Id { get; set; }                 // 포켓몬 폼 번호 (abc 포함이라 10440 최대)
        public int PokemonFormId { get; set; }                // 폼 번호 (안농ABC)
        public int PokemonId { get; set; }    // 포켓몬 종족 번호 (abc 제외로 10271 최대)
        public bool IsDefault { get; set; } // 기본 폼인지 여부
        public bool IsBattleOnly { get; set; } // 전투에서만 쓰이는 폼인지(그렇다면 야생에서 볼 일 없다)


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


    // 기술세팅

    public class MoveCSV
    {
        public int Id { get; set; }
        public string? Identifier { get; set; }
        public int GenerationId { get; set; }
        public int TypeId { get; set; }
        public int? Power { get; set; } // nullable because some moves might not have power
        public  int? Pp { get; set; }
        public int? Accuracy { get; set; }
        public int Priority { get; set; }
        public int TargetId { get; set; }
        public int DamageClassId { get; set; }
        public int EffectId { get; set; }
        public int? EffectChance { get; set; } // nullable because some moves might not have effect chance
        public int ContestTypeId { get; set; }
        public int ContestEffectId { get; set; }
        public int SuperContestEffectId { get; set; }
    }

    public class MoveNameCSV
    {
        public int MoveId { get; set; }
        public int LocalLanguageId { get; set; }
        public string Name { get; set; } = "";
    }

    // 타입 세팅


    public class PokemonTypeCSV    // ABC제외 
    {
        public int PokemonId { get; set; }
        public int TypeId { get; set; }
        public int Slot { get; set; }
    }

    public class PokemonTypePastCSV // 2세대 강철 4세대 로토무 6세대 페어리 타입
    {
        public int PokemonId { get; set; }
        public int GenerationId { get; set; }
        public int TypeId { get; set; }
        public int Slot { get; set; }
    }

    public class TypeNameCSV
    {
        public int TypeId { get; set; }
        public int LocalLanguageId { get; set; }
        public string? Name { get; set; }
    }


    // 특성 세팅

    public class AbilityCSV
    {
        public int Id { get; set; }
        public string? Identifier { get; set; }
        public int GenerationId { get; set; }
        public bool IsMainSeries { get; set; }
    }

    public class AbilityNameCSV
    {
        public int AbilityId { get; set; }
        public int LocalLanguageId { get; set; }
        public string? Name { get; set; }
    }

    public class PokemonAbilityCSV
    {
        public int PokemonId { get; set; }
        public int AbilityId { get; set; }
        public bool IsHidden { get; set; }
        public int Slot { get; set; }
    }
}

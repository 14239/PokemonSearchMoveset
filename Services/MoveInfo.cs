using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

public class MoveInfoService
{
	private readonly HttpClient _httpClient;

	public MoveInfoService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<MoveInfo>> LoadDangerousMovesAsync()
	{
		List<MoveInfo> dangerousMoves = new List<MoveInfo>();

		// 파일 내용을 문자열로 받아옵니다.
		var csvData = await _httpClient.GetStringAsync("psmdata/dangermoves.csv");

		using (var reader = new StringReader(csvData))
		{
			reader.ReadLine();
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				string[] parts = line.Split(',');
				MoveInfo moveInfo = new MoveInfo
				{
					Tier = int.Parse(parts[0]),
					TierName = parts[1],
					Category = parts[2],
					MoveName = parts[3],
					MoveId = int.Parse(parts[4])
				};
				dangerousMoves.Add(moveInfo);
			}
		}

		return dangerousMoves;
	}

	public async Task<List<AbilityInfo>> LoadDangerousAbilitiesAsync()
	{
		List<AbilityInfo> dangerousAbilitiess = new List<AbilityInfo>();

		// 파일 내용을 문자열로 받아옵니다.
		var csvData = await _httpClient.GetStringAsync("psmdata/dangerabilities.csv");

		using (var reader = new StringReader(csvData))
		{
			reader.ReadLine();
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				string[] parts = line.Split(',');
				AbilityInfo abilityInfo = new AbilityInfo
				{
					Category = parts[0],
					AbilityName = parts[1],
					AbilityId = int.Parse(parts[2]),
					ChangeMethod = parts[3]
				};
				dangerousAbilitiess.Add(abilityInfo);
			}
		}

		return dangerousAbilitiess;
	}
}

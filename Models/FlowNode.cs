using PokemonSearchMoveset.Pages;
using System.Xml.Linq;
using static PokemonSearchMoveset.Pages.Wild_Table;

public enum NodeType
{
    Start,//시작지점
    Decision,//두개로 갈라지는 곳
    Action,// 결과 혹은 그냥 직렬로 이어지는 노드
}

public class Child
{
	public FlowNode Node { get; set; }
	public Func<Wild_Simulation.WildPokemon, bool> Condition { get; set; }
	public string ArrowLabel { get; set; }

	public bool IsHighlightedLine { get; set; } = false;


	public Child(FlowNode node, Func<Wild_Simulation.WildPokemon, bool> condition, string arrowLabel)
	{
		Node = node;
		Condition = condition;
		ArrowLabel = arrowLabel;
	}
}

public class FlowNode
{
	public NodeType Type { get; set; }
	public List<Child> Children { get; set; } = new();
	public string? Content { get; set; }


	public int X { get; set; }
    public int Y { get; set; }

	public int GetAbsoluteX(List<FlowNode> allNodes)
	{
		var parent = allNodes.FirstOrDefault(node => node.Children.Any(child => child.Node == this));
		return parent != null ? X + parent.GetAbsoluteX(allNodes) : X;
	}

	public int GetAbsoluteY(List<FlowNode> allNodes)
	{
		var parent = allNodes.FirstOrDefault(node => node.Children.Any(child => child.Node == this));
		return parent != null ? Y + parent.GetAbsoluteY(allNodes) : Y;
	}

	public bool IsHighlighted { get; set; }

}



// decision에 사용할 데이터 구분
public class MoveInfo
{
    public int Tier { get; set; }
    public string TierName { get; set; }
    public string Category { get; set; }
    public string MoveName { get; set; }
    public int MoveId { get; set; }
}
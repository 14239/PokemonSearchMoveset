public enum NodeType
{
    Start,
    Decision,
    Action,
    Direction
}

public class FlowNode
{
    public FlowNode Parent { get; set; } 
    public NodeType Type { get; set; }
    public string Content { get; set; }             // 상자 내용

    public string ArrowLabel { get; set; }          // 선에 적힐 내용


    public int X { get; set; }
    public int Y { get; set; }

    public int GetAbsoluteX()
    {
        return Parent != null ? X + Parent.GetAbsoluteX() : X;
    }

    public int GetAbsoluteY()
    {
        return Parent != null ? Y + Parent.GetAbsoluteY() : Y;
    }

    public bool IsHighlighted { get; set; }
}

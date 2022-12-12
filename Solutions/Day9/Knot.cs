namespace Solutions.Day9;

public record Knot
{
    private int _row;
    private int _column;
    
    public int Row
    {
        get => _row;
        set
        {
            PreviousRow = _row;
            PreviousColumn = _column;
            _row = value;
        }
    }

    public int Column
    {
        get => _column;
        set
        {
            PreviousRow = _row;
            PreviousColumn = _column;
            _column = value;
        }
    }
    public string Name { get; set; }
    public int PreviousRow { get; private set; }
    public int PreviousColumn { get; private set; }
}
namespace PerlinNoise2D_1;

internal static class Debug
{
    private static readonly int MAX_LINES = 420;

    private static RichTextBox _debugTextBox = new RichTextBox();

    public static void Initialize(RichTextBox richTextBox)
    {
        _debugTextBox = richTextBox;
    }

    public static void SetVisibility(bool arg)
    {
        _debugTextBox.Visible = arg;
    }

    private static void Clear()
    {
        if (_debugTextBox.Lines.Length >= MAX_LINES)
            _debugTextBox.Clear();
    }

    public static void Log(params object[] args)
    {
        Clear();
        for (int i = 0; i < args.Length; i++)
            _debugTextBox.AppendText(args[i].ToString() + " ");
    }

    public static void LogLine(params object[] args)
    {
        Clear();
        for (int i = 0; i < args.Length; i++)
            _debugTextBox.AppendText(args[i].ToString() + " ");
        _debugTextBox.AppendText("\n");
    }

}

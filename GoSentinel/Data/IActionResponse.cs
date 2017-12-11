namespace GoSentinel.Data
{
    public interface IActionResponse<T> : IActionResponse where T : IAction
    {
        T BotAction { get; set; }
    }

    public interface IActionResponse
    {}
}

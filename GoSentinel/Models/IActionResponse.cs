namespace GoSentinel.Models
{
    public interface IActionResponse<T> : IActionResponse where T : IAction
    {
        T Action { get; set; }
    }

    public interface IActionResponse
    {}
}

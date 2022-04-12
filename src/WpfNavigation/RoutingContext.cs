namespace WpfNavigation;

public class RoutingContext
{
    public RoutingContext(object? from, object? to)
    {
        From = @from;
        To = to;
    }
    public object? From { get; }
    public object? To { get; }
}
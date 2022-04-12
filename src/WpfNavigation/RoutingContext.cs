namespace WpfNavigation;

public class RoutingContext
{
    public RoutingContext(object? from, object? to, Navigator navigator)
    {
        From = @from;
        To = to;
        Navigator = navigator;
    }
    public object? From { get; }
    public object? To { get; }
    public Navigator Navigator { get; }
}
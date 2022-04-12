namespace WpfNavigation;

public class RoutingRequestEvent
{
    public RoutingRequestEvent(string routeName)
    {
        RouteName = routeName;
    }
    
    public string RouteName { get; }
}
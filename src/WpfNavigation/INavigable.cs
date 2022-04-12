namespace WpfNavigation;

public interface INavigable
{
    void PreDeactivation(RoutingContext ctx);
    void PostDeactivation(RoutingContext ctx);
    void PreActivation(RoutingContext ctx);
    void PostActivation(RoutingContext ctx);
}
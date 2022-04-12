using PCC.Libraries.EventAggregator;

namespace WpfNavigation;

public class Navigator
{
    private readonly EventAggregator _eventAggregator;
    private readonly IProvider _provider;
    private readonly Dictionary<string, Route> _routes = new();

    public Navigator(EventAggregator eventAggregator, IProvider provider)
    {
        _eventAggregator = eventAggregator;
        _eventAggregator.Subscribe<RoutingRequestEvent>((o,e) => OnRoutingRequested(e.RouteName));
        _provider = provider;
    }

    private void OnRoutingRequested(string name)
    {
        if (!ContainsRoute(name)) return;
        UseRoute(name);
    }

    public Navigator AddRoute(string name, TemplateSettings templateSettings, TargetSettings targetSettings)
    {
        _routes.Add(name, new Route(templateSettings, targetSettings, _provider));
        return this;
    }

    public bool ContainsRoute(string name)
    {
        return _routes.ContainsKey(name);
    }

    public void UseRoute(string name)
    {
        var route = _routes[name];
        route.Execute();
    }
}
namespace WpfNavigation;

public class Navigator
{
    private readonly IProvider _provider;
    private readonly Dictionary<string, Route> _routes = new();

    public Navigator(IProvider provider)
    {
        _provider = provider;
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
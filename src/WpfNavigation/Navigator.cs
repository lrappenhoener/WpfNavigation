using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfNavigation;

public class Navigator
{
    private readonly Dictionary<string, Route> _routes = new Dictionary<string, Route>();

    public Navigator AddRoute(string name, TemplateSettings templateSettings, TargetSettings targetSettings)
    {
        _routes.Add(name, new Route(name, templateSettings, targetSettings));
        return this;
    }

    public bool ContainsRoute(string routeName)
    {
        return true;
    }
}
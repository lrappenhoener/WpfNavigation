using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfNavigation;

internal class Route
{
    private readonly string _name;
    private readonly TemplateSettings _templateSettings;
    private readonly TargetSettings _targetSettings;
    private readonly IProvider _provider;

    public Route(string name, TemplateSettings templateSettings, TargetSettings targetSettings, IProvider provider)
    {
        _name = name;
        _templateSettings = templateSettings;
        _targetSettings = targetSettings;
        _provider = provider;
        RouteTemplates.Add(targetSettings.ContentType, templateSettings);
    }

    public void Execute()
    {
        var content = _provider.Resolve(_targetSettings.ContentType);
        var target = RouteTargetFinder.Find(_targetSettings.Root, _targetSettings.Uid);
        target.Content = content;
    }
}
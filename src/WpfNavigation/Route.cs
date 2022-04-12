namespace WpfNavigation;

internal class Route
{
    private readonly TargetSettings _targetSettings;
    private readonly IProvider _provider;

    public Route(TemplateSettings templateSettings, TargetSettings targetSettings, IProvider provider)
    {
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
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
        var newContentNavigable = content as INavigable;
        var target = RouteTargetFinder.Find(_targetSettings.Root, _targetSettings.Uid);
        var currentContentNavigable = target.Content as INavigable;

        var ctx = new RoutingContext(target.Content, content);
        currentContentNavigable?.PreDeactivation(ctx);
        newContentNavigable?.PreActivation(ctx);
        target.Content = content;
        newContentNavigable?.PostActivation(ctx);
        currentContentNavigable?.PostDeactivation(ctx);
    }
}
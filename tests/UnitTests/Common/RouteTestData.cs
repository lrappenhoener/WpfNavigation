using System;
using System.Windows;

namespace WpfNavigation.UnitTests.Common;

internal class RouteTestData<TView, TContent>
{
    public RouteTestData(string name, ResourceDictionary resources, string targetUid, UIElement targetRoot)
    {
        Name = name;
        Resources = resources;
        ViewType = typeof(TView);
        ContentType = typeof(TContent);
        TargetSettings = new TargetSettings<TContent>(targetRoot, targetUid);
        TemplateSettings = new TemplateSettings<TView>(resources);
    }

    public Type ViewType { get; }

    public ResourceDictionary Resources { get; }

    public string Name { get; }

    public Type ContentType { get; }

    public TargetSettings<TContent> TargetSettings { get; }
    public TemplateSettings<TView> TemplateSettings { get; }
}
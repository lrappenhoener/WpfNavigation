using System.Windows;

namespace WpfNavigation;

public class TemplateSettings<TView>
{
    public TemplateSettings(ResourceDictionary resources)
    {
        Resources = resources;
        ViewType = typeof(TView);
    }

    public Type ViewType { get; }
    public ResourceDictionary Resources { get; }
}
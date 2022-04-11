using System.Windows;

namespace WpfNavigation;

public class TemplateSettings
{
    public TemplateSettings(Type viewType, ResourceDictionary resources)
    {
        Resources = resources;
        ViewType = viewType;
    }

    public Type ViewType { get; }
    public ResourceDictionary Resources { get; }
}
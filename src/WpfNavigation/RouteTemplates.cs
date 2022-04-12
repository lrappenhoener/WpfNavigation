using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfNavigation;

internal static class RouteTemplates
{
    private static readonly string CONTENT_TYPE_NAMESPACE = "[CONTENT_TYPE_NAMESPACE]";
    private static readonly string CONTENT_TYPE = "[CONTENT_TYPE]";
    private static readonly string VIEW_TYPE_NAMESPACE = "[VIEW_TYPE_NAMESPACE]";
    private static readonly string VIEW_TYPE = "[VIEW_TYPE]";
    private static readonly string _templateXaml = $@"
            <UserControl
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                    xmlns:contentType=""clr-namespace={CONTENT_TYPE_NAMESPACE}""
                    xmlns:viewType=""clr-namespace={VIEW_TYPE_NAMESPACE}"">
                <UserControl.Resources>
                    <DataTemplate
                        DataType=""contentType:{CONTENT_TYPE}"">
                            <viewType:{VIEW_TYPE} />
                    </DataTemplate>    
                </UserControl.Resources>
            </UserControl>";

    internal static void Add(Type contentType, TemplateSettings templateSettings)
    {
        var key = new DataTemplateKey(contentType);
        var template = CreateTemplateFor(contentType, templateSettings.ViewType);
        var container = XamlReader.Parse(template) as UserControl;
        var enumerator = container.Resources.Values.GetEnumerator();
        enumerator.MoveNext();
        templateSettings.Resources.Add(key, enumerator.Current);        
    }

    private static string CreateTemplateFor(Type contentType, Type viewType)
    {
        return _templateXaml
            .Replace(CONTENT_TYPE_NAMESPACE, contentType.Namespace)
            .Replace(CONTENT_TYPE, contentType.Name)
            .Replace(VIEW_TYPE_NAMESPACE, viewType.Namespace)
            .Replace(VIEW_TYPE, viewType.Name);
    }
}
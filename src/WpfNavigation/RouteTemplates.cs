using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfNavigation;

internal static class RouteTemplates
{
    private static readonly string ContentTypeNamespace = "[CONTENT_TYPE_NAMESPACE]";
    private static readonly string ContentType = "[CONTENT_TYPE]";
    private static readonly string ViewTypeNamespace = "[VIEW_TYPE_NAMESPACE]";
    private static readonly string ViewType = "[VIEW_TYPE]";
    private static readonly string TemplateXaml = $@"
            <UserControl
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                    xmlns:contentType=""clr-namespace={ContentTypeNamespace}""
                    xmlns:viewType=""clr-namespace={ViewTypeNamespace}"">
                <UserControl.Resources>
                    <DataTemplate
                        DataType=""contentType:{ContentType}"">
                            <viewType:{ViewType} />
                    </DataTemplate>    
                </UserControl.Resources>
            </UserControl>";

    internal static void Add(Type contentType, TemplateSettings templateSettings)
    {
        var key = new DataTemplateKey(contentType);
        var template = CreateTemplateFor(contentType, templateSettings.ViewType);
        var container = XamlReader.Parse(template) as UserControl;
        if (container == null) throw new ArgumentException();
        var enumerator = container.Resources.Values.GetEnumerator();
        enumerator.MoveNext();
        templateSettings.Resources.Add(key, enumerator.Current);        
    }

    private static string CreateTemplateFor(Type contentType, Type viewType)
    {
        return TemplateXaml
            .Replace(ContentTypeNamespace, contentType.Namespace)
            .Replace(ContentType, contentType.Name)
            .Replace(ViewTypeNamespace, viewType.Namespace)
            .Replace(ViewType, viewType.Name);
    }
}
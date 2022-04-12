using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfNavigation;

internal static class RouteTemplates
{
    internal static void Add(Type contentType, TemplateSettings templateSettings)
    {
        var key = new DataTemplateKey(contentType);
        var container = XamlReader.Parse(@"
            <UserControl
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                    xmlns:local=""clr-namespace=WpfNavigation.UnitTests.Common"">
                <UserControl.Resources>
                    <DataTemplate
                        DataType=""local:SampleViewModel"">
                            <local:SampleView x:Name=""SampleView"" />
                    </DataTemplate>    
                </UserControl.Resources>
            </UserControl>") as UserControl;
        var enumerator = container.Resources.Values.GetEnumerator();
        enumerator.MoveNext();
        templateSettings.Resources.Add(key, enumerator.Current);        
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfNavigation;

public class Navigator
{
    public Navigator AddRoute(string name, TemplateSettings templateSettings, TargetSettings targetSettings)
    {
        var key = new DataTemplateKey(targetSettings.ContentType);
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
        return this;
    }

    public bool ContainsRoute(string routeName)
    {
        return true;
    }
}
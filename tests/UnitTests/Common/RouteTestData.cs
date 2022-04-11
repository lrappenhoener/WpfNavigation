using System;
using System.Windows;
using System.Xml;
using System.Windows.Markup;

namespace WpfNavigation.UnitTests.Common;

internal class RouteTestData
{
    private readonly string _rootXaml = @$"
            <UserControl 
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                    xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
                    xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
                    xmlns:local=""clr-namespace:UnitTests""
                    mc:Ignorable=""d""
                    d:DesignHeight=""300"" d:DesignWidth=""300"">
                <Grid>
                    <ItemsControl>
                        <ItemsControl.Items>
                            <StackPanel>
                                <ItemsControl>
                                    <ItemsControl.Items>
                                        <TextBlock Text=""Not Here :)""></TextBlock>
                                    </ItemsControl.Items>
                                </ItemsControl>
                            </StackPanel>
                            <ItemsControl>
                                <ItemsControl.Items>
                                    <ContentControl Uid=""TestTarget"" Content=""42""></ContentControl>
                                </ItemsControl.Items>
                            </ItemsControl>
                        </ItemsControl.Items>
                    </ItemsControl>
                </Grid>
            </UserControl>
        ";

    public RouteTestData(string name, Type viewType, Type contentType, ResourceDictionary resources)
    {
        Name = name;
        Resources = resources;
        ViewType = viewType;
        ContentType = contentType;
        TargetSettings = new TargetSettings(contentType, XamlReader.Parse(_rootXaml) as UIElement, "TestTarget");
        TemplateSettings = new TemplateSettings(viewType, resources);
    }

    public Type ViewType { get; }
    public ResourceDictionary Resources { get; }
    public string Name { get; }
    public Type ContentType { get; }
    public TargetSettings TargetSettings { get; }
    public TemplateSettings TemplateSettings { get; }
}
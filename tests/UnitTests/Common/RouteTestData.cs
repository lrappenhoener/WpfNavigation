using System;
using System.Windows;
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
                    xmlns:local=""clr-namespace:WpfNavigation.UnitTests.Common""
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
        var root = XamlReader.Parse(_rootXaml) as UIElement;
        if (root == null) throw new ArgumentException();
        TargetSettings = new TargetSettings(contentType, root, "TestTarget");
        TemplateSettings = new TemplateSettings(viewType, resources);
    }
    
    public Type ViewType { get; set; }
    public ResourceDictionary Resources { get; set; }
    public string Name { get; set; }
    public Type ContentType { get; set; }
    public TargetSettings TargetSettings { get; set; }
    public TemplateSettings TemplateSettings { get; set; }
}
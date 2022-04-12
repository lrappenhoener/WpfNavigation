using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfNavigation.UnitTests.Common;

public class TestTargetDataProvider
{
    public static IEnumerable<object> Get()
    {
        return new object[]
        {
            new[]
            {
                XamlReader.Parse(@$"
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
                    "),
                "TestTarget",
                new Func<UIElement, ContentControl?>((root) =>
                    (((((root as UserControl)?.Content as Grid)?.Children[0] as ItemsControl)?.Items[1] as
                        ItemsControl)?.Items[0] as ContentControl))
            },
            new[]
            {
                XamlReader.Parse(@$"
                        <UserControl 
                                xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                                xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
                                xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
                                xmlns:local=""clr-namespace:WpfNavigation.UnitTests.Common""
                                mc:Ignorable=""d""
                                d:DesignHeight=""300"" d:DesignWidth=""300"">
                            <Grid>
                                <Button>
                                    <StackPanel>
                                        <ItemsControl>
                                            <ItemsControl.Items>
                                                <TextBlock Text=""Not Here :)""></TextBlock>
                                                <ContentControl Uid=""TestTarget"" Content=""42""></ContentControl>
                                            </ItemsControl.Items>
                                        </ItemsControl>
                                    </StackPanel>
                                </Button>
                            </Grid>
                        </UserControl>
                    "),
                "TestTarget",
                new Func<UIElement, ContentControl?>((root) =>
                    (((((root as UserControl)?.Content as Grid)?.Children[0] as Button)?.Content as
                        StackPanel)?.Children[0] as ItemsControl)?.Items[1] as ContentControl)
            },
            new[]
            {
                XamlReader.Parse(@$"
                        <UserControl 
                                xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                                xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
                                xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
                                xmlns:local=""clr-namespace:WpfNavigation.UnitTests.Common""
                                mc:Ignorable=""d""
                                d:DesignHeight=""300"" d:DesignWidth=""300"">
                            <Grid>
                                <Button>
                                    <Button>
                                        <StackPanel>
                                            <ItemsControl>
                                                <ItemsControl.Items>
                                                    <TextBlock Text=""Not Here :)""></TextBlock>
                                                    <ContentControl>
                                                        <ContentControl Uid=""TestTarget"" Content=""42""></ContentControl>
                                                    </ContentControl>
                                                </ItemsControl.Items>
                                            </ItemsControl>
                                        </StackPanel>
                                    </Button>
                                </Button>
                            </Grid>
                        </UserControl>
                    "),
                "TestTarget",
                new Func<UIElement, ContentControl?>((root) =>
                    (((((((root as UserControl)?.Content as Grid)?.Children[0] as Button)?.Content as Button)?.Content as 
                        StackPanel)?.Children[0] as ItemsControl)?.Items[1] as ContentControl)?.Content as ContentControl)
            }
        };
    }
}
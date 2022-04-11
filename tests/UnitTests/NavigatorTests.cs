using System.Linq;
using System.Reflection;
using Xunit;
using System.Windows;
using System.Xaml;
using FluentAssertions;
using WpfNavigation.UnitTests.Common;

namespace WpfNavigation.UnitTests;

public class NavigatorTests
{
    [StaFact]
    public void AddRoute_Adds_DataTemplate_To_Resources()
    {
        var routeData = CreateSampleRouteData<SampleView, SampleViewModel>();
        var sut = CreateSut();

        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);

        ResourcesContainTemplate(routeData).Should().BeTrue();
    }

    private bool ResourcesContainTemplate(RouteTestData routeData)
    {
        var expectedKey = new DataTemplateKey(routeData.TargetSettings.ContentType);
        routeData.Resources.Contains(expectedKey).Should().BeTrue();
        if (routeData.Resources[expectedKey] is not DataTemplate dataTemplate || 
            dataTemplate.Template.GetType().GetRuntimeProperties().ElementAt(0).GetValue(dataTemplate.Template) is not XamlType rootType) 
            return false;
        return rootType.Name == routeData.ViewType.Name;
    }

    private RouteTestData CreateSampleRouteData<TView, TContent>()
    {
        return new RouteTestData("RouteName", typeof(TView), typeof(TContent), new ResourceDictionary());
    }

    private static Navigator CreateSut()
    {
        return new Navigator();
    }
}
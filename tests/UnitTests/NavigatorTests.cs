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
    public void Adding_Route_Creates_DataTemplate_In_Resources()
    {
        var routeData = CreateSampleRouteData();
        var sut = CreateSut();

        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);

        ResourcesContainTemplate(routeData).Should().BeTrue();
    }
    
    [StaFact]
    public void Adding_Same_Route_Multiple_Times_Throws()
    {
        var routeData = CreateSampleRouteData();
        var sut = CreateSut();
        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);

        var exception = Record.Exception(() => sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings));

        exception.Should().NotBeNull();
    }

    [StaFact]
    public void Contains_Added_Route_Returns_True()
    {
        var routeData = CreateSampleRouteData();
        var sut = CreateSut();
        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);

        sut.ContainsRoute(routeData.Name).Should().BeTrue();
    }
    
    [StaFact]
    public void Contains_Unknown_Route_Returns_False()
    {
        var routeData = CreateSampleRouteData();
        var sut = CreateSut();

        sut.ContainsRoute(routeData.Name).Should().BeFalse();
    }

    [StaFact]
    public void Using_Non_Existing_Route_Throws()
    {
        var sut = CreateSut();
        var routeName = "dont exists";

        var exceptions = Record.Exception(() => sut.UseRoute(routeName));

        exceptions.Should().NotBeNull();
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

    private RouteTestData CreateSampleRouteData()
    {
        return new RouteTestData("RouteName", typeof(SampleView), typeof(SampleViewModel), new ResourceDictionary());
    }

    private static Navigator CreateSut()
    {
        return new Navigator();
    }
}
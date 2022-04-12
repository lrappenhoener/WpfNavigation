using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using System.Windows;
using System.Windows.Controls;
using System.Xaml;
using FluentAssertions;
using PCC.Libraries.EventAggregator;
using WpfNavigation.UnitTests.Common;

namespace WpfNavigation.UnitTests;

public class NavigatorTests
{
    [StaTheory]
    [InlineData(typeof(SampleView), typeof(SampleViewModel))]
    [InlineData(typeof(AnotherSampleView), typeof(AnotherSampleViewModel))]
    [InlineData(typeof(SampleView), typeof(AnotherSampleViewModel))]
    [InlineData(typeof(AnotherSampleView), typeof(SampleViewModel))]
    public void Adding_Route_Creates_DataTemplate_In_Resources(Type viewType, Type contentType)
    {
        var routeData = CreateSampleRouteData(viewType, contentType);
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
    
    [StaTheory, MemberData(nameof(TestTargetDataProvider.Get), MemberType = typeof(TestTargetDataProvider))]
    public void Using_Existing_Route_Sets_Content_At_Target(UIElement root, string uid, Func<UIElement, ContentControl> targetFinder)
    {
        var routeData = CreateSampleRouteData(root, uid);
        var expectedContent = new SampleViewModel();
        var fakeProvider = new FakeProvider(new Dictionary<Type, object>
        {
            {typeof(SampleViewModel), expectedContent}
        });
        var sut = CreateSut(fakeProvider);
        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);
        
        sut.UseRoute(routeData.Name);

        targetFinder(root).Content.Should().Be(expectedContent);
    }
    
    [StaTheory, MemberData(nameof(TestTargetDataProvider.Get), MemberType = typeof(TestTargetDataProvider))]
    public void Using_Existing_Route_Calls_Navigable_Content_Methods(UIElement root, string uid, Func<UIElement, ContentControl> targetFinder)
    {
        var routeData = CreateSampleRouteData(root, uid);
        var activated = new NavigableSampleViewModel();
        var deactivated = new NavigableSampleViewModel();
        targetFinder(root).Content = deactivated;
        var fakeProvider = new FakeProvider(new Dictionary<Type, object>
        {
            {typeof(SampleViewModel), activated}
        });
        var sut = CreateSut(fakeProvider);
        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);
        
        sut.UseRoute(routeData.Name);

        activated.PreActivationOrder.Should().Be(1);
        activated.PostActivationOrder.Should().Be(2);
        deactivated.PreDeactivationOrder.Should().Be(1);
        deactivated.PostDeactivationOrder.Should().Be(2);
    }

    [StaTheory, MemberData(nameof(TestTargetDataProvider.Get), MemberType = typeof(TestTargetDataProvider))]
    public void Use_Known_Route_When_RoutingRequestEvent_Triggered(UIElement root, string uid, Func<UIElement, ContentControl> targetFinder)
    {
        var expectedContent = new SampleViewModel();
        var fakeProvider = new FakeProvider(new Dictionary<Type, object>
        {
            {typeof(SampleViewModel), expectedContent}
        });
        var eventAggregator = new EventAggregator();
        var routeData = CreateSampleRouteData(root, uid);
        var sut = CreateSut(eventAggregator, fakeProvider);
        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);

        eventAggregator.Publish(this, new RoutingRequestEvent(routeData.Name));
        
        targetFinder(root).Content.Should().Be(expectedContent);
    }
    
    [StaTheory, MemberData(nameof(TestTargetDataProvider.Get), MemberType = typeof(TestTargetDataProvider))]
    public void Dont_Use_Route_When_Unknown_RoutingRequestEvent_Triggered(UIElement root, string uid, Func<UIElement, ContentControl> targetFinder)
    {
        var expectedContent = new SampleViewModel();
        var fakeProvider = new FakeProvider(new Dictionary<Type, object>
        {
            {typeof(SampleViewModel), expectedContent}
        });
        var eventAggregator = new EventAggregator();
        var routeData = CreateSampleRouteData(root, uid);
        var sut = CreateSut(eventAggregator, fakeProvider);
        sut.AddRoute(routeData.Name, routeData.TemplateSettings, routeData.TargetSettings);

        eventAggregator.Publish(this, new RoutingRequestEvent("Unknown RouteName"));
        
        targetFinder(root).Content.Should().Be("42");
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
        return CreateSampleRouteData(typeof(SampleView), typeof(SampleViewModel));
    }
    
    private RouteTestData CreateSampleRouteData(UIElement root, string uid)
    {
        var data = CreateSampleRouteData(typeof(SampleView), typeof(SampleViewModel));
        data.TargetSettings = new TargetSettings(typeof(SampleViewModel), root, uid);
        return data;
    }
    
    private RouteTestData CreateSampleRouteData(Type viewType, Type contentType)
    {
        return new RouteTestData("RouteName", viewType, contentType, new ResourceDictionary());
    }

    private static Navigator CreateSut()
    {
        return CreateSut(new FakeProvider(new Dictionary<Type, object>
        {
            {typeof(SampleViewModel), new SampleViewModel()}
        }));
    }

    private static Navigator CreateSut(IProvider provider)
    {
        return CreateSut(new EventAggregator(), provider); 
    }
    
    private static Navigator CreateSut(EventAggregator eventAggregator, IProvider provider)
    {
        return new Navigator(eventAggregator, provider); 
    }
}
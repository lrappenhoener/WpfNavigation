namespace WpfNavigation.UnitTests.Common;

public class NavigableSampleViewModel : INavigable
{
    public bool PreDeactivationInvoked { get; set; }
    public bool PostDeactivationInvoked { get; set; }
    public bool PreActivationInvoked { get; set; }
    public bool PostActivationInvoked { get; set; }
    public void PreDeactivation(RoutingContext ctx)
    {
        PreDeactivationInvoked = true;
    }

    public void PostDeactivation(RoutingContext ctx)
    {
        PostDeactivationInvoked = true;;
    }

    public void PreActivation(RoutingContext ctx)
    {
        PreActivationInvoked = true;
    }

    public void PostActivation(RoutingContext ctx)
    {
        PostActivationInvoked = true;
    }
}
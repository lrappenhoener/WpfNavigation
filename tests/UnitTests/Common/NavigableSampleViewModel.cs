namespace WpfNavigation.UnitTests.Common;

public class NavigableSampleViewModel : INavigable
{
    private int _order = 1;
    public bool PreDeactivationInvoked { get; set; }
    public bool PostDeactivationInvoked { get; set; }
    public bool PreActivationInvoked { get; set; }
    public bool PostActivationInvoked { get; set; }
    public int PreDeactivationOrder { get; set; }
    public int PostDeactivationOrder { get; set; }
    public int PreActivationOrder { get; set; }
    public int PostActivationOrder { get; set; }
    public void PreDeactivation(RoutingContext ctx)
    {
        PreDeactivationInvoked = true;
        PreDeactivationOrder = _order++;
    }

    public void PostDeactivation(RoutingContext ctx)
    {
        PostDeactivationInvoked = true;
        PostDeactivationOrder = _order++;
    }

    public void PreActivation(RoutingContext ctx)
    {
        PreActivationInvoked = true;
        PreActivationOrder = _order++;
    }

    public void PostActivation(RoutingContext ctx)
    {
        PostActivationInvoked = true;
        PostActivationOrder = _order++;
    }
}
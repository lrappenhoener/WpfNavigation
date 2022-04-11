using System.Windows;

namespace WpfNavigation;

public class TargetSettings<TContent>
{
    public TargetSettings(UIElement root, string uid)
    {
        Root = root;
        Uid = uid;
        ContentType = typeof(TContent);
    }

    public Type ContentType { get; set; }
    public UIElement Root { get; }
    public string Uid { get; }
}
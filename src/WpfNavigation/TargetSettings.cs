using System.Windows;

namespace WpfNavigation;

public class TargetSettings
{
    public TargetSettings(Type contentType, UIElement root, string uid)
    {
        Root = root;
        Uid = uid;
        ContentType = contentType;
    }

    public Type ContentType { get; set; }
    public UIElement Root { get; }
    public string Uid { get; }
}
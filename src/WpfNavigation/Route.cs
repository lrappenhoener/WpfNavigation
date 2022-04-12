using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfNavigation;

internal class Route
{
    private readonly string _name;
    private readonly TemplateSettings _templateSettings;
    private readonly TargetSettings _targetSettings;
    private readonly IProvider _provider;

    public Route(string name, TemplateSettings templateSettings, TargetSettings targetSettings, IProvider provider)
    {
        _name = name;
        _templateSettings = templateSettings;
        _targetSettings = targetSettings;
        _provider = provider;
        RouteTemplates.Add(targetSettings.ContentType, templateSettings);
    }

    public void Execute()
    {
        var content = _provider.Resolve(_targetSettings.ContentType);
        var target = FindTarget();
        target.Content = content;
    }

    private ContentControl FindTarget()
    {
        var queue = new Queue<UIElement>();
        queue.Enqueue(_targetSettings.Root);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Uid == _targetSettings.Uid) return current as ContentControl;
            if (current is Panel panel)
            {
                foreach (UIElement child in panel.Children)
                {
                    queue.Enqueue(child);
                }
            }
            else if (current is ItemsControl itemsControl)
            {
                foreach (var child in itemsControl.Items)
                {
                    if (child is UIElement uiChild)
                        queue.Enqueue(uiChild);
                }
            }
            else if (current is ContentControl contentControl)
            {
                if (contentControl.Content is UIElement contentChild)
                    queue.Enqueue(contentChild);
            }
        }

        return null;
    }
}
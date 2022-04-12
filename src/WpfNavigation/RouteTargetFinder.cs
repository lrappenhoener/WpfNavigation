using System.Windows;
using System.Windows.Controls;

namespace WpfNavigation;

internal static class RouteTargetFinder
{
    internal static ContentControl Find(UIElement root, string uid)
    {
        var queue = new Queue<UIElement>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Uid == uid) return current as ContentControl;
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
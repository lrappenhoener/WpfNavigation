using System.Windows;
using System.Windows.Controls;

namespace WpfNavigation;

internal static class RouteTargetFinder
{
    internal static ContentControl? Find(UIElement root, string uid)
    {
        var queue = new Queue<UIElement>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Uid == uid) return current as ContentControl;
            var children = GetChildren(current);
            QueueChildren(queue, children);
        }
        return null;
    }

    private static void QueueChildren(Queue<UIElement> queue, IEnumerable<UIElement> children)
    {
        foreach (var child in children)
            queue.Enqueue(child);
    }

    private static IEnumerable<UIElement> GetChildren(UIElement current)
    {
        if (current is Panel panel) return panel.Children.OfType<UIElement>();
        if (current is ItemsControl itemsControl) return itemsControl.Items.OfType<UIElement>();
        if (current is ContentControl contentControl && contentControl.Content is UIElement uiElement)
            return new List<UIElement> {uiElement};
        return new List<UIElement>();
    }
}
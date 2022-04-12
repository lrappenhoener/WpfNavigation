namespace WpfNavigation;

public interface IProvider
{
    object Resolve(Type type);
}
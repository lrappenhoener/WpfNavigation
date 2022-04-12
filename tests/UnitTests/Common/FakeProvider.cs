using System;
using System.Collections.Generic;

namespace WpfNavigation.UnitTests.Common;

internal class FakeProvider : IProvider
{
    private readonly Dictionary<Type, object> _values;

    public FakeProvider(Dictionary<Type, object> values)
    {
        _values = values;
    }
    public object Resolve(Type type)
    {
        return _values[type];
    }
}
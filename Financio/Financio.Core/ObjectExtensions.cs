using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financio.Core;

public static class ObjectExtensions
{
    public static T Also<T>(this T @this, Action<T> action)
    {
        action(@this);
        return @this;
    }
}

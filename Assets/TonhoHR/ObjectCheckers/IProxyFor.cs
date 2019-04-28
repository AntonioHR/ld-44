using UnityEngine;
using System.Collections;

namespace TonhoHR.ObjectCheckers
{
    public interface IProxyFor<T>
    {
        T Owner { get; }
    }
}
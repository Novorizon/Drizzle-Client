using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public interface IHotUpdate
    {
        Assembly GetAssembly(string assemblyName);
        Type GetType(string typeName);
        void Invoke(Type type, string methodName, params object[] args);
    }
}

using System;
using System.Linq;
using TradingAPI.Core.Interfaces;

namespace TradingAPI.Core
{
    static class VentorFactory
    {
        public static IVentorService GenerateVentor(string clarty)
        {
            Type type = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(x => x.GetTypes())
                                .FirstOrDefault(t => t.Name == clarty); 

            var instance = (IVentorService)Activator.CreateInstance(type);
            return instance;
        }
    }
}

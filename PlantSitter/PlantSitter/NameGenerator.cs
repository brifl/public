using System;

namespace PlantSitter
{
    internal class NameGenerator
    {
        public static string NameFor<T>(string providedName)
        {
            if (string.IsNullOrEmpty(providedName))
            {
                providedName = typeof(T).Name + "_" + Guid.NewGuid().ToString("N");
            }

            return providedName;
        }
    }
}

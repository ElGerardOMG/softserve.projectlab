using System.Reflection;

namespace API.Utils.Implementations
{
    public class Comparer
    {
        public static bool Compare<T1, T2>(T1 source, T2 target)
        {
            if (source == null || target == null)
                return false;

            var sourceProperties = typeof(T1).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in sourceProperties)
            {
                // Ignorar propiedades que sean ICollection (genéricas o no)
                if (typeof(ICollection<>).IsAssignableFrom(prop.PropertyType) ||
                    (prop.PropertyType.IsGenericType &&
                     typeof(ICollection<>).IsAssignableFrom(prop.PropertyType.GetGenericTypeDefinition())))
                {
                    continue;
                }

                var targetProp = typeof(T2).GetProperty(prop.Name, BindingFlags.Public | BindingFlags.Instance);
                if (targetProp == null)
                    return false;

                var sourceValue = prop.GetValue(source);
                var targetValue = targetProp.GetValue(target);

                if (!object.Equals(sourceValue, targetValue))
                    return false;
            }

            return true;
        }
    }
}

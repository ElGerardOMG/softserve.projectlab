using API.Utils.Interfaces;
using System.Collections;
using System.Reflection;

namespace API.Utils.Implementations
{
    public class DtoMapper
    {

        public static U Mapper<T, U>(T source, bool? ignoreDateFields = false) where T : new()
        {
            U destination = (U)Activator.CreateInstance(typeof(U));
            if (source == null)
            {
                throw new ArgumentNullException("Source or Destination cannot be null");
            }

            Type sourceType = typeof(T);
            Type destinationType = typeof(U);
            PropertyInfo destinationProperty = null;
            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty != null && destinationProperty.CanWrite && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
            if(sourceType.BaseType.GetProperty("Id") != null)
            {
                destinationProperty = destinationType.BaseType.GetProperty("Id");
                destinationProperty.SetValue(destination, sourceType.BaseType.GetProperty("Id").GetValue(source));
            }
            if(ignoreDateFields == false)
            {
                destinationProperty = destinationType.GetProperty("CreatedAt");
                destinationProperty.SetValue(destination, DateTime.Now);
                destinationProperty = destinationType.GetProperty("IsActive");
                destinationProperty.SetValue(destination, true);
            }
            return destination;
        }

        public static ICollection<U> ExtractCollection<T, U>(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance), "The instance cannot be null.");
            }

            // Busca un atributo de tipo ICollection en la clase
            PropertyInfo collectionProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => typeof(ICollection<U>).IsAssignableFrom(p.PropertyType));

            if (collectionProperty == null)
            {
                throw new InvalidOperationException("No se encontró un atributo de tipo ICollection en la clase.");
            }

            // Obtiene el valor del atributo ICollection
            ICollection<U> collection = collectionProperty.GetValue(instance) as ICollection<U>;

            if (collection == null)
            {
                throw new InvalidOperationException("El atributo de tipo ICollection es nulo.");
            }

            // Retorna la colección para que pueda ser iterada
            return collection;
        }
    }
}

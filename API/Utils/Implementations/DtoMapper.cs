using API.Utils.Interfaces;
using System.Collections;

using System.Data;
using System.Reflection;

namespace API.Utils.Implementations
{
    public class DtoMapper
    {

        public static U Mapper<T, U>(T source, bool? ignoreDateFields = false) where T : new()
        {
            U destination = (U) Activator.CreateInstance(typeof(U));
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
            if ((sourceType.BaseType.GetProperty("Id") != null )&&( destinationType.BaseType.GetProperty("Id") != null))
            {
                destinationProperty = destinationType.BaseType.GetProperty("Id");
                destinationProperty.SetValue(destination, sourceType.BaseType.GetProperty("Id").GetValue(source));
            }
            if (ignoreDateFields == false)
            {
                destinationProperty = destinationType.GetProperty("CreatedAt");
                destinationProperty.SetValue(destination, DateTime.Now);
                destinationProperty = destinationType.GetProperty("IsActive");
                destinationProperty.SetValue(destination, true);
            }
            return destination;
        }

        /** 
         * This method updates properties of the target instance with the values of the source instance if their names and types match
         * 
         * @param TSource the type of the source instance object
         * @param TTarget the type of the target instance object
         * 
         * @param source the source instance object
         * @param target the target instance object
         * 
         * @param ignoreProperties A list of property names to be ignored. A property on the target instance will be not affected
         * if its name is in this list
         * 
         * @param NullPolicy: 
         * * 0 - Null values on source instance will be ommited
         * * 1 - Null values on source will replace the ones on the destination instance
         * 
         * Notes: Property names are case-sensitive.
         */
        public static void Updater<TSource, TTarget>(TSource source, TTarget target, List<string> ignoreProperties = null, int NullPolicy = 0)
        {

            if (source == null || target == null)
            {
                throw new ArgumentNullException("Source or Destination cannot be null");
            }

            if(ignoreProperties == null)
            {
                ignoreProperties = new List<string>();
            }

            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TTarget);

            PropertyInfo? destinationProperty = null;

            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                if ( ignoreProperties.Contains( sourceProperty.Name ))
                {
                    continue;
                }

                if ((NullPolicy == 0) && (sourceProperty.GetValue(source) == null))
                {
                    continue;
                }

                destinationProperty = destinationType.GetProperty(sourceProperty.Name);

                if ((destinationProperty != null) && 
                    (destinationProperty.CanWrite) && 
                    (destinationProperty.PropertyType == sourceProperty.PropertyType))
                {
                    destinationProperty.SetValue(target, sourceProperty.GetValue(source));
                }
            }
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
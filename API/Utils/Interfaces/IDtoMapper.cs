using System.Reflection;

namespace API.Utils.Interfaces
{
    public interface IDtoMapper
    {
        public U Mapper<T, U>(T source, U destination);
    }
}

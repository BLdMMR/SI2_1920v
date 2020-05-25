using System.Collections;
using System.Collections.Generic;

namespace Instituto.Mappers
{
    public interface IMapper<T, U>
    {
        void Create(T entity);
        IEnumerable<T> Read(U id);
        void Update(T entity);
        void Delete(T entity);
    }
}
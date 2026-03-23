using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicIinterfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T AddItem(T item);
        void UpdateItem(int id, T item);
        void DeleteItem(int id);
    }
}

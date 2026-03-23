using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInterfaces
{
    public interface ICompositeDelete<T>
    {
        void DeleteByKeys(int key1, int key2);
    }
}

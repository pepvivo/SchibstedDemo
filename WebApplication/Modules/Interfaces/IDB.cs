using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Modules.Interfaces
{
    public interface IDB<T>
    {
        T Get(string name);
        List<T> GetAll();
        bool Create(T UserData);
        bool Update(T UserData);
        bool Delete(string name);
    }
}

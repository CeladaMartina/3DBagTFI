using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DBag
{
    public interface ISubject
    {
        void Attach(IObserver Observer);
        void NotificarCambio();
        string TraducirObserver(string st);
    }
}

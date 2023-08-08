using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class Venta
    {
        Acceso_DAL.Venta Mapper = new Acceso_DAL.Venta();
        Propiedades_BE.Venta VentaTemp = new Propiedades_BE.Venta();
                
        public List<Propiedades_BE.Venta> Listar()
        {
            return Mapper.Listar();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Venta
    {
        Acceso_BD Acceso = new Acceso_BD();
        Seguridad Seguridad = new Seguridad();
        
        public List<Propiedades_BE.Venta> Listar()
        {
            List<Propiedades_BE.Venta> ListaVenta = new List<Propiedades_BE.Venta>();
            DataTable Tabla = Acceso.Leer("ListarVenta", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Venta V = new Propiedades_BE.Venta();
                V.NumVenta = int.Parse(R["NumVenta"].ToString());
                V.DNICliente = Seguridad.Desencriptar(R["DNIcliente"].ToString());
                V.Nombre = R["Nombre"].ToString();
                V.Apellido = R["Apellido"].ToString();
                V.Fecha = new DateTime(long.Parse(R["Fecha"].ToString()));
                V.Descripcion = R["Descripcion"].ToString();                
                V.Monto = decimal.Parse(R["Monto"].ToString());
                ListaVenta.Add(V);
            }
            return ListaVenta;
        }
    }
}

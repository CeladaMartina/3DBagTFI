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

        //public List<Propiedades_BE.Venta> ConsultaVenta(DateTime _FechaDesde, DateTime _FechaHasta, string consultaCliente, string consultaMonto)
        //{
        //    return Mapper.ConsultaVenta(_FechaDesde, _FechaHasta, consultaCliente, consultaMonto);
        //}

        public string CancelarVenta(int IdVenta)
        {
            return Mapper.CancelarVenta(IdVenta);
        }

        public int TraerIdVenta()
        {
            return Mapper.TraerIdVenta();
        }

        public int Alta(int IdUsuario, DateTime Fecha)
        {
            VentaTemp.IdUsuario = IdUsuario;
            VentaTemp.Fecha = Fecha;

            return Mapper.Alta(VentaTemp);
        }
        public bool VerificarExistenciaMonto(int IdVenta)
        {
            return Mapper.VerificarExistenciaMonto(IdVenta);
        }
        public void Vender(int IdVenta)
        {
            Mapper.Vender(IdVenta);
        }

        public int ExisteVenta(int IdUsuario, DateTime Fecha)
        {
            return Mapper.ExisteVenta(IdUsuario, Fecha);
        }
    }
}

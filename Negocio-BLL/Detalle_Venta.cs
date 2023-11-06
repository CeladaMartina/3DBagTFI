using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class Detalle_Venta
    {
        Acceso_DAL.Detalle_Venta Mapper = new Acceso_DAL.Detalle_Venta();
        Propiedades_BE.Detalle_Venta DVTemp = new Propiedades_BE.Detalle_Venta();
        Acceso_DAL.Seguridad Seguridad = new Acceso_DAL.Seguridad();
        Acceso_DAL.Producto Articulo = new Acceso_DAL.Producto();
        Acceso_DAL.Usuario MapperU = new Acceso_DAL.Usuario();

        long DV = 0;

        public List<Propiedades_BE.Detalle_Venta> ListarDV(int IdVenta)
        {
            DVTemp.IdVenta = IdVenta;
            List<Propiedades_BE.Detalle_Venta> Lista = Mapper.ListarDV(DVTemp);
            return Lista;
        }

        public int AltaDV(decimal PUnit, int IdArticulo, string descripcion, int DVH, int Cantidad, int IdVenta)
        {
            DVTemp.PUnit = PUnit;
            DVTemp.IdArticulo = IdArticulo;
            DVTemp.Descrip = descripcion;
            DVTemp.DVH = DVH;
            DVTemp.Cant = Cantidad;
            DVTemp.IdVenta = IdVenta;
            int i = Mapper.AltaDV(DVTemp);
            DV = Seguridad.CalcularDVH("select * from Detalle_Venta where IdVenta= " + DVTemp.IdVenta + " and IdDetalle= (select TOP 1 IdDetalle from Detalle_Venta ORDER BY IdDetalle DESC)", "Detalle_Venta");
            Articulo.EjecutarConsulta("Update Detalle_Venta set DVH= " + DV + " where IdVenta= " + DVTemp.IdVenta + " and IdDetalle= (select TOP 1 IdDetalle from Detalle_Venta ORDER BY IdDetalle DESC)");
            Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));

            return i;
        }

        public int ModificarDV(int IdDetalle, int IdArticulo, decimal PUnit, int Cantidad, int DVH, int IdVenta)
        {
            DVTemp.IdDetalle = IdDetalle;
            DVTemp.IdVenta = IdVenta;
            DVTemp.IdArticulo = IdArticulo;
            DVTemp.PUnit = PUnit;
            DVTemp.Cant = Cantidad;
            DVTemp.DVH = DVH;

            int i = Mapper.ModificarDV(DVTemp);
            DV = Seguridad.CalcularDVH("select * from Detalle_Venta where IdVenta = " + DVTemp.IdVenta + " and IdDetalle= " + DVTemp.IdDetalle + "", "Detalle_Venta");
            Articulo.EjecutarConsulta("Update Detalle_Venta set DVH= " + DV + " where IdVenta= " + DVTemp.IdVenta + " and IdDetalle= " + DVTemp.IdDetalle + "");
            Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));

            return i;
        }

        public int BajaDV(int IdDetalle, int IdArticle)
        {            
            DVTemp.IdDetalle = IdDetalle;
            DVTemp.IdArticulo = IdArticle;
            int i = Mapper.BajaDV(DVTemp);

            //DV = Seguridad.CalcularDVH("select * from Detalle_Venta where IdVenta = " + DVTemp.IdVenta + " and IdDetalle= " + DVTemp.IdDetalle + "", "Detalle_Venta");
            //Articulo.EjecutarConsulta("Update Detalle_Venta set DVH= " + DV + " where IdVenta= " + DVTemp.IdVenta + " and IdDetalle= " + DVTemp.IdDetalle + "");
            Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));

            return i;
        }

        public decimal SubTotal(int IdVenta)
        {
            return Mapper.SubTotal(IdVenta);
        }

        public int ChequearStock(int IdArticulo, int IdVenta, int Cantidad, int IdDetalle)
        {
            return Mapper.ChequearStock(IdArticulo, IdVenta, Cantidad, IdDetalle);
        }

        public bool ExisteProducto(int IdVenta, int IdArticulo)
        {
            return Mapper.ExisteProducto(IdVenta, IdArticulo);
        }

        public void UnificarArticulos(int IdVenta, int IdArticulo, int Cantidad)
        {
            Mapper.UnificarArticulos(IdVenta, IdArticulo, Cantidad);
            DV = Seguridad.CalcularDVH("Select * from Detalle_Venta where IdArticulo = " + IdArticulo + " and IdVenta = " + IdVenta + "", "Detalle_Venta");
            Articulo.EjecutarConsulta("Update Detalle_Venta set DVH = " + DV + " where IdArticulo = " + IdArticulo + " and IdVenta = " + IdVenta + "");
            Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));
        }

        //public int ObtenerCantidad(int IdVenta, int IdArticulo)
        //{
        //    return Mapper.ObtenerCantidad(IdVenta, IdArticulo);
        //}

        #region VerificarIntegridad

        public List<Propiedades_BE.Detalle_Venta> ListaVerificacion()
        {
            return Mapper.ListaVerificacion();
        }

        #endregion

        #region verificacion integral
        public string VerificarIntegridadDV(int GlobalIdUsuario)
        {
            return Mapper.VerificarIntegridadDV(GlobalIdUsuario);
        }

        public void RecalcularDVH()
        {
            Mapper.RecalcularDVH();
        }

        #endregion

        public string SeleccionarNick(int idUsuario)
        {
            return MapperU.SeleccionarNick(idUsuario);
        }
    }
}

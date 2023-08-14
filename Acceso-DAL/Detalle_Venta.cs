using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Detalle_Venta
    {
        Acceso_BD Acceso = new Acceso_BD();
        Seguridad Seguridad = new Seguridad();

        public List<Propiedades_BE.Detalle_Venta> ListarDV(Propiedades_BE.Detalle_Venta DetalleVenta)
        {
            List<Propiedades_BE.Detalle_Venta> ListarDetalle = new List<Propiedades_BE.Detalle_Venta>();
            SqlParameter[] P = new SqlParameter[1];
            P[0] = new SqlParameter("@IdVenta", DetalleVenta.IdVenta);

            DataTable Tabla = Acceso.Leer("ListarDVParametros", P);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Detalle_Venta DV = new Propiedades_BE.Detalle_Venta();
                DV.IdDetalle = int.Parse(R["IdDetalle"].ToString());
                DV.CodProd = int.Parse(R["CodProd"].ToString());
                DV.Descrip = R["Nombre"].ToString();
                DV.PUnit = decimal.Parse(R["PUnit"].ToString());
                DV.Cant = int.Parse(R["Cant"].ToString());
                ListarDetalle.Add(DV);
            }
            return ListarDetalle;
        }

        public int AltaDV(Propiedades_BE.Detalle_Venta DV)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[6];
            P[0] = new SqlParameter("@IdVenta", DV.IdVenta);
            P[1] = new SqlParameter("@IdArticulo", DV.IdArticulo);
            P[2] = new SqlParameter("@Nombre", DV.Descrip);
            P[3] = new SqlParameter("@Cant", DV.Cant);
            P[4] = new SqlParameter("@PUnit", DV.PUnit);
            P[5] = new SqlParameter("@DVH", DV.DVH);
            fa = Acceso.Escribir("AltaDetalleVenta", P);
            return fa;
        }

        public int BajaDV(Propiedades_BE.Detalle_Venta DV)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[1];
            P[0] = new SqlParameter("@IdDetalle", DV.IdDetalle);
            fa = Acceso.Escribir("BajaDetalleVenta", P);
            return fa;
        }

        public int ModificarDV(Propiedades_BE.Detalle_Venta DV)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[5];
            P[0] = new SqlParameter("@IdDetalle", DV.IdDetalle);
            P[1] = new SqlParameter("@IdArticulo", DV.IdArticulo);
            P[2] = new SqlParameter("@Cant", DV.Cant);
            P[3] = new SqlParameter("@PUnit", DV.PUnit);
            P[4] = new SqlParameter("@DVH", DV.DVH);
            fa = Acceso.Escribir("ModificarDetalleVenta", P);
            return fa;
        }

        public decimal SubTotal(int IdVenta)
        {
            decimal SubTOT = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select ISNULL(SUM(Cant * PUnit),0) from Detalle_Venta where IdVenta = '" + IdVenta + "'";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            SubTOT = Lector.GetDecimal(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return SubTOT;
        }

        public int ChequearStock(int IdArticulo, int IdVenta, int Cantidad, int IdDetalle)
        {
            int Stock = 0;
            Acceso.AbrirConexion();
            var cmd = new SqlCommand();
            cmd.Connection = Acceso.Conexion;
            if (IdDetalle != 0)
            {
                cmd.CommandText = "select ISNULL(SUM(Cant),0) from Detalle_Venta where IdVenta = " + IdVenta + " and IdArticulo = " + IdArticulo + " and IdDetalle <> " + IdDetalle + "";
            }
            else
            {
                cmd.CommandText = "select ISNULL(SUM(Cant),0) from Detalle_Venta where IdVenta = " + IdVenta + " and IdArticulo = " + IdArticulo + "";
            }
            var Leer = cmd.ExecuteReader();
            if (Leer.Read())
            {
                Stock = Leer.GetInt32(0);
            }
            Leer.Close();
            Acceso.CerrarConexion();
            return Stock + Cantidad;
        }

        public bool ExisteProducto(int IdVenta, int IdArticulo)
        {
            bool a = false;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select * from Detalle_Venta where IdVenta = " + IdVenta + " and IdArticulo = " + IdArticulo + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            a = true;
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return a;
        }

        public void UnificarArticulos(int IdVenta, int IdArticulo, int Cantidad)
        {
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "update Detalle_Venta set Cant = " + Cantidad + " + Cant where IdVenta = " + IdVenta + " and IdArticulo = " + IdArticulo + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    Cmd.ExecuteNonQuery();
                }
                Acceso.CerrarConexion();
            }
        }

        #region VerificarIntegridad

        public List<Propiedades_BE.Detalle_Venta> ListaVerificacion()
        {
            List<Propiedades_BE.Detalle_Venta> Lista = new List<Propiedades_BE.Detalle_Venta>();
            DataTable Tabla = Acceso.Leer("ListarVentaVerificacion", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Detalle_Venta DV = new Propiedades_BE.Detalle_Venta();
                DV.IdVenta = int.Parse(R["IdVenta"].ToString());
                DV.IdDetalle = int.Parse(R["IdDetalle"].ToString());
                DV.IdArticulo = int.Parse(R["IdArticulo"].ToString());
                DV.Descrip = R["Nombre"].ToString();
                DV.PUnit = decimal.Parse(R["PUnit"].ToString());
                DV.Cant = int.Parse(R["Cant"].ToString());
                DV.DVH = int.Parse(R["DVH"].ToString());
                Lista.Add(DV);
            }
            return Lista;
        }

        public string VerificarIntegridadDV(int GlobalIdUsuario)
        {
            long Suma = 0;
            long DVH = 0;
            string msj = "";
            string msj2 = "";

            List<int> CamposFallidos = new List<int>();
            List<Propiedades_BE.Detalle_Venta> DetalleV = ListaVerificacion();

            foreach (Propiedades_BE.Detalle_Venta Dv in DetalleV.ToList())
            {
                string IdVenta = Dv.IdVenta.ToString();
                string IdDetalle = Dv.IdDetalle.ToString();
                string IdArticulo = Dv.IdArticulo.ToString();
                string nombre = Dv.Descrip;
                string Cant = Dv.Cant.ToString();
                string punit = Dv.PUnit.ToString();
                string dvh = Dv.DVH.ToString();

                long IdVentaDV = Seguridad.ObtenerAscii(IdVenta);
                long IdDetalleDV = Seguridad.ObtenerAscii(IdDetalle);
                long IdArticuloDV = Seguridad.ObtenerAscii(IdArticulo);
                long nombreDV = Seguridad.ObtenerAscii(nombre);
                long CantDV = Seguridad.ObtenerAscii(Cant);
                long pu = Seguridad.ObtenerAscii(punit);
                long dvhDV = long.Parse(dvh);

                Suma = IdVentaDV + IdDetalleDV + IdArticuloDV + nombreDV + CantDV + pu;
                DVH += Suma;

                if (dvhDV == Suma)
                {
                    DetalleV.Remove(Dv);
                }
            }
            if (DVH != Seguridad.VerificacionDVV("Detalle_Venta"))
            {
                msj += "Se encontro un error en la tabla Detalle Venta \n";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Error en la tabla Det_Ven", "Alta", 0);

                if (DVH < Seguridad.VerificacionDVV("Detalle_Venta"))
                {
                    msj += "Posibilidad de eliminacion de 1 o mas registros de Detalle de Venta \n";
                    Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Eliminacion registros Det_Ven", "Alta", 0);
                }
            }
            foreach (Propiedades_BE.Detalle_Venta MalCampo in DetalleV)
            {
                CamposFallidos.Add(MalCampo.IdDetalle);
            }
            foreach (var item in CamposFallidos)
            {

                msj += "Se encontro un fallo en la fila con Id Detalle: " + item + " \n";
                msj2 = "Error Det_Ven IdDet:" + item + "";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, msj2, "Alta", 0);
                msj2 = "";
            }
            return msj;
        }

        public void RecalcularDVH()
        {
            long suma = 0;

            List<Propiedades_BE.Detalle_Venta> DV = ListaVerificacion();
            foreach (Propiedades_BE.Detalle_Venta Det_Ven in DV.ToList())
            {
                suma = 0;

                string IdVenta = Det_Ven.IdVenta.ToString();
                string IdDetalle = Det_Ven.IdDetalle.ToString();
                string IdArticulo = Det_Ven.IdArticulo.ToString();
                string Nombre = Det_Ven.Descrip;
                string Cant = Det_Ven.Cant.ToString();
                string punit = Det_Ven.PUnit.ToString();
                string dvh = Det_Ven.DVH.ToString();

                long IdVentaDC = Seguridad.ObtenerAscii(IdVenta);
                long IdDetalleDV = Seguridad.ObtenerAscii(IdDetalle);
                long IdArticuloDv = Seguridad.ObtenerAscii(IdArticulo);
                long NombreDV = Seguridad.ObtenerAscii(Nombre);
                long CantDv = Seguridad.ObtenerAscii(Cant);
                long pu = Seguridad.ObtenerAscii(punit);
                long dvhDv = long.Parse(dvh);

                suma = IdVentaDC + IdDetalleDV + IdArticuloDv + NombreDV + CantDv + pu;
                Acceso.EjecutarConsulta("Update Detalle_Venta set DVH = " + suma + " where IdDetalle = " + IdDetalle + "");
            }
        }

        #endregion
    }
}

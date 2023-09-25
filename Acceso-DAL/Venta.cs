using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                V.Descripcion = R["Nombre"].ToString();
                V.Cantidad = int.Parse(R["Cant"].ToString());
                V.NumVenta = int.Parse(R["NumVenta"].ToString());
                V.Nombre = R["Cliente"].ToString();
                V.Fecha = new DateTime(long.Parse(R["Fecha"].ToString()));
                V.Monto = decimal.Parse(R["Monto"].ToString());
                ListaVenta.Add(V);
            }
            return ListaVenta;
        }

        public bool VerificarExistenciaMonto(int IdVenta)
        {
            bool i = false;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select * from Detalle_Venta where IdVenta = " + IdVenta + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            i = true;
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return i;
        }

        public int TraerIdVenta()
        {
            int Id = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryIdLast = "select TOP 1 IdVenta from Venta ORDER BY IdVenta DESC";
                using (SqlCommand Cmd = new SqlCommand(QueryIdLast, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Id = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Id;
        }

        public int Alta(Propiedades_BE.Venta Venta)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[3];
            P[0] = new SqlParameter("@IdUsuario", Venta.IdUsuario);
            P[1] = new SqlParameter("@Fecha", Venta.Fecha.Ticks);
            P[2] = new SqlParameter("@DVH", Venta.DVH);
            fa = Acceso.Escribir("AltaVenta", P);
            return fa;
        }
        public string CancelarVenta(int IdVenta)
        {
            string Mensaje = "";
            try
            {
                Acceso.EjecutarConsulta("Delete from Detalle_Venta where IdVenta= " + IdVenta + "");
                Acceso.EjecutarConsulta("Delete from Venta where IdVenta= " + IdVenta + "");
                Seguridad.ActualizarDVV("Detalle_Venta", Seguridad.SumaDVV("Detalle_Venta"));
                Mensaje = "Se ha cancelado la venta";
            }

            catch (Exception)
            {
                Mensaje = "La venta no se cancelo";
            }
            return Mensaje;
        }

        public void Vender(int IdVenta)
        {
            List<Propiedades_BE.Detalle_Venta> ListaDV = new List<Propiedades_BE.Detalle_Venta>();
            Acceso.AbrirConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select IdArticulo,Cant from Detalle_Venta where IdVenta = " + IdVenta + " ";
            cmd.Connection = Acceso.Conexion;

            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Propiedades_BE.Detalle_Venta DV = new Propiedades_BE.Detalle_Venta();
                DV.IdArticulo = int.Parse(lector["IdArticulo"].ToString());
                DV.Cant = int.Parse(lector["Cant"].ToString());
                ListaDV.Add(DV);
            }
            lector.Close();
            Acceso.CerrarConexion();
            foreach (var item in ListaDV)
            {
                Acceso.EjecutarConsulta("update Articulo set Stock = (Stock - " + item.Cant + ") where IdArticulo = " + item.IdArticulo + "");
            }
        }

        public int ExisteVenta(int IdUsuario, DateTime Fecha)
        {
            int IdVenta = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select IdVenta from Venta where IdUsuario = " + IdUsuario + " and Fecha = " + Fecha.Ticks + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            IdVenta = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return IdVenta;
        }

        public void EjecutarConsulta(string Consulta)
        {
            Acceso.AbrirConexion();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Acceso.Conexion;
            Cmd.CommandText = Consulta;
            Cmd.ExecuteNonQuery();
            Acceso.CerrarConexion();
        }

        #region verificar integridad

        public List<Propiedades_BE.Venta> ListaVerificacion()
        {
            List<Propiedades_BE.Venta> Lista = new List<Propiedades_BE.Venta>();
            DataTable Tabla = Acceso.Leer("ListarVentaVVerificacion", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Venta V = new Propiedades_BE.Venta();
                V.IdVenta = int.Parse(R["IdVenta"].ToString());
                V.IdUsuario = int.Parse(R["IdUsuario"].ToString());
                V.Fecha = new DateTime(long.Parse(R["Fecha"].ToString()));
                V.FechaV = V.Fecha.Ticks; 
                V.DVH = int.Parse(R["DVH"].ToString());
                Lista.Add(V);
            }
            return Lista;
        }

        public string VerificarIntegridadVenta(int GlobalIdUsuario)
        {
            long Suma = 0;
            long DVH = 0;
            string msj = "";
            string msj2 = "";

            List<int> CamposFallidos = new List<int>();
            List<Propiedades_BE.Venta> DetalleV = ListaVerificacion();

            foreach (Propiedades_BE.Venta Dv in DetalleV.ToList())
            {
                string IdVenta = Dv.IdVenta.ToString();
                string IdUsuario = Dv.IdUsuario.ToString();
                string Fecha = Dv.FechaV.ToString();                
                string dvh = Dv.DVH.ToString();

                long IdVentaV = Seguridad.ObtenerAscii(IdVenta);
                long IdUsuarioV = Seguridad.ObtenerAscii(IdUsuario);
                long FechaV = Seguridad.ObtenerAscii(Fecha);
                long dvhV = long.Parse(dvh);

                Suma = IdVentaV + IdUsuarioV + FechaV;
                DVH += Suma;

                if (dvhV == Suma)
                {
                    DetalleV.Remove(Dv);
                }
            }
            if (DVH != Seguridad.VerificacionDVV("Venta"))
            {
                msj += "Se encontro un error en la tabla Venta \n";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Error en la tabla Venta", "Alta", 0);

                if (DVH < Seguridad.VerificacionDVV("Venta"))
                {
                    msj += "Posibilidad de eliminacion de 1 o mas registros de Venta \n";
                    Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Eliminacion registros Venta", "Alta", 0);
                }
            }
            foreach (Propiedades_BE.Venta MalCampo in DetalleV)
            {
                CamposFallidos.Add(MalCampo.IdVenta);
            }
            foreach (var item in CamposFallidos)
            {

                msj += "Se encontro un fallo en la fila con Id Venta: " + item + " \n";
                msj2 = "Error Venta IdVenta:" + item + "";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, msj2, "Alta", 0);
                msj2 = "";
            }
            return msj;
        }

        public void RecalcularDVH()
        {
            long suma = 0;

            List<Propiedades_BE.Venta> DetalleV = ListaVerificacion();
            foreach (Propiedades_BE.Venta Dv in DetalleV.ToList())
            {
                suma = 0;

                string IdVenta = Dv.IdVenta.ToString();
                string IdUsuario = Dv.IdUsuario.ToString();
                string Fecha = Dv.FechaV.ToString();
                string dvh = Dv.DVH.ToString();

                long IdVentaV = Seguridad.ObtenerAscii(IdVenta);
                long IdUsuarioV = Seguridad.ObtenerAscii(IdUsuario);
                long FechaV = Seguridad.ObtenerAscii(Fecha);
                long dvhV = long.Parse(dvh);

                suma = IdVentaV + IdUsuarioV + FechaV;
                Acceso.EjecutarConsulta("Update Venta set DVH = " + suma + " where IdVenta = " + IdVenta + "");
            }
        }

        #endregion
    }
}

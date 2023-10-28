using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Idioma
    {
        Acceso_BD Acceso = new Acceso_BD();
        Seguridad Seguridad = new Seguridad();

        public List<Propiedades_BE.Idioma> Listar()
        {
            List<Propiedades_BE.Idioma> ListarIdioma = new List<Propiedades_BE.Idioma>();
            DataTable tabla = Acceso.Leer("ListarIdioma", null);

            foreach (DataRow R in tabla.Rows)
            {
                Propiedades_BE.Idioma I = new Propiedades_BE.Idioma();
                I.IdIdioma = int.Parse(R["IdIdioma"].ToString());
                I.NombreIdioma = R["NombreIdioma"].ToString();
                I.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                ListarIdioma.Add(I);
            }
            return ListarIdioma;
        }

        public List<string> NombreIdioma()
        {
            List<string> NomIdioma = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string query = "select NombreIdioma from Idioma where BajaLogica <> 1";
                using (SqlCommand command = new SqlCommand(query, Acceso.Conexion))
                {
                    using (SqlDataReader lector = command.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            NomIdioma.Add(lector.GetString(0).ToString());
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return NomIdioma;
        }

        public int Alta(Propiedades_BE.Idioma I)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@IdIdioma", I.IdIdioma);
            Param[1] = new SqlParameter("@NombreIdioma", I.NombreIdioma);
            fa = Acceso.Escribir("AltaIdioma", Param);
            return fa;
        }

        public int Baja(Propiedades_BE.Idioma I)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@IdIdioma", I.IdIdioma);
            fa = Acceso.Escribir("BajaIdioma", Param);
            return fa;
        }

        public int Modificar(Propiedades_BE.Idioma I)
        {
            int fa = 0;
            SqlParameter[] Param = new SqlParameter[2];
            Param[0] = new SqlParameter("@IdIdioma", I.IdIdioma);
            Param[1] = new SqlParameter("@NombreIdioma", I.NombreIdioma);
            fa = Acceso.Escribir("ModificarIdioma", Param);
            return fa;
        }

        #region verificacion integridad 

        public List<Propiedades_BE.Idioma> ListaVerificacion()
        {
            List<Propiedades_BE.Idioma> Lista = new List<Propiedades_BE.Idioma>();
            DataTable Tabla = Acceso.Leer("ListarIdiomaVerificacion", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Idioma Idioma = new Propiedades_BE.Idioma();
                Idioma.IdIdioma = int.Parse(R["IdIdioma"].ToString());
                Idioma.NombreIdioma = R["NombreIdioma"].ToString();
                Idioma.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                Idioma.DVH = int.Parse(R["DVH"].ToString());
                Lista.Add(Idioma);
            }
            return Lista;
        }

        public string VerificarIntegridadIdioma(int GlobalIdUsuario)
        {
            long Suma = 0;
            long DVH = 0;
            string msj = "";
            string msj2 = "";

            List<int> CamposFallidos = new List<int>();
            List<Propiedades_BE.Idioma> DetalleI = ListaVerificacion();

            foreach (Propiedades_BE.Idioma Idioma in DetalleI.ToList())
            {
                string IdIdioma = Idioma.IdIdioma.ToString();
                string NombreIdioma = Idioma.NombreIdioma.ToString();
                string BajaLogica = Idioma.BajaLogica.ToString();
                string dvh = Idioma.DVH.ToString();
               

                long IdIdiomaI = Seguridad.ObtenerAscii(IdIdioma);
                long NombreIdiomaI = Seguridad.ObtenerAscii(NombreIdioma);
                long BajaLogicaI = Seguridad.ObtenerAscii(BajaLogica);                               
                long dvhI = long.Parse(dvh);

                Suma = IdIdiomaI + NombreIdiomaI + BajaLogicaI;
                DVH += Suma;

                if (dvhI == Suma)
                {
                    DetalleI.Remove(Idioma);
                }
            }
            if (DVH != Seguridad.VerificacionDVV("Idioma"))
            {
                msj += "Se encontro un error en la tabla Idioma <br />";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Error en la tabla Idioma", "Alta", 0);

                if (DVH < Seguridad.VerificacionDVV("Idioma"))
                {
                    msj += "Posibilidad de eliminacion de 1 o mas registros de Idioma <br />";
                    Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Eliminacion registros Idioma", "Alta", 0);
                }
            }
            foreach (Propiedades_BE.Idioma MalCampo in DetalleI)
            {
                CamposFallidos.Add(MalCampo.IdIdioma);
            }
            foreach (var item in CamposFallidos)
            {

                msj += "Se encontro un fallo en la fila con Id Detalle: " + item + " <br />";
                msj2 = "Error Idioma IdVejta:" + item + "";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, msj2, "Alta", 0);
                msj2 = "";
            }
            return msj;
        }

        public void RecalcularDVH()
        {
            long suma = 0;

            List<Propiedades_BE.Idioma> DetalleI = ListaVerificacion();
            foreach (Propiedades_BE.Idioma Idioma in DetalleI.ToList())
            {
                suma = 0;

                string IdIdioma = Idioma.IdIdioma.ToString();
                string NombreIdioma = Idioma.NombreIdioma.ToString();
                string BajaLogica = Idioma.BajaLogica.ToString();
                string dvh = Idioma.DVH.ToString();

                long IdIdiomaI = Seguridad.ObtenerAscii(IdIdioma);
                long NombreIdiomaI = Seguridad.ObtenerAscii(NombreIdioma);
                long BajaLogicaI = Seguridad.ObtenerAscii(BajaLogica);
                long dvhI = long.Parse(dvh);

                suma = IdIdiomaI + NombreIdiomaI + BajaLogicaI;
                Acceso.EjecutarConsulta("Update Idioma set DVH = " + suma + " where IdIdioma = " + IdIdioma + "");
            }
        }
        #endregion
    }
}

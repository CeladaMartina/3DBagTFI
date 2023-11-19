using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Producto
    {
        Acceso_BD Acceso = new Acceso_BD();
        Seguridad Seguridad = new Seguridad();

        #region Seguridad

        public void EjecutarConsulta(string Consulta)
        {
            Acceso.AbrirConexion();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = Acceso.Conexion;
            Cmd.CommandText = Consulta;
            Cmd.ExecuteNonQuery();
            Acceso.CerrarConexion();
        }

        public int SeleccionarIdArticulo(int CodProd)
        {
            int ID = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select IdArticulo from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            ID = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return ID;
        }

        public int SeleccionarCodArticulo(string DescripcionProducto)
        {
            int Cod = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select CodProd from Articulo where Descripcion = '" + DescripcionProducto + "'";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Cod = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Cod;
        }

        public string SeleccionarNombreArt(int CodProd)
        {
            string Descripcion = "";
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Descripcion from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Descripcion = Lector.GetString(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Descripcion;
        }

        public int VerificarCantStock(int IdArticulo)
        {
            int Cant = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Stock from Articulo where IdArticulo = " + IdArticulo + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Cant = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Cant;
        }

        public decimal SeleccionPUnit(int CodProd)
        {
            decimal PUnit = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select PUnit from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            PUnit = decimal.Parse(Lector.GetValue(0).ToString());
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return PUnit;
        }

        public List<string> CodProdArticulo()
        {
            List<string> CodProd = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select CodProd from Articulo where BajaLogica != 1";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            CodProd.Add(Lector.GetInt32(0).ToString());
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return CodProd;
        }

        public List<Propiedades_BE.Articulo> consultarCodProd(int CodProd)
        {
            List<Propiedades_BE.Articulo> codProdList = new List<Propiedades_BE.Articulo>();
            Acceso.AbrirConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from Articulo where CodProd = '" + CodProd + "'";

            cmd.Connection = Acceso.Conexion;
            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Propiedades_BE.Articulo A = new Propiedades_BE.Articulo();
                A.IdArticulo = int.Parse(lector["IdArticulo"].ToString());
                A.CodProd = int.Parse(lector["CodProd"].ToString());
                A.Nombre = lector["Nombre"].ToString();
                A.Descripcion = lector["Descripcion"].ToString();
                A.Material = lector["Material"].ToString();
                A.Stock = int.Parse(lector["Stock"].ToString());
                A.PUnit = decimal.Parse(lector["PUnit"].ToString());
                //se convierte la imagen  de byte a base64 y despues a img
                byte[] bytes = (byte[])lector["Imagen"];
                string str = Convert.ToBase64String(bytes);
                string URL = "data:Image/png;base64," + str;
                A.Imagen = URL;
                //fin de imagen
                codProdList.Add(A);
            }
            lector.Close();
            Acceso.CerrarConexion();
            return codProdList;
        }

        public List<string> DescripcionProd()
        {
            List<string> DescProd = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string query = "Select Descripcion From Articulo where BajaLogica != 1";
                using (SqlCommand cmd = new SqlCommand(query, Acceso.Conexion))
                {
                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            DescProd.Add(lector.GetString(0));
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return DescProd;
        }

        public int SeleccionarStock(int CodProd)
        {
            int Stock = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Stock from Articulo where CodProd = " + CodProd + "";
                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            Stock = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Stock;
        }

        #endregion

        #region ABML

        public List<Propiedades_BE.Articulo> Listar()
        {
            List<Propiedades_BE.Articulo> ListarArticulo = new List<Propiedades_BE.Articulo>();
            DataTable Tabla = Acceso.Leer("ListarArticulo", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Articulo A = new Propiedades_BE.Articulo();
                A.IdArticulo = int.Parse(R["IdArticulo"].ToString());
                A.CodProd = int.Parse(R["CodProd"].ToString());
                A.Nombre = (R["Nombre"].ToString());
                A.Descripcion = (R["Descripcion"].ToString());
                A.Material = (R["Material"].ToString());               
                A.Stock = int.Parse(R["Stock"].ToString());
                //eliminamos los ultimos 2 digitos
                string precio = R["PUnit"].ToString();
                precio = precio.Remove(precio.Length - 2);
                A.PUnit = decimal.Parse(precio);
                //se convierte la imagen  de byte a base64 y despues a img
                byte[] bytes = (byte[])R["Imagen"];
                string str = Convert.ToBase64String(bytes);
                string URL = "data:Image/png;base64," + str;                
                A.Imagen = URL;
                //fin de imagen
                A.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                ListarArticulo.Add(A);
            }
            return ListarArticulo;
        }

        public int Alta(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[9];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);            
            P[1] = new SqlParameter("@CodProd", A.CodProd);
            P[2] = new SqlParameter("@Nombre", A.Nombre);
            P[3] = new SqlParameter("@Descripcion", A.Descripcion);
            P[4] = new SqlParameter("@Material", A.Material);            
            P[5] = new SqlParameter("@Stock", A.Stock);
            P[6] = new SqlParameter("@PUnit", A.PUnit);
            P[7] = new SqlParameter("@Imagen", A.ImagenByte);
            P[8] = new SqlParameter("@DVH", A.DVH);
            fa = Acceso.Escribir("AltaArticulo", P);
            return fa;
        }

        public int Modificar(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[8];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);           
            P[1] = new SqlParameter("@CodProd", A.CodProd);
            P[2] = new SqlParameter("@Nombre", A.Nombre);
            P[3] = new SqlParameter("@Descripcion", A.Descripcion);
            P[4] = new SqlParameter("@Material", A.Material);
            P[5] = new SqlParameter("@Stock", A.Stock);
            P[6] = new SqlParameter("@PUnit", A.PUnit);            
            P[7] = new SqlParameter("@DVH", A.DVH);
            fa = Acceso.Escribir("ModificarArticulo", P);
            return fa;
        }

        public int GuardarImagenProd(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[2];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);
            P[1] = new SqlParameter("@Imagen", A.ImagenByte);
            fa = Acceso.Escribir("GuardarImagenProd", P);
            return fa;
        }

        public int Baja(Propiedades_BE.Articulo A)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[1];
            P[0] = new SqlParameter("@IdArticulo", A.IdArticulo);
            fa = Acceso.Escribir("BajaArticulo", P);
            return fa;
        }

        public List<Propiedades_BE.Articulo> ListarTopProductos()
        {
            List<Propiedades_BE.Articulo> ListarTopProductos = new List<Propiedades_BE.Articulo>();
            DataTable Tabla = Acceso.Leer("ListarTopProductos", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Articulo A = new Propiedades_BE.Articulo();
                A.CodProd = int.Parse(R["CodProd"].ToString());
                A.Nombre = R["Nombre"].ToString();
                A.Descripcion = R["Descripcion"].ToString();
                A.PUnit = decimal.Parse(R["Precio"].ToString());
                ListarTopProductos.Add(A);
            }
            return ListarTopProductos;
        }


        #endregion

        #region verificacion integral

        public List<Propiedades_BE.Articulo> ListaVerificacion()
        {
            List<Propiedades_BE.Articulo> Lista = new List<Propiedades_BE.Articulo>();
            DataTable Tabla = Acceso.Leer("ListarProductoVerificacion", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Articulo Prod = new Propiedades_BE.Articulo();
                Prod.IdArticulo = int.Parse(R["IdArticulo"].ToString());
                Prod.CodProd = int.Parse(R["CodProd"].ToString());
                Prod.Nombre = R["Nombre"].ToString();
                Prod.Descripcion = R["Descripcion"].ToString();
                Prod.Material = R["Material"].ToString();
                Prod.Stock = int.Parse(R["Stock"].ToString());
                Prod.PUnit = decimal.Parse(R["PUnit"].ToString());
                Prod.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                Prod.Imagen = R["Imagen"].ToString();
                Prod.DVH = int.Parse(R["DVH"].ToString());
                Lista.Add(Prod);
            }
            return Lista;
        }

        public string VerificarIntegridadProducto(int GlobalIdUsuario)
        {
            long Suma = 0;
            long DVH = 0;
            string msj = "";
            string msj2 = "";

            List<int> CamposFallidos = new List<int>();
            List<Propiedades_BE.Articulo> Articulo = ListaVerificacion();

            foreach (Propiedades_BE.Articulo Prod in Articulo.ToList())
            {
                string IdArticulo = Prod.IdArticulo.ToString();
                string CodProd = Prod.CodProd.ToString();
                string Nombre = Prod.Nombre;
                string Descripcion = Prod.Descripcion;
                string Material = Prod.Material;
                string Stock = Prod.Stock.ToString();
                string PUnit = Prod.PUnit.ToString();
                string BajaLogica = Prod.BajaLogica.ToString();
                string Imagen = Prod.Imagen;
                string dvh = Prod.DVH.ToString();

                long IdArticuloP = Seguridad.ObtenerAscii(IdArticulo);
                long CodProdP = Seguridad.ObtenerAscii(CodProd);
                long NombreP = Seguridad.ObtenerAscii(Nombre);
                long DescripcionP = Seguridad.ObtenerAscii(Descripcion);
                long MaterialP = Seguridad.ObtenerAscii(Material);
                long StockP = Seguridad.ObtenerAscii(Stock);
                long PUnitP = Seguridad.ObtenerAscii(PUnit);
                long BajaLogicaP = Seguridad.ObtenerAscii(BajaLogica);
                long ImagenP = Seguridad.ObtenerAscii(Imagen);
                long dvhP = long.Parse(dvh);

                Suma = IdArticuloP + CodProdP + NombreP + DescripcionP + MaterialP + StockP + PUnitP + BajaLogicaP + ImagenP;
                DVH += Suma;

                if (dvhP == Suma)
                {
                    Articulo.Remove(Prod);
                }
            }
            if (DVH != Seguridad.VerificacionDVV("Articulo"))
            {
                msj += "Se encontro un error en la tabla Articulo <br />";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Error en la tabla Articulo", "Alta", 0);

                if (DVH < Seguridad.VerificacionDVV("Artciulo"))
                {
                    msj += "Posibilidad de eliminacion de 1 o mas registros de Articulo <br />";
                    Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Eliminacion registros Articulo", "Alta", 0);
                }
            }
            foreach (Propiedades_BE.Articulo MalCampo in Articulo)
            {
                CamposFallidos.Add(MalCampo.IdArticulo);
            }
            foreach (var item in CamposFallidos)
            {

                msj += "Se encontro un fallo en la fila con Id Articulo: " + item + "";
                msj2 = "Error Articulo IdArticulo:" + item + "";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, msj2, "Alta", 0);
                msj2 = "";
            }
            return msj;
        }

        public void RecalcularDVH()
        {
            long suma = 0;

            List<Propiedades_BE.Articulo> prod = ListaVerificacion();
            foreach (Propiedades_BE.Articulo Prod in prod.ToList())
            {
                suma = 0;

                string IdArticulo = Prod.IdArticulo.ToString();
                string CodProd = Prod.CodProd.ToString();
                string Nombre = Prod.Nombre;
                string Descripcion = Prod.Descripcion;
                string Material = Prod.Material;
                string Stock = Prod.Stock.ToString();
                string PUnit = Prod.PUnit.ToString();
                string BajaLogica = Prod.BajaLogica.ToString();
                string Imagen = Prod.Imagen;
                string dvh = Prod.DVH.ToString();

                long IdArticuloP = Seguridad.ObtenerAscii(IdArticulo);
                long CodProdP = Seguridad.ObtenerAscii(CodProd);
                long NombreP = Seguridad.ObtenerAscii(Nombre);
                long DescripcionP = Seguridad.ObtenerAscii(Descripcion);
                long MaterialP = Seguridad.ObtenerAscii(Material);
                long StockP = Seguridad.ObtenerAscii(Stock);
                long PUnitP = Seguridad.ObtenerAscii(PUnit);
                long BajaLogicaP = Seguridad.ObtenerAscii(BajaLogica);
                long ImagenP = Seguridad.ObtenerAscii(Imagen);
                long dvhP = long.Parse(dvh);

                suma = IdArticuloP + CodProdP + NombreP + DescripcionP + MaterialP + StockP + PUnitP + BajaLogicaP + ImagenP;
                Acceso.EjecutarConsulta("Update Articulo set DVH = " + suma + " where IdArticulo = " + IdArticulo + "");
            }
        }
        #endregion
    }
}

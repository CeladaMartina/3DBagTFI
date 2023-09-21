using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Usuario
    {
        Acceso_BD Acceso = new Acceso_BD();
        Seguridad Seguridad = new Seguridad();

        long Dv;
        public void GenerarConexion(string Conexion)
        {
            Acceso.GenerarConexion(Conexion);
        }

        public string GetConexion()
        {
            return Acceso.GlobalConexion;
        }

        #region verificacion integridad
        public void RecalcularDVH()
        {
            long suma = 0;

            List<Propiedades_BE.Usuario> Us = Listar();
            foreach (Propiedades_BE.Usuario Usu in Us.ToList())
            {
                suma = 0;

                string id = Usu.IdUsuario.ToString();
                string nombre = Usu.Nombre;
                string nick = Seguridad.EncriptarAES(Usu.Nick);
                string contraseña = Usu.Contraseña;
                string mail = Usu.Mail;
                string estado = Usu.Estado.ToString();
                string contador = Usu.Contador.ToString();
                string bajalogica = Usu.BajaLogica.ToString();
                string ididioma = Usu.IdIdioma.ToString();
                //string idioma = Usu.Idioma;
                string dvh = Usu.DVH.ToString();

                long idU = Seguridad.ObtenerAscii(id);
                long nombreU = Seguridad.ObtenerAscii(nombre);
                long nickU = Seguridad.ObtenerAscii(nick);
                long contraseñaU = Seguridad.ObtenerAscii(contraseña);
                long mailU = Seguridad.ObtenerAscii(mail);
                long estadoU = Seguridad.ObtenerAscii(estado);
                long contadorU = Seguridad.ObtenerAscii(contador);
                long bajalogicaU = Seguridad.ObtenerAscii(bajalogica);
                long idiomaU = Seguridad.ObtenerAscii(ididioma);
                long digito = long.Parse(dvh);

                suma = idU + nombreU + nickU + contraseñaU + mailU + estadoU + contadorU + bajalogicaU + idiomaU;
                EjecutarConsulta("Update Usuario set DVH = " + suma + " where IdUsuario = " + id + "");
            }
        }

        public string VerificarIntegridadUsuario(int GlobalIdUsuario)
        {
            long Suma = 0;
            long DVH = 0;
            string msj = "";
            string msj2 = "";

            List<int> CamposFallidos = new List<int>();
            List<Propiedades_BE.Usuario> Us = Listar();

            foreach (Propiedades_BE.Usuario Usu in Us.ToList())
            {
                string Id = Usu.IdUsuario.ToString();
                string Nick = Seguridad.EncriptarAES(Usu.Nick);
                string Contraseña = Usu.Contraseña;
                string Nombre = Usu.Nombre;
                string Mail = Usu.Mail;
                string Estado = Usu.Estado.ToString();
                string Contador = Usu.Contador.ToString();
                //string Idioma = Usu.Idioma;
                string IdIdioma = Usu.IdIdioma.ToString();
                string BajaLogica = Usu.BajaLogica.ToString();
                string dvh = Usu.DVH.ToString();

                long IdU = Seguridad.ObtenerAscii(Id);
                long NickU = Seguridad.ObtenerAscii(Nick);
                long ContraseñaU = Seguridad.ObtenerAscii(Contraseña);
                long NombreU = Seguridad.ObtenerAscii(Nombre);
                long MailU = Seguridad.ObtenerAscii(Mail);
                long EstadoU = Seguridad.ObtenerAscii(Estado);
                long ContadorU = Seguridad.ObtenerAscii(Contador);
                long IdiomaU = Seguridad.ObtenerAscii(IdIdioma);
                long BajaLogicaU = Seguridad.ObtenerAscii(BajaLogica);
                long DVHU = long.Parse(dvh);

                Suma = IdU + NickU + ContraseñaU + NombreU + MailU + EstadoU + ContadorU + IdiomaU + BajaLogicaU;
                DVH += Suma;

                if (DVHU == Suma)
                {
                    Us.Remove(Usu);
                }
            }
            if (DVH != Seguridad.VerificacionDVV("Usuario"))
            {
                msj += "Se encontro un error en la tabla USUARIO \n";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Error en la tabla Usuario", "Alta", 0);

                if (DVH < Seguridad.VerificacionDVV("Usuario"))
                {
                    msj += "Posibilidad de eliminacion de 1 o mas registros Usuario \n";
                    Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, "Eliminacion registros Usuarios", "Alta", 0);
                }
            }
            foreach (Propiedades_BE.Usuario MalCampo in Us)
            {
                CamposFallidos.Add(MalCampo.IdUsuario);
            }
            foreach (var item in CamposFallidos)
            {
                msj += "Se encontro un fallo en la fila con ID: " + item + " \n";
                msj2 = "Error usuario en fila " + item + " ";
                Seguridad.CargarBitacora(GlobalIdUsuario, DateTime.Now, msj2, "Alta", 0);
                msj2 = "";
            }
            return msj;
        }

        #endregion

        public List<Propiedades_BE.Usuario> Listar()
        {
            List<Propiedades_BE.Usuario> ListarUsuario = new List<Propiedades_BE.Usuario>();
            DataTable Tabla = Acceso.Leer("ListarUsuario", null);

            foreach (DataRow R in Tabla.Rows)
            {
                Propiedades_BE.Usuario U = new Propiedades_BE.Usuario();
                U.IdUsuario = int.Parse(R["IdUsuario"].ToString());
                U.Nick = Seguridad.Desencriptar(R["Nick"].ToString());
                U.Contraseña = R["Contraseña"].ToString();
                U.Nombre = R["Nombre"].ToString();
                U.Mail = R["Mail"].ToString();
                U.Estado = bool.Parse(R["Estado"].ToString());
                U.Contador = int.Parse(R["Contador"].ToString());
                U.IdIdioma = int.Parse(R["IdIdioma"].ToString());
                U.Idioma = R["NombreIdioma"].ToString(); //Traigo nombre para mostrar
                U.BajaLogica = bool.Parse(R["BajaLogica"].ToString());
                U.DVH = int.Parse(R["DVH"].ToString());

                ListarUsuario.Add(U);
            }
            return ListarUsuario;
        }

        public bool VerificarEstados(string Nick)
        {
            bool i = false;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryEstado = "select * from Usuario where (Nick = '" + Seguridad.EncriptarAES(Nick) + "' and Estado = 1) or (Nick = '" + Seguridad.EncriptarAES(Nick) + "' and BajaLogica = 1)";

                using (SqlCommand Cmd = new SqlCommand(QueryEstado, Acceso.Conexion))
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

        public int VerificarContador(string Nick)
        {
            int i = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryContador = "select Contador from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";

                using (SqlCommand Cmd = new SqlCommand(QueryContador, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            i = Lector.GetInt32(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return i;
        }

        public void EjecutarConsulta(string Consulta)
        {
            Acceso.EjecutarConsulta(Consulta);
        }

        public int VerificarUsuarioContraseña(string Nick, string Contraseña, int Integridad)
        {
            int i = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryUsCon = "select * from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "' and Contraseña = '" + Seguridad.EncriptarMD5(Contraseña.ToString()) + "'";

                using (SqlCommand Cmd = new SqlCommand(QueryUsCon, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            i = 1;
                        }
                        else
                        {
                            if (Integridad == 0)
                            {
                                EjecutarConsulta("Update Usuario set Contador = ((select Contador from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "' ) + 1) where Nick = '" + Seguridad.EncriptarAES(Nick) + "' and Contador < 3");
                                string QueryDv = "select * from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";
                                Dv = Seguridad.CalcularDVH(QueryDv, "Usuario");
                                EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + Seguridad.EncriptarAES(Nick) + "'");
                                Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));
                            }
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return i;
        }

        public void BloquearUsuario(string Nick)
        {
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryBloquear = "Update Usuario set Estado = 1 where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";

                using (SqlCommand Cmd = new SqlCommand(QueryBloquear, Acceso.Conexion))
                {
                    Cmd.ExecuteNonQuery();
                    string QueryDv = "select * from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";
                    Dv = Seguridad.CalcularDVH(QueryDv, "Usuario");
                    EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + Seguridad.EncriptarAES(Nick) + "'");
                    Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));
                }
                Acceso.CerrarConexion();
            }
        }

        public void ReiniciarIntentos(string Nick)
        {
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryReiniciar = "Update Usuario set Contador = 0 where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";

                using (SqlCommand Cmd = new SqlCommand(QueryReiniciar, Acceso.Conexion))
                {
                    Cmd.ExecuteNonQuery();
                    string QueryDv = "select * from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";
                    Dv = Seguridad.CalcularDVH(QueryDv, "Usuario");
                    EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + Seguridad.EncriptarAES(Nick) + "'");
                    Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));
                }
                Acceso.CerrarConexion();
            }
        }

        public void Desbloquear(string Nick)
        {
            try
            {
                string Query = "Update Usuario set Estado = 0 where Nick = '" + Nick + "'";
                EjecutarConsulta(Query);
                ReiniciarIntentos(Seguridad.Desencriptar(Nick));
                string QueryDv = "Select * from Usuario where Nick = '" + Nick + "'";
                Dv = Seguridad.CalcularDVH(Query, "Usuario");
                EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + Nick + "'");
                Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));
            }
            catch (Exception)
            {

            }
        }

        public bool VerificarNickMail(string Nick, string Mail)
        {
            bool i = false;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "Select * from Usuario where Nick = '" + Nick + "' and Mail = '" + Mail + "'";
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

        public int SeleccionarIDNick(string Nick)
        {
            int Id = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryID = "select IdUsuario from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";

                using (SqlCommand Cmd = new SqlCommand(QueryID, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            Id = Lector.GetInt32(0);
                        }
                        else
                        {
                            Id = -1;
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Id;
        }

        //traigo el nick del usuario logueado
        public string SeleccionarNick(int IdUsuario)
        {
            string nick = "";
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string Query = "select Nombre from Usuario where IdUsuario = '" + IdUsuario + "'";

                using (SqlCommand Cmd = new SqlCommand(Query, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            nick = Lector.GetString(0);
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return nick;
        }

        public List<string> NickUsuario()
        {
            List<string> NickUs = new List<string>();
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryPatentes = "select Nick from Usuario";

                using (SqlCommand Cmd = new SqlCommand(QueryPatentes, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        while (Lector.Read())
                        {
                            NickUs.Add(Seguridad.Desencriptar(Lector.GetString(0).ToString()));
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return NickUs;
        }

        public List<Propiedades_BE.Usuario> consultarNick(string Nick)
        {
            List<Propiedades_BE.Usuario> NickUs = new List<Propiedades_BE.Usuario>();
            Acceso.AbrirConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "'";
            
            cmd.Connection = Acceso.Conexion;
            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Propiedades_BE.Usuario U = new Propiedades_BE.Usuario();
                U.IdUsuario = int.Parse(lector["IdUsuario"].ToString());
                U.Nick = Seguridad.Desencriptar(lector["Nick"].ToString());
                U.Nombre = lector["Nombre"].ToString();
                U.Mail = lector["Mail"].ToString();
                U.Estado = bool.Parse(lector["Estado"].ToString());
                U.IdIdioma = int.Parse(lector["IdIdioma"].ToString());
                U.BajaLogica = bool.Parse(lector["BajaLogica"].ToString());
                NickUs.Add(U);
            }
            lector.Close();
            Acceso.CerrarConexion();
            return NickUs;
        }

        public int AltaUsuario(Propiedades_BE.Usuario U)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[9];
            P[0] = new SqlParameter("@IdUsuario", U.IdUsuario);
            P[1] = new SqlParameter("@Nick", U.Nick);
            P[2] = new SqlParameter("@Contraseña", U.Contraseña);
            P[3] = new SqlParameter("@Nombre", U.Nombre);
            P[4] = new SqlParameter("@Mail", U.Mail);
            P[5] = new SqlParameter("@Estado", U.Estado);
            P[6] = new SqlParameter("@Contador", U.Contador);
            P[7] = new SqlParameter("@Idioma", U.Idioma);
            P[8] = new SqlParameter("@DVH", U.DVH);
            fa = Acceso.Escribir("AltaUsuario", P);
            return fa;
        }

        public int Modificar(Propiedades_BE.Usuario U)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[8];
            P[0] = new SqlParameter("@IdUsuario", U.IdUsuario);
            P[1] = new SqlParameter("@Nick", U.Nick);
            P[2] = new SqlParameter("@Nombre", U.Nombre);
            P[3] = new SqlParameter("@Mail", U.Mail);
            P[4] = new SqlParameter("@Estado", U.Estado);
            P[5] = new SqlParameter("@Contador", U.Contador);
            P[6] = new SqlParameter("@Idioma", U.Idioma);
            P[7] = new SqlParameter("@DVH", U.DVH);
            fa = Acceso.Escribir("ModificarUsuario", P);
            return fa;
        }

        public int Baja(Propiedades_BE.Usuario U)
        {
            int fa = 0;
            SqlParameter[] P = new SqlParameter[2];
            P[0] = new SqlParameter("@IdUsuario", U.IdUsuario);
            P[1] = new SqlParameter("@DVH", U.DVH);
            fa = Acceso.Escribir("BajaUsuario", P);
            return fa;
        }

        public void GuardarPermiso(Propiedades_BE.Usuario U)
        {
            try
            {
                var cnn = new SqlConnection(Acceso.GlobalConexion);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                var sql = $@"delete from usuarios_permisos where id_usuario=@id;";

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("id", U.IdUsuario));
                cmd.ExecuteNonQuery();

                foreach (var item in U.Permisos)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;

                    sql = $@"insert into usuarios_permisos (id_usuario,id_permiso) values (@id_usuario,@id_permiso) ";

                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("id_usuario", U.IdUsuario));
                    cmd.Parameters.Add(new SqlParameter("id_permiso", item.Id));

                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acceso_DAL;

namespace Negocio_BLL
{
    public class Usuario
    {
        Acceso_DAL.Usuario Mapper = new Acceso_DAL.Usuario();        
        Propiedades_BE.Usuario UsuarioTemp = new Propiedades_BE.Usuario();
        Seguridad Seguridad = new Seguridad();

        #region Seguridad
        public void GenerarConexion(string usuario, string basedatos)
        {
            string Conexion = "";
            string DataSourceRaw = "";
            string DataSourceRaw2 = "";
            DataSourceRaw = usuario;
            DataSourceRaw2 = basedatos;           
            Conexion = @"Data Source=" + DataSourceRaw + ";Initial Catalog=" + DataSourceRaw2 + ";Integrated Security=True; MultipleActiveResultSets=true";
            Mapper.GenerarConexion(Conexion);
        }

        public string GetConexion()
        {
            return Mapper.GetConexion();
        }        

        public string VerificarIntegridadUsuario(int GlobalIdUsuario)
        {
            return Mapper.VerificarIntegridadUsuario(GlobalIdUsuario);
        }

        public bool VerificarEstado(string Nick)
        {
            return Mapper.VerificarEstados(Nick);
        }

        public int VerificarContador(string Nick)
        {
            return Mapper.VerificarContador(Nick);
        }

        public void EjecutarConsulta(string Consulta)
        {
            Mapper.EjecutarConsulta(Consulta);
        }

        public void BloquearUsuario(string Nick)
        {
            Mapper.BloquearUsuario(Nick);
        }

        public void ReiniciarIntentos(string Nick)
        {
            Mapper.ReiniciarIntentos(Nick);
        }

        public int VerificarUsuarioContraseña(string Nick, string Contraseña, int Integridad)
        {
            return Mapper.VerificarUsuarioContraseña(Nick, Contraseña, Integridad);
        }

        public void RecalcularDVH()
        {
            Mapper.RecalcularDVH();
        }

        #endregion

        #region usuario

        public List<string> NickUsuario()
        {
            return Mapper.NickUsuario();
        }

        public int SeleccionarIDNick(string Nick)
        {
            return Mapper.SeleccionarIDNick(Nick);
        }

        public void Desbloquear(string Nick)
        {
            Mapper.Desbloquear(Nick);
        }

        public bool VerificarNickMail(string Nick, string Mail)
        {
            return Mapper.VerificarNickMail(Nick, Mail);
        }

        public void LogIn(Propiedades_BE.Usuario U)
        {
            (new Acceso_DAL.Permisos()).FillUserComponents(U);
            Propiedades_BE.SingletonLogin.GetInstance.LogIn(U);
        }

        public void LogOut()
        {
            Propiedades_BE.SingletonLogin.GetInstance.LogOut();
        }

        public void GuardarPermisos(Propiedades_BE.Usuario u)
        {
            Mapper.GuardarPermiso(u);
        }

        public void ConfirmarCambioContraseña(string Nick, string Contraseña, string Mail)
        {
            string Query = "Update Usuario set Contraseña= '" + Seguridad.EncriptarMD5(Contraseña) + "' where Nick = '" + Seguridad.EncriptarAES(Nick) + "' and Mail= '" + Mail + "'";
            Mapper.EjecutarConsulta(Query);
            long Dv;
            Dv = Seguridad.CalcularDVH("Select * from Usuario where Nick = '" + Seguridad.EncriptarAES(Nick) + "' and Mail = '" + Mail + "'", "Usuario");
            Mapper.EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + Seguridad.EncriptarAES(Nick) + "' and Mail= '" + Mail + "'");
            Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));
        }
        #endregion

        #region ABML Usuario
        public List<Propiedades_BE.Usuario> consultarNick(string Nick)
        {
            return Mapper.consultarNick(Nick);
        }

        public List<Propiedades_BE.Usuario> Listar()
        {
            List<Propiedades_BE.Usuario> Lista = Mapper.Listar();
            return Lista;
        }

        public int AltaUsuario(string Nick, string Contraseña, string Nombre, string Mail, bool Estado, int Contador, string Idioma, int DVH)
        {
            UsuarioTemp.IdUsuario = SeleccionarIDNick(Nick);
            UsuarioTemp.Nick = Seguridad.EncriptarAES(Nick);
            UsuarioTemp.Contraseña = Seguridad.EncriptarMD5(Contraseña);
            UsuarioTemp.Nombre = Nombre;
            UsuarioTemp.Mail = Mail;
            UsuarioTemp.Estado = Estado;
            UsuarioTemp.Contador = Contador;
            UsuarioTemp.Idioma = Idioma;
            UsuarioTemp.DVH = DVH;

            int i = Mapper.AltaUsuario(UsuarioTemp);
            long Dv = Seguridad.CalcularDVH("select * From Usuario where Nick = '" + UsuarioTemp.Nick + "'", "Usuario");
            EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + UsuarioTemp.Nick + "'");
            Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));

            return i;
        }

        public int Modificar(int IdUsuario, string Nick, string Nombre, string Mail, bool Estado, int Contador, string Idioma, int DVH)
        {
            UsuarioTemp.IdUsuario = IdUsuario;
            UsuarioTemp.Nick = Seguridad.EncriptarAES(Nick);
            UsuarioTemp.Nombre = Nombre;
            UsuarioTemp.Mail = Mail;
            UsuarioTemp.Estado = Estado;
            UsuarioTemp.Contador = Contador;
            UsuarioTemp.Idioma = Idioma;
            UsuarioTemp.DVH = DVH;


            int i = Mapper.Modificar(UsuarioTemp);
            long Dv = Seguridad.CalcularDVH("select * From Usuario where Nick = '" + UsuarioTemp.Nick + "'", "Usuario");
            EjecutarConsulta("Update Usuario set DVH = " + Dv + " where Nick = '" + UsuarioTemp.Nick + "'");
            Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));

            return i;
        }

        public int Baja(int IdUsuario, int DVH)
        {
            UsuarioTemp.IdUsuario = IdUsuario;
            UsuarioTemp.DVH = DVH;

            int i = Mapper.Baja(UsuarioTemp);
            long Dv = Seguridad.CalcularDVH("select * From Usuario where IdUsuario = '" + UsuarioTemp.IdUsuario + "'", "Usuario");
            EjecutarConsulta("Update Usuario set DVH = " + Dv + " where IdUsuario = '" + UsuarioTemp.IdUsuario + "'");
            Seguridad.ActualizarDVV("Usuario", Seguridad.SumaDVV("Usuario"));

            return i;
        }

        public string TraerMail(int IdUsuario)
        {
            return Mapper.TraerMail(IdUsuario);
        } 
        #endregion



    }
}

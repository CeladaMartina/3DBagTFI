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

        #region Seguridad

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

        public void RecalcularDVH()
        {
            Mapper.RecalcularDVH();
        }

        #endregion

        public void LogIn(Propiedades_BE.Usuario U)
        {
            (new Acceso_DAL.Permisos()).FillUserComponents(U);
            Propiedades_BE.SingletonLogin.GetInstance.LogIn(U);
        }

        public void LogOut()
        {
            Propiedades_BE.SingletonLogin.GetInstance.LogOut();
        }
    }
}

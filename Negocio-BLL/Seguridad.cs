using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Negocio_BLL
{
    public class Seguridad
    {
        Acceso_DAL.Seguridad Mapper = new Acceso_DAL.Seguridad();
        Acceso_DAL.Acceso_BD Acceso = new Acceso_DAL.Acceso_BD();

        #region DigitoVerificador

        public long ObtenerAscii(string Texto)
        {
            return Mapper.ObtenerAscii(Texto);
        }

        public long CalcularDVH(string Consulta, string Tabla)
        {
            return Mapper.CalcularDVH(Consulta, Tabla);
        }

        public int VerificacionDVV(string Tabla)
        {
            return Mapper.VerificacionDVV(Tabla);
        }

        public int SumaDVV(string Tabla)
        {
            return Mapper.SumaDVV(Tabla);
        }

        public void ActualizarDVV(string NombreTabla, int Suma)
        {
            Mapper.ActualizarDVV(NombreTabla, Suma);
        }

        public void RecalcularDVV()
        {
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Usuario) where NombreTabla = 'Usuario'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Bitacora) where NombreTabla = 'Bitacora'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Detalle_Venta) where NombreTabla = 'Detalle_Venta'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Permiso) where NombreTabla = 'Permiso'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Articulo) where NombreTabla = 'Articulo'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Idioma) where NombreTabla = 'Idioma'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Venta) where NombreTabla = 'Venta'");
        }

        public long ObtenerDVV(string NombreTabla)
        {
            return Mapper.ObtenerDVV(NombreTabla);
        }

        public void EjecutarConsulta(string Consulta)
        {
            Acceso.EjecutarConsulta(Consulta);
        }


        #endregion

        #region Encriptacion

        public string EncriptarMD5(string Texto)
        {
            return Mapper.EncriptarMD5(Texto);
        }

        public string EncriptarAES(string Texto)
        {
            return Mapper.EncriptarAES(Texto);
        }

        public string Desencriptar(string Texto)
        {
            return Mapper.Desencriptar(Texto);
        }
        #endregion

        #region contraseña
        public bool ValidarClave(string clave)
        {
            return Mapper.ValidarClave(clave);
        }

        public string GenerarClave()
        {
            return Mapper.GenerarClave();
        }
        #endregion


        #region backuprestore

        public string GenerarBackUp(string Nombre, string Ruta)
        {
            return Mapper.GenerarBackup(Nombre, Ruta);
        }

        public string Restaurar(string ruta)
        {
            return Mapper.Restaurar(ruta);
        }

        #endregion

        #region Bitacora

        public List<Propiedades_BE.Bitacora> Listar()
        {
            return Mapper.Listar();
        }

        public List<Propiedades_BE.Bitacora> ConsultarBitacora(DateTime fechaDesde, DateTime fechaHasta, string consultaCriticidad, string consultaUsuario)
        {
            return Mapper.ConsultarBitacora(fechaDesde, fechaHasta, consultaCriticidad, consultaUsuario);
        }

        public void CargarBitacora(int IdUsuario, DateTime Fecha, string Descripcion, string Criticidad, int DVH)
        {
            Mapper.CargarBitacora(IdUsuario, Fecha, Descripcion, Criticidad, DVH);
        }

        //dejar x ahora
        public void SerializarBitacora(string Ruta, DataGridView GridViewBitacora)
        {
            List<Propiedades_BE.Bitacora> ListarBitacora = new List<Propiedades_BE.Bitacora>();

            foreach (DataGridViewRow DataBitacora in GridViewBitacora.Rows)
            {
                Propiedades_BE.Bitacora Bi = new Propiedades_BE.Bitacora();
                Bi.NickUs = DataBitacora.Cells["NickUs"].Value.ToString();
                Bi.Descripcion = DataBitacora.Cells["Descripcion"].Value.ToString();
                Bi.Fecha = new DateTime(long.Parse(DataBitacora.Cells["Fecha"].Value.ToString()));
                Bi.Criticidad = DataBitacora.Cells["Criticidad"].Value.ToString();
                ListarBitacora.Add(Bi);
            }

            Directory.CreateDirectory(Ruta);
            DirectorySecurity sec = Directory.GetAccessControl(Ruta);

            SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(Ruta, sec);

            XmlSerializer Formateador = new XmlSerializer(typeof(List<Propiedades_BE.Bitacora>));

            string fecha_final = DateTime.Now.ToShortDateString().Replace("/", "-");
            XmlTextWriter Stream = new XmlTextWriter("" + Ruta + "\\Serializacion_Bitacora_" + fecha_final + ".xml", Encoding.UTF8);

            Stream.Formatting = Formatting.Indented;
            Stream.Indentation = 1;
            Stream.IndentChar = '\t';

            Formateador.Serialize(Stream, ListarBitacora);
            Stream.Close();
        }

        //verificacion integral
        public string VerificarIntegridadBitacora(int GlobalIdUsuario)
        {
            return Mapper.VerificarIntegridadBitacora(GlobalIdUsuario);
        }

        public void RecalcularDVH()
        {
            Mapper.RecalcularDVH();
        }

        //webservice
        public List<Propiedades_BE.Bitacora> UsuarioMasLogin()
        {
            return Mapper.UsuarioMasLogin();
        }
        #endregion
    }
}

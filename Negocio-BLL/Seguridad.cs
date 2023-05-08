using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Bitacora) where NombreTabla = 'Bitacora'");
            Mapper.EjecutarConsulta("Update DVV set DVV.DVV = (select ISNULL(SUM(DVH), 0) from Detalle_Venta) where NombreTabla = 'Detalle_Venta'");
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

    }
}

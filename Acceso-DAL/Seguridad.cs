using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Seguridad
    {
        Acceso_BD Acceso = new Acceso_BD();
        public static Random Random = new Random();

        public void EjecutarConsulta(string Consulta)
        {
            Acceso.EjecutarConsulta(Consulta);
        }

        #region DigitoVerificador

        long AsciiHorizontal;

        public DataSet EjecutarConsultaDSTabla(string Consulta, string Tabla)
        {
            DataSet Ds = new DataSet();
            Acceso.AbrirConexion();
            SqlDataAdapter DA = new SqlDataAdapter(Consulta, Acceso.Conexion);
            DA.Fill(Ds, Tabla);
            Acceso.CerrarConexion();
            return Ds;
        }

        public long ObtenerAscii(string Texto)
        {
            long SumAscii = 0;

            if (Texto != null)
            {
                int i;
                for (i = 0; i <= Texto.Length - 1; i++)
                {
                    SumAscii += Convert.ToInt64((Strings.Asc(Texto[i]).ToString()));
                }
            }
            return SumAscii;
        }

        public long CalcularDVH(string Consulta, string Tabla)
        {
            AsciiHorizontal = default(long);
            DataSet ds = new DataSet();
            ds = EjecutarConsultaDSTabla(Consulta, Tabla);

            for (int i = 0; i <= Information.UBound(ds.Tables[0].Rows[0].ItemArray, 1) - 1; i++)
            {
                try
                {
                    AsciiHorizontal += ObtenerAscii((ds.Tables[0].Rows[0].ItemArray[i]).ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return AsciiHorizontal;
        }

        public int VerificacionDVV(string Tabla)
        {
            int i = 0;
            Acceso.AbrirConexion();
            string Consulta = "select DVV from DVV where NombreTabla = '" + Tabla + "'";
            SqlCommand Cmd = new SqlCommand(Consulta, Acceso.Conexion);
            SqlDataReader Dr = Cmd.ExecuteReader();
            if (Dr.HasRows)
            {
                try
                {
                    Dr.Read();
                    i = Dr.GetInt32(0);
                }
                catch (Exception)
                {
                    i = 0;
                }
            }
            Acceso.CerrarConexion();
            return i;
        }

        public int SumaDVV(string Tabla)
        {
            int i = 0;
            Acceso.AbrirConexion();
            string Consulta = " SELECT SUM(DVH) FROM " + Tabla + "";
            SqlCommand Cmd = new SqlCommand(Consulta, Acceso.Conexion);
            SqlDataReader Dr = Cmd.ExecuteReader();

            if (Dr.HasRows)
            {
                try
                {
                    Dr.Read();
                    i = Dr.GetInt32(0);
                }
                catch (Exception)
                {
                    i = 0;
                }
            }
            Acceso.CerrarConexion();
            return i;
        }

        public void ActualizarDVV(string NombreTabla, int Suma)
        {
            Acceso.AbrirConexion();
            string Consulta = "Update DVV set DVV.DVV = " + Suma + " WHERE DVV.NombreTabla = '" + NombreTabla + "'";
            SqlCommand Cmd = new SqlCommand(Consulta, Acceso.Conexion);
            Cmd.ExecuteNonQuery();
            Acceso.CerrarConexion();
        }

        public long ObtenerDVV(string NombreTabla)
        {
            long i = 0;
            Acceso.AbrirConexion();
            string Consulta = "select DVV from DVV where NombreTabla = '" + NombreTabla + "'";
            SqlCommand Cmd = new SqlCommand(Consulta, Acceso.Conexion);
            SqlDataReader Dr = Cmd.ExecuteReader();

            if (Dr.HasRows)
            {
                Dr.Read();
                i = Dr.GetInt32(0);
            }

            Acceso.CerrarConexion();
            return i;
        }

        #endregion

        #region Encriptacion

        private static string Key = "asdfqwertuniolnmajeolikoajskidjq";
        private static string IV = "asdfghjklopqwert";

        public string EncriptarMD5(string Texto)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Texto));
            byte[] Resultado = MD5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Resultado.Length; i++)
            {
                str.Append(Resultado[i].ToString("X2"));
            }
            return str.ToString();
        }

        public string EncriptarAES(string Texto)
        {
            byte[] TextoPlano = System.Text.ASCIIEncoding.ASCII.GetBytes(Texto);
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider();
            AES.BlockSize = 128;
            AES.KeySize = 256;
            AES.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
            AES.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CBC;
            ICryptoTransform Crypto = AES.CreateEncryptor(AES.Key, AES.IV);
            byte[] Encriptar = Crypto.TransformFinalBlock(TextoPlano, 0, TextoPlano.Length);
            Crypto.Dispose();
            return Convert.ToBase64String(Encriptar);
        }

        public string Desencriptar(string Texto)
        {
            byte[] Encriptado = Convert.FromBase64String(Texto);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(Key);
            aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform Crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] des = Crypto.TransformFinalBlock(Encriptado, 0, Encriptado.Length);
            Crypto.Dispose();
            return System.Text.ASCIIEncoding.ASCII.GetString(des);
        }
        #endregion
    }
}

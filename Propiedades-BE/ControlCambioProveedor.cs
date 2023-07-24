using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class ControlCambioProveedor
    {
        private int _idproveedor;

        public int IdProveedor
        {
            get { return _idproveedor; }
            set { _idproveedor = value; }
        }

        private string _cuit;

        public string CUIT
        {
            get { return _cuit; }
            set { _cuit = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private DateTime _fechanac;

        public DateTime FechaNac
        {
            get { return _fechanac; }
            set { _fechanac = value; }
        }

        private int _tel;

        public int Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        private string _mail;

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private bool _bajalogica;

        public bool BajaLogica
        {
            get { return _bajalogica; }
            set { _bajalogica = value; }
        }

        private int _idusuario;

        public int IdUsuario
        {
            get { return _idusuario; }
            set { _idusuario = value; }
        }


        private string _nick;

        public string Nick
        {
            get { return _nick; }
            set { _nick = value; }
        }


        private DateTime _fechamo;

        public DateTime FechaModificacion
        {
            get { return _fechamo; }
            set { _fechamo = value; }
        }
    }
}

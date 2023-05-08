using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class Usuario
    {
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
        private string _contraseña;

        public string Contraseña
        {
            get { return _contraseña; }
            set { _contraseña = value; }
        }
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _mail;

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }
        private bool _estado;

        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        private int _contador;

        public int Contador
        {
            get { return _contador; }
            set { _contador = value; }
        }
        private int _IdIdioma;

        public int IdIdioma
        {
            get { return _IdIdioma; }
            set { _IdIdioma = value; }
        }

        private string _idioma;

        public string Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }
        private bool _baja;

        public bool BajaLogica
        {
            get { return _baja; }
            set { _baja = value; }
        }
        private int _dvh;

        public int DVH
        {
            get { return _dvh; }
            set { _dvh = value; }
        }

        private string _nomperm;

        public string NombrePermiso
        {
            get { return _nomperm; }
            set { _nomperm = value; }
        }

        List<Componente> _permisos;
        public Usuario()
        {
            _permisos = new List<Componente>();
        }

        public List<Componente> Permisos
        {
            get
            {
                return _permisos;
            }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}

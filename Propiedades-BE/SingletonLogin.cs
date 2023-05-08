using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propiedades_BE
{
    public class SingletonLogin
    {
        Usuario _usuario;
        static SingletonLogin _sesion;

        static int _globalid;

        public static int GlobalIdUsuario
        {
            get { return _globalid; }
            set { _globalid = value; }
        }

        static int _globalIntegridad = 0;

        public static int GlobalIntegridad
        {
            get { return _globalIntegridad; }
            set { _globalIntegridad = value; }
        }

        public static void SumarIntegridadGeneral(int sum)
        {
            GlobalIntegridad += sum;
        }

        public static void SetIdUsuario(Propiedades_BE.Usuario Us)
        {
            GlobalIdUsuario = Us.IdUsuario;
        }

        public static SingletonLogin GetInstance
        {
            get
            {
                if (_sesion == null) _sesion = new SingletonLogin();
                return _sesion;
            }
        }

        public bool IsLoggedIn()
        {
            return _usuario != null;
        }

        public void LogIn(Propiedades_BE.Usuario Us)
        {
            _sesion = new SingletonLogin();
            GlobalIdUsuario = Us.IdUsuario;
            _sesion._usuario = Us;
        }

        public void LogOut()
        {
            _sesion._usuario = null;
            GlobalIdUsuario = 0;
            GlobalIntegridad = 0;
        }

        bool isInRole(Componente c, TipoPermiso permiso, bool existe)
        {
            if (c.Permiso.Equals(permiso))
            {
                existe = true;
            }
            else
            {
                foreach (var item in c.Hijos)
                {
                    existe = isInRole(item, permiso, existe);
                    if (existe) return true;
                }
            }
            return existe;
        }

        public bool IsInRole(TipoPermiso permiso)
        {
            bool existe = false;
            foreach (var item in _usuario.Permisos)
            {
                if (item.Permiso.Equals(permiso)) return true;
                else
                {
                    existe = isInRole(item, permiso, existe);
                    if (existe) return true;
                }
            }
            return existe;
        }
    }
}


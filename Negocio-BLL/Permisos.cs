using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class Permisos
    {
        Acceso_DAL.Permisos _permisos;


        public Permisos()
        {
            _permisos = new Acceso_DAL.Permisos();
        }

        #region Componente
        public Propiedades_BE.Componente GuardarComponente(Propiedades_BE.Componente p, bool esfamilia)
        {
            return _permisos.GuardarComponente(p, esfamilia);
        }

        public Propiedades_BE.Componente EliminarComponente(Propiedades_BE.Componente p, bool esfamilia)
        {
            return _permisos.EliminarComponente(p, esfamilia);
        }

        public IList<Propiedades_BE.Componente> GetAll(string familia)
        {
            return _permisos.GetAll(familia);

        }

        public void FillUserComponents(Propiedades_BE.Usuario u)
        {
            _permisos.FillUserComponents(u);
        }

        //devuelve en forma de lista los permisos del usuario, para ponerlo en  listbox
        public IList<Propiedades_BE.Componente> FillUserComponentsList(Propiedades_BE.Usuario u)
        {
            return _permisos.FillUserComponentsList(u);
        }

        //devuelve en forma de lista las familias del usuario, para ponerlo en  listbox
        public IList<Propiedades_BE.Componente> FillUserComponentsListF(Propiedades_BE.Usuario u)
        {
            return _permisos.FillUserComponentsListF(u);
        }

        public void FillFamilyComponents(Propiedades_BE.Familia familia)
        {
            _permisos.FillFamilyComponents(familia);
        }
        #endregion

        #region Familia
        public void GuardarFamilia(Propiedades_BE.Familia c)
        {
            _permisos.GuardarFamilia(c);
        }

        public void EliminarFamilia(string NomFam, string NomPat, bool isfam)
        {
            _permisos.EliminarFamilia(NomFam, NomPat, isfam);
        }

        public void ModificarFamilia(string NomOriginal, string NomNuevo)
        {
            _permisos.ModificarFamilia(NomOriginal, NomNuevo);
        }

        public IList<Propiedades_BE.Familia> GetAllFamilias()
        {
            return _permisos.GetAllFamilias();
        }

        public bool Contains(Propiedades_BE.Componente component, Propiedades_BE.Componente includes)
        {
            bool exist = false;
            if (component.Id.Equals(includes.Id))
            {
                exist = true;
            }
            else
            {
                foreach (Propiedades_BE.Componente item in component.Hijos)
                {
                    if (Contains(item, includes))
                    {
                        return true;
                    }

                }
            }
            return exist;
        }
        #endregion

        #region VerificarIntegridad

        public string VerificarIntegridadPermiso(int GlobalIdUsuario)
        {
            return _permisos.VerificarIntegridadPermiso(GlobalIdUsuario);
        }

        public void RecalcularDVH()
        {
            _permisos.RecalcularDVH();
        }

        #endregion

        #region Patentes
        public IList<Propiedades_BE.Patente> GetAllPatentes()
        {
            return _permisos.GetAllPatentes();
        }


        public int traerIDPermiso(string nombre)
        {
            return _permisos.traerIDPermiso(nombre);
        }

        public string traerPermiso(int id)
        {
            return _permisos.traerPermiso(id);
        }
        #endregion

        #region Verificar Borrado
        public int VerificarBorradoFam(string NombreFamilia)
        {
            return _permisos.VerificarBorradoFam(NombreFamilia);
        }

        public int VerificarBorradoPatFam(string NombreFamilia, string NombrePatente)
        {
            return _permisos.VerificarBorradoPatFam(NombreFamilia, NombrePatente);
        }

        public int VerificarBorradoPatente(string NombrePatente)
        {
            return _permisos.VerificarBorradoPatente(NombrePatente);
        }

        #endregion

        #region Permisos

        public Array GetAllPermisos()
        {
            return _permisos.GetAllPermisos();
        }

        public IList<Propiedades_BE.Familia> GetAllPermisosPermisos()
        {
            return _permisos.GetAllPermisosPermisos();
        }

        public bool Existe(Propiedades_BE.Componente c, int id)
        {
            bool existe = false;

            if (c.Id.Equals(id))
                existe = true;
            else

                foreach (var item in c.Hijos)
                {

                    existe = Existe(item, id);
                    if (existe) return true;
                }

            return existe;

        }
        #endregion
    }
}

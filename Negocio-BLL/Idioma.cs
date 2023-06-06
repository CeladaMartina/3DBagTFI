using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class Idioma
    {
        Acceso_DAL.Idioma Mapper = new Acceso_DAL.Idioma();
        Propiedades_BE.Idioma IdiomaTemp = new Propiedades_BE.Idioma();

        public List<Propiedades_BE.Idioma> Listar()
        {
            return Mapper.Listar();
        }

        public List<string> NombreIdioma()
        {
            return Mapper.NombreIdioma();
        }

        public int Alta(int IdIdioma, string Nombre)
        {
            IdiomaTemp.IdIdioma = IdIdioma;
            IdiomaTemp.NombreIdioma = Nombre;
            return Mapper.Alta(IdiomaTemp);
        }

        public int Baja(int IdIdioma)
        {
            IdiomaTemp.IdIdioma = IdIdioma;
            return Mapper.Baja(IdiomaTemp);
        }

        public int Modificar(int IdIdioma, string Nombre)
        {
            IdiomaTemp.IdIdioma = IdIdioma;
            IdiomaTemp.NombreIdioma = Nombre;
            return Mapper.Modificar(IdiomaTemp);
        }
    }
}

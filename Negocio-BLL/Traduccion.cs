using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio_BLL
{
    public class Traduccion
    {
        Acceso_DAL.Traduccion Mapper = new Acceso_DAL.Traduccion();
        Propiedades_BE.Traduccion TraduccionTemp = new Propiedades_BE.Traduccion();

        public List<Propiedades_BE.Traduccion> Listar(string NombreIdioma)
        {
            TraduccionTemp.NombreIdioma = NombreIdioma;
            List<Propiedades_BE.Traduccion> Lista = Mapper.Listar(TraduccionTemp);
            return Lista;
        }

        public List<Propiedades_BE.Traduccion> ListarTraduccionDicionario(string NombreIdioma)
        {
            TraduccionTemp.NombreIdioma = NombreIdioma;
            List<Propiedades_BE.Traduccion> Lista = Mapper.ListarTraduccionDiccionario(TraduccionTemp);
            return Lista;
        }
        public List<Propiedades_BE.Traduccion> IdiomaDefault()
        {
            List<Propiedades_BE.Traduccion> T = Mapper.IdiomaDefault();
            return T;
        }

        public List<Propiedades_BE.Traduccion> FiltrarIdiomaDefault(string Original)
        {
            List<Propiedades_BE.Traduccion> T = Mapper.FiltrarIdiomaDefault(Original);
            return T;
        }

        public int Alta(string NombreIdioma, string Original, string Traducido)
        {
            TraduccionTemp.NombreIdioma = NombreIdioma;
            TraduccionTemp.Original = Original;
            TraduccionTemp.Traducido = Traducido;
            return Mapper.Alta(TraduccionTemp);
        }

        public int Baja(int IdTraduccion)
        {
            TraduccionTemp.IdTraduccion = IdTraduccion;
            return Mapper.Baja(TraduccionTemp);
        }

        public int Modificar(int IdTraduccion, string NombreIdioma, string Original, string Traducido)
        {
            TraduccionTemp.IdTraduccion = IdTraduccion;
            TraduccionTemp.NombreIdioma = NombreIdioma;
            TraduccionTemp.Original = Original;
            TraduccionTemp.Traducido = Traducido;
            return Mapper.Modificar(TraduccionTemp);
        }
    }
}

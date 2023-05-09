using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_DAL
{
    public class Permisos
    {
        Acceso_BD Acceso = new Acceso_BD();

        public void FillUserComponents(Propiedades_BE.Usuario u)
        {
            var cnn = new SqlConnection(Acceso.GlobalConexion);
            cnn.Open();

            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from usuarios_permisos up inner join Permiso p on up.id_permiso=p.id where id_usuario=@id;";
            cmd2.Parameters.AddWithValue("id", u.IdUsuario);

            var reader = cmd2.ExecuteReader();
            u.Permisos.Clear();
            while (reader.Read())
            {
                var idp = reader.GetInt32(reader.GetOrdinal("id"));
                var nombrep = reader.GetString(reader.GetOrdinal("nombre"));

                var permisop = String.Empty;
                if (reader["permiso"] != DBNull.Value)
                    permisop = reader.GetString(reader.GetOrdinal("permiso"));

                Propiedades_BE.Componente c1;
                if (!String.IsNullOrEmpty(permisop))
                {
                    c1 = new Propiedades_BE.Patente();
                    c1.Id = idp;
                    c1.Nombre = nombrep;
                    c1.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permisop);
                    u.Permisos.Add(c1);
                }
                else
                {
                    c1 = new Propiedades_BE.Familia();
                    c1.Id = idp;
                    c1.Nombre = nombrep;

                    var f = GetAll("=" + idp);

                    foreach (var familia in f)
                    {
                        c1.AgregarHijo(familia);
                    }
                    u.Permisos.Add(c1);
                }
            }
            reader.Close();
        }

        public IList<Propiedades_BE.Componente> GetAll(string familia)
        {
            var where = "is NULL";

            if (!String.IsNullOrEmpty(familia))
            {
                where = familia;
            }

            var cnn = new SqlConnection(Acceso.GlobalConexion);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            //var sql = "with recursivo as (select sp2.id_permiso_padre, sp2.id_permiso_hijo  from permiso_permiso SP2 where sp2.id_permiso_padre " + where + "  UNION ALL select sp.id_permiso_padre, sp.id_permiso_hijo from permiso_permiso sp inner join recursivo r on r.id_permiso_hijo= sp.id_permiso_padre) select r.id_permiso_padre,r.id_permiso_hijo,p.id,p.nombre, p.permiso from recursivo r inner join permiso p on r.id_permiso_hijo = p.id ";
            var sql = $@"with recursivo as (
                        select sp2.id_permiso_padre, sp2.id_permiso_hijo  from permiso_permiso SP2
                        where sp2.id_permiso_padre {where} --acá se va variando la familia que busco
                        UNION ALL 
                        select sp.id_permiso_padre, sp.id_permiso_hijo from permiso_permiso sp 
                        inner join recursivo r on r.id_permiso_hijo= sp.id_permiso_padre
                        )
                        select r.id_permiso_padre,r.id_permiso_hijo,p.id,p.nombre, p.permiso
                        from recursivo r 
                        inner join permiso p on r.id_permiso_hijo = p.id 
						
                        ";
            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Propiedades_BE.Componente>();

            while (reader.Read())
            {
                int id_padre = 0;
                if (reader["id_permiso_padre"] != DBNull.Value)
                {
                    id_padre = reader.GetInt32(reader.GetOrdinal("id_permiso_padre"));
                }

                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var nombre = reader.GetString(reader.GetOrdinal("nombre"));

                var permiso = string.Empty;
                if (reader["permiso"] != DBNull.Value)
                    permiso = reader.GetString(reader.GetOrdinal("permiso"));

                Propiedades_BE.Componente c;

                if (string.IsNullOrEmpty(permiso)) //usamos este campo para identificar. Solo las patentes van a tener un permiso del sistema relacionado
                    c = new Propiedades_BE.Familia();

                else
                    c = new Propiedades_BE.Patente();

                c.Id = id;
                c.Nombre = nombre;
                if (!string.IsNullOrEmpty(permiso))
                    c.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permiso);

                var padre = GetComponent(id_padre, lista);

                if (padre == null)
                {
                    lista.Add(c);
                }
                else
                {
                    padre.AgregarHijo(c);
                }
            }
            reader.Close();
            cnn.Close();
            return lista;
        }

        private Propiedades_BE.Componente GetComponent(int id, IList<Propiedades_BE.Componente> lista)
        {
            Propiedades_BE.Componente component = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

            if (component == null && lista != null)
            {
                foreach (var c in lista)
                {
                    var l = GetComponent(id, c.Hijos);
                    if (l != null && l.Id == id) return l;
                    else
                        if (l != null)
                        return GetComponent(id, l.Hijos);
                }
            }
            return component;
        }
    }
}

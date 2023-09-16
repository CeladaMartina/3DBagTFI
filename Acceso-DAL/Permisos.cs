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

        #region Seguridad

        public Array GetAllPermisos()
        {
            return Enum.GetValues(typeof(Propiedades_BE.TipoPermiso));
        }

        public Propiedades_BE.Componente GuardarComponente(Propiedades_BE.Componente p, bool esfamilia)
        {
            try
            {
                var cnn = new SqlConnection(Acceso.GlobalConexion);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                var sql = "insert into Permiso (nombre,permiso) values (@nombre,@permiso);  SELECT ID AS LastID FROM Permiso WHERE ID = @@Identity;";

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));

                if (esfamilia)
                {
                    cmd.Parameters.Add(new SqlParameter("permiso", DBNull.Value)); //al ser null ver tipopermiso
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("permiso", p.Permiso.ToString()));
                }

                var id = cmd.ExecuteScalar();
                p.Id = (int)id;
                return p;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Propiedades_BE.Componente EliminarComponente(Propiedades_BE.Componente p, bool esfamilia)
        {
            try
            {
                var cnn = new SqlConnection(Acceso.GlobalConexion);
                cnn.Open();
                var cmd1 = new SqlCommand();
                cmd1.Connection = cnn;

                var sql1 = "";
                if (esfamilia)
                {
                    sql1 = "select ((select count(*) from permiso_permiso where id_permiso_hijo = (select id from Permiso where nombre = @nombre and permiso is NULL))+(select count(*) from usuarios_permisos where id_permiso = (select id from Permiso where nombre = @nombre and permiso is NULL)) )";
                }
                else
                {
                    sql1 = "select ((select count(*) from permiso_permiso where id_permiso_hijo = (select id from Permiso where nombre = @nombre and permiso = @permiso))+(select count(*) from usuarios_permisos where id_permiso = (select id from Permiso where nombre = @nombre and permiso = @permiso)) )";
                }

                cmd1.CommandText = sql1;
                cmd1.Parameters.Add(new SqlParameter("nombre", p.Nombre));
                if (esfamilia)
                {
                    BorrarRelacionesFamilia(p.Nombre);
                }
                else
                {
                    cmd1.Parameters.Add(new SqlParameter("permiso", p.Permiso.ToString()));
                }

                var tot = cmd1.ExecuteScalar();

                if (int.Parse(tot.ToString()) == 0)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    var sql = "";

                    if (esfamilia)
                        sql = "delete from Permiso where nombre = @nombre and permiso is NULL ;";
                    else
                        sql = "delete from Permiso where nombre = @nombre and permiso = @permiso ;";

                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("nombre", p.Nombre));


                    if (esfamilia) { }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("permiso", p.Permiso.ToString()));
                    }
                    var id = cmd.ExecuteNonQuery();
                    p.Id = (int)id;
                    return p;
                }
                else
                {
                    int id = 0;
                    p.Id = id;
                    return p;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GuardarFamilia(Propiedades_BE.Familia c)
        {
            try
            {
                var cnn = new SqlConnection(Acceso.GlobalConexion);
                cnn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                var sql = $@"delete from permiso_permiso where id_permiso_padre=@id;";

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("id", c.Id));
                cmd.ExecuteNonQuery();

                foreach (var item in c.Hijos)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;

                    sql = $@"insert into permiso_permiso (id_permiso_padre,id_permiso_hijo) values (@id_permiso_padre,@id_permiso_hijo) ";
                    //sql = "if not exists (select * from permiso_permiso where (id_permiso_padre=@id_permiso_padre and id_permiso_hijo=@id_permiso_hijo) ) insert into permiso_permiso (id_permiso_padre,id_permiso_hijo) values (@id_permiso_padre,@id_permiso_hijo) ";

                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SqlParameter("id_permiso_padre", c.Id));
                    cmd.Parameters.Add(new SqlParameter("id_permiso_hijo", item.Id));

                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarFamilia(string NomFam, string NomPat, bool isfam)
        {
            try
            {
                var cnn = new SqlConnection(Acceso.GlobalConexion);
                cnn.Open();
                var cmd = new SqlCommand();

                cmd.Connection = cnn;
                var sql = "";
                if (isfam == true)
                {
                    sql = "delete from permiso_permiso where (id_permiso_padre=(select id from Permiso where nombre = '" + NomFam + "' and permiso IS NULL) and id_permiso_hijo=(select id from Permiso where nombre = '" + NomPat + "' and permiso IS NULL)) ";
                }
                else
                {
                    sql = "delete from permiso_permiso where (id_permiso_padre=(select id from Permiso where nombre = '" + NomFam + "' and permiso IS NULL) and id_permiso_hijo=(select id from Permiso where nombre = '" + NomPat + "' and permiso IS NOT NULL)) ";
                }

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
        }

        public void ModificarFamilia(string NomOriginal, string NomNuevo)
        {
            try
            {
                var cnn = new SqlConnection(Acceso.GlobalConexion);
                cnn.Open();
                var cmd = new SqlCommand();

                cmd = new SqlCommand();
                cmd.Connection = cnn;

                var sql = "update Permiso set Nombre = @Nombre where Id = (Select Id from Permiso where Nombre = @NombreOriginal) and permiso is null";

                cmd.CommandText = sql;
                cmd.Parameters.Add(new SqlParameter("NombreOriginal", NomOriginal));
                cmd.Parameters.Add(new SqlParameter("Nombre", NomNuevo));

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Propiedades_BE.Patente> GetAllPatentes()
        {
            var cnn = new SqlConnection(Acceso.GlobalConexion);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = "select * from Permiso p where p.permiso is not null;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Propiedades_BE.Patente>();

            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var nombre = reader.GetString(reader.GetOrdinal("nombre"));
                var permiso = reader.GetString(reader.GetOrdinal("permiso"));

                Propiedades_BE.Patente c = new Propiedades_BE.Patente();

                c.Id = id;
                c.Nombre = nombre;
                c.Permiso = (Propiedades_BE.TipoPermiso)Enum.Parse(typeof(Propiedades_BE.TipoPermiso), permiso);
                lista.Add(c);
            }
            reader.Close();
            cnn.Close();
            return lista;
        }

        public IList<Propiedades_BE.Familia> GetAllFamilias()
        {
            var cnn = new SqlConnection(Acceso.GlobalConexion);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = "select * from Permiso p where p.permiso is  null;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Propiedades_BE.Familia>();

            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var nombre = reader.GetString(reader.GetOrdinal("nombre"));

                Propiedades_BE.Familia c = new Propiedades_BE.Familia();

                c.Id = id;
                c.Nombre = nombre;
                lista.Add(c);
            }
            reader.Close();
            cnn.Close();
            return lista;
        }

        public IList<Propiedades_BE.Familia> GetAllPermisosPermisos()
        {
            var cnn = new SqlConnection(Acceso.GlobalConexion);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = "select (select nombre from Permiso where id = pp.id_permiso_padre) as 'NomFam',p.nombre as 'NomPatente' from permiso_permiso pp inner join Permiso p on p.id = pp.id_permiso_hijo";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Propiedades_BE.Familia>();

            while (reader.Read())
            {
                var nombre_fam = reader.GetString(reader.GetOrdinal("NomFam"));
                var nombre_pat = reader.GetString(reader.GetOrdinal("NomPatente"));

                Propiedades_BE.Familia f = new Propiedades_BE.Familia();
                f.Nombre = nombre_pat;
                f.NombreFamilia = nombre_fam;
                lista.Add(f);
            }
            reader.Close();
            cnn.Close();
            return lista;
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


        //devuelve en forma de metodo los permisos del usuario, para ponerlo en treeview
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


        //devuelve en forma de lista los permisos del usuario, para ponerlo en  listbox
        public IList<Propiedades_BE.Componente> FillUserComponentsList(Propiedades_BE.Usuario u)
        {
            var cnn = new SqlConnection(Acceso.GlobalConexion);
            cnn.Open();

            var cmd2 = new SqlCommand();
            cmd2.Connection = cnn;
            cmd2.CommandText = "select p.* from usuarios_permisos up inner join Permiso p on up.id_permiso=p.id where id_usuario=@id;";
            cmd2.Parameters.AddWithValue("id", u.IdUsuario);

            var reader = cmd2.ExecuteReader();
            var lista = new List<Propiedades_BE.Componente>();

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
                    lista.Add(c1);
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
                    lista.Add(c1);
                }               

            }
            reader.Close();
            return lista;
        }
        public void FillFamilyComponents(Propiedades_BE.Familia familia)
        {
            familia.VaciarHijo();
            foreach (var item in GetAll("=" + familia.Id))
            {
                familia.AgregarHijo(item);
            }
        }

        //prueba
        public int traerIDPermiso(string nombre)
        {
            int Id = 0;
            using (Acceso.Conexion)
            {
                Acceso.AbrirConexion();
                string QueryID = " select id from Permiso where nombre='" + nombre + "'";

                using (SqlCommand Cmd = new SqlCommand(QueryID, Acceso.Conexion))
                {
                    using (SqlDataReader Lector = Cmd.ExecuteReader())
                    {
                        if (Lector.Read())
                        {
                            Id = Lector.GetInt32(0);
                        }
                        else
                        {
                            Id = -1;
                        }
                    }
                }
                Acceso.CerrarConexion();
            }
            return Id;
        }

        #endregion

        #region VerificarBorrado

        public int VerificarBorradoFam(string NombreFamilia)
        {
            int i = 0;
            SqlConnection Connection = new SqlConnection(Acceso.GlobalConexion);
            Connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "select id_permiso_hijo from permiso_permiso where id_permiso_padre = (select Id from Permiso where nombre = '" + NombreFamilia + "' and permiso is NULL) and id_permiso_hijo not in (select id from Permiso where permiso is null) union select id_permiso_hijo from permiso_permiso where id_permiso_padre in (select id_permiso_hijo from permiso_permiso where id_permiso_padre = (select Id from Permiso where nombre = '" + NombreFamilia + "') and id_permiso_hijo in (select id from Permiso where permiso is null))";
            var QueryPatentes = cmd.ExecuteReader();
            List<int> ListaPatetentesFamilia = new List<int>();
            while (QueryPatentes.Read())
            {
                ListaPatetentesFamilia.Add(QueryPatentes.GetInt32(0));
            }
            QueryPatentes.Close();
            foreach (int Patente in ListaPatetentesFamilia)
            {
                cmd.CommandText = "select * from usuarios_permisos where (id_usuario in (select IdUsuario from Usuario where BajaLogica != 1 and Estado != 1) and id_permiso = " + Patente + ") or (id_permiso in (select id_permiso_padre from permiso_permiso where id_permiso_hijo = " + Patente + " and id_permiso_padre != (select Id from Permiso where nombre = '" + NombreFamilia + "' and permiso is NULL)) and id_usuario in (select IdUsuario from Usuario where BajaLogica != 1 and Estado != 1))";
                var QueryVerificar = cmd.ExecuteReader();
                if (QueryVerificar.Read())
                {
                    i += 0;
                }
                else
                {
                    i += 1;
                }
                QueryVerificar.Close();
            }
            Connection.Close();

            return i;
        }

        public int VerificarBorradoPatFam(string NombreFamilia, string NombrePatente)
        {
            int i = 0;
            SqlConnection Connection = new SqlConnection(Acceso.GlobalConexion);
            Connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "select * from usuarios_permisos where (id_usuario in (select IdUsuario from Usuario where BajaLogica != 1 and Estado != 1) and id_permiso = (select Id from Permiso where nombre = '" + NombrePatente + "' and permiso is not NULL)) or (id_permiso in (select id_permiso_padre from permiso_permiso where id_permiso_hijo = (select Id from Permiso where nombre = '" + NombrePatente + "' and permiso is not NULL) and id_permiso_padre != (select Id from Permiso where nombre = '" + NombreFamilia + "' and permiso is NULL)) and id_usuario in (select IdUsuario from Usuario where BajaLogica != 1 and Estado != 1))";
            var QueryPatFam = cmd.ExecuteReader();
            if (QueryPatFam.Read())
            {
                i = 0;
            }
            else
            {
                i = 1;
            }
            QueryPatFam.Close();
            Connection.Close();
            return i;
        }

        public void BorrarRelacionesFamilia(string NombreFamilia)
        {
            SqlConnection Connection = new SqlConnection(Acceso.GlobalConexion);
            Connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "delete from usuarios_permisos where id_permiso = (select id from Permiso where nombre = '" + NombreFamilia + "' and permiso IS NULL)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from permiso_permiso where id_permiso_padre = (select id from Permiso where nombre = '" + NombreFamilia + "' and permiso IS NULL)";
            cmd.ExecuteNonQuery();
            Connection.Close();
        }

        public int VerificarBorradoPatente(string NombrePatente)
        {
            int i = 0;
            SqlConnection Connection = new SqlConnection(Acceso.GlobalConexion);
            Connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "select * from permiso_permiso where id_permiso_hijo = (select id from Permiso where permiso is not null and nombre = '" + NombrePatente + "')";
            var QueryPatentes = cmd.ExecuteReader();
            if (QueryPatentes.Read())
            {
                i = 0;
            }
            else
            {
                QueryPatentes.Close();
                cmd.CommandText = "select * from usuarios_permisos where id_permiso = (select id from Permiso where permiso is not null and nombre = '" + NombrePatente + "')";
                var QueryUsuarios = cmd.ExecuteReader();
                if (QueryUsuarios.Read())
                {
                    i = 0;
                }
                else
                {
                    i = 1;
                    QueryUsuarios.Close();
                }
                QueryUsuarios.Close();
            }
            QueryPatentes.Close();
            Connection.Close();
            return i;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelos.Conexion_DB;

namespace Modelos.Datos
{
    public static class RolDatos
    {
        public static DataTable Listar()
        {
            return Conexion.Consultar("SELECT idRol, nombre, descripcion FROM rol ORDER BY nombre");
        }

        public static DataTable ListarPermisos()
        {
            return Conexion.Consultar("SELECT idPermiso, codigo, descripcion FROM permiso ORDER BY codigo");
        }

        public static HashSet<int> PermisosDeRol(int idRol)
        {
            DataTable t = Conexion.Consultar("SELECT idPermiso FROM rolPermiso WHERE idRol=@r", Conexion.P("@r", idRol));
            HashSet<int> ids = new HashSet<int>();
            foreach (DataRow f in t.Rows) ids.Add((int)f["idPermiso"]);
            return ids;
        }

        /// <summary>Reemplaza los permisos del rol dentro de una transacción (todo o nada).</summary>
        public static void GuardarPermisos(int idRol, string nombreRol, IEnumerable<int> idsPermisos, IEnumerable<string> codigos)
        {
            if (nombreRol == "Administrador")
            {
                List<string> lista = new List<string>(codigos);
                if (!lista.Contains("USUARIOS_GESTIONAR") || !lista.Contains("ROLES_GESTIONAR"))
                    throw new InvalidOperationException(
                        "El rol Administrador debe conservar los permisos de gestión de usuarios y de roles.");
            }

            using (SqlConnection cn = Conexion.Conectar())
            using (SqlTransaction tx = cn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand del = new SqlCommand("DELETE FROM rolPermiso WHERE idRol=@r", cn, tx))
                    {
                        del.Parameters.AddWithValue("@r", idRol);
                        del.ExecuteNonQuery();
                    }
                    foreach (int idPermiso in idsPermisos)
                    {
                        using (SqlCommand ins = new SqlCommand("INSERT INTO rolPermiso (idRol, idPermiso) VALUES (@r, @p)", cn, tx))
                        {
                            ins.Parameters.AddWithValue("@r", idRol);
                            ins.Parameters.AddWithValue("@p", idPermiso);
                            ins.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}

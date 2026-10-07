using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Modelos
{
    public class PermisosDAL : DALBase
    {
        public DataTable Listar()
        {
            return Consultar("SELECT idPermiso, codigo, descripcion FROM permiso ORDER BY codigo");
        }

        public HashSet<int> PermisosDeRol(int idRol)
        {
            DataTable t = Consultar("SELECT idPermiso FROM rolPermiso WHERE idRol=@r", P("@r", idRol));
            HashSet<int> ids = new HashSet<int>();
            foreach (DataRow f in t.Rows) ids.Add((int)f["idPermiso"]);
            return ids;
        }

        public void Guardar(int idRol, string nombreRol, IEnumerable<int> idsPermisos, IEnumerable<string> codigos)
        {
            if (nombreRol == "Administrador")
            {
                List<string> lista = new List<string>(codigos);
                if (!lista.Contains("USUARIOS_GESTIONAR") || !lista.Contains("ROLES_GESTIONAR"))
                    throw new AppException("ERR-NEG-001",
                        "El rol Administrador debe conservar los permisos de gestión de usuarios y de roles.");
            }

            try
            {
                using (SqlConnection con = _conexion.ObtenerConexion())
                {
                    con.Open();
                    using (SqlTransaction tx = con.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand del = new SqlCommand("DELETE FROM rolPermiso WHERE idRol=@r", con, tx))
                            {
                                del.Parameters.AddWithValue("@r", idRol);
                                del.ExecuteNonQuery();
                            }
                            foreach (int idPermiso in idsPermisos)
                            {
                                using (SqlCommand ins = new SqlCommand(
                                    "INSERT INTO rolPermiso (idRol, idPermiso) VALUES (@r, @p)", con, tx))
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
            catch (Exception ex)
            {
                throw Traducir(ex);
            }
        }
    }
}

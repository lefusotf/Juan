using System;
using System.Data;
using System.Data.SqlClient;

namespace Modelos
{
    public abstract class DALBase
    {
        protected readonly Conexion _conexion = new Conexion();

        protected static SqlParameter P(string nombre, object valor)
        {
            if (valor == null || (valor is string s && string.IsNullOrWhiteSpace(s)))
                return new SqlParameter(nombre, DBNull.Value);
            return new SqlParameter(nombre, valor);
        }

        protected static SqlParameter Like(string nombre, string filtro)
        {
            return new SqlParameter(nombre, "%" + (filtro ?? "") + "%");
        }

        protected static Exception Traducir(Exception ex)
        {
            if (ex is AppException) return ex;
            SqlException sqlEx = ex as SqlException;
            if (sqlEx != null)
                return new AppException(CatalogoErrores.MapearSqlException(sqlEx.Number), sqlEx);
            return new AppException("ERR-GEN-001", ex);
        }

        protected DataTable Consultar(string sql, params SqlParameter[] parametros)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = _conexion.ObtenerConexion())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (parametros != null) cmd.Parameters.AddRange(parametros);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw Traducir(ex);
            }
        }

        protected int Ejecutar(string sql, params SqlParameter[] parametros)
        {
            try
            {
                using (SqlConnection con = _conexion.ObtenerConexion())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (parametros != null) cmd.Parameters.AddRange(parametros);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw Traducir(ex);
            }
        }

        protected object Escalar(string sql, params SqlParameter[] parametros)
        {
            try
            {
                using (SqlConnection con = _conexion.ObtenerConexion())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (parametros != null) cmd.Parameters.AddRange(parametros);
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw Traducir(ex);
            }
        }

        protected bool Existe(string sql, params SqlParameter[] parametros)
        {
            return Convert.ToInt32(Escalar(sql, parametros)) > 0;
        }
    }
}

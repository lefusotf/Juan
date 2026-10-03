using System;

namespace Modelos.Seguridad
{
    /// <summary>Encriptación de contraseñas con la librería BCrypt.Net.</summary>
    public static class EncriptadorContrasena
    {
        private const int FactorCosto = 11;

        public static string Hashear(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena, FactorCosto);
        }

        public static bool Verificar(string contrasena, string hash)
        {
            if (string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(hash)) return false;
            try
            {
                return BCrypt.Net.BCrypt.Verify(contrasena, hash);
            }
            catch (Exception)
            {
                // Hash con formato inválido en la BD: se trata como credencial incorrecta
                return false;
            }
        }
    }
}

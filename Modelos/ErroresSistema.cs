using System;
using System.Collections.Generic;

namespace Modelos
{
    public class ErrorInfo
    {
        public string Codigo { get; set; }
        public string MensajeUsuario { get; set; }
        public string DescripcionTecnica { get; set; }

        public ErrorInfo(string codigo, string mensajeUsuario, string descripcionTecnica)
        {
            Codigo = codigo;
            MensajeUsuario = mensajeUsuario;
            DescripcionTecnica = descripcionTecnica;
        }
    }

    public static class CatalogoErrores
    {
        private static readonly Dictionary<string, ErrorInfo> _errores = new Dictionary<string, ErrorInfo>
        {
            { "ERR-SQL-001", new ErrorInfo("ERR-SQL-001", "No se pudo establecer conexión con el servidor de base de datos.", "Error de conexión, timeout o credenciales de SQL Server.") },
            { "ERR-SQL-002", new ErrorInfo("ERR-SQL-002", "El registro que intenta ingresar ya existe en el sistema.", "Violación de restricción UNIQUE o Primary Key (Error SQL 2601/2627).") },
            { "ERR-SQL-003", new ErrorInfo("ERR-SQL-003", "No se puede eliminar o modificar el registro porque está siendo utilizado en otra parte del sistema.", "Violación de Foreign Key (Error SQL 547).") },
            { "ERR-SQL-004", new ErrorInfo("ERR-SQL-004", "El valor proporcionado excede la longitud o tipo de dato permitido.", "Truncamiento de datos o desbordamiento de tipo (Error SQL 2628/8152).") },
            { "ERR-SQL-005", new ErrorInfo("ERR-SQL-005", "Se produjo un conflicto en la base de datos al procesar la solicitud.", "Bloqueo o deadlock en la base de datos (Error SQL 1205).") },
            { "ERR-SQL-006", new ErrorInfo("ERR-SQL-006", "La base de datos 'Veterinaria' no existe o no está disponible. Ejecute el script BaseDatos\\Veterinaria.sql.", "Error SQL 4060: base de datos inaccesible.") },
            { "ERR-SQL-999", new ErrorInfo("ERR-SQL-999", "Ocurrió un problema no especificado en la base de datos.", "Excepción SQL genérica no catalogada.") },

            { "ERR-VAL-001", new ErrorInfo("ERR-VAL-001", "Por favor complete todos los campos obligatorios.", "Validación de campos vacíos o nulos fallida.") },
            { "ERR-VAL-002", new ErrorInfo("ERR-VAL-002", "El formato del dato ingresado no es válido.", "Validación de formato (correo, teléfono, DUI) fallida.") },
            { "ERR-VAL-003", new ErrorInfo("ERR-VAL-003", "Las credenciales proporcionadas no son válidas.", "Fallo en validación de seguridad o formato de credencial.") },

            { "ERR-NEG-001", new ErrorInfo("ERR-NEG-001", "La operación no se puede completar porque incumple una regla del sistema.", "Regla de negocio incumplida (historial, último administrador, permisos obligatorios).") },

            { "ERR-GEN-001", new ErrorInfo("ERR-GEN-001", "Ocurrió un error inesperado en el sistema. Si el problema persiste, contacte a soporte técnico.", "Excepción genérica del sistema (System.Exception).") }
        };

        public static ErrorInfo Obtener(string codigo)
        {
            if (_errores.ContainsKey(codigo))
                return _errores[codigo];

            return _errores["ERR-GEN-001"];
        }

        public static string MapearSqlException(int sqlNumber)
        {
            switch (sqlNumber)
            {
                case -2:
                case 2:
                case 20:
                case 53:
                case 18456:
                    return "ERR-SQL-001";

                case 4060:
                    return "ERR-SQL-006";

                case 2601:
                case 2627:
                    return "ERR-SQL-002";

                case 547:
                    return "ERR-SQL-003";

                case 2628:
                case 8152:
                    return "ERR-SQL-004";

                case 1205:
                    return "ERR-SQL-005";

                default:
                    return "ERR-SQL-999";
            }
        }
    }

    public class AppException : Exception
    {
        public string CodigoError { get; }
        public ErrorInfo DetalleError { get; }

        public AppException(string codigoError, Exception innerException = null)
            : base(CatalogoErrores.Obtener(codigoError).MensajeUsuario, innerException)
        {
            CodigoError = codigoError;
            DetalleError = CatalogoErrores.Obtener(codigoError);
        }

        public AppException(string codigoError, string mensajePersonalizado, Exception innerException = null)
            : base(mensajePersonalizado, innerException)
        {
            CodigoError = codigoError;
            DetalleError = CatalogoErrores.Obtener(codigoError);
        }
    }
}

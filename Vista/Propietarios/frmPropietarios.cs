using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Propietarios
{
    public class frmPropietarios : FormCrudBase
    {
        private readonly TextBox txtNombre = new TextBox { MaxLength = 150 };
        private readonly TextBox txtDui = new TextBox { MaxLength = 10 };
        private readonly TextBox txtTelefono = new TextBox { MaxLength = 15 };
        private readonly TextBox txtCorreo = new TextBox { MaxLength = 150 };
        private readonly TextBox txtDireccion = new TextBox { MaxLength = 250 };

        protected override string TituloFormulario { get { return "Propietarios"; } }
        protected override string Modulo { get { return "Propietarios"; } }
        protected override string ColumnaId { get { return "idPropietario"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.PropietariosGestionar); } }

        protected override void ConstruirCampos()
        {
            AgregarCampo("Nombre completo *", txtNombre);
            AgregarCampo("DUI *", txtDui);
            AgregarCampo("Teléfono *", txtTelefono);
            AgregarCampo("Correo", txtCorreo);
            AgregarCampo("Dirección", txtDireccion, true);
        }

        protected override DataTable ObtenerDatos(string filtro)
        {
            return PropietarioDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idPropietario");
            Encabezado("nombre", "Nombre");
            Encabezado("dui", "DUI");
            Encabezado("telefono", "Teléfono");
            Encabezado("correo", "Correo");
            Encabezado("direccion", "Dirección");
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            txtNombre.Text = f.Cells["nombre"].Value.ToString();
            txtDui.Text = f.Cells["dui"].Value.ToString();
            txtTelefono.Text = f.Cells["telefono"].Value.ToString();
            txtCorreo.Text = f.Cells["correo"].Value.ToString();
            txtDireccion.Text = f.Cells["direccion"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            txtNombre.Clear(); txtDui.Clear(); txtTelefono.Clear(); txtCorreo.Clear(); txtDireccion.Clear();
            txtNombre.Focus();
        }

        protected override bool Validar()
        {
            if (Falla(Validaciones.Requerido(txtNombre.Text, "Nombre completo"), txtNombre)) return false;
            if (Falla(Validaciones.Requerido(txtDui.Text, "DUI") ?? Validaciones.Dui(txtDui.Text), txtDui)) return false;
            if (Falla(Validaciones.Requerido(txtTelefono.Text, "Teléfono") ?? Validaciones.Telefono(txtTelefono.Text), txtTelefono)) return false;
            if (Falla(Validaciones.Correo(txtCorreo.Text), txtCorreo)) return false;
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            Propietario p = new Propietario
            {
                IdPropietario = IdSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                Dui = txtDui.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
            };
            if (esNuevo) PropietarioDatos.Insertar(p); else PropietarioDatos.Actualizar(p);
        }

        protected override void EliminarRegistro(int id)
        {
            PropietarioDatos.Eliminar(id);
        }
    }
}

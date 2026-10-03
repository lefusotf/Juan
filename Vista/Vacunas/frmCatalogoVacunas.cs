using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Vacunas
{
    public class frmCatalogoVacunas : FormCrudBase
    {
        private readonly TextBox txtNombre = new TextBox { MaxLength = 100 };
        private readonly ComboBox cboEspecie = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown, MaxLength = 50 };
        private readonly NumericUpDown nudIntervalo = new NumericUpDown { Minimum = 1, Maximum = 3650, Value = 365 };
        private readonly TextBox txtDescripcion = new TextBox { MaxLength = 250 };

        protected override string TituloFormulario { get { return "Catálogo de vacunas"; } }
        protected override string Modulo { get { return "Vacunas"; } }
        protected override string ColumnaId { get { return "idVacuna"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.VacunasGestionar); } }

        protected override void ConstruirCampos()
        {
            cboEspecie.Items.AddRange(new object[] { "Perro", "Gato", "Ave", "Conejo", "Roedor", "Reptil" });
            AgregarCampo("Nombre *", txtNombre);
            AgregarCampo("Especie destino *", cboEspecie);
            AgregarCampo("Intervalo (días) *", nudIntervalo);
            AgregarCampo("Descripción", txtDescripcion);
        }

        protected override DataTable ObtenerDatos(string filtro)
        {
            return VacunaDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idVacuna");
            Encabezado("nombre", "Vacuna");
            Encabezado("descripcion", "Descripción");
            Encabezado("especieDestino", "Especie");
            Encabezado("intervaloDias", "Intervalo (días)");
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            txtNombre.Text = f.Cells["nombre"].Value.ToString();
            cboEspecie.Text = f.Cells["especieDestino"].Value.ToString();
            nudIntervalo.Value = Convert.ToDecimal(f.Cells["intervaloDias"].Value);
            txtDescripcion.Text = f.Cells["descripcion"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            txtNombre.Clear(); cboEspecie.Text = ""; nudIntervalo.Value = 365; txtDescripcion.Clear();
            txtNombre.Focus();
        }

        protected override bool Validar()
        {
            if (Falla(Validaciones.Requerido(txtNombre.Text, "Nombre"), txtNombre)) return false;
            if (Falla(Validaciones.Requerido(cboEspecie.Text, "Especie destino"), cboEspecie)) return false;
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            Vacuna v = new Vacuna
            {
                IdVacuna = IdSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                EspecieDestino = cboEspecie.Text.Trim(),
                IntervaloDias = (int)nudIntervalo.Value,
                Descripcion = txtDescripcion.Text.Trim()
            };
            if (esNuevo) VacunaDatos.Insertar(v); else VacunaDatos.Actualizar(v);
        }

        protected override void EliminarRegistro(int id)
        {
            VacunaDatos.Eliminar(id);
        }
    }
}

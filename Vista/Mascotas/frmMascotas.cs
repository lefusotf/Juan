using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Mascotas
{
    public class frmMascotas : FormCrudBase
    {
        private readonly ComboBox cboPropietario = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly TextBox txtNombre = new TextBox { MaxLength = 100 };
        private readonly ComboBox cboEspecie = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown, MaxLength = 50 };
        private readonly TextBox txtRaza = new TextBox { MaxLength = 80 };
        private readonly ComboBox cboSexo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker dtpNacimiento = new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true };
        private readonly NumericUpDown nudPeso = new NumericUpDown { DecimalPlaces = 2, Maximum = 999, Minimum = 0 };
        private readonly TextBox txtColor = new TextBox { MaxLength = 50 };

        protected override string TituloFormulario { get { return "Mascotas"; } }
        protected override string Modulo { get { return "Mascotas"; } }
        protected override string ColumnaId { get { return "idMascota"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.MascotasGestionar); } }

        protected override void ConstruirCampos()
        {
            cboSexo.Items.AddRange(new object[] { "Macho", "Hembra" });
            cboEspecie.Items.AddRange(new object[] { "Perro", "Gato", "Ave", "Conejo", "Roedor", "Reptil" });

            AgregarCampo("Propietario *", cboPropietario);
            AgregarCampo("Nombre *", txtNombre);
            AgregarCampo("Especie *", cboEspecie);
            AgregarCampo("Raza", txtRaza);
            AgregarCampo("Sexo *", cboSexo);
            AgregarCampo("Fecha nacimiento", dtpNacimiento);
            AgregarCampo("Peso (kg)", nudPeso);
            AgregarCampo("Color", txtColor);

            try
            {
                cboPropietario.DataSource = PropietarioDatos.Listar("");
                cboPropietario.DisplayMember = "nombre";
                cboPropietario.ValueMember = "idPropietario";
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        protected override DataTable ObtenerDatos(string filtro)
        {
            return MascotaDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idMascota", "idPropietario");
            Encabezado("propietario", "Propietario");
            Encabezado("nombre", "Nombre");
            Encabezado("especie", "Especie");
            Encabezado("raza", "Raza");
            Encabezado("sexo", "Sexo");
            Encabezado("fechaNacimiento", "Nacimiento");
            Encabezado("peso", "Peso (kg)");
            Encabezado("color", "Color");
            if (dgv.Columns.Contains("fechaNacimiento")) dgv.Columns["fechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            cboPropietario.SelectedValue = Convert.ToInt32(f.Cells["idPropietario"].Value);
            txtNombre.Text = f.Cells["nombre"].Value.ToString();
            cboEspecie.Text = f.Cells["especie"].Value.ToString();
            txtRaza.Text = f.Cells["raza"].Value.ToString();
            cboSexo.SelectedIndex = f.Cells["sexo"].Value.ToString() == "M" ? 0 : 1;
            if (f.Cells["fechaNacimiento"].Value is DateTime)
            {
                dtpNacimiento.Value = (DateTime)f.Cells["fechaNacimiento"].Value;
                dtpNacimiento.Checked = true;
            }
            else
            {
                dtpNacimiento.Checked = false;
            }
            nudPeso.Value = f.Cells["peso"].Value is decimal ? (decimal)f.Cells["peso"].Value : 0;
            txtColor.Text = f.Cells["color"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            if (cboPropietario.Items.Count > 0) cboPropietario.SelectedIndex = 0;
            txtNombre.Clear(); cboEspecie.Text = ""; txtRaza.Clear(); txtColor.Clear();
            cboSexo.SelectedIndex = 0;
            dtpNacimiento.Value = DateTime.Today;
            dtpNacimiento.Checked = false;
            nudPeso.Value = 0;
            txtNombre.Focus();
        }

        protected override bool Validar()
        {
            if (cboPropietario.SelectedValue == null)
            {
                Mensajes.Advertencia("Debe existir y seleccionarse un propietario. Registre primero al propietario.");
                cboPropietario.Focus();
                return false;
            }
            if (Falla(Validaciones.Requerido(txtNombre.Text, "Nombre"), txtNombre)) return false;
            if (Falla(Validaciones.Requerido(cboEspecie.Text, "Especie"), cboEspecie)) return false;
            if (dtpNacimiento.Checked && dtpNacimiento.Value.Date > DateTime.Today)
            {
                Mensajes.Advertencia("La fecha de nacimiento no puede ser futura.");
                dtpNacimiento.Focus();
                return false;
            }
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            Mascota m = new Mascota
            {
                IdMascota = IdSeleccionado,
                IdPropietario = Convert.ToInt32(cboPropietario.SelectedValue),
                Nombre = txtNombre.Text.Trim(),
                Especie = cboEspecie.Text.Trim(),
                Raza = txtRaza.Text.Trim(),
                Sexo = cboSexo.SelectedIndex == 0 ? "M" : "H",
                FechaNacimiento = dtpNacimiento.Checked ? (DateTime?)dtpNacimiento.Value.Date : null,
                Peso = nudPeso.Value > 0 ? (decimal?)nudPeso.Value : null,
                Color = txtColor.Text.Trim()
            };
            if (esNuevo) MascotaDatos.Insertar(m); else MascotaDatos.Actualizar(m);
        }

        protected override void EliminarRegistro(int id)
        {
            MascotaDatos.Eliminar(id);
        }
    }
}

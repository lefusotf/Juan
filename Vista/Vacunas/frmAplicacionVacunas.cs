using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Vacunas
{
    /// <summary>Registro de vacunas aplicadas a las mascotas por el veterinario.</summary>
    public class frmAplicacionVacunas : FormCrudBase
    {
        private readonly ComboBox cboMascota = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboVacuna = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboVeterinario = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker dtpAplicacion = new DateTimePicker { Format = DateTimePickerFormat.Short };
        private readonly DateTimePicker dtpProxima = new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true };
        private readonly TextBox txtObservaciones = new TextBox { MaxLength = 250 };

        protected override string TituloFormulario { get { return "Aplicación de vacunas"; } }
        protected override string Modulo { get { return "Vacunas"; } }
        protected override string ColumnaId { get { return "idAplicacion"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.VacunasGestionar); } }

        protected override void ConstruirCampos()
        {
            AgregarCampo("Mascota *", cboMascota);
            AgregarCampo("Vacuna *", cboVacuna);
            AgregarCampo("Veterinario *", cboVeterinario);
            AgregarCampo("Fecha aplicación *", dtpAplicacion);
            AgregarCampo("Próxima dosis", dtpProxima);
            AgregarCampo("Observaciones", txtObservaciones);

            try
            {
                cboMascota.DataSource = MascotaDatos.ListarParaCombo();
                cboMascota.DisplayMember = "descripcion";
                cboMascota.ValueMember = "idMascota";

                cboVacuna.DataSource = VacunaDatos.ListarParaCombo();
                cboVacuna.DisplayMember = "nombre";
                cboVacuna.ValueMember = "idVacuna";

                cboVeterinario.DataSource = UsuarioDatos.ListarVeterinarios();
                cboVeterinario.DisplayMember = "nombreCompleto";
                cboVeterinario.ValueMember = "idUsuario";
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }

            // La próxima dosis se sugiere según el intervalo de la vacuna elegida
            cboVacuna.SelectionChangeCommitted += (s, e) => SugerirProximaDosis();
            dtpAplicacion.ValueChanged += (s, e) => SugerirProximaDosis();
        }

        private void SugerirProximaDosis()
        {
            DataRowView fila = cboVacuna.SelectedItem as DataRowView;
            if (fila == null) return;
            dtpProxima.Value = dtpAplicacion.Value.Date.AddDays(Convert.ToInt32(fila["intervaloDias"]));
            dtpProxima.Checked = true;
        }

        protected override DataTable ObtenerDatos(string filtro)
        {
            return AplicacionVacunaDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idAplicacion", "idMascota", "idVacuna", "idVeterinario");
            Encabezado("mascota", "Mascota");
            Encabezado("vacuna", "Vacuna");
            Encabezado("veterinario", "Veterinario");
            Encabezado("fechaAplicacion", "Aplicación");
            Encabezado("proximaDosis", "Próxima dosis");
            Encabezado("observaciones", "Observaciones");
            if (dgv.Columns.Contains("fechaAplicacion")) dgv.Columns["fechaAplicacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgv.Columns.Contains("proximaDosis")) dgv.Columns["proximaDosis"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            cboMascota.SelectedValue = Convert.ToInt32(f.Cells["idMascota"].Value);
            cboVacuna.SelectedValue = Convert.ToInt32(f.Cells["idVacuna"].Value);
            cboVeterinario.SelectedValue = Convert.ToInt32(f.Cells["idVeterinario"].Value);
            dtpAplicacion.Value = (DateTime)f.Cells["fechaAplicacion"].Value;
            if (f.Cells["proximaDosis"].Value is DateTime)
            {
                dtpProxima.Value = (DateTime)f.Cells["proximaDosis"].Value;
                dtpProxima.Checked = true;
            }
            else
            {
                dtpProxima.Checked = false;
            }
            txtObservaciones.Text = f.Cells["observaciones"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            if (cboMascota.Items.Count > 0) cboMascota.SelectedIndex = 0;
            if (cboVacuna.Items.Count > 0) cboVacuna.SelectedIndex = 0;
            if (Sesion.UsuarioActual.Rol == "Veterinario")
                cboVeterinario.SelectedValue = Sesion.UsuarioActual.IdUsuario;
            else if (cboVeterinario.Items.Count > 0)
                cboVeterinario.SelectedIndex = 0;
            dtpAplicacion.Value = DateTime.Today;
            SugerirProximaDosis();
            txtObservaciones.Clear();
            cboMascota.Focus();
        }

        protected override bool Validar()
        {
            if (cboMascota.SelectedValue == null || cboVacuna.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione la mascota y la vacuna a aplicar.");
                return false;
            }
            if (cboVeterinario.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione el veterinario que aplica la vacuna.");
                cboVeterinario.Focus();
                return false;
            }
            if (dtpAplicacion.Value.Date > DateTime.Today)
            {
                Mensajes.Advertencia("La fecha de aplicación no puede ser futura.");
                dtpAplicacion.Focus();
                return false;
            }
            if (dtpProxima.Checked && dtpProxima.Value.Date <= dtpAplicacion.Value.Date)
            {
                Mensajes.Advertencia("La próxima dosis debe ser posterior a la fecha de aplicación.");
                dtpProxima.Focus();
                return false;
            }
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            AplicacionVacuna a = new AplicacionVacuna
            {
                IdAplicacion = IdSeleccionado,
                IdMascota = Convert.ToInt32(cboMascota.SelectedValue),
                IdVacuna = Convert.ToInt32(cboVacuna.SelectedValue),
                IdVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue),
                FechaAplicacion = dtpAplicacion.Value.Date,
                ProximaDosis = dtpProxima.Checked ? (DateTime?)dtpProxima.Value.Date : null,
                Observaciones = txtObservaciones.Text.Trim()
            };
            if (esNuevo) AplicacionVacunaDatos.Insertar(a); else AplicacionVacunaDatos.Actualizar(a);
        }

        protected override void EliminarRegistro(int id)
        {
            AplicacionVacunaDatos.Eliminar(id);
        }
    }
}

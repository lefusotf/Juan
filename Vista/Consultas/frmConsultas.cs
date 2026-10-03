using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Consultas
{
    /// <summary>Historial médico: cada consulta registrada por el veterinario forma parte del expediente de la mascota.</summary>
    public class frmConsultas : FormCrudBase
    {
        private readonly ComboBox cboMascota = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboVeterinario = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker dtpFecha = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };
        private readonly TextBox txtMotivo = new TextBox { MaxLength = 250 };
        private readonly TextBox txtDiagnostico = new TextBox { MaxLength = 500, Multiline = true, Height = 50, ScrollBars = ScrollBars.Vertical };
        private readonly TextBox txtTratamiento = new TextBox { MaxLength = 500, Multiline = true, Height = 50, ScrollBars = ScrollBars.Vertical };
        private readonly TextBox txtObservaciones = new TextBox { MaxLength = 500 };

        protected override string TituloFormulario { get { return "Historial médico (consultas)"; } }
        protected override string Modulo { get { return "Consultas"; } }
        protected override string ColumnaId { get { return "idConsulta"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.ConsultasGestionar); } }

        protected override void ConstruirCampos()
        {
            AgregarCampo("Mascota *", cboMascota);
            AgregarCampo("Veterinario *", cboVeterinario);
            AgregarCampo("Fecha y hora *", dtpFecha);
            AgregarCampo("Motivo *", txtMotivo);
            AgregarCampo("Diagnóstico *", txtDiagnostico, true);
            AgregarCampo("Tratamiento", txtTratamiento, true);
            AgregarCampo("Observaciones", txtObservaciones, true);

            try
            {
                cboMascota.DataSource = MascotaDatos.ListarParaCombo();
                cboMascota.DisplayMember = "descripcion";
                cboMascota.ValueMember = "idMascota";

                cboVeterinario.DataSource = UsuarioDatos.ListarVeterinarios();
                cboVeterinario.DisplayMember = "nombreCompleto";
                cboVeterinario.ValueMember = "idUsuario";
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        protected override DataTable ObtenerDatos(string filtro)
        {
            return ConsultaDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idConsulta", "idMascota", "idVeterinario", "observaciones");
            Encabezado("mascota", "Mascota");
            Encabezado("propietario", "Propietario");
            Encabezado("veterinario", "Veterinario");
            Encabezado("fecha", "Fecha");
            Encabezado("motivo", "Motivo");
            Encabezado("diagnostico", "Diagnóstico");
            Encabezado("tratamiento", "Tratamiento");
            if (dgv.Columns.Contains("fecha")) dgv.Columns["fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            cboMascota.SelectedValue = Convert.ToInt32(f.Cells["idMascota"].Value);
            cboVeterinario.SelectedValue = Convert.ToInt32(f.Cells["idVeterinario"].Value);
            dtpFecha.Value = (DateTime)f.Cells["fecha"].Value;
            txtMotivo.Text = f.Cells["motivo"].Value.ToString();
            txtDiagnostico.Text = f.Cells["diagnostico"].Value.ToString();
            txtTratamiento.Text = f.Cells["tratamiento"].Value.ToString();
            txtObservaciones.Text = f.Cells["observaciones"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            if (cboMascota.Items.Count > 0) cboMascota.SelectedIndex = 0;
            // Si quien registra es veterinario, se preselecciona a sí mismo
            if (Sesion.UsuarioActual.Rol == "Veterinario")
                cboVeterinario.SelectedValue = Sesion.UsuarioActual.IdUsuario;
            else if (cboVeterinario.Items.Count > 0)
                cboVeterinario.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            txtMotivo.Clear(); txtDiagnostico.Clear(); txtTratamiento.Clear(); txtObservaciones.Clear();
            cboMascota.Focus();
        }

        protected override bool Validar()
        {
            if (cboMascota.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione una mascota.");
                cboMascota.Focus();
                return false;
            }
            if (cboVeterinario.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione el veterinario que atendió la consulta.");
                cboVeterinario.Focus();
                return false;
            }
            if (dtpFecha.Value > DateTime.Now.AddMinutes(5))
            {
                Mensajes.Advertencia("La fecha de la consulta no puede ser futura.");
                dtpFecha.Focus();
                return false;
            }
            if (Falla(Validaciones.Requerido(txtMotivo.Text, "Motivo"), txtMotivo)) return false;
            if (Falla(Validaciones.Requerido(txtDiagnostico.Text, "Diagnóstico"), txtDiagnostico)) return false;
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            DateTime v = dtpFecha.Value;
            Consulta c = new Consulta
            {
                IdConsulta = IdSeleccionado,
                IdMascota = Convert.ToInt32(cboMascota.SelectedValue),
                IdVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue),
                Fecha = new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0),
                Motivo = txtMotivo.Text.Trim(),
                Diagnostico = txtDiagnostico.Text.Trim(),
                Tratamiento = txtTratamiento.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim()
            };
            if (esNuevo) ConsultaDatos.Insertar(c); else ConsultaDatos.Actualizar(c);
        }

        protected override void EliminarRegistro(int id)
        {
            ConsultaDatos.Eliminar(id);
        }
    }
}

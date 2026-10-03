using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Entidades;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Citas
{
    public class frmCitas : FormCrudBase
    {
        private readonly ComboBox cboMascota = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox cboVeterinario = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker dtpFechaHora = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };
        private readonly ComboBox cboEstado = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly TextBox txtMotivo = new TextBox { MaxLength = 250 };

        protected override string TituloFormulario { get { return "Citas médicas"; } }
        protected override string Modulo { get { return "Citas"; } }
        protected override string ColumnaId { get { return "idCita"; } }
        protected override bool PuedeGestionar { get { return Sesion.Tiene(Permisos.CitasGestionar); } }

        protected override void ConstruirCampos()
        {
            cboEstado.Items.AddRange(new object[] { "Programada", "Atendida", "Cancelada" });

            AgregarCampo("Mascota *", cboMascota);
            AgregarCampo("Veterinario *", cboVeterinario);
            AgregarCampo("Fecha y hora *", dtpFechaHora);
            AgregarCampo("Estado", cboEstado);
            AgregarCampo("Motivo *", txtMotivo, true);

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
            return CitaDatos.Listar(filtro);
        }

        protected override void ConfigurarColumnas()
        {
            OcultarColumnas("idCita", "idMascota", "idVeterinario");
            Encabezado("mascota", "Mascota");
            Encabezado("propietario", "Propietario");
            Encabezado("veterinario", "Veterinario");
            Encabezado("fechaHora", "Fecha y hora");
            Encabezado("motivo", "Motivo");
            Encabezado("estado", "Estado");
            if (dgv.Columns.Contains("fechaHora")) dgv.Columns["fechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        protected override void MostrarFila(DataGridViewRow f)
        {
            cboMascota.SelectedValue = Convert.ToInt32(f.Cells["idMascota"].Value);
            cboVeterinario.SelectedValue = Convert.ToInt32(f.Cells["idVeterinario"].Value);
            dtpFechaHora.Value = (DateTime)f.Cells["fechaHora"].Value;
            cboEstado.SelectedItem = f.Cells["estado"].Value.ToString();
            txtMotivo.Text = f.Cells["motivo"].Value.ToString();
        }

        protected override void LimpiarCampos()
        {
            if (cboMascota.Items.Count > 0) cboMascota.SelectedIndex = 0;
            if (cboVeterinario.Items.Count > 0) cboVeterinario.SelectedIndex = 0;
            dtpFechaHora.Value = DateTime.Now.AddHours(1);
            cboEstado.SelectedIndex = 0;
            txtMotivo.Clear();
            cboMascota.Focus();
        }

        private DateTime FechaSinSegundos()
        {
            DateTime v = dtpFechaHora.Value;
            return new DateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, 0);
        }

        protected override bool Validar()
        {
            if (cboMascota.SelectedValue == null)
            {
                Mensajes.Advertencia("Seleccione una mascota. Si no existe, regístrela primero en el módulo de mascotas.");
                cboMascota.Focus();
                return false;
            }
            if (cboVeterinario.SelectedValue == null)
            {
                Mensajes.Advertencia("No hay veterinarios activos disponibles. El administrador debe registrar uno.");
                cboVeterinario.Focus();
                return false;
            }
            if (Falla(Validaciones.Requerido(txtMotivo.Text, "Motivo"), txtMotivo)) return false;

            bool programada = cboEstado.SelectedItem.ToString() == "Programada";
            if (programada && IdSeleccionado == 0 && FechaSinSegundos() < DateTime.Now)
            {
                Mensajes.Advertencia("La fecha y hora de una cita nueva no puede estar en el pasado.");
                dtpFechaHora.Focus();
                return false;
            }
            if (programada)
            {
                try
                {
                    if (CitaDatos.VeterinarioOcupado(Convert.ToInt32(cboVeterinario.SelectedValue), FechaSinSegundos(), IdSeleccionado))
                    {
                        Mensajes.Advertencia("El veterinario ya tiene una cita programada a esa fecha y hora.");
                        dtpFechaHora.Focus();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Mensajes.Error(Modulo, ex, "guardar");
                    return false;
                }
            }
            return true;
        }

        protected override void GuardarRegistro(bool esNuevo)
        {
            Cita c = new Cita
            {
                IdCita = IdSeleccionado,
                IdMascota = Convert.ToInt32(cboMascota.SelectedValue),
                IdVeterinario = Convert.ToInt32(cboVeterinario.SelectedValue),
                FechaHora = FechaSinSegundos(),
                Motivo = txtMotivo.Text.Trim(),
                Estado = cboEstado.SelectedItem.ToString(),
                IdUsuarioRegistro = Sesion.UsuarioActual.IdUsuario
            };
            if (esNuevo) CitaDatos.Insertar(c); else CitaDatos.Actualizar(c);
        }

        protected override void EliminarRegistro(int id)
        {
            CitaDatos.Eliminar(id);
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.Mascotas
{
    public partial class Mascotas : FrmBase
    {
        private readonly PropietarioDAL _propietarioDAL = new PropietarioDAL();
        private readonly MascotaDAL _mascotaDAL = new MascotaDAL();
        private const string Modulo = "Mascotas";
        private int _id;
        private bool _cargando;

        public Mascotas()
        {
            InitializeComponent();
        }

        private void Mascotas_Load(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
            ConfigurarEntradas();
            CargarCombos();

            bool puede = Sesion.Tiene(Permisos.MascotasGestionar);
            btnNuevo.Enabled = puede;
            btnGuardar.Enabled = puede;
            btnEliminar.Enabled = puede;
            pnlDatos.Enabled = puede;

            CargarDatos();
            Nuevo();
        }

        private void ConfigurarEntradas()
        {
            txtBuscar.MaxLength = 60;
            Entrada.SoloLetras(txtNombre);
            Entrada.SoloLetras(cboEspecie);
            Entrada.SoloLetras(txtRaza);
            Entrada.SoloLetras(txtColor);
        }

        private void CargarCombos()
        {
            cboPropietario.DataSource = _propietarioDAL.Listar("");
            cboPropietario.DisplayMember = "nombre";
            cboPropietario.ValueMember = "idPropietario";
        }

        private void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = _mascotaDAL.Listar(txtBuscar.Text.Trim());
                ConfigurarColumnas();
                dgv.ClearSelection();
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
            }
            finally
            {
                _cargando = false;
            }
        }

        private void ConfigurarColumnas()
        {
            GridUtil.Ocultar(dgv, "idMascota", "idPropietario");
            GridUtil.Encabezado(dgv, "propietario", "Propietario");
            GridUtil.Encabezado(dgv, "nombre", "Nombre");
            GridUtil.Encabezado(dgv, "especie", "Especie");
            GridUtil.Encabezado(dgv, "raza", "Raza");
            GridUtil.Encabezado(dgv, "sexo", "Sexo");
            GridUtil.Encabezado(dgv, "fechaNacimiento", "Nacimiento");
            GridUtil.Encabezado(dgv, "peso", "Peso (kg)");
            GridUtil.Encabezado(dgv, "color", "Color");
            GridUtil.Formato(dgv, "fechaNacimiento", "dd/MM/yyyy");
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                _id = Convert.ToInt32(dgv.CurrentRow.Cells["idMascota"].Value);
                MostrarFila(dgv.CurrentRow);
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
            }
        }

        private void MostrarFila(DataGridViewRow f)
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

        private void Nuevo()
        {
            _id = 0;
            LimpiarCampos();
            dgv.ClearSelection();
        }

        private void LimpiarCampos()
        {
            if (cboPropietario.Items.Count > 0) cboPropietario.SelectedIndex = 0;
            txtNombre.Clear();
            cboEspecie.Text = "";
            txtRaza.Clear();
            txtColor.Clear();
            cboSexo.SelectedIndex = 0;
            dtpNacimiento.Value = DateTime.Today;
            dtpNacimiento.Checked = false;
            nudPeso.Value = 0;
            txtNombre.Focus();
        }

        private bool Validar()
        {
            if (cboPropietario.SelectedValue == null)
            {
                Mensajes.Advertencia("Debe existir y seleccionarse un propietario. Registre primero al propietario.");
                cboPropietario.Focus();
                return false;
            }
            if (Mensajes.Invalido(Validaciones.Nombre(txtNombre.Text, "Nombre"), txtNombre)) return false;
            if (Mensajes.Invalido(Validaciones.Nombre(cboEspecie.Text, "Especie"), cboEspecie)) return false;
            if (Mensajes.Invalido(Validaciones.NombreOpcional(txtRaza.Text, "Raza"), txtRaza)) return false;
            if (Mensajes.Invalido(Validaciones.NombreOpcional(txtColor.Text, "Color"), txtColor)) return false;

            if (dtpNacimiento.Checked)
            {
                if (dtpNacimiento.Value.Date > DateTime.Today)
                {
                    Mensajes.Invalido("La fecha de nacimiento no puede ser futura.", dtpNacimiento);
                    return false;
                }
                if (dtpNacimiento.Value.Date < DateTime.Today.AddYears(-40))
                {
                    Mensajes.Invalido("La fecha de nacimiento no puede ser de hace más de 40 años.", dtpNacimiento);
                    return false;
                }
            }

            if (nudPeso.Value > 150 &&
                !Mensajes.Confirmar("El peso ingresado (" + nudPeso.Value + " kg) es muy alto para una mascota.\n¿Es correcto?"))
            {
                nudPeso.Focus();
                return false;
            }

            try
            {
                string nombre = Texto.Capitalizar(txtNombre.Text);
                if (_mascotaDAL.ExisteNombreEnPropietario(Convert.ToInt32(cboPropietario.SelectedValue), nombre, _id))
                {
                    Mensajes.Invalido("Este propietario ya tiene una mascota llamada '" + nombre + "'.", txtNombre);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
                return false;
            }
            return true;
        }

        private void GuardarRegistro(bool esNuevo)
        {
            int idPropietario = Convert.ToInt32(cboPropietario.SelectedValue);
            string nombre = Texto.Capitalizar(txtNombre.Text);
            string especie = Texto.Capitalizar(cboEspecie.Text);
            string raza = Texto.Capitalizar(txtRaza.Text);
            string sexo = cboSexo.SelectedIndex == 0 ? "M" : "H";
            DateTime? nacimiento = dtpNacimiento.Checked ? (DateTime?)dtpNacimiento.Value.Date : null;
            decimal? peso = nudPeso.Value > 0 ? (decimal?)nudPeso.Value : null;
            string color = Texto.Capitalizar(txtColor.Text);
            if (esNuevo) _mascotaDAL.Insertar(idPropietario, nombre, especie, raza, sexo, nacimiento, peso, color);
            else _mascotaDAL.Actualizar(_id, idPropietario, nombre, especie, raza, sexo, nacimiento, peso, color);
        }

        private void EliminarRegistro(int id)
        {
            _mascotaDAL.Eliminar(id);
        }

        private void Mascotas_Resize(object sender, EventArgs e)
        {
            Responsive.Aplicar(tlpCampos, ClientSize.Width);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CargarDatos();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;
            bool esNuevo = _id == 0;
            try
            {
                GuardarRegistro(esNuevo);
                Logger.Info(Modulo, esNuevo ? "Registro creado" : "Registro actualizado (id " + _id + ")");
                Mensajes.Info(esNuevo ? "Registro guardado correctamente." : "Registro actualizado correctamente.");
                CargarDatos();
                Nuevo();
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                Mensajes.Advertencia("Seleccione un registro de la tabla para eliminarlo.");
                return;
            }
            if (!Mensajes.Confirmar("¿Está seguro de eliminar el registro seleccionado?")) return;
            try
            {
                int id = _id;
                EliminarRegistro(id);
                Logger.Info(Modulo, "Registro eliminado (id " + id + ")");
                Mensajes.Info("Registro eliminado correctamente.");
                CargarDatos();
                Nuevo();
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, Modulo);
            }
        }
    }
}

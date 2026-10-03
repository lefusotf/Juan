using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Modelos.Utilidades;

namespace Vista.Comun
{
    /// <summary>
    /// Formulario base para los mantenimientos (CRUD). Construye una interfaz uniforme:
    /// título, buscador, campos de captura, botones y tabla. Cada formulario hijo solo define
    /// sus campos y cómo guardar/eliminar, así todo el sistema mantiene la misma experiencia de usuario.
    /// </summary>
    public abstract class FormCrudBase : Form
    {
        protected readonly Label lblTitulo = new Label();
        protected readonly TextBox txtBuscar = new TextBox();
        protected readonly Button btnBuscar = new Button();
        protected readonly GroupBox gbDatos = new GroupBox();
        protected readonly TableLayoutPanel tlpCampos = new TableLayoutPanel();
        protected readonly Button btnNuevo = new Button();
        protected readonly Button btnGuardar = new Button();
        protected readonly Button btnEliminar = new Button();
        protected readonly DataGridView dgv = new DataGridView();

        private int _columna;
        private int _fila;
        private bool _cargando;

        protected int IdSeleccionado { get; private set; }

        // ---- A implementar en cada formulario ----
        protected abstract string TituloFormulario { get; }
        protected abstract string Modulo { get; }
        protected abstract string ColumnaId { get; }
        protected abstract bool PuedeGestionar { get; }
        protected abstract void ConstruirCampos();
        protected abstract DataTable ObtenerDatos(string filtro);
        protected abstract void MostrarFila(DataGridViewRow fila);
        protected abstract void LimpiarCampos();
        protected abstract bool Validar();
        protected abstract void GuardarRegistro(bool esNuevo);
        protected abstract void EliminarRegistro(int id);
        protected virtual void ConfigurarColumnas() { }

        protected FormCrudBase()
        {
            Font = Estilo.Fuente;
            BackColor = Estilo.Fondo;
            Text = "Mantenimiento";
            ConstruirDiseno();
        }

        private void ConstruirDiseno()
        {
            TableLayoutPanel raiz = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(16, 10, 16, 10)
            };
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            raiz.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            raiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            raiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = Estilo.FuenteTitulo;
            lblTitulo.ForeColor = Estilo.PrimarioOscuro;
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;

            // Barra de búsqueda
            FlowLayoutPanel busqueda = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
            Label lblBuscar = new Label { Text = "Buscar:", AutoSize = true, Margin = new Padding(0, 8, 6, 0) };
            txtBuscar.Width = 280;
            txtBuscar.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; CargarDatos(); }
            };
            btnBuscar.Text = "Buscar";
            Estilo.Boton(btnBuscar, Estilo.Neutro);
            btnBuscar.Height = 28;
            btnBuscar.Width = 80;
            btnBuscar.Click += (s, e) => CargarDatos();
            busqueda.Controls.AddRange(new Control[] { lblBuscar, txtBuscar, btnBuscar });

            // Campos
            gbDatos.Text = "Datos";
            gbDatos.Dock = DockStyle.Fill;
            gbDatos.AutoSize = true;
            gbDatos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbDatos.Padding = new Padding(10, 6, 10, 10);
            tlpCampos.Dock = DockStyle.Top;
            tlpCampos.AutoSize = true;
            tlpCampos.ColumnCount = 4;
            tlpCampos.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tlpCampos.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            gbDatos.Controls.Add(tlpCampos);

            // Botones
            FlowLayoutPanel botones = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(0, 8, 0, 0) };
            btnNuevo.Text = "Nuevo";
            btnGuardar.Text = "Guardar";
            btnEliminar.Text = "Eliminar";
            Estilo.Boton(btnNuevo, Estilo.Neutro);
            Estilo.Boton(btnGuardar, Estilo.Primario);
            Estilo.Boton(btnEliminar, Estilo.Peligro);
            btnNuevo.Click += (s, e) => Nuevo();
            btnGuardar.Click += (s, e) => Guardar();
            btnEliminar.Click += (s, e) => Eliminar();
            botones.Controls.AddRange(new Control[] { btnNuevo, btnGuardar, btnEliminar });

            // Tabla
            Estilo.Grid(dgv);
            dgv.SelectionChanged += (s, e) => SeleccionCambio();

            raiz.Controls.Add(lblTitulo, 0, 0);
            raiz.Controls.Add(busqueda, 0, 1);
            raiz.Controls.Add(gbDatos, 0, 2);
            raiz.Controls.Add(botones, 0, 3);
            raiz.Controls.Add(dgv, 0, 4);
            Controls.Add(raiz);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblTitulo.Text = TituloFormulario;
            ConstruirCampos();

            // Control de permisos: sin permiso de gestión solo se puede consultar
            btnNuevo.Enabled = PuedeGestionar;
            btnGuardar.Enabled = PuedeGestionar;
            btnEliminar.Enabled = PuedeGestionar;
            gbDatos.Enabled = PuedeGestionar;

            CargarDatos();
            Nuevo();
        }

        /// <summary>Agrega una etiqueta con su control al panel de captura (dos campos por fila).</summary>
        protected void AgregarCampo(string etiqueta, Control control, bool anchoCompleto = false)
        {
            if (anchoCompleto && _columna != 0) { _columna = 0; _fila++; }

            Label lbl = new Label { Text = etiqueta, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 8, 8, 3) };
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(3, 4, 12, 4);

            if (tlpCampos.RowCount <= _fila) tlpCampos.RowCount = _fila + 1;

            tlpCampos.Controls.Add(lbl, _columna, _fila);
            tlpCampos.Controls.Add(control, _columna + 1, _fila);

            if (anchoCompleto)
            {
                tlpCampos.SetColumnSpan(control, 3);
                _columna = 0;
                _fila++;
            }
            else
            {
                _columna += 2;
                if (_columna >= 4) { _columna = 0; _fila++; }
            }
        }

        /// <summary>Muestra un mensaje de validación y devuelve true si hay error (también enfoca el control).</summary>
        protected bool Falla(string error, Control foco)
        {
            if (error == null) return false;
            Mensajes.Advertencia(error);
            if (foco != null) foco.Focus();
            return true;
        }

        protected void OcultarColumnas(params string[] nombres)
        {
            foreach (string n in nombres)
                if (dgv.Columns.Contains(n)) dgv.Columns[n].Visible = false;
        }

        protected void Encabezado(string columna, string texto)
        {
            if (dgv.Columns.Contains(columna)) dgv.Columns[columna].HeaderText = texto;
        }

        protected void CargarDatos()
        {
            try
            {
                _cargando = true;
                dgv.DataSource = ObtenerDatos(txtBuscar.Text.Trim());
                ConfigurarColumnas();
                dgv.ClearSelection();
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
            finally
            {
                _cargando = false;
            }
        }

        private void SeleccionCambio()
        {
            if (_cargando || dgv.CurrentRow == null || !dgv.CurrentRow.Selected) return;
            try
            {
                IdSeleccionado = Convert.ToInt32(dgv.CurrentRow.Cells[ColumnaId].Value);
                MostrarFila(dgv.CurrentRow);
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "cargar");
            }
        }

        protected void Nuevo()
        {
            IdSeleccionado = 0;
            LimpiarCampos();
            dgv.ClearSelection();
        }

        private void Guardar()
        {
            if (!Validar()) return;
            bool esNuevo = IdSeleccionado == 0;
            try
            {
                GuardarRegistro(esNuevo);
                Logger.Info(Modulo, (esNuevo ? "Registro creado" : "Registro actualizado") + " (id " + IdSeleccionado + ")");
                Mensajes.Info(esNuevo ? "Registro guardado correctamente." : "Registro actualizado correctamente.");
                CargarDatos();
                Nuevo();
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "guardar");
            }
        }

        private void Eliminar()
        {
            if (IdSeleccionado == 0)
            {
                Mensajes.Advertencia("Seleccione un registro de la tabla para eliminarlo.");
                return;
            }
            if (!Mensajes.Confirmar("¿Está seguro de eliminar el registro seleccionado?")) return;
            try
            {
                int id = IdSeleccionado;
                EliminarRegistro(id);
                Logger.Info(Modulo, "Registro eliminado (id " + id + ")");
                Mensajes.Info("Registro eliminado correctamente.");
                CargarDatos();
                Nuevo();
            }
            catch (Exception ex)
            {
                Mensajes.Error(Modulo, ex, "eliminar");
            }
        }
    }
}

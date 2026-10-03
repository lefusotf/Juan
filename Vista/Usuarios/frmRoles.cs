using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Usuarios
{
    /// <summary>Asignación de permisos a cada rol desde dentro del sistema.</summary>
    public class frmRoles : Form
    {
        private class ItemPermiso
        {
            public int IdPermiso;
            public string Codigo;
            public string Descripcion;
            public override string ToString() { return Descripcion + "  [" + Codigo + "]"; }
        }

        private readonly ListBox lstRoles = new ListBox();
        private readonly CheckedListBox clbPermisos = new CheckedListBox();
        private readonly Label lblDescripcion = new Label();
        private readonly Button btnGuardar = new Button();
        private DataTable _roles;
        private bool _cargando;

        public frmRoles()
        {
            Font = Estilo.Fuente;
            BackColor = Estilo.Fondo;
            Text = "Roles y permisos";

            Label titulo = new Label
            {
                Text = "Roles y permisos",
                Font = Estilo.FuenteTitulo,
                ForeColor = Estilo.PrimarioOscuro,
                Dock = DockStyle.Top,
                Height = 48,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0)
            };

            GroupBox gbRoles = new GroupBox { Text = "Roles", Dock = DockStyle.Left, Width = 260, Padding = new Padding(10) };
            lstRoles.Dock = DockStyle.Fill;
            lstRoles.SelectedIndexChanged += (s, e) => MostrarPermisosDelRol();
            gbRoles.Controls.Add(lstRoles);

            GroupBox gbPermisos = new GroupBox { Text = "Permisos del rol seleccionado", Dock = DockStyle.Fill, Padding = new Padding(10) };
            lblDescripcion.Dock = DockStyle.Top;
            lblDescripcion.Height = 30;
            clbPermisos.Dock = DockStyle.Fill;
            clbPermisos.CheckOnClick = true;

            btnGuardar.Text = "Guardar permisos";
            Estilo.Boton(btnGuardar, Estilo.Primario);
            btnGuardar.Width = 170;
            btnGuardar.Dock = DockStyle.Bottom;
            btnGuardar.Click += (s, e) => GuardarPermisos();

            gbPermisos.Controls.Add(clbPermisos);
            gbPermisos.Controls.Add(btnGuardar);
            gbPermisos.Controls.Add(lblDescripcion);

            Panel cuerpo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16, 0, 16, 16) };
            cuerpo.Controls.Add(gbPermisos);
            cuerpo.Controls.Add(gbRoles);

            Controls.Add(cuerpo);
            Controls.Add(titulo);

            Load += (s, e) => CargarCatalogos();
        }

        private void CargarCatalogos()
        {
            try
            {
                _cargando = true;
                foreach (DataRow f in RolDatos.ListarPermisos().Rows)
                {
                    clbPermisos.Items.Add(new ItemPermiso
                    {
                        IdPermiso = (int)f["idPermiso"],
                        Codigo = f["codigo"].ToString(),
                        Descripcion = f["descripcion"].ToString()
                    });
                }

                _roles = RolDatos.Listar();
                lstRoles.DataSource = _roles;
                lstRoles.DisplayMember = "nombre";
                lstRoles.ValueMember = "idRol";
            }
            catch (Exception ex)
            {
                Mensajes.Error("Roles", ex, "cargar");
            }
            finally
            {
                _cargando = false;
            }
            MostrarPermisosDelRol();
        }

        private void MostrarPermisosDelRol()
        {
            if (_cargando || lstRoles.SelectedItem == null) return;
            try
            {
                DataRowView fila = (DataRowView)lstRoles.SelectedItem;
                lblDescripcion.Text = fila["descripcion"].ToString();
                HashSet<int> asignados = RolDatos.PermisosDeRol((int)fila["idRol"]);
                for (int i = 0; i < clbPermisos.Items.Count; i++)
                    clbPermisos.SetItemChecked(i, asignados.Contains(((ItemPermiso)clbPermisos.Items[i]).IdPermiso));
            }
            catch (Exception ex)
            {
                Mensajes.Error("Roles", ex, "cargar");
            }
        }

        private void GuardarPermisos()
        {
            if (lstRoles.SelectedItem == null)
            {
                Mensajes.Advertencia("Seleccione un rol.");
                return;
            }
            if (!Mensajes.Confirmar("¿Guardar los permisos seleccionados para este rol?")) return;

            try
            {
                DataRowView fila = (DataRowView)lstRoles.SelectedItem;
                List<int> ids = new List<int>();
                List<string> codigos = new List<string>();
                foreach (object o in clbPermisos.CheckedItems)
                {
                    ItemPermiso p = (ItemPermiso)o;
                    ids.Add(p.IdPermiso);
                    codigos.Add(p.Codigo);
                }

                RolDatos.GuardarPermisos((int)fila["idRol"], fila["nombre"].ToString(), ids, codigos);
                Logger.Info("Roles", "Permisos actualizados para el rol " + fila["nombre"]);
                Mensajes.Info("Permisos guardados. Los usuarios del rol los verán en su próximo inicio de sesión.");
            }
            catch (Exception ex)
            {
                Mensajes.Error("Roles", ex, "guardar");
            }
        }
    }
}

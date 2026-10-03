using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Utilidades;
using Vista.Comun;

namespace Vista.Usuarios
{
    /// <summary>Asignación de permisos a cada rol desde dentro del sistema.</summary>
    public partial class frmRoles : Form
    {
        private class ItemPermiso
        {
            public int IdPermiso;
            public string Codigo;
            public string Descripcion;
            public override string ToString() { return Descripcion + "  [" + Codigo + "]"; }
        }

        private bool _cargando;

        public frmRoles()
        {
            InitializeComponent();
        }

        private void frmRoles_Load(object sender, EventArgs e)
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

                lstRoles.DataSource = RolDatos.Listar();
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

        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void btnGuardar_Click(object sender, EventArgs e)
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

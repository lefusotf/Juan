using System;
using System.Data;
using System.Windows.Forms;
using dashboardVet.Base;
using dashboardVet.Controles;
using dashboardVet.Helpers;
using Modelos;

namespace dashboardVet.Inicio
{
    public partial class Inicio : UserControl
    {
        private readonly DashboardDAL _dashboardDAL = new DashboardDAL();
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            if (DesignMode || !Sesion.HaySesion) return;

            lblBienvenida.Text = "Hola, " + Sesion.UsuarioActual.NombreCompleto;
            lblSubtitulo.Text = "Resumen general de la clínica";

            pnlTProp.Visible = Sesion.Tiene(Permisos.PropietariosVer);
            pnlTMasc.Visible = Sesion.Tiene(Permisos.MascotasVer);
            pnlTCita.Visible = Sesion.Tiene(Permisos.CitasVer);
            pnlTCons.Visible = Sesion.Tiene(Permisos.ConsultasVer);
            pnlTVac.Visible = Sesion.Tiene(Permisos.VacunasVer);
            pnlCitas.Visible = Sesion.Tiene(Permisos.CitasVer);

            try
            {
                DataRow r = _dashboardDAL.Resumen();
                lblVProp.Text = r["propietarios"].ToString();
                lblVMasc.Text = r["mascotas"].ToString();
                lblVCita.Text = r["citas"].ToString();
                lblVCons.Text = r["consultas"].ToString();
                lblVVac.Text = r["vacunas"].ToString();

                if (pnlCitas.Visible)
                {
                    dgvCitas.DataSource = _dashboardDAL.ProximasCitas();
                    GridUtil.Encabezado(dgvCitas, "fechaHora", "Fecha y hora");
                    GridUtil.Encabezado(dgvCitas, "mascota", "Mascota");
                    GridUtil.Encabezado(dgvCitas, "propietario", "Propietario");
                    GridUtil.Encabezado(dgvCitas, "veterinario", "Veterinario");
                    GridUtil.Encabezado(dgvCitas, "motivo", "Motivo");
                    GridUtil.Formato(dgvCitas, "fechaHora", "dd/MM/yyyy HH:mm");
                }
            }
            catch (Exception ex)
            {
                ManejadorUIErrores.MostrarError(ex, "Inicio");
            }
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using Modelos.Datos;
using Modelos.Seguridad;
using Vista.Comun;

namespace Vista.Dashboard
{
    /// <summary>Pantalla de inicio: tarjetas con estadísticas y próximas citas.</summary>
    public partial class ucInicio : UserControl
    {
        public ucInicio()
        {
            InitializeComponent();
        }

        private void ucInicio_Load(object sender, EventArgs e)
        {
            if (DesignMode || !Sesion.HaySesion) return;   // el diseñador de Visual Studio también ejecuta Load

            lblBienvenida.Text = "Hola, " + Sesion.UsuarioActual.NombreCompleto;
            lblSubtitulo.Text = "Resumen general de la clínica";

            // Cada tarjeta solo se muestra si el rol puede consultar ese módulo
            pnlTProp.Visible = Sesion.Tiene(Permisos.PropietariosVer);
            pnlTMasc.Visible = Sesion.Tiene(Permisos.MascotasVer);
            pnlTCita.Visible = Sesion.Tiene(Permisos.CitasVer);
            pnlTCons.Visible = Sesion.Tiene(Permisos.ConsultasVer);
            pnlTVac.Visible = Sesion.Tiene(Permisos.VacunasVer);
            pnlCitas.Visible = Sesion.Tiene(Permisos.CitasVer);

            try
            {
                DataRow r = InicioDatos.Resumen();
                lblVProp.Text = r["propietarios"].ToString();
                lblVMasc.Text = r["mascotas"].ToString();
                lblVCita.Text = r["citas"].ToString();
                lblVCons.Text = r["consultas"].ToString();
                lblVVac.Text = r["vacunas"].ToString();

                if (pnlCitas.Visible)
                {
                    dgvCitas.DataSource = InicioDatos.ProximasCitas();
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
                Mensajes.Error("Inicio", ex, "cargar");
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Modelos;

namespace dashboardVet.Helpers
{
    public static class ManejadorUIErrores
    {
        public static void MostrarError(Exception ex, string tituloFormulario = "Atención")
        {
            Logger.Error(tituloFormulario, ex);

            if (ex is AppException appEx)
            {
                string mensajeModal = $"{appEx.Message}\n\n[Código de Error: {appEx.CodigoError}]";
                MessageBox.Show(mensajeModal, tituloFormulario, MessageBoxButtons.OK, appEx.CodigoError.StartsWith("ERR-NEG") || appEx.CodigoError.StartsWith("ERR-VAL") ? MessageBoxIcon.Warning : MessageBoxIcon.Error);
            }
            else
            {
                string mensajeModal = "Ocurrió un error inesperado en el sistema.\n\n[Código de Error: ERR-GEN-001]";
                MessageBox.Show(mensajeModal, tituloFormulario, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AsignarTooltipsFormulario(Control contenedor)
        {
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 400,
                ReshowDelay = 100,
                ShowAlways = true,
                ToolTipIcon = ToolTipIcon.Info,
                ToolTipTitle = "Ayuda rápida"
            };

            foreach (Control ctrl in contenedor.Controls)
            {
                if (ctrl.HasChildren)
                {
                    AsignarTooltipsFormulario(ctrl);
                }

                if (string.IsNullOrEmpty(toolTip.GetToolTip(ctrl)))
                {
                    string nombreCtrl = ctrl.Name.ToLower();

                    if (ctrl is Button btn)
                    {
                        if (nombreCtrl.Contains("guardar") || nombreCtrl.Contains("save") ||
                            nombreCtrl.Contains("add") || nombreCtrl.Contains("crear") ||
                            nombreCtrl.Contains("registrar") || nombreCtrl.Contains("aceptar") ||
                            nombreCtrl.Contains("insertar") || nombreCtrl.Contains("ingresar"))
                        {
                            toolTip.SetToolTip(btn, "Guarda o registra la información ingresada en el sistema.");
                        }
                        else if (nombreCtrl.Contains("editar") || nombreCtrl.Contains("update") ||
                                 nombreCtrl.Contains("modificar") || nombreCtrl.Contains("actualizar"))
                        {
                            toolTip.SetToolTip(btn, "Modifica los datos del registro seleccionado.");
                        }
                        else if (nombreCtrl.Contains("eliminar") || nombreCtrl.Contains("delete") ||
                                 nombreCtrl.Contains("borrar") || nombreCtrl.Contains("quitar") ||
                                 nombreCtrl.Contains("remover"))
                        {
                            toolTip.SetToolTip(btn, "Elimina permanentemente el registro seleccionado.");
                        }
                        else if (nombreCtrl.Contains("buscar") || nombreCtrl.Contains("search") ||
                                 nombreCtrl.Contains("filtrar") || nombreCtrl.Contains("consultar"))
                        {
                            toolTip.SetToolTip(btn, "Realiza la búsqueda con los criterios ingresados.");
                        }
                        else if (nombreCtrl.Contains("pdf") || nombreCtrl.Contains("reporte") ||
                                 nombreCtrl.Contains("exportar") || nombreCtrl.Contains("excel") ||
                                 nombreCtrl.Contains("imprimir"))
                        {
                            toolTip.SetToolTip(btn, "Genera y exporta la información actual a un documento.");
                        }
                        else if (nombreCtrl.Contains("limpiar") || nombreCtrl.Contains("clear") ||
                                 nombreCtrl.Contains("cancelar") || nombreCtrl.Contains("nuevo"))
                        {
                            toolTip.SetToolTip(btn, "Limpia los campos del formulario para un nuevo registro.");
                        }
                    }
                    else if (ctrl is TextBox txt)
                    {
                        if (nombreCtrl.Contains("buscar") || nombreCtrl.Contains("filtro"))
                            toolTip.SetToolTip(txt, "Escriba aquí el término o nombre que desea buscar.");
                        else
                            toolTip.SetToolTip(txt, "Ingrese la información correspondiente a este campo.");
                    }
                    else if (ctrl is ComboBox)
                    {
                        toolTip.SetToolTip(ctrl, "Despliegue para seleccionar una opción de la lista.");
                    }
                    else if (ctrl is DateTimePicker)
                    {
                        toolTip.SetToolTip(ctrl, "Seleccione una fecha de la lista desplegable.");
                    }
                    else if (ctrl is DataGridView)
                    {
                        toolTip.SetToolTip(ctrl, "Haga clic en un registro para seleccionarlo o en los encabezados para ordenar.");
                    }
                }
            }
        }
    }
}

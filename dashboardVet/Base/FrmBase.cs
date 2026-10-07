using System;
using System.Windows.Forms;
using dashboardVet.Helpers;

namespace dashboardVet.Base
{
    public class FrmBase : Form
    {
        public FrmBase()
        {
            DoubleBuffered = true;
            KeyPreview = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
                ManejadorUIErrores.AsignarTooltipsFormulario(this);
        }
    }
}

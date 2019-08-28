using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MetroFramework.Controls.MetroTextBox;

namespace WFAPersonelTakibi.Helpers
{
    public class HelperMethod
    {
        public static void ClearControls(Control cs)
        {
            foreach (Control control in cs.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
                else if (control is ComboBox)
                {
                    ComboBox cmb = (ComboBox)control;
                    cmb.SelectedIndex = -1;
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)control;
                    dtp.Value = DateTime.Now;
                }
                else if (control is PictureBox)
                {
                    PictureBox pcb = (PictureBox)control;
                    pcb.Image = null;
                }
                else if (control is GroupBox)
                {
                    GroupBox gb = (GroupBox)control;
                    ClearControls(gb);

                }
            }
        }
    }
}

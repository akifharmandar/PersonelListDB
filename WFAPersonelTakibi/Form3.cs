using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using WFAPersonelTakibi.Models.Context;
using WFAPersonelTakibi.Models.Entities;
namespace WFAPersonelTakibi
{
    public partial class Form3 : MetroForm
    {
        private Guid _iD;

        public Form3()
        {
            InitializeComponent();
        }

        MyContext Context = new MyContext();

        public Form3(Guid iD):this()
        {
            this._iD = iD;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Personel personel = Context.Personels.Find(_iD);
            foreach (PropertyInfo property in personel.GetType().GetProperties())
            {
                var controls = this.Controls.Find(property.Name, true);

                if (controls[0] is Label)
                {
                    controls[0].Text = property.GetValue(personel).ToString();
                }
                else if (controls[0] is PictureBox)
                {
                    PictureBox pcb = (PictureBox)controls[0];
                    pcb.Image = Image.FromFile(Environment.CurrentDirectory + @"\..\..\Images" + property.GetValue(personel).ToString());

                }
            }
        }
    }
}

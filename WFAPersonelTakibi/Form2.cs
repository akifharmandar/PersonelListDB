using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using WFAPersonelTakibi.Models.Context;
using WFAPersonelTakibi.Models.Entities;


namespace WFAPersonelTakibi
{
    using MetroFramework.Forms;
    using Models.Context;
    using System;
    using System.Linq;

    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        MyContext context = new MyContext();
       

        

        void PersonelEkleme()
        {
            dgvEmployees.DataSource = context.Personels.Select(x => new
            {
                x.ID,
                x.Name,
                x.LastName,
                x.Gender,
                x.Mail,
                x.Phone,
                x.Address,
                Departman = x.Department.Name
            });
                
        }

        void PersonelEkleme(Guid ID)
        {
            dgvEmployees.DataSource = context.Personels
                .Where (x=> x.Department.DepartmentID ==ID)
                .Select(x => new
            {
                x.ID,
                x.Name,
                x.LastName,
                x.Gender,
                x.Mail,
                x.Phone,
                x.Address,
                Departman = x.Department.Name
                });

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PersonelEkleme();
            comboBox1.DataSource = context.Personels.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "DepartmentID";
            comboBox1.SelectedIndex = -1;

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PersonelEkleme((Guid)comboBox1.SelectedValue);
        }

        private void tsmDuzenle_Click(object sender, EventArgs e)
        {
           Guid ID =(Guid)dgvEmployees.SelectedRows[0].Cells[0].Value;

            Form4 frm = new Form4(ID);
            this.Hide();
            frm.ShowDialog();

        }

        private void tsmSil_Click(object sender, EventArgs e)
        {
            Guid ID = (Guid)dgvEmployees.SelectedRows[0].Cells[0].Value;
            var personel = context.Personels.Find(ID);
            context.Personels.Remove(personel);
            context.SaveChanges();
            PersonelEkleme();
        }

        private void tsmYeni_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();
        }

        private void tsmDetay_Click(object sender, EventArgs e)
        {
            Guid ID = (Guid)dgvEmployees.SelectedRows[0].Cells[0].Value;

            Form3 frm = new Form3(ID);
            this.Hide();
            frm.ShowDialog();
        }       
    }
}

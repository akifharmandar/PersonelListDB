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
using static WFAPersonelTakibi.Helpers.HelperMethod;

namespace WFAPersonelTakibi
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        MyContext context = new MyContext();

        void PersonelEkleme()
        {
            txtAddress.Text = FakeData.PlaceData.GetAddress();
            txtFirstName.Text = FakeData.NameData.GetFirstName();
            txtLastName.Text = FakeData.NameData.GetSurname();
            txtMail.Text = FakeData.NetworkData.GetEmail();
            txtPhone.Text = FakeData.PhoneNumberData.GetPhoneNumber();
            dtBirthDate.Value = FakeData.DateTimeData.GetDatetime();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbDepartment.DataSource = context.Departments.ToList();
            cmbDepartment.DisplayMember = "Name";
            cmbDepartment.ValueMember = "DepartmentID";
            
        }

        Personel personel;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (personel==null)
            {
                personel = new Personel();
            }

            if (!personel.HasImage)
            {
                MessageBox.Show(this,"Lütfen resim yükleyiniz","Resim Yükleme Bildirimi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            
            personel.Name = txtFirstName.Text;
            personel.LastName = txtLastName.Text;
            personel.BirthDate = dtBirthDate.Value;
            personel.Mail = txtMail.Text;
            for (int i = 0; i < metroPanel1.Controls.Count; i++)
            {
                if (metroPanel1.Controls[i] is RadioButton)
                {
                    RadioButton rd = (RadioButton)metroPanel1.Controls[i];
                    if (rd.Checked)
                    {
                        personel.Gender = (Gender)Enum.Parse(typeof(Gender), rd.Text);
                    }
                }
            }
            personel.Address = txtAddress.Text;
            personel.Phone = txtPhone.Text;
            personel.Department.DepartmentID = (Guid)cmbDepartment.SelectedValue;

            context.Personels.Add(personel);
            bool result = context.SaveChanges() > 0;
            personel = null;
            
            ClearControls(this);
            MessageBox.Show(this,result ? "Resim Eklendi":"Kayıt Başarısız Oldu","Kayıt Ekleme Bildirimi",MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            MessageBox.Show(this,result ? "Kayıt Eklendi" : "Kayıt Ekleme İşlemi Başarısız","Kayıt Ekleme İşlemi",MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void pcbImageUrl_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "jpg files (*.jpg)|*jpg|png files (*.png)|*.png";
            if (opf.ShowDialog()==DialogResult.OK)
            {
                if (personel==null)
                {
                    personel = new Personel();
                }

                pcbImageUrl.Image = Image.FromFile(opf.FileName);
                personel.ImageUrl = $"{Guid.NewGuid()}.{System.IO.Path.GetExtension(opf.FileName)}";
                pcbImageUrl.Image.Save($@"{Environment.CurrentDirectory}\..\..\Images\{ personel.ImageUrl}");
                personel.HasImage = true;

            }
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {
            PersonelEkleme();
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();
        }
    }
}

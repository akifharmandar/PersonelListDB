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
    public partial class Form4 : MetroForm
    {
        private Guid _iD;

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(Guid iD):this()
        {
            this._iD = iD;
        }
        MyContext context = new MyContext();

        Personel personel_;

        private void Form4_Load(object sender, EventArgs e)
        {
            cmbDepartment.DataSource = context.Personels.ToList();
            cmbDepartment.DisplayMember = "Name";
            cmbDepartment.ValueMember = "DepartmentID";

            personel_ = context.Personels.Find(_iD);

            txtFirstName.Text = personel_.Name;
            txtLastName.Text = personel_.LastName;
            dtBirthDate.Value= personel_.BirthDate;
            txtMail.Text = personel_.Mail;
            txtAddress.Text = personel_.Address;
            txtPhone.Text = personel_.Phone;
            cmbDepartment.SelectedValue = personel_.Department.DepartmentID;

            var collection = metroPanel1.Controls.Find("rd" + personel_.Gender.ToString(),true);
            RadioButton radio = (RadioButton)collection[0];
            radio.Checked = true;
            pcbImageUrl.Image = Image.FromFile(Environment.CurrentDirectory + @"\..\..\Images\" + personel_.ImageUrl);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (personel_.ImageUrl==null)
            {
                MessageBox.Show(this, "Lütfen resim yükleyiniz", "Resim Yükleme Bildirimi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            personel_.Name = txtFirstName.Text;
            personel_.LastName = txtLastName.Text;
            personel_.BirthDate = dtBirthDate.Value;
            personel_.Mail = txtMail.Text;
            for (int i = 0; i < metroPanel1.Controls.Count; i++)
            {
                if (metroPanel1.Controls[i] is RadioButton)
                {
                    RadioButton rd = (RadioButton)metroPanel1.Controls[i];
                    if (rd.Checked)
                    {
                        personel_.Gender = (Gender)Enum.Parse(typeof(Gender), rd.Text);
                    }
                }
            }
            personel_.Address = txtAddress.Text;
            personel_.Phone = txtPhone.Text;
            personel_.Department.DepartmentID = (Guid)cmbDepartment.SelectedValue;

            
            bool result = context.SaveChanges() > 0;
            personel_ = null;

            ClearControls(this);
            MessageBox.Show(this, result ? "Resim Eklendi" : "Kayıt Başarısız Oldu", "Kayıt Ekleme Bildirimi", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            MessageBox.Show(this, result ? "Kayıt Eklendi" : "Kayıt Ekleme İşlemi Başarısız", "Kayıt Ekleme İşlemi", MessageBoxButtons.OK,
                result ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void pcbImageUrl_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "jpg files (*.jpg)|*jpg|png files (*.png)|*.png";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                if (personel_ == null)
                {
                    personel_ = new Personel();
                }

                pcbImageUrl.Image = Image.FromFile(opf.FileName);
                personel_.ImageUrl = $"{Guid.NewGuid()}.{System.IO.Path.GetExtension(opf.FileName)}";
                pcbImageUrl.Image.Save($@"{Environment.CurrentDirectory}\..\..\Images\{ personel_.ImageUrl}");
                personel_.HasImage = true;

            }
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            this.Hide();
            frm.ShowDialog();
        }
    }
    }


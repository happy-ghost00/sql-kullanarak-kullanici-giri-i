using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
         
        Database1Entities database1Entities = new Database1Entities();
        private DialogResult uyar;

        private void Form1_Load(object sender, EventArgs e)
        {
            database1Entities.Database.Connection.ConnectionString = "data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=C:\\Users\\stajer\\source\\repos\\WindowsFormsApp1\\WindowsFormsApp1\\Database1.mdf;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            var liste = database1Entities.Tabledenemes.ToList();
            dataGridView1.DataSource = liste;
        }
        void son()
        {
            var liste = database1Entities.Tabledenemes.ToList();
            dataGridView1.DataSource = liste;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var user = database1Entities.Tabledenemes.Where(x => x.username == textBox1.Text.Trim() && x.password == textBox2.Text.Trim() && x.age == Convert.ToInt32(textBox3.Text.Trim()));

            if (user.Count() == 1 )
            {
                MessageBox.Show("Doğru Girdiniz");
            }
            else
            {
                MessageBox.Show("Yanlış Giriş","ERROR");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        { 
            if (database1Entities.Tabledenemes.Where(x=>x.username==textBox1.Text.Trim()).Any())
            {
                MessageBox.Show("Böyle Bir Kullanıcı Zaten Var!!", "ERROR");
            }
            else
            {
                Tabledeneme ROW = new Tabledeneme();

                ROW.username = textBox1.Text.Trim();
                ROW.password= textBox2.Text.Trim();
                ROW.age = Convert.ToInt32(textBox3.Text.Trim());
                
                database1Entities.Tabledenemes.Add(ROW);
                MessageBox.Show("Kaydediliyor");

                database1Entities.SaveChanges();
                son();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            uyar = MessageBox.Show(this, textBox1.Text + " Kullanıcısının Kaydını Silmek istiyor musunuz?", "SİLME UYARISI", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (uyar == DialogResult.Yes)
            {
                Tabledeneme a= database1Entities.Tabledenemes.Where(x => x.username == textBox1.Text.Trim()).FirstOrDefault();
                if (a != null)
                {
                    database1Entities.Tabledenemes.Remove(a);
                    database1Entities.SaveChanges();
                    son();
                }
                else
                {
                      MessageBox.Show("Böyle Bir Kullanıcı Yok", "Hata");
                }
            }
            else
            { }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var guncelle_un = database1Entities.Tabledenemes.Find(passwordDataGridViewTextBoxColumn);
            guncelle_un.username = textBox1.Text;
            
            database1Entities.SaveChanges();
            son();
            MessageBox.Show("Bilgileriniz Başarıyla Güncellendi");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var guncelle_pas = database1Entities.Tabledenemes.Find(usernameDataGridViewTextBoxColumn);
            guncelle_pas.password = textBox2.Text;
            
            database1Entities.SaveChanges();
            son();
            MessageBox.Show("Şifreniz Başarıyla Güncellendi");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {        }
        private void button6_Click(object sender, EventArgs e)
        {
            var guncelle_age = database1Entities.Tabledenemes.Where(w => w.username == textBox1.Text.Trim()).FirstOrDefault();
            if (guncelle_age != null)
            {
                guncelle_age.age = Convert.ToInt32(textBox3.Text.Trim());

                database1Entities.SaveChanges();
                son();
                MessageBox.Show("Yaşınız Değiştirildi");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

            var max_age = database1Entities.Tabledenemes.Max(w => w.age);
            var a=database1Entities.Tabledenemes.Where(w => w.age == max_age).FirstOrDefault();
            label5.Text = a.username;
        }
    }
}
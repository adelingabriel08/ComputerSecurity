using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetInitialData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CryptographyService.SetSymmetricAlgorithm(this.comboBox1.SelectedIndex.ToString());
        }

        private void SetInitialData()
        {
            this.comboBox1.DataSource = Enum.GetValues(typeof(SymmetricAlgorithmEnum));
            var (key, iv) = CryptographyService.InitKeyAndIV();
            textBox5.Text = Convert.ToHexString(key);
            textBox6.Text = Convert.ToHexString(iv);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CryptographyService.InitKeyAndIV();
            var (key, iv) = CryptographyService.InitKeyAndIV();
            textBox5.Text = Convert.ToHexString(key);
            textBox6.Text = Convert.ToHexString(iv);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            CryptographyService.SetKey(Convert.FromHexString(textBox5.Text));
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
                CryptographyService.SetIV(Convert.FromHexString(textBox6.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bytes = Encoding.UTF8.GetBytes(textBox1.Text);
            var cipher = CryptographyService.Encrypt(bytes);
            textBox2.Text = Convert.ToHexString(cipher);
            label3.Text = CryptographyService.EncryptTime.ElapsedMilliseconds.ToString() + " ms";
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var bytes = Convert.FromHexString(textBox3.Text);
            var ascii = CryptographyService.Decrypt(bytes);
            textBox4.Text = Encoding.UTF8.GetString(ascii);
            label12.Text = CryptographyService.DecryptTime.ElapsedMilliseconds.ToString() + " ms";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

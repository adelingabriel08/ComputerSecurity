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
            CryptographyService.InitKeyAndIV();
            textBox5.Text = CryptographyService.Key;
            textBox6.Text = CryptographyService.IV;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

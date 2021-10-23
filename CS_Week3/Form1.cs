using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Week3
{
    public partial class Form1 : Form
    {
        private HMAC myMAC = new HMACSHA1();
        private byte[] Key;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Key = Encoding.ASCII.GetBytes(textBox1.Text);
        }

        public byte[] ComputeMAC(byte[] mes, byte[] key)
        {
            myMAC.Key = key;
            return myMAC.ComputeHash(mes);
        }

        public int MACByteLength() => myMAC.HashSize / 8;

        private bool CompareByteArrays(byte[] a, byte[] b, int len)
        {
            for (int i = 0; i < len; i++)
                if (a[i] != b[i]) return false;
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mac = ComputeMAC(Encoding.ASCII.GetBytes(textBox2.Text), Key);
            textBox3.Text = Convert.ToBase64String(mac);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

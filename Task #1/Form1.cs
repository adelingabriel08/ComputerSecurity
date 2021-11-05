using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CS_Week3
{
    public partial class Form1 : Form
    {
        private HMAC myMAC = new HMACSHA1();
        private byte[] Key = new byte[16];
        private Stopwatch timer = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            var algorithms = new List<Enum>();
            algorithms.AddRange(Enum.GetValues(typeof(HMACs)).Cast<Enum>());
            this.comboBox1.DataSource = algorithms;
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
            byte[] computedMac;
            if (textBox3.Text.Length > 0) computedMac = Convert.FromBase64String(textBox3.Text);
            else computedMac = Convert.FromHexString(textBox4.Text);

            timer.Reset();
            timer.Start();
            var result = ComputeMAC(Encoding.ASCII.GetBytes(textBox2.Text), Key);
            timer.Stop();

            if (CompareByteArrays(computedMac, result, computedMac.Length)) label9.Text = "It's a match!";
            else label9.Text = "It's not a match!";

            label8.Text = "Time spent: " + (1000000000.0 * (double)timer.ElapsedTicks / Stopwatch.Frequency) + " ns";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Reset();
            timer.Start();
            var mac = ComputeMAC(Encoding.ASCII.GetBytes(textBox2.Text), Key);
            timer.Stop();

            textBox3.Text = Convert.ToBase64String(mac);
            textBox4.Text = Convert.ToHexString(mac);

            label8.Text = "Time spent: " + (1000000000.0 * (double)timer.ElapsedTicks / Stopwatch.Frequency) + " ns";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var alg = Enum.Parse(typeof(HMACs), this.comboBox1.SelectedIndex.ToString());
            switch (alg)
            {
                case HMACs.HMACSHA1:
                    myMAC = new HMACSHA1();
                    break;
                case HMACs.HMACSHA256:
                    myMAC = new HMACSHA256();
                    break;
                case HMACs.HMACSHA384:
                    myMAC = new HMACSHA384();
                    break;
                case HMACs.HMACSHA512:
                    myMAC = new HMACSHA512();
                    break;
                case HMACs.HMACMD5:
                    myMAC = new HMACMD5();
                    break;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}

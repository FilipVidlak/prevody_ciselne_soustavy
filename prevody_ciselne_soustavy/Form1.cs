using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prevody_ciselne_soustavy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int BinToDec(string hodnota)
        {
            char[] cisla = hodnota.ToCharArray();
            Array.Reverse(cisla); // zprava od nejmensiho po nejvetsi
            //dal beru index a davam dvojku na i-tou
            // pokud je i rovno nule, je to 2 na nultou, pricitam jednicku
            int soucet = 0;
            for (int i = 0; i < cisla.Length; i++)
            {
                if (cisla[i] == '1')
                {
                    if (i == 0) { soucet++; }
                    else { soucet += (int)Math.Pow(2, i); }
                }
            }
            return soucet;
        }
        private string DecToBin(int cislo)
        {
            string vysledek = "";
            int d = cislo;
            int a = -1;
            int b = -1;
            while (a != 1)
            {
                b = d % 2;
                a = d / 2;
                d = a;
                vysledek += b.ToString();
            }
            vysledek += a.ToString();
            return vysledek;

        }
        private int HexToDec(string value)
        {
            int vysledek = 0;
            int pocet = value.Length - 1;
            for (int i = 0; i < value.Length; i++)
            {
                int x = 0;
                switch (value[i])
                {
                    case 'A': x = 10; break;
                    case 'B': x = 11; break;
                    case 'C': x = 12; break;
                    case 'D': x = 13; break;
                    case 'E': x = 14; break;
                    case 'F': x = 15; break;
                    default: x = -48/*ascii kod*/ + (int)value[i]; break;
                }
                vysledek += x * (int)(Math.Pow(16, pocet));
            }
            return vysledek;
        }
        private string DecToHex(int x)
        {
            string vys = "";

            while (x != 0)
            {
                if ((x % 16) < 10)
                    vys = x % 16 + vys;
                else
                {
                    string znaky = "";

                    switch (x % 16)
                    {
                        case 10: znaky = "A"; break;
                        case 11: znaky = "B"; break;
                        case 12: znaky = "C"; break;
                        case 13: znaky = "D"; break;
                        case 14: znaky = "E"; break;
                        case 15: znaky = "F"; break;
                    }

                    vys = znaky + vys;
                }

                x = x / 16;
            }

            return vys;
        }
        private string HexToBin(string hex)
        {
            string bin = "";
            bin = (Convert.ToInt32(hex, 16), 2).ToString();
            return bin;
        }
        private string BinToHex(string bin)
        {
            int cast = bin.Length % 4;
            if (cast != 0)
                bin = new string('0', 4 - cast) + bin;

            string vystup = "";

            for (int i = 0; i <= bin.Length - 4; i += 4)
            {
                vystup += string.Format("{0:X}", Convert.ToByte(bin.Substring(i, 4), 2));
            }
            return vystup;
        }
        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != '0' && e.KeyChar != '1') { e.Handled = true; }
                int vystup = BinToDec(textBox1.Text);
                textBox2.Text = vystup.ToString();
                textBox3.Text = BinToHex(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Nastala chyba!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (int.Parse(e.KeyChar.ToString()) < 0 && int.Parse(e.KeyChar.ToString()) > 9)
                {
                    e.Handled = true;
                }
                string vystup = DecToHex(int.Parse(textBox2.Text));
                textBox3.Text = vystup;
                textBox1.Text = DecToBin(int.Parse(textBox2.Text));
            }
            catch
            {
                e.Handled = true;
            }
        }


        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar < 48 || (int)e.KeyChar > 70 || (int)e.KeyChar > 57 && (int)e.KeyChar < 65)
            {
                e.Handled = true;
            }
            textBox1.Text = HexToBin(textBox3.Text);
            int vystup = HexToDec(textBox1.Text);
            textBox2.Text = vystup.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            Application.Restart();
        }
    }
}
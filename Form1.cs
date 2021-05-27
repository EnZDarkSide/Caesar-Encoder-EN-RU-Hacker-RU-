using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var key = textBox3.Text;

            if(!key.All(ch => Vigenere.c_alphabet.Contains(ch)))
            {
                MessageBox.Show("Ключ должен состоять из символов русского алфавита");
                return;
            }

            textBox2.Text = Vigenere.Encode(textBox1.Text, key);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var key = "";

            textBox2.Text = Vigenere.Hack(textBox1.Text, ref key);
            textBox3.Text = key;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var key = textBox3.Text;
            if (!key.All(ch => Vigenere.c_alphabet.Contains(ch)))
            {
                MessageBox.Show("Ключ должен состоять из символов русского алфавита");
                return;
            }
            textBox2.Text = Vigenere.Encode(textBox1.Text, textBox3.Text, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

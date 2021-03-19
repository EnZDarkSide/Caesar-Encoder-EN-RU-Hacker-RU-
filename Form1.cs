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
            int shift = 0;
            try
            {
                shift = int.Parse(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Сдвигом должно быть целое число");
            }

            textBox2.Text = Caesar.Encode(textBox1.Text, shift);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int key = 0;
            textBox2.Text = Caesar.Hack(textBox1.Text, ref key);

            textBox3.Text = key.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int shift = 0;
            try
            {
                shift = int.Parse(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Сдвигом должно быть целое число");
            }

            textBox2.Text = Caesar.Encode(textBox1.Text, -shift);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}

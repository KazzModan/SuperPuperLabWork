using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace lw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1_TextUpdate(null, null);
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (var item in comboBox1.Items)
            {
                if (comboBox1.Text == item.ToString())
                    counter++;
            }
            if (counter == 0)
                comboBox1.Items.Add(comboBox1.Text);
            else
                MessageBox.Show("This city already exist");
        }
        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (var item in comboBox2.Items)
            {
                if (comboBox2.Text == item.ToString())
                    counter++;
            }
            if (counter == 0)
                comboBox2.Items.Add(comboBox2.Text);
            else
                MessageBox.Show("This street already exist");
        }
        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (var item in comboBox3.Items)
            {
                if (comboBox3.Text == item.ToString())
                    counter++;
            }
            if (counter == 0)
                comboBox3.Items.Add(comboBox3.Text);
            else
                MessageBox.Show("This position already exist");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2_TextUpdate(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadFromFile();
            comboBox1.Items.AddRange(LoadParameter("..\\..\\City.txt"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox3_TextUpdate(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text + " " + comboBox1.Text + " " + comboBox2.Text
                + " " + comboBox3.Text + " " + textBox2.Text + "$";
            if (comboBox1.Text != "" && comboBox2.Text != ""
                && comboBox3.Text != "" && textBox1.Text != "" && textBox2.Text != "")
                checkedListBox1.Items.Add(text);
            else
                MessageBox.Show("one of fields is empty");
            WriteToFile();
        }
        void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter("..\\..\\text.txt"))
            {

                foreach (var item in checkedListBox1.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }
        void ReadFromFile()
        {
            using (StreamReader reader = new StreamReader("..\\..\\text.txt"))
            {
                string str = "";
                while (!reader.EndOfStream)
                {
                    str = reader.ReadLine();
                    checkedListBox1.Items.Add(str);
                }
                LaunchProgressBar();
            }
        }
        void LaunchProgressBar()
        {
            progressBar1.Maximum = checkedListBox1.Items.Count;
            progressBar1.Step = 1;
            for (int i = 0; i < progressBar1.Maximum; i++)
            {
                progressBar1.PerformStep();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                int size = checkedListBox1.CheckedItems.Count;
                for (int i = 0; i < size; i++)
                    checkedListBox1.Items.Remove(checkedListBox1.CheckedItems[0]);
            }
            else
                MessageBox.Show("Please, choose at least 1 item");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == ""
             || comboBox3.Text == "" || textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("one of fields is empty");
            else if (checkedListBox1.CheckedItems.Count == 1)
            {
                string text = textBox1.Text + " " + comboBox1.Text + " " + comboBox2.Text
              + " " + comboBox3.Text + " " + textBox2.Text + "$";
                int index = checkedListBox1.Items.IndexOf(checkedListBox1.SelectedItem);
                List<string> list = new List<string>();
                for (int i = index + 1; i < checkedListBox1.Items.Count; i++)
                {
                    list.Add(checkedListBox1.Items[i].ToString());
                    checkedListBox1.Items.Remove(checkedListBox1.Items[i]);
                }
                checkedListBox1.Items.RemoveAt(index);
                checkedListBox1.Items.Add(text);
                checkedListBox1.Items.AddRange(list.ToArray());
            }
            else if (checkedListBox1.CheckedItems.Count == 0)
                MessageBox.Show("Please, choose at least 1 item");

            else
                MessageBox.Show("You choosed too many items");
        }

        private string[] LoadParameter(string file)
        {
            if (!File.Exists(file))
                return new string[0];

            string[] splitDate = new string[0];


            try
            {
                string data = File.ReadAllText(file);
                splitDate = data.Split('\n');

                string[] delete = new string[]
                {
                        "<option>",
                        "</option>",
                };

                for (int i = 0; i < splitDate.Length; i++)
                {
                    for (int j = 0; j < delete.Length; j++)
                    {
                        int index = splitDate[i].IndexOf(delete[j]);

                        if (index == -1)
                            continue;

                        splitDate[i] = splitDate[i].Remove(index, delete[j].Length);
                     
                    }
                    int indexR = splitDate[i].Length - 1;

                    if (indexR == -1)
                        continue;

                    splitDate[i] = splitDate[i].Remove(indexR, 1);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("corrapted file");
                return new string[0];
            }

            return splitDate;
        }
    }
}

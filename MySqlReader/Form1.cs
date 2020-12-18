using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySqlReader
{
    public partial class Form1 : Form
    {
        private VisualSetter _visual;
        private DatabaseConnector _connector;
        private AddForm _adder;
        public Form1()
        {
            InitializeComponent();
            
            comboBox1.Items.AddRange(new string[] { "Материнські плати", "Виробники мат. плат", "Чіпсети", "Сокети", "Звукові чіпи", "Оперативна пам'ять" });
            _visual = new VisualSetter(dataGridView_output);

            _connector = new DatabaseConnector();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                _visual.Show(_connector.LoadItems(comboBox1.SelectedIndex));
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            string[] names = new string[dataGridView_output.Columns.Count];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = dataGridView_output.Columns[i].Name;
            }
            if (comboBox1.SelectedIndex != -1)
            {
                _adder = new AddForm(_connector);
                _adder.ShowWindow(comboBox1.SelectedItem.ToString(), names);
            }
                
        }
    }
}

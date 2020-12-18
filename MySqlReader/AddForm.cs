using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MySqlReader
{
    public partial class AddForm : Form
    {
        private DatabaseConnector database;
        string[] _names;
        int _table;
        public AddForm(DatabaseConnector connector)
        {
            InitializeComponent();

            database = connector;
        }

        public void ShowWindow(string tableName, string[] names)
        {
            _names = names;
            
            switch (tableName)
            {
                case "Материнські плати":
                    _table = 1;
                    Motherboards();
                    break;
                case "Виробники мат. плат":
                    _table = 2;
                    Producers();
                    break;
                case "Чіпсети":
                    _table = 3;
                    Chipsets();
                    break;
                case "Сокети":
                    _table = 4;
                    Sockets();
                    break;
                case "Звукові чіпи":
                    _table = 5;
                    SoundChips();
                    break;
                case "Оперативна пам'ять":
                    _table = 6;
                    Ram();
                    break;
                default:
                    break;
            }
            Show();
        }

        private void Ram()
        {
            for (int i = 0; i < 6; i++)
            {
                TextBox text = new TextBox();
                text.PlaceholderText = _names[i + 1];
                text.Location = new Point(10, 10 + 50 * i);
                text.Width = 500;
                Controls.Add(text);
            }
        }

        private void SoundChips()
        {
            for (int i = 0; i < 4; i++)
            {
                TextBox text = new TextBox();
                text.PlaceholderText = _names[i + 1];
                text.Location = new Point(10, 10 + 50 * i);
                text.Width = 500;
                Controls.Add(text);
            }
        }

        private void Sockets()
        {
            for (int i = 0; i < 5; i++)
            {
                TextBox text = new TextBox();
                text.PlaceholderText = _names[i + 1];
                text.Location = new Point(10, 10 + 50 * i);
                text.Width = 500;
                Controls.Add(text);
            }
        }

        private void Chipsets()
        {
            for (int i = 0; i < 4; i++)
            {
                TextBox text = new TextBox();
                text.PlaceholderText = _names[i + 1];
                text.Location = new Point(10, 10 + 50 * i);
                text.Width = 500;
                Controls.Add(text);
            }
        }

        private void Producers()
        {
            for (int i = 0; i < 4; i++)
            {
                TextBox text = new TextBox();
                text.PlaceholderText = _names[i + 1];
                text.Location = new Point(10, 10 + 50 * i);
                text.Width = 500;
                Controls.Add(text);
            }
        }

        private void Motherboards()
        {
            for (int i = 0; i < 18; i++)
            {
                if (i >= 2 && i < 6)
                {
                    ComboBox box = new ComboBox();
                    Label l = new Label() { Text = _names[i + 1] };
                    box.Location = new Point(110, 10 + 50 * i);
                    box.Width = 500;

                    box.Items.AddRange(database.Load(ConfigurationManager.AppSettings["get" + i]).ToArray());

                    l.Location = new Point(10, 10 + 50 * i);
                    Controls.Add(box);
                    Controls.Add(l);
                }
                else
                {
                    TextBox text = new TextBox();
                    text.PlaceholderText = _names[i + 1];
                    text.Location = new Point(110, 10 + 50 * i);
                    text.Width = 500;
                    Controls.Add(text);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = ConfigurationManager.AppSettings["add" + _table];
            List<string> values = new List<string>();
            if (_table != 1)
            {
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        values.Add(item.Text);
                    }
                }
            }
            string zzz = string.Format(query, values.ToArray());
            database.Send(zzz);
                
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}

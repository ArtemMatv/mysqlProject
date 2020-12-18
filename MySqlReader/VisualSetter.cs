using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MySqlReader
{
    public class VisualSetter
    {
        private DataGridView _data;
        public VisualSetter(DataGridView table)
        {
            _data = table;
        }

        public void Show(List<string[]> data)
        {
            _data.Columns.Clear();
            _data.Rows.Clear();

            _data.ColumnCount = data[0].Length;
            _data.RowCount = 1;

            for (int i = 0; i < data[0].Length; i++)
                _data.Columns[i].Name = data[0][i];

            for (int i = 1; i < data.Count; i++)
            {
                _data.RowCount++;
                for (int j = 0; j < data[i].Length; j++)
                {
                    _data[j, i - 1].Value = data[i][j];
                }
            }
        }
    }
}

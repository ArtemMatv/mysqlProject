using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows.Forms;

namespace MySqlReader
{
    public class DatabaseConnector
    {
        private string _connection;

        public DatabaseConnector()
        {
            _connection = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
        }

        public List<string[]> LoadItems(int index)
        {
            List<string[]> result = new List<string[]>();
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                string query = "";
                bool isMotherboard = false;

                switch (index)
                {
                    case 0:
                        query = ConfigurationManager.AppSettings["selectMotherboards"];
                        isMotherboard = true;
                        break;
                    case 1:
                        query = ConfigurationManager.AppSettings["selectMotherboarsProducers"];
                        break;
                    case 2:
                        query = ConfigurationManager.AppSettings["selectChipsets"];
                        break;
                    case 3:
                        query = ConfigurationManager.AppSettings["selectSockets"];
                        break;
                    case 4:
                        query = ConfigurationManager.AppSettings["selectSoundChips"];
                        break;
                    case 5:
                        query = ConfigurationManager.AppSettings["selectRam"];
                        break;
                    default:
                        break;
                }

                MySqlCommand comm = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader reader = comm.ExecuteReader();

                int fieldCount = reader.FieldCount + (isMotherboard ? 1 : 0);
                result.Add(new string[fieldCount]);

                int j = 1;

                while (reader.Read())
                {
                    result.Add(new string[fieldCount]);

                    int decreaser = 0;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        if (i == 11 && isMotherboard)
                        {
                            result[0][i] = "Підтримувані ОЗП";
                            decreaser = 1;
                        }
                        else
                        {
                            result[0][i] = reader.GetName(i - decreaser);
                            result[j][i] = reader[i - decreaser].ToString();
                        }
                    }
                    j++;
                }

                reader.Close();
                reader.Dispose();

                if (isMotherboard)
                    for (int i = 1; i < result.Count; i++)
                    {
                        string RamSupport = "";
                        MySqlCommand commRAM = new MySqlCommand(ConfigurationManager.AppSettings["selectRamSupport"] + result[i][0], conn);
                        using (MySqlDataReader readerRAM = commRAM.ExecuteReader())
                        {
                            while (readerRAM.Read())
                                RamSupport += readerRAM[0] + ";\n";

                            result[i][11] = RamSupport;
                        }
                    }
            }
            return result;
        }

        internal List<string> Load(string query)
        {
            List<string> result = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                MySqlCommand comm = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["name"].ToString());
                }
            }
            return result;
        }

        internal void Send(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(_connection))
            {
                MySqlCommand comm = new MySqlCommand(query, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
    }
}

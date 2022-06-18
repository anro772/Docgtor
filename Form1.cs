using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace doctor
{
    public partial class Form1 : Form
    {
        public AddForm addForm;
        public EditForm editForm;
        public List<Doctor> doctors = new List<Doctor>();
        public const string ConnectionString = "Data Source=database.db";
        public Form1()
        {
            InitializeComponent();
            //loadDoctors();
            selectDB();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            addForm = new AddForm(this);
            addForm.Show();
        }
        public void loadDoctors()
        {
            lvDoctors.Items.Clear();
            foreach(Doctor d in doctors)
            {
                ListViewItem lv = new ListViewItem(d.Id.ToString());
                lv.SubItems.Add(d.Name);
                lv.SubItems.Add(d.BirthDate.ToShortDateString());
                lv.SubItems.Add(d.Wage.ToString());
                lv.SubItems.Add(d.IdSpecialty.ToString());
                lvDoctors.Items.Add(lv);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string index = lvDoctors.SelectedItems[0].SubItems[0].Text;
            int i = int.Parse(index);
            foreach(Doctor d in doctors)
            {
                if (d.Id == i)
                {
                    doctors.Remove(d);
                    break;
                }
            }
            deleteDB(i);
            loadDoctors();
        }

        public void deleteDB(int id)
        {
            string query = "DELETE FROM Doctor WHERE Id=" + id;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                
            }
        }

        private void lvDoctors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lvDoctors.SelectedIndices[0];
            editForm = new EditForm(this, index, doctors[index]);
            editForm.Show();
        }
        public void addDB(Doctor d)
        {
            string query = "INSERT INTO Doctor(Name,BirthDate,Wage,IdSpecialty) Values(@Name,@BirthDate,@Wage,@IdSpecialty);"
                + "SELECT last_insert_rowid()";
            using(SQLiteConnection connection=new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Name", d.Name);
                command.Parameters.AddWithValue("@BirthDate", d.BirthDate);
                command.Parameters.AddWithValue("@Wage", d.Wage);
                command.Parameters.AddWithValue("@IdSpecialty", d.IdSpecialty);
                d.Id = (int)(long)command.ExecuteScalar();
            }
        }
        public void selectDB()
        {
            string query = "SELECT * FROM Doctor";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Doctor d = new Doctor(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), 
                            reader.GetDecimal(3), reader.GetInt32(4));
                        doctors.Add(d);
                    }
                }
            }
            loadDoctors();
        }

        private void tsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog.Title = "Save an XML File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Doctor>));
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    serializer.Serialize(writer, doctors);
                }

            }

        }
    }
}

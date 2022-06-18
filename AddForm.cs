using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doctor
{
    public partial class AddForm : Form
    {
        public Form1 parent;
        public List<Specialty> specialties = new List<Specialty>();
        public static int id = 0;
        public AddForm()
        {
            InitializeComponent();
        }
        public AddForm(Form1 form)
        {
            InitializeComponent();
            parent = form;
            readFile();
        }
        public void readFile()
        {
            string[] lines = File.ReadAllLines("C:\\C# WAP\\doctor\\doctor\\Specialties.txt");
            foreach(string line in lines)
            {
                listBox.Items.Add(line);
                Specialty s = new Specialty(id, line);
                id++;
                specialties.Add(s);
            }
        }

        public static int doctorId = 0;
        private void addBtn_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            DateTime birthDate = tbBirth.Value;
            decimal wage = decimal.Parse(tbWage.Text);
            int idSpecialty = listBox.SelectedIndex;
            Doctor d = new Doctor(name, birthDate, wage, idSpecialty);
            parent.addDB(d);
            parent.doctors.Add(d);
            parent.loadDoctors();
            
        }
    }
}

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
    public partial class EditForm : Form
    {

        public Form1 parent;
        public int i;
        public EditForm()
        {
            InitializeComponent();
        }
        public EditForm(Form1 form, int index, Doctor d)
        {
            InitializeComponent();
            parent = form;
            readFile();
            tbName.Text = d.Name;
            tbBirth.Value = d.BirthDate;
            tbWage.Text = d.Wage.ToString();
            listBox.SelectedIndex = d.IdSpecialty;
            i = index;
        }

        public void readFile()
        {
            string[] lines = File.ReadAllLines("C:\\C# WAP\\doctor\\doctor\\Specialties.txt");
            foreach (string line in lines)
            {
                listBox.Items.Add(line);        
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            parent.doctors[i].Name = tbName.Text;
            parent.doctors[i].BirthDate = tbBirth.Value;
            parent.doctors[i].Wage = decimal.Parse(tbWage.Text);
            parent.doctors[i].IdSpecialty = listBox.SelectedIndex;
            parent.loadDoctors();
            this.Close();
        }
    }
}

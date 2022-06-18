using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctor
{
    [Serializable]
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Wage { get; set; }
        public int IdSpecialty { get; set; }
        public Doctor()
        {
            
        }
        public Doctor(int id, string Name, DateTime birthDate, decimal wage, int IdSpecialty)
        {
            this.Id = id;
            this.Name = Name;
            this.BirthDate = birthDate;
            this.Wage = wage;
            this.IdSpecialty = IdSpecialty;
        }
        public Doctor(string Name, DateTime birthDate, decimal wage, int IdSpecialty)
        {
           
            this.Name = Name;
            this.BirthDate = birthDate;
            this.Wage = wage;
            this.IdSpecialty = IdSpecialty;
        }
    }
}

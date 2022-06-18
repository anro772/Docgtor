using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctor
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public Specialty(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}

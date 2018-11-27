using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionGroup.DomainClasses
{
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int PhoneNr { get; set; }
        public string Email { get; set; }
        public Project AktivProject { get; set; }
        public Project InaktivProject { get; set; }
        public void PrintInfo()
        {
            Console.WriteLine($"Navn: {Name} "
                + $"\n Telefon: {PhoneNr} "
                + $"\n Email: {Email} "
                + $"\n  Aktive projekter: {AktivProject}"
                + $"\n Inaktive projekter: {InaktivProject}");



        }
    }

}

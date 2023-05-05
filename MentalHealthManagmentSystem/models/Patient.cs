using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentalHealthManagmentSystem.models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public int PatientAge { get; set; }
        public string PatientHistory { get; set; }
        public string PatientGender { get; set; }


    }
}

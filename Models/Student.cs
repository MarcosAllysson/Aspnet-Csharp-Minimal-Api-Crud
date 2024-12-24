using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalApiCrud.Models
{
    public class Student : Base
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public Student(string name, bool isActive = true)
        {
            Name = name;
            IsActive = isActive;
        }

        public void UpdateStudentName(string name)
        {
            Name = name;
        }

        public void InactivateStudent()
        {
            IsActive = false;
        }
    }
}
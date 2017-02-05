using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movemate_api.Models
{
    public class StudentFacade
    {
        public Student CreateStudent(String name, String surname, String email, String facebookId)
        {
            Student student = new Models.Student();
            student.Name = name;
            student.Surname = surname;
            student.Email = email;
            student.Verified = false;
            Random generator = new Random();
            String code = generator.Next(100000, 1000000).ToString("D6");
            student.VerificationCode = code;
            return student;
        }

    }
}
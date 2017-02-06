using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace movemate_api.Models
{
    public class StudentFacade
    {
        static readonly char[] AvailableCharacters = {
             '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };
        public Student CreateStudent(String name, String surname, String email, String facebookId)
        {
            Student student = new Models.Student();
            student.Name = name;
            student.Surname = surname;
            student.Email = email;
            student.Verified = false;
            String code = GenerateRandomCode(6);
            student.VerificationCode = code;
            return student;
        }

        public Student AddVerificationCode(Student student)
        {
            String code = GenerateRandomCode(6);
            student.VerificationCode = code;
            return student;
        }

        private static String GenerateRandomCode(int length)
        {
            char[] identifier = new char[length];
            byte[] random = new byte[length];
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(random);
            }
            for (int i = 0; i<identifier.Length; i++)
            {
                int pos = random[i] % AvailableCharacters.Length;
                identifier[i] = AvailableCharacters[pos];
            }
            return new string(identifier);
        }

    }
}
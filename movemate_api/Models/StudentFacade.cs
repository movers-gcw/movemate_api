﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace movemate_api.Models
{
    public static class StudentFacade
    {
        static readonly char[] AvailableCharacters = {
             '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
        };
        public static Student CreateStudent(String name, String surname, String email, String facebookId)
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

        public static Student AddVerificationCode(Student student)
        {
            String code = GenerateRandomCode(6);
            student.VerificationCode = code;
            return student;
        }

        public static StudentView ViewFromStudent(Student student)
        {
            var view = new StudentView();
            view.Name = student.Name;
            view.Surname = student.Surname;
            view.Email = student.Email;
            view.StudentId = student.StudentId;
            return view;
        }

        public static List<StudentView> ViewFromParticipants(List<Student> students)
        {
            var list = new List<StudentView>();
            
            foreach(Student s in students)
            {
                list.Add(StudentFacade.ViewFromStudent(s));
            }
            return list;
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
using System;
using System.Collections.Generic;

namespace BOSSAZ
{
    abstract class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        private int age;
        public int Age
        {
            get => age;
            set
            {
                if (value < 18)
                {
                    throw new Exception("Age should be at least 18!");
                }
                age = value;
            }
        }

        private string mail;
        public string Mail {
            get => mail;
            set
            {
                if (!value.Contains('@'))
                {
                    throw new Exception("Wrong email type!");
                }
                mail = value;
            }
        }
        
        private string phone;
        public string Phone  
        {
            get => phone; 
            set
            {
                if (!(value.Length > 6 && value.Length < 15))
                {
                    throw new Exception("Length of phone number is wrong!");
                }
                phone = value;
            }
        }

        public string Username  { get; set; }

        private string password;
        public string Password 
        {
            get => password;
            set
            {
                if (value.Length < 8)
                {
                    throw new Exception("Password is too short!");
                }
                password = value;
            }
        }

        public List<string> Notification { get; set; }

        public abstract void FillInfo();//the person fills in the information

        public override string ToString()
        {
            return $@"
Name: {Name} 
Surname: {Surname} 
Age: {Age} 
Mail: {Mail} 
Phone: {Phone} ";
        }
    }
}

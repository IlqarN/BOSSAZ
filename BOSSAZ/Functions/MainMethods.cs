using Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    static partial class MainMethods
    {

        static readonly string[] skills = new string[] { "C", "C++", "C#", "SQL", "JAVA", "PHP", "HTML5","CSS", "JavaScript", "Pyton","Microsoft Office", "Adobe Photoshop", "Adobe Illustrator", "Affinity Designer.", "Other", "End" };
        static readonly string[] language = new string[] { "Azerbaijan", "English", "Russian", "Turkish", "French","Other", "End" };
        static readonly string[] profession = new string[] { "Cyber security", "Frontend Developer", "Backend Developer", "Computer Engineer", "Graphic Designer","Copy Writer","IT", "Other" };

        public static int SignUp(Users users)
        {
            int index = SelectingMenu.Choose(new string[] { "Worker", "Employer" ,"Back"});
            Console.Clear();
            if (index == 0) //Signup as worker
            {
                var worker = new Worker();
                var cv = new CV();
                worker.FillInfo();
                Console.Clear();
                cv.Profession = SelectingMenu.ChooseByString(profession);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("University: ");
                Console.ForegroundColor = ConsoleColor.White;
                cv.School = Console.ReadLine();
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Experience: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (int.TryParse(Console.ReadLine(), out int a))
                    {
                        cv.Experience = a;
                        break;
                    }
                }
                Console.Clear();
                List<string> skillList = new();
                while (true)
                {
                    string temp = SelectingMenu.ChooseByString(skills);
                    if (temp == "End")
                        break;
                    else 
                    {
                        skillList.Add(temp);
                    }
                }
                skillList = skillList.Distinct().ToList();
                Console.Clear();
                cv.Skill = skillList;
                List<string> lang = new();
                while (true)
                {
                    string temp = SelectingMenu.ChooseByString(language);
                    if (temp == "End")
                        break;
                    else
                    {
                        lang.Add(temp);
                    }
                }
                lang = lang.Distinct().ToList();
                cv.Language = lang;
                worker.Cvs = cv;
                users.SignUp(worker);
            }
            else if(index==1)//Employer
            {
                var employer = new Employer();
                employer.FillInfo();
                users.SignUp(employer);
            }
            return -1;
        }
        private static int[] Choose(List<Vacancy> items)
        {
            int size = items.Count + 1;
            bool[] isChoosenLine = new bool[size];
            int index = 0;
            string[] arr = items.Select(it => it.Title).ToArray();
            Array.Resize(ref arr, arr.Length + 1);
            arr[^1] = "Back";
            while (true)
            {
                Console.Clear();
                int j = 0;
                foreach (var item in arr)
                {
                    string temp = item;
                    if(temp.Length > 40)
                    {
                        temp = temp.Substring(0, 40);
                        temp += "...";
                    }
                    if (isChoosenLine[j])
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(item);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    j++;
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                ConsoleKeyInfo rKey = Console.ReadKey();
                if (rKey.Key == ConsoleKey.UpArrow)
                {
                    index--;
                    if (index == -1)
                        index = size - 1;
                }
                else if (rKey.Key == ConsoleKey.DownArrow)
                {
                    index++;
                    if (index == size)
                        index = 0;
                }
                else if (rKey.Key == ConsoleKey.Enter)
                {
                    if (index == size - 1)
                        return new int[] { -1, -1 };
                    return new int[] { items[index].ID, items[index].EmployerID };
                }
                for (int i = 0; i < size; i++)
                {
                    isChoosenLine[i] = false;
                }
                isChoosenLine[index] = true;

            }
        }
    }
}

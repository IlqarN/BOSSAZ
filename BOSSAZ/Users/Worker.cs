using Menu;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    class Worker :Person
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public CV Cvs { get; set; }

        public override void FillInfo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t\t\t\t\t /-~-~-~-~-~-~-~-~-~-~-~-\\");
            Console.WriteLine("\t\t\t\t\t\t<  Register as an worker  >");
            Console.WriteLine("\t\t\t\t\t\t \\-~-~-~-~-~-~-~-~-~-~-~-/\n");
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Name: ");
                Console.ForegroundColor = ConsoleColor.White;
                Name = Console.ReadLine();
                if (Name.Length > 0)
                    break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Surname: ");
                Console.ForegroundColor = ConsoleColor.White;
                Surname = Console.ReadLine();
                if (Surname.Length > 0)
                    break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Age: ");
                Console.ForegroundColor = ConsoleColor.White;
                int a;
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    try
                    {
                        Age = a;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t\t\t\t\t  #" + ex.Message);
                        logger.Warn(ex.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    break;
                }
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Mail: ");
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    Mail = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t\t  #" + ex.Message);
                    logger.Warn(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Phone: ");
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    Phone = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t\t  #" + ex.Message);
                    logger.Warn(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Username: ");
                Console.ForegroundColor = ConsoleColor.White;
                Username = Console.ReadLine();
                if (Username.Length > 0)
                    break;
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Password: ");
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    Password = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t\t\t\t  #" + ex.Message);
                    logger.Warn(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                break;
            }
        }

        public Worker()
        {
            Notification = new List<string>();
        }

        public override string ToString()
        {
            return $"{base.ToString()} \n\nCV:{Cvs}";
        }

    }

    static partial class MainMethods
    {
        public static int ShowPost(int workerID, List<Vacancy> vacancies, Users users)
        {
            int[] ids;
            while (true)
            {
                ids = Choose(vacancies);
                if (ids[0] == -1)
                    break;
                else
                {
                    var emp = users.GetEmployerByID(ids[1]);
                    string str = "Company Name: " + emp.CompanyName + emp.GetVacancyByID(ids[0]).ToString();
                    int index = SelectingMenu.Choose(str, new string[] { "Apply for job", "Back" });
                    if (index == 0)
                    {
                        users.ApplyForJob(workerID, ids[0], ids[1]);
                        break;
                    }
                }
            }
            return 21;
        }
        
        public static List<Vacancy> SearchText(List<Vacancy> vacancies, string searchText)
        {
            searchText = searchText.ToLower();
            return vacancies.FindAll(vac =>
            {
                if (vac.Title.ToLower().Contains(searchText) || vac.Salary.ToString().Contains(searchText) || vac.MinExperience.ToString().Contains(searchText) || vac.Body.ToLower().Contains(searchText) || vac.Profession.ToLower().Contains(searchText) || vac.Skill.Contains(searchText) || vac.Language.Contains(searchText))
                    return true;
                return false;
            });
        }

        public static List<Vacancy> Filter(List<Vacancy> vacancies, Users users)
        {
            List<Vacancy> tempVacancy = new();
            SortedList<int, List<int>> filter = new();
            while (true)
            {
                List<string> texts = new();
                int index = SelectingMenu.Choose(new string[] { "Profession", "Salary", "Skill", "Language", "Back" });
                Console.Clear();
                switch (index)
                {
                    case 0: //Profession
                        {
                            string[] profs = new string[profession.Length + 1];
                            int j = 0;
                            foreach (var item in profession)
                            {
                                profs[j++] = item;
                            }
                            profs[j++] = "End";
                            while (true)
                            {
                                string prof;
                                prof = SelectingMenu.ChooseByString(profs);
                                if (prof == "End")
                                    break;
                                texts.Add(prof);
                            }
                            texts = texts.Distinct().ToList();
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Profession == text)
                                        {
                                            tempVacancy.Add(vac);
                                        }

                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Profession == text)
                                            return true;
                                    }
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 1://Salary
                        {
                            int salary;
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("Min Salary: ");
                                Console.ForegroundColor = ConsoleColor.White;
                                if (int.TryParse(Console.ReadLine(), out int a))
                                {
                                    salary = a;
                                    break;
                                }
                            }
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    if (vac.Salary >= salary)
                                    {
                                        tempVacancy.Add(vac);
                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    if (vac.Salary >= salary)
                                        return true;
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 2: //Skill
                        {
                            while (true)
                            {
                                string skill;
                                skill = SelectingMenu.ChooseByString(skills);
                                if (skill == "End")
                                    break;
                                texts.Add(skill);
                            }
                            texts = texts.Distinct().ToList();
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Skill.Contains(text))
                                        {
                                            tempVacancy.Add(vac);
                                        }

                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Skill.Contains(text))
                                            return true;
                                    }
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 3: //Language
                        {
                            while (true)
                            {
                                string lang;
                                lang = SelectingMenu.ChooseByString(language);
                                if (lang == "End")
                                    break;
                                texts.Add(lang);
                            }
                            texts = texts.Distinct().ToList();
                            if (tempVacancy.Count == 0)
                            {
                                vacancies.ForEach(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Language.Contains(text))
                                        {
                                            tempVacancy.Add(vac);
                                        }

                                    }
                                });
                            }
                            else
                            {
                                tempVacancy = tempVacancy.Where(vac =>
                                {
                                    foreach (var text in texts)
                                    {
                                        if (vac.Language.Contains(text))
                                            return true;
                                    }
                                    return false;
                                }).ToList();
                            }
                        }
                        break;
                    case 4: //Back
                        {
                            return tempVacancy;
                        }
                }
            }

        }

        public static int OptionsForWorker(int id, Users users)
        {
            string[] list = new string[] { "Change Username", "Change Password", "Add Language ", "Add Skill", "Delete Account", "Back" };
            int index = SelectingMenu.Choose(list);
            Console.Clear();
            switch (index)
            {
                case 0:
                    {
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("New Username: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            users.GetWorkerByID(id).Username = Console.ReadLine();
                            if (users.GetWorkerByID(id).Username.Length > 0)
                            {
                                users.UpdateWorkerJson();
                                return -1;
                            }
                        }
                    }
                case 1:
                    {
                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("New Password: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            users.GetWorkerByID(id).Password = Console.ReadLine();
                            if (users.GetWorkerByID(id).Password.Length > 0)
                            {
                                users.UpdateWorkerJson();
                                return -1;
                            }
                        }
                    }
                case 2:
                    {
                        while (true)
                        {
                            string temp = SelectingMenu.ChooseByString(language);
                            if (temp == "End")
                                break;
                            else
                            {
                                users.GetWorkerByID(id).Cvs.Language.Add(temp);
                            }
                        }
                        users.GetWorkerByID(id).Cvs.Language = users.GetWorkerByID(id).Cvs.Language.Distinct().ToList();
                        users.UpdateWorkerJson();
                    }
                    break;
                case 3:
                    {
                        while (true)
                        {
                            string temp = SelectingMenu.ChooseByString(skills);
                            if (temp == "End")
                                break;
                            else
                            {
                                users.GetWorkerByID(id).Cvs.Skill.Add(temp);
                            }
                        }
                        users.GetWorkerByID(id).Cvs.Skill = users.GetWorkerByID(id).Cvs.Skill.Distinct().ToList();
                        users.UpdateWorkerJson();
                    }
                    break;
                case 4:
                    users.DeleteWorkerByID(id);
                    break;
                case 5: return 21;
            }

            return -1;
        }
    }
}

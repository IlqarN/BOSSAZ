using Menu;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOSSAZ
{
    class Employer: Person
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public string CompanyName { get; set; }

        public override void FillInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\t\t /-~-~-~-~-~-~-~-~-~-~-~-\\");
            Console.WriteLine("\t\t\t\t\t\t< Register as an employer >");
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
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\t\t\t\t\t\t   Company Name: ");
                Console.ForegroundColor = ConsoleColor.White;
                CompanyName = Console.ReadLine();
                if (CompanyName.Length > 0)
                    break;
            }
        }

        public List<Vacancy> Vacancies { get; set; }
        public int SetVacancyID()
        {
            if (Vacancies.Count == 0)
            {
                return 0;
            }
            else
            {
                return Vacancies.Last().ID + 1;
            }
        }

        public Vacancy GetVacancyByID(int id)
        {
            return Vacancies.Find(vac => vac.ID == id);
        }

        public void DeleteVacancyByID(int id)
        {
            Vacancies.Remove(GetVacancyByID(id));
        }

        public Employer()
        {
            Vacancies = new List<Vacancy>();
            Notification = new List<string>();
        }
    }

    static partial class MainMethods
    {

        public static int PostVacancy(int id, Users users)
        {
            var vacancy = new Vacancy();
            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Title (min 10): ");
                Console.ForegroundColor = ConsoleColor.White;
                vacancy.Title = Console.ReadLine();
                if (vacancy.Title.Length > 9)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Title length is short!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Body: ");
            Console.ForegroundColor = ConsoleColor.White;
            vacancy.Body = Console.ReadLine();
            int a;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Salary: ");
                Console.ForegroundColor = ConsoleColor.White;
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    vacancy.Salary = a;
                    break;
                }
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Min Experience: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    vacancy.MinExperience = a;
                    break;
                }
            }
            Console.Clear();
            vacancy.Profession = SelectingMenu.ChooseByString(profession);
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
            vacancy.Skill = skillList;
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
            vacancy.Language = lang;
            users.PostVacancyByID(id, vacancy);
            return 11;
        }

        public static int ShowYourPost(int id, Users users)
        {
            string[] posts = new string[users.GetEmployerByID(id).Vacancies.Count + 1];
            int i = 0;
            if (posts.Length > 1)
            {
                foreach (var vacancy in users.GetEmployerByID(id).Vacancies)
                {
                    posts[i++] = vacancy.Title + " " + vacancy.WorkerID.Count;
                }
                posts[i] = "Back";
                return SelectingMenu.Choose(posts);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Post Something!!!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press any key to continue!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            return -1;
        }

        public static int ShowWorkerWhoWantJob(int employerID, int vacancyID, Users users)
        {
            int index, index2;
            while (true)
            {
                string vacancy = users.GetEmployerByID(employerID).GetVacancyByID(vacancyID).ToString();
                int size = users.GetEmployerByID(employerID).GetVacancyByID(vacancyID).WorkerID.Count + 2;
                string[] worker = new string[size];
                int i = 0;
                if (worker.Length > 2)
                {
                    foreach (var emp in users.WorkerWantJob(employerID, vacancyID))
                    {
                        worker[i++] = emp.Name + " " + emp.Surname;
                    }
                }
                worker[i++] = "Delete Vacancy";
                worker[i] = "Back";
                index = SelectingMenu.Choose(vacancy, worker);
                if (index < size - 2)
                {
                    int[] ids = users.GetEmployerByID(employerID).GetVacancyByID(vacancyID).WorkerID.ToArray();
                    string cv = users.GetWorkerByID(ids[index]).Cvs.ToString();
                    index2 = SelectingMenu.Choose(cv, new string[] { "Hire", "Reject", "Back" });
                    if (index2 == 0) //Hire
                    {
                        users.HireForJob(ids[index], vacancyID, employerID);
                        break;
                    }
                    else if (index2 == 1) //Reject
                    {
                        users.RejectForJob(ids[index], vacancyID, employerID);
                        break;
                    }
                }
                if (index == size - 2) //Delete Vacancy
                {
                    users.DeleteVacancy(employerID, vacancyID);
                    break;
                }
                else if (index == size - 1) { break; }//Back
            }
            return 13;
        }

        public static int OptionsForEmployer(int id, Users users)
        {
            string[] list = new string[] { "Change Username", "Change Password", "Change Company Name", "Delete Account", "Back" };
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
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            users.GetEmployerByID(id).Username = Console.ReadLine();
                            if (users.GetEmployerByID(id).Username.Length > 0)
                            {
                                users.UpdateEmployerJson();
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
                            users.GetEmployerByID(id).Password = Console.ReadLine();
                            if (users.GetEmployerByID(id).Password.Length > 0)
                            {
                                users.UpdateEmployerJson();
                                return -1;
                            }
                        }
                    }
                case 2:
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("New Company Name: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        while (true)
                        {
                            users.GetEmployerByID(id).CompanyName = Console.ReadLine();
                            if (users.GetEmployerByID(id).CompanyName.Length > 0)
                            {
                                users.UpdateEmployerJson();
                                return -1;
                            }
                        }
                    }
                case 3:
                    users.DeleteEmployerByID(id);
                    break;
                case 4: return 11;
            }

            return -1;
        }

    }
}

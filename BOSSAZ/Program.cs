using System;
using System.Collections.Generic;
using System.Threading;
using Menu;
using NLog;

namespace BOSSAZ
{
    class Program
    {
        
        static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            int index = -1;
            int id = -1;
            int vacancyID = -1;
            string text = "";
            Users users = new();
            List<Vacancy> filterVac = new();

            while (true)
            {
                Console.Clear();
                switch (index)
                {
                    case -1: //SignMenu
                        {
                            index = SelectingMenu.Choose(new string[] { "Sing Up", "Sing In", "Exit" });
                        } break;
                    case 0: //Sing Up
                        {
                            try
                            {
                                index = MainMethods.SignUp(users);
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                logger.Warn(ex.Message);
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(700);
                                goto case 3;
                            }
                        }break;
                    case 1: //Sign In
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Username: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            string username = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Password: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            string password = Console.ReadLine();
                            try
                            {
                                var person = users.SignIn(username, password);
                                id = person.ID;
                                if (person is Employer) 
                                    index = 11;
                                else
                                    index = 21;
                            }
                            catch (Exception ex) //if username or password is wrong
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ex.Message);
                                logger.Warn(ex.Message);
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(900);
                                goto case 3;
                            }
                        }
                        break;
                    case 11: //Sign in Employer
                        {
                            string[] signAsEmployer = new string[] { "Post Vacancy", "Your Posts", "Notification","Options", "Sign out" };
                            signAsEmployer[2] += " " + users.GetEmployerByID(id).Notification.Count;
                            index = index = SelectingMenu.Choose(signAsEmployer) + 12;
                        }
                        break;
                    case 12: //Post Vacancy
                        {
                            index = MainMethods.PostVacancy(id, users);
                        }break;
                    case 13://Your Post
                        {
                            vacancyID = MainMethods.ShowYourPost(id, users);
                            if (vacancyID == -1)
                            {
                                index = 11;
                                continue;
                            }
                            else if (vacancyID < users.GetEmployerByID(id).Vacancies.Count)
                                index = 131;
                            else
                                index = 11;
                        }
                        break; 
                    case 131://Worker Who want job
                        {
                            index = MainMethods.ShowWorkerWhoWantJob(id, vacancyID, users);
                        }break;
                    case 14: //Notification for employer
                        {
                            users.GetEmployerByID(id).Notification.ForEach(not => Console.WriteLine(not));
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Press Any Key to Continue...");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ReadLine();
                            users.GetEmployerByID(id).Notification.Clear();
                            users.UpdateEmployerJson();
                            index = 11;
                        }
                        break;
                    case 15: //Option for employer
                        {
                            index = MainMethods.OptionsForEmployer(id, users);
                        }break;
                    case 16: goto case 3;//Sign Out
                    case 21: //Sign in Worker
                        {
                            string[] signAsWorker = new string[] { "Search", "Posts", "Notification ", "Option", "Sign out" };
                            signAsWorker[2] += users.GetWorkerByID(id).Notification.Count;
                            index = SelectingMenu.Choose(signAsWorker) + 22;                        }
                        break;
                    case 22: //Search
                        {
                            index = SelectingMenu.Choose(new string[] { "Text", "Filter", "Search", "Back" }) + 221;
                        }
                        break;
                    case 221: //Text
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Text: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            text = Console.ReadLine();
                            index = 22;
                        }break;
                    case 222: //Filter
                        {
                            filterVac = MainMethods.Filter(users.GetAllVacancies(), users);
                            index = 22;
                        }
                        break;
                    case 223: //Search Button
                        {
                            if (filterVac.Count == 0 && text.Length > 0)// if filter not used
                                filterVac = MainMethods.SearchText(users.GetAllVacancies(), text);
                            if (filterVac.Count > 0 && text.Length > 0)
                                filterVac = MainMethods.SearchText(filterVac, text);
                            if (filterVac.Count > 0) 
                                MainMethods.ShowPost(id, filterVac, users);
                            else 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Not Found !!!");
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(1000);
                            }
                            index = 22; 
                            filterVac.Clear();
                            text = "";
                        }
                        break;
                    case 224: filterVac.Clear(); index = 21; break; //Back
                    case 23: //Posts
                        {
                            index = MainMethods.ShowPost(id, users.GetAllVacancies(),users);
                        }
                        break;
                    case 24: //Notification For Worker
                        {
                            users.GetWorkerByID(id).Notification.ForEach(not => Console.WriteLine(not));
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Press Any Key to Continue...");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ReadLine();
                            users.GetWorkerByID(id).Notification.Clear();
                            users.UpdateWorkerJson();
                            index = 21;
                        }
                        break;
                    case 25://Option for Worker
                        {
                            index = MainMethods.OptionsForWorker(id, users);
                        }
                        break;
                    case 26: goto case 3; //Sign Out
                    case 2: return; //Exit
                    case 3: index = -1; break; //return home page
                }
            }
        }
    }
}

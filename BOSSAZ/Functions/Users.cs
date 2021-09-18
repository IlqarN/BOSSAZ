using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOSSAZ
{
    class Users
    {
        readonly List<Worker> Worker;
        readonly List<Employer> Employer;

        public void SignUp(Worker worker)//worker
        {
            if (IsUserExist(worker))
                throw new Exception("This username already exist!");
            if (this.Worker.Count == 0)
                worker.ID = 0;
            else
                worker.ID = this.Worker.Last().ID + 1;
            this.Worker.Add(worker);
            UpdateWorkerJson();
        }

        public void SignUp(Employer employer)//empkoyer
        {
            if (IsUserExist(employer))
                throw new Exception("This username already exist!");
            if (this.Employer.Count == 0)
                employer.ID = 0;
            else
                employer.ID = this.Employer.Last().ID + 1;
            this.Employer.Add(employer);
            UpdateEmployerJson();
        }

        public Person SignIn(string username, string password)
        {
            foreach (var item in Worker)
            {
                if (item.Username == username && item.Password == password)
                    return item;
            }
            foreach (var item in Employer)
            {
                if (item.Username == username && item.Password == password)
                    return item;
            }
            throw new Exception("Username or password is wrong!");
        }

        private bool IsUserExist (Person person)
        {
            foreach (var worker in this.Worker)
            {
                if (person.Username == worker.Username)
                    return true;
            }
            foreach (var employer in this.Employer)
            {
                if (person.Username == employer.Username)
                    return true;
            }
            return false;
        }

        public void PostVacancyByID(int id, Vacancy vacancy)
        {
            vacancy.EmployerID = id;
            vacancy.ID = GetEmployerByID(id).SetVacancyID();
            GetEmployerByID(id).Vacancies.Add(vacancy);
            UpdateEmployerJson();
        }

        public List<Vacancy> GetAllVacancies()
        {
            List<Vacancy> vacancies = new();
            foreach (var employer in this.Employer)
            {
                foreach (var vacancy in employer.Vacancies)
                {
                    vacancies.Add(vacancy);
                }
            }
            return vacancies;
        }

        public Employer GetEmployerByID(int id)
        {
            return Employer.Find(emp => emp.ID == id);
        }

        public Worker GetWorkerByID(int id)
        {
            return Worker.Find(emp => emp.ID == id);
        }

        public List<Worker> WorkerWantJob(int employerID, int vacancyID)
        {
            List<int> ids = GetEmployerByID(employerID).GetVacancyByID(vacancyID).WorkerID;
            return Worker.Where(emp =>
            {
                foreach (var id in ids)
                {
                    if (emp.ID == id)
                        return true;
                }
                return false;
            }).ToList();
        }

        public void ApplyForJob(int workerID, int vacancyID, int employerID)
        {
            if (!GetEmployerByID(employerID).GetVacancyByID(vacancyID).WorkerID.Exists(i => i == workerID))
            {
                var emp = GetWorkerByID(workerID);
                GetEmployerByID(employerID).GetVacancyByID(vacancyID).WorkerID.Add(workerID);
                GetEmployerByID(employerID).Notification.Add(emp.Name + " " + emp.Surname + " Applied for job you posted");
                UpdateEmployerJson();
            }
        }

        public void HireForJob(int workerID,int vacancyID,int employerID)
        {
            var emp = GetEmployerByID(employerID);
            GetWorkerByID(workerID).Notification.Add(emp.CompanyName + " hired you for " + "\"" + emp.GetVacancyByID(vacancyID).Title + "\"");
            DeleteVacancy(employerID,vacancyID);
            UpdateEmployerJson();
            UpdateWorkerJson();
        }

        public void RejectForJob(int workerID, int vacancyID, int employerID)
        {
            var emp = GetEmployerByID(employerID);
            GetWorkerByID(workerID).Notification.Add(emp.CompanyName + " reject your application for " + "\"" + emp.GetVacancyByID(vacancyID).Title + "\"");
            GetEmployerByID(employerID).GetVacancyByID(vacancyID).WorkerID.Remove(workerID);
            UpdateEmployerJson();
            UpdateWorkerJson();
        }

        public void UpdateEmployerJson()
        {
            var jsonFile = JsonConvert.SerializeObject(this.Employer, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("employer.json", jsonFile);
        }

        public void UpdateWorkerJson()
        {
            var jsonFile = JsonConvert.SerializeObject(this.Worker, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("worker.json", jsonFile);
        }

        public void DeleteVacancy(int empoyerID,int vacancyID)
        {
            GetEmployerByID(empoyerID).DeleteVacancyByID(vacancyID);
            GetEmployerByID(empoyerID).Vacancies.ForEach(vac =>
            {
                if (vac.ID > vacancyID)
                    vac.ID--;
            });
            UpdateEmployerJson();
        }

        public void DeleteEmployerByID(int id)
        {
            Employer.Remove(GetEmployerByID(id));
            Employer.ForEach(emp =>
            {
                if (emp.ID > id)
                {
                    emp.Vacancies.ForEach(vac => vac.EmployerID--);
                    emp.ID--;
                }
            });
            UpdateEmployerJson();
        }

        public void DeleteWorkerByID(int id)
        {
            Worker.Remove(GetWorkerByID(id));
            Worker.ForEach(emp =>
            {
                if (emp.ID > id)
                    emp.ID--;
            });
            Employer.ForEach(emp =>
            {
                emp.Vacancies.ForEach(vac =>
                {
                vac.WorkerID.Remove(id);
                vac.WorkerID.ForEach(eid => { if (eid > id) eid--; });
                });
            });
            UpdateWorkerJson();
            UpdateEmployerJson();
        }

        public Users()
        {
            if (!File.Exists("worker.json"))
            {
                File.WriteAllText("worker.json", "");
            }
            if (!File.Exists("employer.json"))
            {
                File.WriteAllText("employer.json", "");
            }

            Worker = new List<Worker>();
            Employer = new List<Employer>();

            var jsonStrW = File.ReadAllText("worker.json");
            var jsonStrJ = File.ReadAllText("employer.json");

            if(jsonStrW.Length > 0)
            Worker = JsonConvert.DeserializeObject<List<Worker>>(jsonStrW);

            if(jsonStrJ.Length > 0)
            Employer = JsonConvert.DeserializeObject<List<Employer>>(jsonStrJ);

        }
    }
}
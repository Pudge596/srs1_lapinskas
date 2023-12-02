using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace consoleapp.Models
{
    public class Helper
    {
        public static Models.Entities s_entities;
        public static Models.Entities GetContext()
        {
            if (s_entities == null)
            {
                s_entities = new Entities();
            }
            return s_entities;
        }
        public void CreateUsers(Models.Users users)
        {
            GetContext().Users.Add(users);
            GetContext().SaveChanges();
        }
        public void UpdateUsers(Models.Users users, Models.Employees employees)
        {
            s_entities.Entry(users).State = System.Data.Entity.EntityState.Modified;
            s_entities.Entry(employees).State = System.Data.Entity.EntityState.Modified;
            s_entities.SaveChanges();
        }
        public void RemoveUsers(int EmployeeID, int UsersID)
        {
            var employeers = s_entities.Employees.Find(EmployeeID);
            var users = s_entities.Users.Find(UsersID);
            s_entities.Users.Remove(users);
            s_entities.Employees.Remove(employeers);
            s_entities.SaveChanges();
        }
        public void FiltrUsers()
        {
            var users = s_entities.Employees.Where(x => x.FirstName.StartsWith("М") || x.FirstName.StartsWith("А"));
        }
        public void SortUsers()
        {
            var employeers = s_entities.Employees.OrderBy(x => x.FirstName);
        }
    }
   
}

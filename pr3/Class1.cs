using pr3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr3
{
    internal class Class1
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
    }
}

using consoleapp.Models;
using HashPasswords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Entities CompEntities = new Entities();
            int roleID;
            while (true)
            {
                Console.WriteLine("Ввежите id должности\n1-Строитель 2-Прораб 3-Директор 4-Менеджер: ");
                var RoleID = Console.ReadLine();
                if (int.TryParse(RoleID, out roleID))
                {
                    if (roleID >= 1 && roleID <= 4) break;
                }
            }
            Console.WriteLine("Ввежите имя пользователя: ");
            string FirstName = Console.ReadLine();
            while (FirstName == string.Empty)
            {
                Console.WriteLine("Пустое значение, введите снова");
                FirstName = Console.ReadLine();
            }
            Console.WriteLine("Ввежите фамилию пользователя: ");
            string LastName = Console.ReadLine();
            while (LastName == string.Empty)
            {
                Console.WriteLine("Пустое значение, введите снова");
                LastName = Console.ReadLine();
            }
            Console.WriteLine("Ввежите Отчество пользователя: ");
            string SurName = Console.ReadLine();
            while (SurName == string.Empty)
            {
                Console.WriteLine("Пустое значение, введите снова");
                SurName = Console.ReadLine();
            }
            Console.WriteLine("Ввежите логин пользователя: ");
            string login = Console.ReadLine();
            while (login == string.Empty)
            {
                Console.WriteLine("Пустое значение, введите снова");
                login = Console.ReadLine();
            }
            Console.WriteLine("Ввежите пароль пользователя: ");
            string password = Console.ReadLine();
            while (password == string.Empty)
            {
                Console.WriteLine("Пустое значение, введите снова");
                password = Console.ReadLine();
            }

            hash hasher = new hash();
            string hashpassword = hasher.Hashing(password);
            Console.WriteLine(hashpassword);
            var emp = new Models.Employees
            {
                RoleID = roleID,
                FirstName = FirstName,
                LastName = LastName,
                SurName = SurName,

            };
            CompEntities.Employees.Add(emp);
            CompEntities.SaveChanges();

            var user = new Models.Users
            {
                UsersID = emp.EmployeeID,
                Login = login,
                Password = hashpassword
            };

            CompEntities.Users.Add(user);
            CompEntities.SaveChanges();

            Console.WriteLine("Учетная запись добавлена");
            Console.WriteLine($"Логин сотрудника{login} Пароль сотрудника{hashpassword}");
            Console.ReadKey();
        }
    }
}

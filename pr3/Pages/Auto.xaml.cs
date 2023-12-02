using pr3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr3.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Page
    {
        public Auto()
        {
            InitializeComponent();
        }
        
        private static string GetHashPass(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] sourceByteCode = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(sourceByteCode);
                string hashPassw = BitConverter.ToString(hash).Replace("-", String.Empty);
                return hashPassw;
            }
        }

        private int countUnsuccessful = 0;
        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null));
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtbLogin.Text.Trim();
            string password = txtPassword.Password.Trim();
            if (login.Length > 0 && password.Length > 0)
            {
                
                if (countUnsuccessful < 1)
                {
                    string hashPassw = GetHashPass(password);
                    var user = Class1.GetContext().Users.Where(u => u.Login == login && u.Password == hashPassw).FirstOrDefault();
                   
                    if (user != null)
                    {
                        var id = user.Employees.RoleID;
                        if(id == 1)
                        {
                            NavigationService.Navigate(new Stroitel());
                        }
                        if (id == 2)
                        {
                            NavigationService.Navigate(new Prorab());
                        }
                        if (id == 3)
                        {
                            NavigationService.Navigate(new Director());
                        }
                        if (id == 4)
                        {
                            NavigationService.Navigate(new Manager());
                        }

                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином или паролем не существует!", "Ошибка!", MessageBoxButton.OK);
                        countUnsuccessful++;
                        GenerateCaptcha();
                    }
                }
                else
                {
                    string hashPassw = GetHashPass(password);
                    var user = Class1.GetContext().Users.Where(u => u.Login == login && u.Password == hashPassw).FirstOrDefault();

                    string captcha = txtBoxCaptcha.Text.Trim();

                    if (user != null && captcha == txtBoxCaptcha.Text)
                    {
                        txtBlockCaptcha.Visibility = Visibility.Hidden;
                        txtboxCaptcha.Visibility = Visibility.Hidden;
                        txtBoxCaptcha.Text = "";
                        txtBlockCaptcha.Text = "";
                        countUnsuccessful = 0;
                    }
                    else
                     {
                       MessageBox.Show("Неверно введена капча, попробуйте заного!", "Ошибка!", MessageBoxButton.OK);
                       countUnsuccessful++;
                       GenerateCaptcha();
                     }
                    
                }
                
            }
            else
            {
                MessageBox.Show("Не все обязательные поля заполнены! Заполните логиин и пароль, и повторите попытку авторизации!", "Внимание!", MessageBoxButton.OK);
            }
        }
        private void GenerateCaptcha()
        {
            txtboxCaptcha.Visibility = Visibility.Visible;
            txtBlockCaptcha.Visibility = Visibility.Visible;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string captcha = new string(Enumerable.Repeat(chars, 6)
            .Select(i => i[random.Next(i.Length)]).ToArray());
            txtBlockCaptcha.Text = captcha;
            txtBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }
    }
}

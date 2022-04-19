using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PM04
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = ContextDB.GetContext().User.ToList();
            var currentuser = user.Where(p => p.Login == LoginBox.Text && p.Password == PasswordBox.Password);
            UsersClass.GetUsers = currentuser.FirstOrDefault();

            if (currentuser.Count() != 0 && UsersClass.GetUsers.ID == 1)
            {
                MessageBox.Show("Здравствуйте, кассир!");
                Manager.MainFrame.Navigate(new DataGridPage());

                // Ключ реестра
                RegistryKey userkey = Registry.CurrentUser;
                RegistryKey eduardokey = userkey.CreateSubKey("EduardoKey");
                eduardokey.SetValue("Login", "admin");
                eduardokey.SetValue("Password", "12345");
                eduardokey.Close();
            }
            else if (currentuser.Count() != 0 && UsersClass.GetUsers.ID == 2)
            {
                MessageBox.Show("Здравствуйте, покупатель!");
                Manager.MainFrame.Navigate(new DataGridPage());

                // Ключ реестра
                RegistryKey userkey = Registry.CurrentUser;
                RegistryKey eduardokey = userkey.CreateSubKey("EduardoKey");
                eduardokey.SetValue("Login", "user");
                eduardokey.SetValue("Password", "12345");
                eduardokey.Close();
            }
            else
                MessageBox.Show("Неверные данные!");
        }
    }
}

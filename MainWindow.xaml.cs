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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UP1.TablePages;

namespace UP1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UP1Entities UP = UP1Entities.GetContext();
        User User;
        EquipmentTypesPage typespage;
        RepairRequestsPage repairrequestspage;
        RepairWorksPage repairworkspage;
        RepairReportsPage repairreportspage;
        UsersPage userspage;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bSignIn_Click(object sender, RoutedEventArgs e)
        {
            Auth(tbLogin.Text, pbPassword.Password);
        }

        public bool Auth(string username, string password)
        {
            string[] logins = UP.Users.Select(u => u.Login).ToArray();
            string[] passwords = UP.Users.Select(u => u.Password).ToArray();
            for (int i = 0; i < logins.Length; i++)
            {
                if (logins[i] == username && passwords[i] == password)
                {
                    User = UP.Users.AsNoTracking().FirstOrDefault(employee => employee.Login == username && employee.Password == password);
                }
            }
            if (User != null)
            {
                SideButtons.Visibility = Visibility.Visible;
                tbLogin.Visibility = Visibility.Hidden;
                a.Visibility = Visibility.Hidden;
                b.Visibility = Visibility.Hidden;
                pbPassword.Visibility = Visibility.Hidden;
                bSignIn.Visibility = Visibility.Hidden;

                typespage = new EquipmentTypesPage();
                userspage = new UsersPage();
                repairrequestspage = new RepairRequestsPage(User);
                repairworkspage = new RepairWorksPage(User);
                repairreportspage = new RepairReportsPage(User);
                return true;

            }
            else if (tbLogin.Text == string.Empty || pbPassword.Password == string.Empty)
            {
                MessageBox.Show("Введите логин и пароль!");
                return false;
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
                return false;
            }
        }

        private void pbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Auth(tbLogin.Text, pbPassword.Password);
            }
        }

        private void ToEquipmentTypes_Click(object sender, RoutedEventArgs e)
        {
            pages.Navigate(typespage);
        }

        private void ToUsers_Click(object sender, RoutedEventArgs e)
        {
            pages.Navigate(userspage);
        }

        private void ToRepairRequests_Click(object sender, RoutedEventArgs e)
        {
            pages.Navigate(repairrequestspage);
        }

        private void ToRepairWorks_Click(object sender, RoutedEventArgs e)
        {
            pages.Navigate(repairworkspage);
        }

        private void ToWorkReports_Click(object sender, RoutedEventArgs e)
        {
            pages.Navigate(repairreportspage);
        }

        private void ToSpareParts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToSparePartsOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

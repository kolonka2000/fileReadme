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
    /// Логика взаимодействия для DataGridPage.xaml
    /// </summary>
    public partial class DataGridPage : Page
    {
        public DataGridPage()
        {
            InitializeComponent();

            var allcounty = ContextDB.GetContext().Country.ToList();
            allcounty.Insert(0, new Country {
                Title = "Все страны"
            });

            if (UsersClass.GetUsers.ID == 2)
            {
                AddBtn.IsEnabled = false;
                CheckBtn.IsEnabled = false;
                SortBox.Visibility = Visibility.Hidden;
                SortCombo.Visibility = Visibility.Hidden;
            }

            FilterCombo.ItemsSource = allcounty;
            SortCombo.SelectedIndex = 0;
            FilterCombo.SelectedIndex = 0;

            Update();
        }

        public void Update()
        {
            // Контекст таблицы
            var currentFlight = ContextDB.GetContext().Flight.ToList();

            // Поиск
            currentFlight = currentFlight.Where(p => p.City.ToLower().Contains(SearchBox.Text.ToLower())).ToList();

            // Фильтр
            if (FilterCombo.SelectedIndex > 0)
                currentFlight = currentFlight.Where(p => p.Country.Title.Contains((FilterCombo.SelectedItem as Country).Title)).ToList();

            // Сортировка
            switch (SortCombo.SelectedIndex)
            {
                case 0:
                    DGrid_Flights.ItemsSource = currentFlight;
                    break;
                case 1:
                    currentFlight = currentFlight.OrderBy(p => p.City).ToList();
                    break;
                case 2:
                    currentFlight = currentFlight.OrderByDescending(p => p.City).ToList();
                    break;
            }

            DGrid_Flights.ItemsSource = currentFlight;
        }

        private void CheckBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.CheckPage());
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.AddEditPage(null));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Pages.AddEditPage((sender as Button).DataContext as Flight));
        }

        // Обновление
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ContextDB.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                DGrid_Flights.ItemsSource = ContextDB.GetContext().Flight.ToList();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void SortCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}

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

namespace PM04.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Flight _currentflight = new Flight();
        public AddEditPage(Flight selectedFlight)
        {
            InitializeComponent();

            if (selectedFlight != null)
            {
                _currentflight = selectedFlight;
            }
            DataContext = _currentflight;

            ComboCountry.ItemsSource = ContextDB.GetContext().Country.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_currentflight.ID == 0)
            {
                ContextDB.GetContext().Flight.Add(_currentflight);
            }
            try
            {
                ContextDB.GetContext().SaveChanges();
                MessageBox.Show("Информация успешно сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}

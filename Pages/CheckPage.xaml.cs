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
using Word = Microsoft.Office.Interop.Word;

namespace PM04.Pages
{
    /// <summary>
    /// Логика взаимодействия для CheckPage.xaml
    /// </summary>
    public partial class CheckPage : Page
    {
        public CheckPage()
        {
            InitializeComponent();
        }

        private void repword(string stupRp, string text, Word.Document wrorddocc)
        {
            var range = wrorddocc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupRp, ReplaceWith: text);
        }

        private void BtnCheckStart_Click(object sender, RoutedEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            try
            {
                var wordDoc = wordApp.Documents.Open(AppDomain.CurrentDomain.BaseDirectory + "ticket.docx");
                repword("{lname}", lname.Text, wordDoc);
                repword("{fname}", fname.Text, wordDoc);
                repword("{mname}", mname.Text, wordDoc);
                repword("{pass}", pass.Text, wordDoc);
                repword("{phone}", phone.Text, wordDoc);
                repword("{city}", city.Text, wordDoc);
                repword("{ryad}", ryad.Text, wordDoc);
                repword("{place}", place.Text, wordDoc);
                wordDoc.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "ticketFinal.docx");
                wordApp.Visible = true;
            }
            catch
            {
                MessageBox.Show("Не удалось сформировать чек!");
            }
        }
    }
}

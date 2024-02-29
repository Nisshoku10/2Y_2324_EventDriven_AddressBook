using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace _2Y_2324_EventDriven_AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entries _ent = new Entries();
        List<string[]> _entries = new List<string[]>();
        private string searchEntry = "";
        public MainWindow()
        {
            InitializeComponent();

            btnAddEnt.IsEnabled = false;
            btnUpdEnt.IsEnabled = false;
            btnDelEnt.IsEnabled = false;
            btnClrEnt.IsEnabled = true;
            btnClrSrc.IsEnabled = true;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnUpdEnt.IsEnabled = true;
            btnDelEnt.IsEnabled = true;

            for (int i = 0; i < lbEntryList.Items.Count; i++)
            {
                if (lbEntryList.SelectedIndex == i)
                {
                    for (int j = 0; j < _entries.Count; j++)
                    {
                        tbName.Text = _entries[i][0];
                        tbAddr.Text = _entries[i][1];
                        tbContNum.Text = _entries[i][2];
                        tbEmAddr.Text = _entries[i][3];
                    }
                }
            }
        }
        private void tbSrcEnt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchEntry = tbSrcEnt.Text;
        }
        private void btnClrSrc_Click(object sender, RoutedEventArgs e)
        {
            tbSrcEnt.Text = string.Empty;
            CheckFields();
        }

        #region SearchBoxEvents
        private void tbSrcEnt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchEntry.Length == 0)
            {
                tbSrcEnt.Text = string.Empty;
            }
        }
        private void btnClrEnt_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = string.Empty;
            tbAddr.Text = string.Empty;
            tbContNum.Text = string.Empty;
            tbEmAddr.Text = string.Empty;
            lbEntryList.SelectedIndex = -1;
        }
        #endregion
        #region FillUpEvents
        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getName = tbName.Text;
        }
        private void tbAddr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getAddress = tbAddr.Text;
            CheckFields();
        }

        private void tbContNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getPhoneNum = tbContNum.Text;
            CheckFields();
        }

        private void tbEmAddr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getEmailAddress = tbEmAddr.Text;
            CheckFields();
        }
        #endregion
        private void btnAddEnt_Click(object sender, RoutedEventArgs e)
        {
            string[] person = [_ent.getName, _ent.getAddress, _ent.getPhoneNum, _ent.getEmailAddress];
            _entries.Add(person);
            lbEntryList.Items.Add(person[0]);

            tbName.Text = string.Empty;
            tbAddr.Text = string.Empty;
            tbContNum.Text = string.Empty;
            tbEmAddr.Text = string.Empty;

            MessageBox.Show("Entry was already added");

        }
        private void CheckFields()
        {
            if (tbName.Text.Length > 0 && tbAddr.Text.Length > 0 && tbContNum.Text.Length > 0 && tbEmAddr.Text.Length > 0)
            {
                btnAddEnt.IsEnabled = true;
            }
        }

        private void btnUpdEnt_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lbEntryList.Items.Count; i++)
            {
                if (lbEntryList.SelectedIndex == i)
                {
                    for (int j = 0; j < _entries.Count; j++)
                    {
                        _entries[i][0] = tbName.Text;
                        _entries[i][1] = tbAddr.Text;
                        _entries[i][2] = tbContNum.Text;
                        _entries[i][3] = tbEmAddr.Text;
                    }
                }
            }

            MessageBox.Show("Entry was already updated");
        }

        private void btnDelEnt_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lbEntryList.Items.Count; i++)
            {
                if (lbEntryList.SelectedIndex == i)
                {
                    lbEntryList.Items.RemoveAt(i);
                }
            }

            tbName.Text = string.Empty;
            tbAddr.Text = string.Empty;
            tbContNum.Text = string.Empty;
            tbEmAddr.Text = string.Empty;
        }
    }
}
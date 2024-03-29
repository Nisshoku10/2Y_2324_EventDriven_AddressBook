﻿using System.IO;
using System.Windows;
using System.Windows.Controls;

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
        string path = @"D:\\Codes\\C#\\WPF\\2Y_2324_EventDriven_AddressBook\\2Y_2324_EventDriven_AddressBook\\Entries.csv";
        private int _ContNum = 0;
        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(path))
                ReadFile();
            else
                NoFileExists();

            btnAddEnt.IsEnabled = false;
            btnUpdEnt.IsEnabled = false;
            btnDelEnt.IsEnabled = false;
            btnClrEnt.IsEnabled = true;
            btnClrSrc.IsEnabled = true;
        }
        #region List Box Events
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddEnt.IsEnabled = false;
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

            tbSrcEnt.Text = "";
        }
        private void tbSrcEnt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchEntry = tbSrcEnt.Text;
            lbEntryList.Items.Clear();

            if (searchEntry.Length > 0)
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    if (_entries[i][0].ToLower().Contains(searchEntry.ToLower()))
                    {
                        lbEntryList.Items.Add(_entries[i][0]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    lbEntryList.Items.Add(_entries[i][0]);
                }
            }
        }
        private void btnClrSrc_Click(object sender, RoutedEventArgs e)
        {
            tbSrcEnt.Text = string.Empty;
            CheckFields();
        }

        #endregion
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
            CheckFields();
        }
        private void tbAddr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getAddress = tbAddr.Text;
            CheckFields();
        }

        private void tbContNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getPhoneNum = tbContNum.Text;
            if (!isNum(tbContNum.Text))
            {
                MessageBox.Show("Please enter a valid phone number.");
                btnAddEnt.IsEnabled = false;
                btnUpdEnt.IsEnabled = false;
                return;
            }

            CheckFields();
        }

        private void tbEmAddr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ent.getEmailAddress = tbEmAddr.Text;
            CheckFields();
        }
        private void CheckFields()
        {
            bool checktbLength = tbName.Text.Length > 0 && tbAddr.Text.Length > 0 && tbContNum.Text.Length > 0 && tbEmAddr.Text.Length > 0;
            btnAddEnt.IsEnabled = checktbLength;
        }
        private bool isNum(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            int num = 0;
            if (!int.TryParse(input, out num))
            {
                return false;
            }

            return true;
        }
        #endregion
        #region Button Events
        private void btnAddEnt_Click(object sender, RoutedEventArgs e)
        {
            if (!isNum(tbContNum.Text))
            {
                MessageBox.Show("Please enter a valid phone number.");
                return;
            }

            string[] person = [_ent.getName, _ent.getAddress, tbContNum.Text, _ent.getEmailAddress];
            _entries.Add(person);
            lbEntryList.Items.Add(person[0]);

            tbName.Text = string.Empty;
            tbAddr.Text = string.Empty;
            tbContNum.Text = string.Empty;
            tbEmAddr.Text = string.Empty;

            MessageBox.Show("Entry was already added");

        }
        private void btnUpdEnt_Click(object sender, RoutedEventArgs e)
        {
            if (!isNum(tbContNum.Text))
            {
                MessageBox.Show("Please enter a valid phone number.");
                return;
            }

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
                    for (int j = 0; j < _entries.Count; j++)
                    {
                        _entries.RemoveAt(j);
                    }
                }
            }

            tbName.Text = string.Empty;
            tbAddr.Text = string.Empty;
            tbContNum.Text = string.Empty;
            tbEmAddr.Text = string.Empty;
        }
        #endregion
        #region FileReadWriteEvents
        private void ReadFile()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    _entries.Add(values);
                    lbEntryList.Items.Add(values[0]);
                }
            }
        }
        private void NoFileExists()
        {
            using (StreamWriter ss = new StreamWriter(path))
            {
                ss.WriteLine();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    sw.WriteLine(_entries[i][0] + "," + _entries[i][1] + "," + _entries[i][2] + "," + _entries[i][3]);
                }
            }
        }
        #endregion
    }
}
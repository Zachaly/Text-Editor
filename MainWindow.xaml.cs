using System;
using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace DownTatnik
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currFile;

        private bool FileSaved;
        private string CurrentFile
        {
            get { return _currFile; }
            set
            {
                Title = "DownTatnik: " + value;
                _currFile = value;
            }
        }

        public MainWindow()
        {
            FileSaved = true;
            CurrentFile = "";
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            CheckSaved();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Pick new file";
            dialog.ShowDialog();

            if (dialog.FileName == "") return;

            CurrentFile = dialog.FileName;
            TxtBox.Text = File.ReadAllText(CurrentFile);
            FileSaved = true;
        }

        private void Save()
        {
            if(CurrentFile == "")
            {
                MessageBox.Show("No file picked!");
                return;
            }
            File.WriteAllText(CurrentFile, TxtBox.Text);
        }

        private void TxtBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FileSaved = false;
        }

        public void CheckSaved()
        {
            if (!FileSaved)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save?", "File unsaved", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) Save();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
            FileSaved = true;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            CheckSaved();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Enter name of the new file";
            dialog.ShowDialog();

            CurrentFile = dialog.FileName;

        }
    }
}

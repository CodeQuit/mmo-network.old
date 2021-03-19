using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MMo_Network.src.services;

namespace MMo_Network
{
    /// <summary>
    /// Логика взаимодействия для SetPath.xaml
    /// </summary>
    public partial class SetPathWindow : Window
    {
        int _gameindex;
        string gamepath = "game_patch_";
        string path;
        Register register = new Register();
        int gameindexNew;
        System.Windows.Forms.FolderBrowserDialog FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
        public SetPathWindow(int gameindex)
        {
            InitializeComponent();
            _gameindex = gameindex;
            gameindexNew = _gameindex + 1;
        }

        private void SetPathButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dr1 = FolderDialog.ShowDialog();
            path = FolderDialog.SelectedPath;
            pathBox.Text = path;
        }

        private void savePath_Click(object sender, RoutedEventArgs e)
        {
            if (license.IsChecked == true)
            {
                register.createReg(gamepath + gameindexNew, path);
                Close();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

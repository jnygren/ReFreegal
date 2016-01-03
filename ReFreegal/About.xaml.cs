using System;
using System.Windows;
using System.Reflection;

namespace ReFreegal
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        private string aboutText = 
            "Music files downloaded from Freegal have very cryptic filenames. ReFreegal" +
            " reads the ID3 tag in the .mp3 files, and generates a more informative" +
            " file name, and renames the music file.\r\n\r\n" +
            "\tToDo: \r\n" +
            "DONE - Add 'Rename' and 'Rename All' buttons to UI.\r\n" +
            "DONE - Implement file rename code.\r\n" +
            "Add 'Move (Rename)' or 'Copy' selection.\r\n" +
            "Add destination Path selection for 'Move' or 'Copy'.\r\n" +
            "Implement 'Save options' feature.\r\n" +
            "DONE - Add installer (Setup) project.\r\n" +
            "DONE - Check for/fix invalid filenames.\r\n" +
            "folder browser - scroll into view. \r\n" +
            "folder browser - click to select. \r\n" +
            "DONE - Add program icon.\r\n" +
            " \r\n" +
            " \r\n";
        public string AssemblyTitle { get { return Assembly.GetExecutingAssembly().GetName().Name; } }
        public string AssemblyVersion { get { return string.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()); } }
        public string AboutText { get { return aboutText; } }


        public About()
        {
            InitializeComponent();

            DataContext = this;
        }


    }
}

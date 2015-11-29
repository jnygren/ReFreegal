using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.ComponentModel;
using TagLib;

namespace ReFreegalW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private FileInfo[] _mp3Files;
        private string _filepath;
        private string _newfilename;
        public FileInfo[] MP3Files { get { return _mp3Files; } set { _mp3Files = value; OnPropertyChanged("MP3Files"); } }
        public string FreegalFilePath { get { return _filepath; } set { _filepath = value; OnPropertyChanged("FreegalFilePath"); } }
        public string NewFileName { get { return _newfilename; } set { _newfilename = value; OnPropertyChanged("NewFileName"); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            if (string.IsNullOrEmpty(FreegalFilePath = ConfigurationManager.AppSettings["FreegalFilePath"]))
                FreegalFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        }


        /// <summary>
        /// Browse for Freegal folders
        /// </summary>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browseDlg = new System.Windows.Forms.FolderBrowserDialog();
            browseDlg.SelectedPath = FreegalFilePath;
            browseDlg.Description = "Select Freegal folder";
            browseDlg.ShowNewFolderButton = false;
            browseDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            browseDlg.ShowDialog();
            FreegalFilePath = browseDlg.SelectedPath;
        }


        /// <summary>
        /// Update file list when folder is changed
        /// </summary>
        private void freegalPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            DirectoryInfo di;

            try
            {
                if (FreegalFilePath.Length > 0 && (di = new DirectoryInfo(FreegalFilePath)).Exists)
                {
                    MP3Files = di.GetFiles("*.mp3");
                    statusPanel1.Content = "Ready...";
                }
                else
                {
                    MP3Files = new FileInfo[1];
                    statusPanel1.Content = "Directory does not exist!";
                }
            }
            catch (Exception ex)
            {
                statusPanel1.Content = ex.Message;
            }
        }


        /// <summary>
        /// Display parts when new file is selected
        /// </summary>
        private void fileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MP3Files.Length == 0) { NewFileName = ""; return; }

            ListView tuneList = (ListView)sender;
            if (tuneList.SelectedItems.Count == 0) { NewFileName = ""; return; }

            // TagLib Sharp
            TagLib.File file = TagLib.File.Create(((FileInfo)tuneList.SelectedItems[0]).FullName);
            string album = file.Tag.Album;
            string title = file.Tag.Title;
            uint track = file.Tag.Track;

            // Get file extension
            string tune = ((FileInfo)tuneList.SelectedItems[0]).Name;
            string fileExt = tune.Substring(tune.LastIndexOf(".") + 1);

            NewFileName = string.Format("{0:D2} {1}.{2}", track, title, fileExt);
        }


        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}

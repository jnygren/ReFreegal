using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.ComponentModel;
using TagLib;

namespace ReFreegal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private FileInfo[] _mp3Files;
        private string _filepath;
        private string _newfilename;
        // Properties
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
        /// Rename selected file
        /// </summary>
        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            ListView tuneList = fileListView;
            FileInfo srcFile = (FileInfo)tuneList.SelectedItems[0];
            string newFileName = GetNewFileName(srcFile);
            string dstFile = Path.Combine(srcFile.DirectoryName, newFileName);

            //if (dstFile.Equals(srcFile.FullName))
            if (System.IO.File.Exists(dstFile))
                statusPanel1.Content = string.Format("[{0}] already renamed.", newFileName);
            else
            {
                //File.Move(srcFile, dstFile);  // 'Move' is Rename (as well as Move)
                System.IO.File.Copy(srcFile.FullName, dstFile);
                MP3Files = RefreshFileList(FreegalFilePath);

                statusPanel1.Content = string.Format("File renamed to [{0}].", newFileName);
            }
        }


        /// <summary>
        /// Rename all files in list
        /// </summary>
        private void RenameAll_Click(object sender, RoutedEventArgs e)
        {
            ListView tuneList = fileListView;
            int fileCount = 0;

            foreach (FileInfo srcFile in tuneList.Items)
            {
                string newFileName = GetNewFileName(srcFile);
                string dstFile = Path.Combine(srcFile.DirectoryName, newFileName);

                if (!System.IO.File.Exists(dstFile))
                {
                    System.IO.File.Copy(srcFile.FullName, dstFile);
                    fileCount++;
                    MP3Files = RefreshFileList(FreegalFilePath);
                }
            }
            statusPanel1.Content = string.Format("{0} files renamed.", fileCount);
        }


        /// <summary>
        /// Update file list when folder is changed
        /// </summary>
        private void freegalPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FreegalFilePath) || !Directory.Exists(FreegalFilePath))
            {
                MP3Files = new FileInfo[0];
                statusPanel1.Content = "Error: Directory does not exist!";
            }
            else
            {
                MP3Files = RefreshFileList(FreegalFilePath);

                if (MP3Files.Length > 0)
                    statusPanel1.Content = "Ready...";
                else
                    statusPanel1.Content = "No files to rename.";
            }

            btnRenameAll.IsEnabled = MP3Files.Length > 0;
        }


        /// <summary>
        /// Display parts when new file is selected
        /// </summary>
        private void fileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewFileName = "";

            if (MP3Files.Length > 0)
            {
                ListView tuneList = (ListView)sender;
                if (tuneList.SelectedItems.Count > 0)
                {
                    NewFileName = GetNewFileName((FileInfo)tuneList.SelectedItems[0]);
                }
            }
            btnRename.IsEnabled = !string.IsNullOrEmpty(NewFileName);
        }


        /// <summary>
        /// Get new filename for file with Freegal filename
        /// </summary>
        private string GetNewFileName(FileInfo fileInfo)
        {
            // TagLib Sharp
            TagLib.File file = TagLib.File.Create(fileInfo.FullName);
            string album = file.Tag.Album;
            string title = file.Tag.Title;
            uint track = file.Tag.Track;

            // Get file extension
            string fileExt = fileInfo.Extension.Substring(1);

            return string.Format("{0:D2} {1}.{2}", track, title, fileExt);
        }


        /// <summary>
        /// Get update for MP3 Files list
        /// </summary>
        private FileInfo[] RefreshFileList(string filePath)
        {
            FileInfo[] fileList = new FileInfo[0];
            DirectoryInfo di = new DirectoryInfo(filePath);

            try
            {
                if (di.Exists)
                    fileList = di.GetFiles("*.mp3");
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return fileList;
        }


        /// <summary>
        /// Display 'About' box
        /// </summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            About aboutBox = new About();
            aboutBox.ShowDialog();
        }


        /// <summary>
        /// Display ReFreegal Help
        /// </summary>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Navigate to Freegal file folder.\r\n2. Select file to rename (or all).\r\n3. Select 'Rename'.", 
                            "ReFreegal Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Close program
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        /// <summary>
        /// Support for INotifyPropertyChanged interface implementation
        /// </summary>
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}

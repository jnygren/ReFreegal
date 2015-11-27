using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using TagLib;

namespace ReFreegalW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileInfo[] _mp3Files;
        public FileInfo[] MP3Files { get { return _mp3Files; } set { _mp3Files = value; } }

        public MainWindow()
        {
            InitializeComponent();
            freegalPath.Text = @"E:\TEMP\Music For Nitrous Oxide";
        }


        /// <summary>
        /// Browse for Freegal folders
        /// </summary>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog browseDlg = new FolderBrowserDialog();
            browseDlg.SelectedPath = freegalPath.Text;
            browseDlg.Description = "Select Freegal folder";
            browseDlg.ShowNewFolderButton = false;
            browseDlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            browseDlg.ShowDialog();
            freegalPath.Text = browseDlg.SelectedPath;
        }


        /// <summary>
        /// Update file list when folder is changed
        /// </summary>
        private void freegalPath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(freegalPath.Text);
            MP3Files = di.GetFiles("*.mp3");
            fileListView.ItemsSource = MP3Files;
        }


        /// <summary>
        /// Display parts when new file is selected
        /// </summary>
        private void fileListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListView tuneList = (System.Windows.Controls.ListView)sender;
            string tune = ((FileInfo)tuneList.SelectedItems[0]).Name;
            // TagLib Sharp
            TagLib.File file = TagLib.File.Create(((FileInfo)tuneList.SelectedItems[0]).FullName);
            string album = file.Tag.Album;
            string title = file.Tag.Title;
            uint track = file.Tag.Track;

            // Parse the Freegal file name
            int dot = tune.LastIndexOf(".");
            string fileExt = tune.Substring(dot + 1);
            int titleNumber = 0;

            tune = tune.Substring(0, dot);
            string[] tuneParts = tune.Split('_');
            int numParts = tuneParts.Length;
            if (numParts > 0) txtArtist.Text = tuneParts[0];
            if (numParts > 1) txtTitle.Text = tuneParts[1];
            string[] tuneNumParts = tuneParts[2].Split('-');
            if (tuneNumParts.Length > 0) txtXNumber.Text = tuneNumParts[0];
            if (tuneNumParts.Length > 1) txtYNumber.Text = tuneNumParts[1];
            if (tuneNumParts.Length > 2)
            {
                txtTitleNumber.Text = tuneNumParts[2];
                if (!int.TryParse(tuneNumParts[2], out titleNumber)) titleNumber = 0;
            }
            if (numParts > 3) txtFileType.Text = tuneParts[3];
            if (numParts > 4) txtSampleRate.Text = tuneParts[4];
            txtExt.Text = fileExt;

            string newName = string.Format("{0:D2} {1}.{2}", titleNumber, tuneParts[1], fileExt);
            txtNewFileName.Text = newName;
            txtNewFileName2.Text = string.Format("{0:D2} {1}.{2}", track, title, fileExt);
        }

    }
}

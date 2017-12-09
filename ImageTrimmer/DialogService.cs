using System.Windows.Forms;
using static System.Environment;


namespace ImageTrimmer
{
    class DialogService
    { 
        /// <value>Initial directory of dialogs.</value>
        private string initialDirectory;

        private OpenFileDialog openFileDialog;
        private FolderBrowserDialog folderBrowserDialog;

        /// <value>Returns or sets the current initial directory of dialogs.</value>
        public string InitialDirectory
        {
            get { return initialDirectory; }
            set
            {
                initialDirectory = value.Replace("/", "\\");
                ChangeInitialDirectory();
            }
        }

        /// <summary>
        /// Sets the initial directory and creates dialog instances.
        /// </summary>
        public DialogService()
        {
            initialDirectory = GetFolderPath(SpecialFolder.Desktop);
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initialDirectory;

            folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = initialDirectory;
        }

        /// <summary>
        /// Gets the chosen file name from open file dialog.
        /// </summary>
        /// <param name="title">Dialog window title.</param>
        /// <param name="filter">File names filter.</param>
        /// <returns>
        /// A file name or null.
        /// </returns>
        public string GetFileName(string title, string filter = "Все файлы(*.*)|*.*")
        {
            openFileDialog.Title = title;
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;

            return null;
        }

        /// <summary>
        /// Gets the chosen selected path from folder browser dialog.
        /// </summary>
        /// <param name="description">A description text.</param>
        /// <returns>
        /// The selected path or null.
        /// </returns>
        public string GetDirectory(string description)
        {
            folderBrowserDialog.Description = description;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                return folderBrowserDialog.SelectedPath;

            return null;
        }

        /// <summary>
        /// Changes the initial directories of dialogs.
        /// </summary>
        private void ChangeInitialDirectory()
        {
            openFileDialog.InitialDirectory = InitialDirectory;
            folderBrowserDialog.SelectedPath = InitialDirectory;
        }
    }
}

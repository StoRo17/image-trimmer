using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTrimmer
{
    public partial class Form1 : Form
    {
        DialogService dialogService;
        ImageHandler imageHandler;
        AccessDatabase accessDatabase;
        IProgress<int> progress;

        public Form1()
        {
            InitializeComponent();
            dialogService = new DialogService();
            imageHandler = new ImageHandler();
            accessDatabase = new AccessDatabase();

            progress = new Progress<int>(v =>
            {
                progressBar.Value = v;
            });
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            string fileName = dialogService.GetFileName("Выберите изображение",
                "Изображения(*.png; .*jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp");
            if (fileName == null)
                return;

            Bitmap trimmedImage = imageHandler.Trim(imageHandler.GetBitmapImage(fileName));
            imageHandler.SaveImage(trimmedImage, dialogService.InitialDirectory);

            MessageBox.Show("Изображение успешно обрезано и сохранено в: " + dialogService.InitialDirectory,
            "Изображение сохранено",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        }

        private async void openDirectoryButton_Click(object sender, EventArgs e)
        {
            string directoryPath = dialogService.GetDirectory("Выберите папку с изображениями");
            if (directoryPath == null)
                return;

            string pathToSave = dialogService.GetDirectory("Выберите папку, куда сохранить обработанные изображения");
            if (pathToSave == null)
                return;

            string[] imageFileNames = Directory.GetFiles(directoryPath);
            progressBar.Visible = true;
            progressBar.Maximum = imageFileNames.Length;

            await Task.Run(() =>
            {
                for (int i = 0; i < imageFileNames.Length; i++)
                {
                    Bitmap trimmedImage = imageHandler.Trim(imageHandler.GetBitmapImage(imageFileNames[i]));
                    imageHandler.SaveImage(trimmedImage, pathToSave, $"image_{i}");

                    progress.Report(i);
                }
            });

            progressBar.Value = 0;
            progressBar.Visible = false;

            MessageBox.Show("Изображения успешно обрезаны и сохранены в: " + pathToSave,
                "Изображения сохранены",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private async void retrieveFromDbButton_Click(object sender, EventArgs e)
        {
            string databaseFileName = dialogService.GetFileName("Выберите файл базы данных Access",
                "Access(*.mdb; *.accdb)|*.mdb; *.accdb");
            if (databaseFileName == null)
                return;

            string pathToSave = dialogService.GetDirectory("Выберите папку, в которую вы хотите сохранить изображения");
            if (pathToSave == null)
                return;

            accessDatabase.FileName = databaseFileName;
            Dictionary<int, Bitmap> oleImages = accessDatabase.GetOleImages("SELECT * FROM OLE WHERE idOle > 10");

            progressBar.Visible = true;
            progressBar.Maximum = oleImages.Count();

            await Task.Run(() =>
            {
                int i = 0;
                foreach (KeyValuePair<int, Bitmap> idBitmap in oleImages)
                {
                    Bitmap trimmedImage = imageHandler.Trim(idBitmap.Value);
                    imageHandler.SaveImage(trimmedImage,
                        pathToSave,
                        "image_" + idBitmap.Key);

                    progress.Report(i);
                    i++;
                }
            });

            progressBar.Value = 0;
            progressBar.Visible = false;

            MessageBox.Show("Изображения успешно обрезаны и сохранены в: " + pathToSave,
                "Изображения сохранены",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}

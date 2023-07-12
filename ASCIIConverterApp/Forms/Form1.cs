using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ASCIIConverterApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static Bitmap ResizeBitmap(Bitmap bitmap, int maxWidth)
        {
            const double WIDTH_OFFSET = 2.0;
            var newHeight = bitmap.Height / WIDTH_OFFSET * maxWidth / bitmap.Width;
            if (newHeight > maxWidth || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
            }
            return bitmap;
        }
        public void InsertPicture_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.png; *.jpg;"
            };
            Bitmap bitmap = null;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                    Application.Exit();
                    Environment.Exit(0);
            }
            textBox1.Clear();
            bitmap = new Bitmap(openFileDialog.FileName);//передача пути к файлу в объект bitmap
            pictureBox1.Image = bitmap;
            bitmap = ResizeBitmap(bitmap, (int)numericUpDown1.Value);
            bitmap.ToGrayscale();
            var converter = new ASCIIConverter(bitmap);
            var rows = converter.Convert();
            File.WriteAllLines("image.txt", rows.Select(r => new string(r)));
            foreach (var row in rows)
            {
                textBox1.Text = File.ReadAllText("image.txt");
            }
            File.Delete("image.txt");
        }

        private void SaveInFile_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == string.Empty))
            {
                var saveFileDialog = new SaveFileDialog()
                {
                    Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
                }
            }
            else
            {
                MessageBox.Show("Нельзя сохранить пустой файл.","Вставьте картинку.",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для того чтобы конвертировть фото в ASCII формат нужно выбрать ширину и вставить картинку.","Информация.",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

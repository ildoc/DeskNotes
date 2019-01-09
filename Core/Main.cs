using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Core
{
   public class Main
    {
        public static void Update()
        {
            Settings.Instance.Load();

            var backup = Path.Combine(Settings.Instance.BackupPath, $"backup.bmp");

            // initial settings
            if (string.IsNullOrEmpty(Settings.Instance.WallpaperPath)) Settings.Instance.WallpaperPath = Wallpaper.GetPath();

            // check if wallpaper is changed
            if (Settings.Instance.WallpaperPath != Wallpaper.GetPath() && File.Exists(backup)) File.Delete(backup);

            // backup
            if (!File.Exists(backup)) File.Copy(Wallpaper.GetPath(), backup);

            var wallpaperPath = Path.Combine(Directory.GetCurrentDirectory(), "wallpaper.bmp");
            if (File.Exists(wallpaperPath)) File.Delete(wallpaperPath);
            File.Copy(backup, wallpaperPath);



            var text = Settings.Instance.Text;

            var lista = text.Split(new[] { "\r\n", "\r", "\n" },
    StringSplitOptions.None).ToList(); ;
            var points = lista.Select(x => (x, new PointF(100f, 100f + (40f * lista.IndexOf(x)))));

            Bitmap bitmap = (Bitmap)Image.FromFile(wallpaperPath);//load the image file
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 20))
                {
                    foreach (var p in points)
                        graphics.DrawString(p.x, arialFont, Brushes.Blue, p.Item2);
                }
            }

            Bitmap bmp = new Bitmap(bitmap);
            bitmap.Dispose();

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(wallpaperPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    bmp.Save(memory, ImageFormat.Bmp);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            Wallpaper.Set(wallpaperPath, Wallpaper.Style.Fit);
        }

        public static void Reset()
        {
            Settings.Instance.Load();

            var backup = Path.Combine(Settings.Instance.BackupPath, $"backup.bmp");

            if (File.Exists(backup))
            {
                Wallpaper.Set(backup, Wallpaper.Style.Fit);
                File.Delete(backup);
            }
        }
    }
}

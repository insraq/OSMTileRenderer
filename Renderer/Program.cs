using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Renderer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var http = new HttpClient();
            var level = 4;
            var tileSize = 256;

            var l = (int) (tileSize * Math.Pow(2, level));
            var output = new Bitmap(l, l);
            using (var g = Graphics.FromImage(output))
            {
                g.Clear(Color.White);
                for (int x = 0; x < Math.Pow(2, level); x++)
                {
                    for (int y = 0; y < Math.Pow(2, level); y++)
                    {
                        Console.WriteLine($"Drawing Tile {x},{y}");
                        var i = Image.FromStream(await http.GetStreamAsync(
                            $"http://b.tile.stamen.com/terrain-background/{level}/{x}/{y}.jpg"));
                        g.DrawImage(i, new Point(x * tileSize, y * tileSize));
                    }
                }
            }

            output.Save("output.jpg", ImageFormat.Jpeg);
        }
    }
}
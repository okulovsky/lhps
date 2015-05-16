using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageStretcher
{
    class Program
    {


        static Bitmap GetRow(Bitmap bmp, int y, int height)
        {
            var result = new Bitmap(bmp.Width, height);
            using (var g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, new Rectangle(0, -y, bmp.Width,bmp.Height));
            }
            return result;
        }

        static Bitmap StretchBitmapFromTop(Bitmap bmp, int sampleHeight, int additionalStretchHeight)
        {
            var sample = GetRow(bmp, 0, sampleHeight);
            var result = new Bitmap(bmp.Width, bmp.Height + additionalStretchHeight);
            using (var g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, new Rectangle(0, additionalStretchHeight, bmp.Width,bmp.Height));
                g.DrawImage(sample, new Rectangle(0, 0, bmp.Width, additionalStretchHeight + sampleHeight));
            }
            return result;
        }

        static Bitmap StretchBitmap(Bitmap bmp, double aspectRatio)
        {

        }


        static void Main(string[] args)
        {
            var bmp = (Bitmap)Bitmap.FromFile("input.jpg");
            var result = StretchBitmapFromTop(bmp, 10, 50);
            var bmpPictureBox = new PictureBox
            {
                Size=bmp.Size,
                Image = bmp
            };
            var resultPictureBox = new PictureBox
                    {
                        Size = result.Size,
                        Top = bmpPictureBox.Bottom,
                        Image = result
                    };
            var form = new Form
            {
                Controls = 
                {
                    bmpPictureBox,
                    resultPictureBox
                },
                ClientSize = new Size ( Math.Max(bmpPictureBox.Width,resultPictureBox.Width), resultPictureBox.Bottom)
            };
            
            Application.Run(form);
        }
    }
}

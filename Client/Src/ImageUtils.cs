using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Src
{
    public static class ImageUtils
    {
        public static Image ConvertFromByteToBitmap(byte[] imgBytes)
        {
            Image itemImage;

            using (var ms = new System.IO.MemoryStream(imgBytes))
            {
                itemImage = Image.FromStream(ms);
            }

            return itemImage;
        }
    }
}

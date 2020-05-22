using System;
using System.Collections.Generic;
using System.Text;

namespace BingWallpaper
{
    public interface imagelist
    {
        List<string> getListOfImages();
        bool ChangeWallPaper(string filelocation);

        byte[] GetFileBytes(string filePath);
        bool ChangeWallPaperRes(string fileLocation);
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Utils
{
    public static class PathUtils
    {

        public static string GenerateFileName(IFormFile file)
        {
            var date = DateTime.Now;
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{date.Day}_{date.Month}_{date.Year}_{date.Hour}_{date.Minute}_{date.Second}_{date.Millisecond}_{extension}";
            return fileName;
        }
    }
}

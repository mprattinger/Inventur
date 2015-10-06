using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventur.Model
{
    public static class FileExtensions
    {
        public static Task DeleteAsync(this FileInfo fi) {
            return Task.Factory.StartNew(() => fi.Delete());
        }
    }
}

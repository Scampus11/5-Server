using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SMS.CommonClass
{
    public class Checkpath
    {
        public static bool CheckPathExitsOrNot(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
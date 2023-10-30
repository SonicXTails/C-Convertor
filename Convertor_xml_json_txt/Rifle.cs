using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor_xml_json_txt
{
    public class Rifle
    {
        public Rifle()
        {

        }
        public string model;
        public string caliber;
        public string shooting_mode;
        public Rifle(string Model, string Caliber, string Shooting_mode)
        {
            model = Model;
            caliber = Caliber;
            shooting_mode = Shooting_mode;
        }
    }

    public class Cursor
    {
        public int position = 1;
        public int offset = 1;
        public int max = 1;
    }
    public class OpenedFile
    {
        public string path;
        public string text;
    }
}

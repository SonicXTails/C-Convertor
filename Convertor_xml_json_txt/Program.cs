using Convertor_xml_json_txt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Convertor_xml_json_txt
{
    internal class Program
    {
        static public void Main()
        {
            Cursor cursor = new Cursor();
            cursor.offset = 3;
            Console.Write("Введите путь до файла: ");
            OpenedFile openedFile = new OpenedFile();
            openedFile.path = Console.ReadLine();
            Console.Clear();
            openedFile.text = Editor.Edit(openedFile.path);

            Converters.Convert(openedFile.path);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Convertor_xml_json_txt
{
    public class Converters
    {
        internal static string ToText(string path)
        {
            string text = File.ReadAllText(path);

            List<Rifle> result;
            string ext = path.Split(".")[^1];
            if (ext == "xml")
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Rifle>));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    result = (List<Rifle>)xml.Deserialize(fs);
                }
            }
            else if (ext == "json")
            {
                result = JsonConvert.DeserializeObject<List<Rifle>>(File.ReadAllText(path));
            }
            else
            {
                result = ToList(text);
            }
            string response = "";
            foreach (Rifle rifle in result)
            {
                response += $"{rifle.model}\n{rifle.caliber}\n{rifle.shooting_mode}\n";
            }
            return response;
        }
        internal static List<Rifle> ToList(string text)
        {
            List<string> lines = text.Split("\n").ToList();
            List<Rifle> rifles = new List<Rifle>();
            lines.RemoveAll(x => x == "");
            for (int i = 0; i < lines.Count(); i += 3)
            {
                try
                {
                    string model = lines[i];
                    string caliber = lines[i + 1];
                    string shooting_mode = lines[i + 2];
                    Rifle rifle = new Rifle(model, caliber, shooting_mode);
                    rifles.Add(rifle);
                }
                catch
                {
                    break;
                }
            }
            return rifles;
        }
        internal static string ToJson(string text)
        {
            List<Rifle> rifles = ToList(text);
            return JsonConvert.SerializeObject(rifles, Newtonsoft.Json.Formatting.Indented);
        }
        internal static string ToXml(string text)
        {
            List<Rifle> rifles = ToList(text);
            XmlSerializer xml = new XmlSerializer(typeof(List<Rifle>));
            using (FileStream fs = new FileStream("cache.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, rifles);
            }
            string response = File.ReadAllText("cache.xml");
            File.Delete("cache.xml");
            return response;
        }
        public static void Convert(string path)
        {
            Console.Clear();
            string converted = Converters.ToText(path);

            Console.Write("Введите путь куда хотите сохранить данные, также необходимо ввести его формат: ");
            string exp = Console.ReadLine();
            string ext = exp.Split(".")[^1];
            Console.Clear();
            if (ext == "xml")
            {
                converted = Converters.ToXml(converted);
                File.WriteAllText(exp, converted);
            }
            else if (ext == "json")
            {
                converted = Converters.ToJson(converted);
                File.WriteAllText(exp, converted);
            }
            else
            {
                File.WriteAllText(exp, converted);
            }
            End();
        }

        private static void End()
        {
            Console.Clear();
            Console.WriteLine("Конвертирование успешно завершено!");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor_xml_json_txt
{
    public class Editor
    {
        public static string Edit(string path)
        {
            string readFile = Converters.ToText(path);
            string file = EditFile(readFile.Split("\n").ToList(), path);
            File.WriteAllText(path, file);
            return file;
        }
        private static string EditFile(List<string> text, string path)
        {
            Console.Clear();
            static string ArrayToString(List<char> line)
            {
                string response = "";
                foreach (char c in line)
                {
                    response += c;
                }
                return response;
            }
            static List<char> RemoveChar(List<char> line, int position)
            {
                List<char> new_line = new List<char>();
                int y = 0;
                for (int i = 0; i < line.Count(); i++)
                {
                    if (i != position)
                    {
                        new_line.Add(line[i]);
                        y++;
                    }
                }
                return new_line;
            }
            int pos_x = 0;
            int max_pos_x = 0;
            int pos_y = 0;
            int max_pos_y = 0;
            ConsoleKeyInfo aa;
            bool exit = false;
            while (!exit)
            {
                if (pos_x < 0)
                    pos_x = 0;
                if (pos_y < 0)
                    pos_y = 0;
                if (pos_x > max_pos_x)
                    pos_x = max_pos_x;
                Console.Clear();
                max_pos_y = 0;
                Console.SetCursorPosition(0, 0);
                foreach (string line in text)
                {
                    max_pos_y++;
                    Console.WriteLine(line);
                }
                List<char> currentLine = new List<char>();
                currentLine.AddRange(text[pos_y].ToArray());

                max_pos_x = currentLine.Count();
                Console.SetCursorPosition(pos_x, pos_y);
                aa = Console.ReadKey();
                switch (aa.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (pos_x != max_pos_x)
                            pos_x++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (pos_x != 0)
                            pos_x--;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos_y != 0)
                            pos_y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (pos_y != max_pos_y - 1)
                            pos_y++;
                        break;
                    case ConsoleKey.Backspace:
                        if (pos_x != 0)
                        {
                            pos_x -= 1;
                            currentLine = RemoveChar(currentLine, pos_x);
                            text[pos_y] = ArrayToString(currentLine);
                        }
                        else
                        {
                            if (pos_y != 0)
                            {
                                text[pos_y - 1] += ArrayToString(currentLine);
                                text.RemoveAt(pos_y);
                                pos_y -= 1;
                                pos_x = 0;
                            }
                        }
                        break;
                    case ConsoleKey.F1:
                        exit = true;
                        break;
                    case ConsoleKey.Enter:
                        string partOfLine = ArrayToString(currentLine.ToArray()[pos_x..^0].ToList());
                        text.Insert(pos_y + 1, partOfLine);
                        currentLine = currentLine.ToArray()[0..pos_x].ToList();
                        text[pos_y] = ArrayToString(currentLine);
                        pos_x = 0;
                        pos_y++;
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Delete:
                        break;
                    default:
                        {
                            currentLine.Insert(pos_x, aa.KeyChar);
                            text[pos_y] = ArrayToString(currentLine);
                            pos_x += 1;
                            max_pos_x += 1;
                            break;
                        }
                }
            }
            string response = "";
            foreach (string line in text)
            {
                response += (line + "\n");
            }
            if (path.Contains(".xml"))
            {
                response = Converters.ToXml(response).Replace("\n", "");
            }
            else if (path.Contains(".json"))
            {
                response = Converters.ToJson(response);
            }
            return response;
        }
    }
}

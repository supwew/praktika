using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie2
{
    internal class TextEditor
    {
        public string? CurrentFile = null;   // Путь к открытому файлу

        // Имя открытого файла (без папки)
        public string FileName
        {
            get { return Path.GetFileName(CurrentFile ?? ""); }
        }

        // Открытие файла: возврат его текста
        public string Open(string path)
        {
            string text = File.ReadAllText(path);
            CurrentFile = path;
            return text;
        }

        // Сохранение текста в файл
        public void Save(string path, string text)
        {
            File.WriteAllText(path, text);
            CurrentFile = path;
        }
    }
}

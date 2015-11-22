using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;


namespace Lines
{
    [Serializable]
    struct Player : IComparable<Player> //Структура "олицетворяет" одну запись-строку из таблицы рекордов.
    {
        //public int position;
        public string name;
        public int score;
        public Player(string name, int score)
        {
            //this.position = position;
            this.name = name;
            this.score = score;
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", name, score);
        }
        public int CompareTo(Player obj)
        {
            if (this.score < obj.score)
                return 1;
            if (this.score > obj.score)
                return -1;
            else
                return 0;
        }
    }
    class Statistics
    {
        public List<Player> table; //Коллекция игроков из таблицы рекордов
        string fileName;
        public Statistics(string fileName)
        {
            table = new List<Player>();
            this.fileName = fileName;
            ReadTable();
        }
        private void ReadTable() //Метод чтения данных из таблицы рекордов.
        {
            if (File.Exists(fileName))
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = File.OpenRead(fileName))
                {
                    table = (List<Player>)binFormat.Deserialize(fStream);
                }
            }
        }
        public void WriteTable(string tbName, string tbScore) //Метод записи данных в файл.
        {
            Player player = new Player(tbName, Convert.ToInt32(tbScore));
            table.Add(player);
            table.Sort();
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, table);
            }
        }
        public string ViewItemTable(int number) //Вывод данных из таблицы для просмотра.
        {
            return table[number].name + " - " + table[number].score.ToString();
        }
    }
}

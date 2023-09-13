using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public class DiaryRecord
    {
        int id;
        DateTime dateTime;
        string text;

        public DiaryRecord(int id, DateTime dateTime, string text)
        {
            this.id = id;
            this.dateTime = dateTime;
            this.text = text;
        }

        public int Id { get => id;}
        public DateTime DateTime { get => dateTime;}
        public string Text { get => text;}
        public override string ToString()
        {
            return $"Id: {id}, Date: {dateTime.ToString("yyyy-MM-ddTHH:mm:ss")}, Text: {text}";
        }
    }
}

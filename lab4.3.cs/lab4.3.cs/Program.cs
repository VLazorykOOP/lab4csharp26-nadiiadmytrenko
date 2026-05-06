using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolTask
{
    // 1. Оголошення Структури
    struct StudentStruct
    {
        public string FNP;
        public string Class;
        public string Phone;
        public int Math, Physics, Language, Literature;

        public override string ToString() => $"{FNP}, {Class}, Тел: {Phone} [Оцінки: {Math},{Physics},{Language},{Literature}]";
    }
    // 2. Оголошення Запису (Record)
    record StudentRecord(string FNP, string Class, string Phone, int Math, int Physics, int Language, int Literature);
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            RunStructVariant();
            RunTupleVariant();
            RunRecordVariant();
        }
        // ВАРІАНТ 1: СТРУКТУРИ
        static void RunStructVariant()
        {
            Console.WriteLine("ВАРІАНТ 1: СТРУКТУРИ");
            var list = new List<StudentStruct>
            {
                new StudentStruct { FNP = "Іванов І.І.", Class = "10-A", Phone = "09911", Math = 5, Physics = 4, Language = 2, Literature = 4 },
                new StudentStruct { FNP = "Петров П.П.", Class = "10-B", Phone = "09822", Math = 4, Physics = 5, Language = 4, Literature = 5 }
            };
            // Видалення: якщо хоча б одна оцінка == 2
            list.RemoveAll(s => s.Math == 2 || s.Physics == 2 || s.Language == 2 || s.Literature == 2);
            // Додавання на початок
            list.Insert(0, new StudentStruct { FNP = "Новий (Struct)", Class = "11-A", Phone = "000", Math = 5, Physics = 5, Language = 5, Literature = 5 });
            foreach (var s in list) Console.WriteLine(s);
            Console.WriteLine();
        }
        // ВАРІАНТ 2: КОРТЕЖІ (ValueTuple)
        static void RunTupleVariant()
        {
            Console.WriteLine("ВАРІАНТ 2: КОРТЕЖІ ");
            var list = new List<(string FNP, string Class, string Phone, int Math, int Phys, int Lang, int Lit)>
            {
                ("Сидоров С.С.", "9-Б", "06733", 3, 2, 4, 3),
                ("Коваль О.О.", "10-А", "06344", 5, 5, 5, 5)
            };
            // Видалення
            list.RemoveAll(s => s.Math == 2 || s.Phys == 2 || s.Lang == 2 || s.Lit == 2);
            // Додавання на початок
            list.Insert(0, ("Новий (Tuple)", "1-А", "000", 5, 5, 5, 5));
            foreach (var s in list)
                Console.WriteLine($"{s.FNP}, {s.Class} [Оцінки: {s.Math},{s.Phys},{s.Lang},{s.Lit}]");
            Console.WriteLine();
        }
        // ВАРІАНТ 3: ЗАПИСИ
        static void RunRecordVariant()
        {
            Console.WriteLine(" ВАРІАНТ 3: ЗАПИСИ");
            var list = new List<StudentRecord>
            {
                new StudentRecord("Бондар А.М.", "11-Б", "09555", 2, 3, 3, 4),
                new StudentRecord("Ткаченко В.Г.", "11-А", "09366", 4, 4, 5, 4)
            };
            // Видалення
            list.RemoveAll(s => s.Math == 2 || s.Physics == 2 || s.Language == 2 || s.Literature == 2);
            // Додавання на початок
            list.Insert(0, new StudentRecord("Новий (Record)", "10-А", "000", 5, 5, 5, 5));

            foreach (var s in list) Console.WriteLine(s);
            Console.WriteLine();
        }
    }
}
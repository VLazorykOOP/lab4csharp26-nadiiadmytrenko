using System;

namespace RhombusApp
{
    public class DRomb
    {
        protected int d1;
        protected int d2;
        protected int c;

        public DRomb(int diagonal1, int diagonal2, int color)
        {
            d1 = diagonal1;
            d2 = diagonal2;
            c = color;
        }
        public int D1 { get => d1; set => d1 = value; }
        public int D2 { get => d2; set => d2 = value; }
        public int Color => c;
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return d1;
                    case 1: return d2;
                    case 2: return c;
                    default:
                        Console.WriteLine("Помилка: Невірний індекс!");
                        return -1;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: d1 = value; break;
                    case 1: d2 = value; break;
                    case 2: c = value; break;
                    default:
                        Console.WriteLine("Помилка: Невірний індекс!");
                        break;
                }
            }
        }
        public static DRomb operator ++(DRomb r)
        {
            r.d1++;
            r.d2++;
            return r;
        }
        public static DRomb operator --(DRomb r)
        {
            r.d1--;
            r.d2--;
            return r;
        }
        public static bool operator true(DRomb r)
        {
            return r.d1 == r.d2;
        }
        public static bool operator false(DRomb r)
        {
            return r.d1 != r.d2;
        }
        public static DRomb operator +(DRomb r, int scalar)
        {
            return new DRomb(r.d1 + scalar, r.d2 + scalar, r.c);
        }
        public static implicit operator string(DRomb r)
        {
            return $"Ромб: d1={r.d1}, d2={r.d2}, колір={r.c}";
        }
        public static explicit operator DRomb(string s)
        {
            try
            {
                string[] parts = s.Split(' ');
                return new DRomb(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
            }
            catch
            {
                Console.WriteLine("Помилка перетворення рядка в DRomb");
                return new DRomb(0, 0, 0);
            }
        }
        public override string ToString() => (string)this;
    }
    class Program
    {
        static void Main()
        {
            DRomb romb = new DRomb(10, 12, 5);
            Console.WriteLine($"Через індекс 0 (d1): {romb[0]}");
            romb = romb + 2;
            Console.WriteLine($"Після +2: {romb}");
            romb++;
            Console.WriteLine($"Після ++: {romb}");
            if (romb)
                Console.WriteLine("Це квадрат");
            else
                Console.WriteLine("Це не квадрат");
            string info = romb;
            Console.WriteLine("Рядок: " + info);
            DRomb fromString = (DRomb)"15 15 1";
            Console.WriteLine("З рядка: " + fromString);
            if (fromString) Console.WriteLine("Новий ромб - квадрат!");
        }
    }
}

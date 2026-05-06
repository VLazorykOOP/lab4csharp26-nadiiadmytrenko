using System;

namespace VectorApp
{
    class VectorULong
    {
        protected ulong[] IntArray;
        protected uint size;
        protected int codeError;
        protected static uint num_vec = 0;
        public VectorULong()
        {
            size = 1;
            IntArray = new ulong[size];
            IntArray[0] = 0;
            num_vec++;
        }
        public VectorULong(uint s)
        {
            size = s;
            IntArray = new ulong[size];
            for (int i = 0; i < size; i++) IntArray[i] = 0;
            num_vec++;
        }
        public VectorULong(uint s, ulong val)
        {
            size = s;
            IntArray = new ulong[size];
            for (int i = 0; i < size; i++) IntArray[i] = val;
            num_vec++;
        }

        // Деструктор
        ~VectorULong()
        {
            Console.WriteLine($"Вектор розміром {size} видалено.");
        }

        // Властивості
        public uint Size => size;
        public int CodeError
        {
            get => codeError;
            set => codeError = value;
        }

        // Методи
        public void Input()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Елемент [{i}]: ");
                if (!ulong.TryParse(Console.ReadLine(), out IntArray[i]))
                {
                    IntArray[i] = 0;
                    codeError = -1;
                }
            }
        }

        public void Display()
        {
            Console.Write("[ ");
            foreach (var item in IntArray) Console.Write(item + " ");
            Console.WriteLine("]");
        }

        public void SetValue(ulong val)
        {
            for (int i = 0; i < size; i++) IntArray[i] = val;
        }

        public static uint GetNumVec() => num_vec;

        // Індексатор
        public ulong this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    codeError = -1;
                    return 0;
                }
                return IntArray[index];
            }
            set
            {
                if (index < 0 || index >= size)
                {
                    codeError = -1;
                }
                else
                {
                    IntArray[index] = value;
                }
            }
        }

        // Перевантаження операторів

        // Унарні ++, --, !, ~
        public static VectorULong operator ++(VectorULong v)
        {
            for (int i = 0; i < v.size; i++) v.IntArray[i]++;
            return v;
        }

        public static VectorULong operator --(VectorULong v)
        {
            for (int i = 0; i < v.size; i++) v.IntArray[i]--;
            return v;
        }

        public static bool operator true(VectorULong v)
        {
            if (v.size == 0) return false;
            foreach (var x in v.IntArray) if (x != 0) return true;
            return false;
        }

        public static bool operator false(VectorULong v)
        {
            if (v.size == 0) return true;
            foreach (var x in v.IntArray) if (x != 0) return false;
            return true;
        }

        public static bool operator !(VectorULong v) => v.size != 0;

        public static VectorULong operator ~(VectorULong v)
        {
            VectorULong res = new VectorULong(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = ~v.IntArray[i];
            return res;
        }

        // Бінарні арифметичні та побітові 
        private static VectorULong BinaryOp(VectorULong v1, VectorULong v2, Func<ulong, ulong, ulong> op)
        {
            uint maxSize = Math.Max(v1.size, v2.size);
            uint minSize = Math.Min(v1.size, v2.size);
            VectorULong res = new VectorULong(maxSize);

            // Копіюємо елементи з більшого вектора спочатку
            VectorULong longer = v1.size >= v2.size ? v1 : v2;
            for (int i = 0; i < maxSize; i++) res[i] = longer[i];

            // Виконуємо операцію для спільних індексів
            for (int i = 0; i < minSize; i++) res.IntArray[i] = op(v1.IntArray[i], v2.IntArray[i]);
            return res;
        }

        public static VectorULong operator +(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => a + b);
        public static VectorULong operator +(VectorULong v, ulong s)
        {
            VectorULong res = new VectorULong(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] + s;
            return res;
        }
        public static VectorULong operator -(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => a - b);
        public static VectorULong operator -(VectorULong v, ulong s)
        {
            VectorULong res = new VectorULong(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] - s;
            return res;
        }
        public static VectorULong operator *(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => a * b);
        public static VectorULong operator *(VectorULong v, ulong s)
        {
            VectorULong res = new VectorULong(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] * s;
            return res;
        }
        public static VectorULong operator /(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => b != 0 ? a / b : 0);
        public static VectorULong operator /(VectorULong v, ulong s)
        {
            VectorULong res = new VectorULong(v.size);
            if (s == 0) return res;
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] / s;
            return res;
        }
        public static VectorULong operator %(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => b != 0 ? a % b : 0);
        public static VectorULong operator |(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => a | b);
        public static VectorULong operator ^(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => a ^ b);
        public static VectorULong operator &(VectorULong v1, VectorULong v2) => BinaryOp(v1, v2, (a, b) => a & b);

        public static VectorULong operator <<(VectorULong v, int s)
        {
            VectorULong res = new VectorULong(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] << s;
            return res;
        }
        public static VectorULong operator >>(VectorULong v, int s)
        {
            VectorULong res = new VectorULong(v.size);
            for (int i = 0; i < v.size; i++) res.IntArray[i] = v.IntArray[i] >> s;
            return res;
        }
        // Порівняння
        public static bool operator ==(VectorULong v1, VectorULong v2)
        {
            if (v1.size != v2.size) return false;
            for (int i = 0; i < v1.size; i++) if (v1.IntArray[i] != v2.IntArray[i]) return false;
            return true;
        }
        public static bool operator !=(VectorULong v1, VectorULong v2) => !(v1 == v2);
        public static bool operator >(VectorULong v1, VectorULong v2)
        {
            if (v1.size != v2.size) return false;
            for (int i = 0; i < v1.size; i++) if (!(v1.IntArray[i] > v2.IntArray[i])) return false;
            return true;
        }
        public static bool operator <(VectorULong v1, VectorULong v2)
        {
            if (v1.size != v2.size) return false;
            for (int i = 0; i < v1.size; i++) if (!(v1.IntArray[i] < v2.IntArray[i])) return false;
            return true;
        }
        public static bool operator >=(VectorULong v1, VectorULong v2)
        {
            if (v1.size != v2.size) return false;
            for (int i = 0; i < v1.size; i++) if (!(v1.IntArray[i] >= v2.IntArray[i])) return false;
            return true;
        }
        public static bool operator <=(VectorULong v1, VectorULong v2)
        {
            if (v1.size != v2.size) return false;
            for (int i = 0; i < v1.size; i++) if (!(v1.IntArray[i] <= v2.IntArray[i])) return false;
            return true;
        }
    }
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            VectorULong v1 = new VectorULong(3, 10);
            VectorULong v2 = new VectorULong(3, 5);
            VectorULong v3 = new VectorULong(5, 2); // Більший вектор
            Console.WriteLine("Вектор 1:"); v1.Display();
            Console.WriteLine("Вектор 2:"); v2.Display();
            Console.WriteLine("Вектор 3 (інший розмір):"); v3.Display();
            VectorULong sum = v1 + v2;
            Console.Write("v1 + v2 = "); sum.Display();
            VectorULong mixedSum = v1 + v3;
            Console.Write("v1 + v3 (різні розміри) = "); mixedSum.Display();
            Console.WriteLine($"Кількість створених векторів: {VectorULong.GetNumVec()}");
            v1++;
            Console.Write("v1 після ++: "); v1.Display();
            Console.WriteLine($"v1 > v2: {v1 > v2}");
            Console.WriteLine("Робота з індексатором:");
            v1[1] = 100;
            Console.WriteLine($"v1[1] = {v1[1]}");
            v1[10] = 5; // Помилка індексу
            Console.WriteLine($"Код помилки після v1[10]: {v1.CodeError}");
            if (v1) Console.WriteLine("v1 не порожній (оператор true)");
        }
    }
}

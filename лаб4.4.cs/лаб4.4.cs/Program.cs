using System;

namespace MatrixApp
{
    // Допоміжний клас для множення
    public class VectorULong
    {
        public ulong[] Array;
        public uint Size;
        public VectorULong(uint size) { Size = size; Array = new ulong[size]; }
        public ulong this[int i] { get => Array[i]; set => Array[i] = value; }
    }
    public class MatrixUlong
    {
        // Поля
        protected ulong[,] ULArray;
        protected uint n, m;
        protected int codeError;
        protected static int num_m = 0;

        // Конструктори
        public MatrixUlong()
        {
            n = 1; m = 1;
            ULArray = new ulong[n, m];
            ULArray[0, 0] = 0;
            num_m++;
        }
        public MatrixUlong(uint n, uint m)
        {
            this.n = n; this.m = m;
            ULArray = new ulong[n, m];
            num_m++;
        }
        public MatrixUlong(uint n, uint m, ulong initValue) : this(n, m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    ULArray[i, j] = initValue;
        }
        // Деструктор (Фіналізатор)
        ~MatrixUlong()
        {
            Console.WriteLine("Матриця видалена з пам'яті.");
        }
        // Методи
        public void Input()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"[{i},{j}] = ");
                    ULArray[i, j] = ulong.Parse(Console.ReadLine());
                }
        }
        public void Output()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(ULArray[i, j] + "\t");
                Console.WriteLine();
            }
        }
        public void SetValue(ulong val)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    ULArray[i, j] = val;
        }
        public static int CountMatrices() => num_m;
        // Властивості
        public uint Rows => n;
        public uint Cols => m;
        public int Error { get => codeError; set => codeError = value; }
        // Індексатори
        public ulong this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < n && j >= 0 && j < m) return ULArray[i, j];
                codeError = -1; return 0;
            }
            set
            {
                if (i >= 0 && i < n && j >= 0 && j < m) ULArray[i, j] = value;
                else codeError = -1;
            }
        }
        public ulong this[int k]
        {
            get
            {
                int i = k / (int)m;
                int j = k % (int)m;
                if (i >= 0 && i < n && j >= 0 && j < m) return ULArray[i, j];
                codeError = -1; return 0;
            }
            set
            {
                int i = k / (int)m;
                int j = k % (int)m;
                if (i >= 0 && i < n && j >= 0 && j < m) ULArray[i, j] = value;
                else codeError = -1;
            }
        }
        // Перевантаження унарних операцій
        public static MatrixUlong operator ++(MatrixUlong mat)
        {
            for (int i = 0; i < mat.n; i++)
                for (int j = 0; j < mat.m; j++) mat.ULArray[i, j]++;
            return mat;
        }
        public static MatrixUlong operator --(MatrixUlong mat)
        {
            for (int i = 0; i < mat.n; i++)
                for (int j = 0; j < mat.m; j++) mat.ULArray[i, j]--;
            return mat;
        }
        public static bool operator true(MatrixUlong mat)
        {
            if (mat.n == 0 || mat.m == 0) return false;
            foreach (var x in mat.ULArray) if (x != 0) return true;
            return false;
        }
        public static bool operator false(MatrixUlong mat)
        {
            if (mat.n == 0 || mat.m == 0) return true;
            foreach (var x in mat.ULArray) if (x != 0) return false;
            return true;
        }
        public static bool operator !(MatrixUlong mat) => mat.n != 0 && mat.m != 0;
        public static MatrixUlong operator ~(MatrixUlong mat)
        {
            MatrixUlong res = new MatrixUlong(mat.n, mat.m);
            for (int i = 0; i < mat.n; i++)
                for (int j = 0; j < mat.m; j++) res[i, j] = ~mat[i, j];
            return res;
        }
        // Бінарні операції (Приклад + та *)
        public static MatrixUlong operator +(MatrixUlong a, MatrixUlong b)
        {
            if (a.n != b.n || a.m != b.m) return a;
            MatrixUlong res = new MatrixUlong(a.n, a.m);
            for (int i = 0; i < a.n; i++)
                for (int j = 0; j < a.m; j++) res[i, j] = a[i, j] + b[i, j];
            return res;
        }
        public static MatrixUlong operator +(MatrixUlong a, ulong b)
        {
            MatrixUlong res = new MatrixUlong(a.n, a.m);
            for (int i = 0; i < a.n; i++)
                for (int j = 0; j < a.m; j++) res[i, j] = a[i, j] + b;
            return res;
        }
        public static MatrixUlong operator *(MatrixUlong a, MatrixUlong b)
        {
            if (a.m != b.n) return a;
            MatrixUlong res = new MatrixUlong(a.n, b.m);
            for (int i = 0; i < a.n; i++)
                for (int j = 0; j < b.m; j++)
                    for (int k = 0; k < a.m; k++)
                        res[i, j] += a[i, k] * b[k, j];
            return res;
        }
        // Порівняння
        public static bool operator ==(MatrixUlong a, MatrixUlong b)
        {
            if (a.n != b.n || a.m != b.m) return false;
            for (int i = 0; i < a.n; i++)
                for (int j = 0; j < a.m; j++)
                    if (a[i, j] != b[i, j]) return false;
            return true;
        }
        public static bool operator !=(MatrixUlong a, MatrixUlong b) => !(a == b);
        public static bool operator >(MatrixUlong a, MatrixUlong b)
        {
            if (a.n != b.n || a.m != b.m) return false;
            for (int i = 0; i < a.n; i++)
                for (int j = 0; j < a.m; j++)
                    if (!(a[i, j] > b[i, j])) return false;
            return true;
        }
        public static bool operator <(MatrixUlong a, MatrixUlong b)
        {
            if (a.n != b.n || a.m != b.m) return false;
            for (int i = 0; i < a.n; i++)
                for (int j = 0; j < a.m; j++)
                    if (!(a[i, j] < b[i, j])) return false;
            return true;
        }
        public override bool Equals(object obj) => obj is MatrixUlong m && this == m;
        public override int GetHashCode() => ULArray.GetHashCode();
    }
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MatrixUlong m1 = new MatrixUlong(2, 2, 10);
            MatrixUlong m2 = new MatrixUlong(2, 2, 5);
            Console.WriteLine("Матриця 1:");
            m1.Output();
            Console.WriteLine("\nМатриця 1 + 5 (скаляр):");
            (m1 + 5).Output();
            Console.WriteLine("\nМатриця 1 + Матриця 2:");
            (m1 + m2).Output();
            Console.WriteLine("\nПеревірка m1 > m2:");
            Console.WriteLine(m1 > m2);
            Console.WriteLine($"\nКількість створених матриць: {MatrixUlong.CountMatrices()}");
        }
    }
}

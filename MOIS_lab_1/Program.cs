using System;
using System.Diagnostics;

namespace MOIS_lab_1
{
    class MyClass
    {
        /// <summary>
        /// Перевод строки в натуральное число.
        /// </summary>
        /// <param name="N">Переменная для натурального числа.</param>
        /// <param name="str">Наша строка.</param>
        public static void ToNatural(ref uint N, string str)
        {
            try
            {
                N = Convert.ToUInt32(str);
            }

            catch
            {

            }
        }

        /// <summary>
        /// Создание случайного одномерного массива размера N.
        /// </summary>
        /// <param>uint N - размер массива</param>
        /// <returns>int[] - случайный массив</returns>
        public static int[] GetArray(uint N)
        {
            int[] Arr = new int[N];
            Random rnd = new Random();

            for (uint i = 0; i < Arr.Length; i++)
            {
                Arr[i] = rnd.Next(-15, 16);
            }

            System.Threading.Thread.Sleep(20);

            return Arr;
        }

        /// <summary>
        /// Вывод одномерного массива в консоль.
        /// </summary>
        /// <param name="Arr">int[] Arr - массив, который следует вывести</param>
        public static void OutputArray(int[] Arr)
        {
            for (uint i = 0; i < Arr.Length; i++)
            {
                Console.Write("{0}\t", Arr[i]);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Сортировка массива методом прямой вставки.
        /// </summary>
        /// <param name="array">Массив, который следует отсортировать.</param>
        public static void SortByInsertion(int[] array)
        {
            int _;

            //Рассматриваем элемент и сравниваем его с пред.
            for (int i = 1; i < array.Length; i++)
            {
                //Меняем местами, если пред. элемент больше рассматриваемого.
                for (int j = i; j > 0 && array[j - 1] > array[j]; j--)
                {
                    _ = array[j - 1];
                    array[j - 1] = array[j];
                    array[j] = _;
                }
            }
        }

        /// <summary>
        /// Быстрая сортировка.
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="left">Крайний левый элемент рассматриваемого диапазона</param>
        /// <param name="right">Крайний правый элемент рассматриваемого диапазона</param>
        public static void SortQuickly(int[] array, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int pivot = GetPartition(array, left, right); //Индекс элемента, относительно которого прошло разделение массива на 2 части.
            SortQuickly(array, left, pivot - 1); //Рекурсивно сортируем левую часть.
            SortQuickly(array, pivot + 1, right); //Рекурсивно сортируем правую часть.
        }

        /// <summary>
        /// Разделение рассматриваемой части массива относительно среднего элемента.
        /// </summary>
        /// <param name="array">Сортируемый массив</param>
        /// <param name="left">Крайний левый элемент рассматриваемой части</param>
        /// <param name="right">Крайний правый</param>
        /// <returns>Индекс разделителя</returns>
        public static int GetPartition(int[] array, int left, int right)
        {
            int i = left; //Счётчик для поиска элемента слева.
            int j = right; //Для поиска элемента справа.
            int _;
            int pivotIndex = (left + right) / 2; //Индекс разделителя (серединка).
            int pivot = array[pivotIndex]; //Сам разделитель.

            //Пока левый поиск и правый поиск не пересеклись.
            do
            {
                //Поиск с левой стороны элемента, большего, чем разделитель (макс.: индекс самого разделителя).
                while (i < pivotIndex && array[i] <= pivot)
                {
                    i++;
                }

                //Поиск с правой стороны элемента меньшего, чем разделитель, или равного ему.
                while (array[j] > pivot)
                {
                    j--;
                }

                //Если 1 индекс меньше 2, то меняем элементы местами.
                if (i < j)
                {
                    //Если мы перемещаем разделитель, то меняем его индекс.
                    if (i == pivotIndex)
                    {
                        pivotIndex = j;
                    }

                    else if (j == pivotIndex)
                    {
                        pivotIndex = i;
                    }

                    _ = array[i];
                    array[i] = array[j];
                    array[j] = _;
                }
            }
            while (i < j);

            //Возвращаем индекс разделителя.
            return i;
        }
    }

    class Program
    {
        static void Main()
        {
            int[] array = null; //Генерируемый массив (неповторимый оригинал).
            int[] arrayCopy = null; //Его копия для сортировки (жалкая пародия).

            // создать объект пользовательского класса
            ConsoleKeyInfo K;

            Stopwatch sWatch; //Счётчик для измерения времени.
            TimeSpan tSpan; //Прошедшее время.

            do
            {

                Console.Clear(); //очистка экрана перед выводом меню
                Console.WriteLine("1. Создать массив");
                Console.WriteLine("2. Вывести массив");
                Console.WriteLine("3. Сортировка методом прямой вставки");
                Console.WriteLine("4. Быстрая сортировка\n");
                Console.WriteLine("Esc. Выход из программы.\n");
                Console.WriteLine("Пожалуйста, введите номер команды (1-4):");

                K = Console.ReadKey(); //считывание кода вводимой клавиши

                Console.WriteLine("\n");

                try
                {
                    switch (K.Key)
                    {
                        case ConsoleKey.D1: // если нажата клавиша с цифрой 1
                            {
                                uint N = 0;

                                while (N == 0)
                                {
                                    Console.WriteLine("Введите размер массива:");

                                    MyClass.ToNatural(ref N, Console.ReadLine());

                                    Console.WriteLine();
                                }

                                array = MyClass.GetArray(N);

                                Console.WriteLine("Массив создан.");

                                arrayCopy = null;

                                break;
                            }

                        case ConsoleKey.D2: // если нажата клавиша с цифрой 2
                            {
                                if (array.Length > 0)
                                {
                                    Console.WriteLine("Исходный массив:");

                                    MyClass.OutputArray(array);

                                    Console.WriteLine();
                                }

                                if (arrayCopy != null)
                                {

                                    Console.WriteLine("Отсортированный массив:");

                                    MyClass.OutputArray(arrayCopy);
                                }

                                break;
                            }

                        case ConsoleKey.D3:// если нажата клавиша с цифрой 3
                            {
                                arrayCopy = (int[])array.Clone();
                                sWatch = new Stopwatch();

                                sWatch.Start();

                                MyClass.SortByInsertion(arrayCopy);

                                sWatch.Stop();

                                tSpan = sWatch.Elapsed; //Записываем время.

                                Console.WriteLine("Сортировка методом прямой вставки завершена.");
                                Console.WriteLine("Потрачено: " + tSpan.ToString());

                                break;
                            }

                        case ConsoleKey.D4:
                            {
                                arrayCopy = (int[])array.Clone();
                                sWatch = new Stopwatch();

                                sWatch.Start();

                                MyClass.SortQuickly(arrayCopy, 0, array.Length - 1);

                                sWatch.Stop();

                                tSpan = sWatch.Elapsed; //Записываем время.

                                Console.WriteLine("Быстрая сортировка завершена.");
                                Console.WriteLine("Потрачено: " + tSpan.ToString());

                                break;
                            }

                        default: break;
                    }

                    //Приостанавливаем выполнение текущего потока на заданное число времени в мс.
                    System.Threading.Thread.Sleep(2000); //2 с

                }

                catch
                {

                }
            }
            while (K.Key != ConsoleKey.Escape);//Цикл заканчивается, если нажата клавиша Esc.
        }
    }
}
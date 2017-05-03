using System;
using System.Collections;
using System.Collections.Generic;

// Никаноров Алесандр
// 03.05.2017
// VS 2010

namespace Fibonacci
{
    class Program
    {
        // Лист чисел ряда Фибоначчи.
        static List<int> Fibon;
        static void Main(string[] args)
        {
            Fibon = new List<int>();
            Fibfunc(8, 0);
            Console.WriteLine("Поиск чисел в последовательности Фибоначчи.");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Один из параметров должен быть равен нулю, если Вы хотите использовать второй параметр поиска. Параметры должны быть целочисленные.");
                Console.WriteLine("Введите желаемый индекс:");
                int firstPar = 0;
                if (!int.TryParse(Console.ReadLine(), out firstPar))
                {
                    ShowErrorMessage();
                    continue;
                }
                Console.WriteLine("Введите число, ближайшее к которому Вы хотите найти:");
                int scndPar = 0;
                if (!int.TryParse(Console.ReadLine(), out scndPar))
                {
                    ShowErrorMessage();
                    continue;
                }
                try
                {
                    int result = Fibfunc(firstPar, scndPar);
                    if (result == 0)
                    {
                        Console.WriteLine("Введены неверные параметры, попробуйте еще раз!");
                    }
                    else
                    {
                        Console.WriteLine("Ваше число из последовательности Фибоначчи:");
                        Console.WriteLine(result);
                    }
                }
                // Ошибки зависят от размера входных данных.
                // Если индекс будет слишком велик, если вероятность выхода за пределы Integer.
                // 100% будет SystemOutOfMemoryException для размера списка.
                catch (Exception)
                {
                    Console.WriteLine("Произошла ошибка, попробуйте другие входные данные.");
                }   
                Console.WriteLine("Нажмите любую клавишу, либо Esc для выхода.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// 
        /// </summary>
        static void ShowErrorMessage()
        {
            Console.WriteLine("Введены неверные параметры, попробуйте еще раз!");
            Console.WriteLine("Нажмите любую клавишу, либо Esc для выхода.");
        }

        /// <summary>
        /// Функция нахождения чисел в ряде Фибоначчи.
        /// </summary>
        /// <param name="idx">Индекс числа [1, 2..]</param>
        /// <param name="maxFib">Число, ближайшее к которому в меньшую сторону нужно найти</param>
        public static int Fibfunc(int idx, int maxFib)
        {
            // Метод работает только с одним из параметров. Другой должен быть равен 0.
            if ((idx < 1 && maxFib < 1) || (idx >= 1 && maxFib >= 1) || idx < 0 || maxFib < 0)
            {
                return 0;
            }
            // Нахождение числа по индексу.
            if (idx >= 1)
            {
                if (idx - 1 < Fibon.Count)
                {
                    return Fibon[idx - 1];
                }
                // Если числа с заданным индексом в листе еще нет, посчитать последовательность до этого индекса.
                int count = idx - Fibon.Count;
                for (int i = 0; i < count; i++)
                {
                    int fSummand = Fibon.Count - 1 < 0 ? 1 : Fibon[Fibon.Count - 1];
                    int sSummand = Fibon.Count - 2 < 0 ? 0 : Fibon[Fibon.Count - 2];
                    int newNum = fSummand + sSummand;
                    Fibon.Add(newNum);
                }
                return Fibon[idx - 1];
            }
            // Нахождение числа, ближайшего в меньшую сторону к maxFib.
            // Если число не находится в текущем листе.
            if (Fibon.Count == 0 || maxFib > Fibon[Fibon.Count - 1])
            {
                int newNum = 0;
                while (maxFib > newNum)
                {
                    int fSummand = Fibon.Count - 1 < 0 ? 1 : Fibon[Fibon.Count - 1];
                    int sSummand = Fibon.Count - 2 < 0 ? 0 : Fibon[Fibon.Count - 2];
                    newNum = fSummand + sSummand;
                    Fibon.Add(newNum);
                }
                if (newNum == maxFib)
                    return maxFib;
                return Fibon[Fibon.Count - 2];
            }
            // Если maxFib меньше, чем максимальное число в листе.
            // Искомое число точно находится в листе.
            int low = 0;
            int high = Fibon.Count - 1;
            int mid = (low + high) / 2;
            while (mid != low)
            {
                if (Fibon[mid] > maxFib)
                {
                    high = mid;
                }
                if (Fibon[mid] < maxFib)
                {
                    low = mid + 1;
                    if (Fibon[low] > maxFib)
                        return Fibon[low - 1];
                }
                if (Fibon[mid] == maxFib)
                {
                    return maxFib;
                }
                mid = (low + high) / 2;
            }
            return Fibon[mid];
        }
    }
}

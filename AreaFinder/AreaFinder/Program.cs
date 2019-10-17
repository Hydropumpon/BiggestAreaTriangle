using System;
using System.IO;
using System.Linq.Expressions;


/**********************************************************************************************************************************************************************************************************
 **********************************************************************************************************************************************************************************************************
 * Программа считывает из файла args[0] координаты вершин равнобедренных треугольников и записывает координаты равнобедренного треугольника с наибольшей
 * площадью в файл args[1]. Если имеются несколько равнобедренных треугольников с одинаковой площадью - программа запишет координаты первого встретившегося в файле.
 * Если в строке находится меньше 6 координат вершин - выдается сообщение об ошибке и данная строка не учитывается. Сообшение об ошибке содержит номер строки. Если число строк в файле больше int64.Max -
 * счетчик при переполнении сбрасывается и начинает считать с 0.
 * Если в строке находится больше 6 координат вершин - берутся первые 6 вершин и преобразуются в целые числа.
 * Если в ходе обработки строки находится нечисленное значение, или указанное значение больше Int64.Max - строка пропускается, выдается сообщение об ошибке. Сообшение об ошибке содержит номер строки.
 * Если программа не может открыть файл для записи результатов - выдается сообщение об ошибке и результат выводится в командную строку.
 * По окончанию работы программы запрашивает нажатие любой клавиши от пользователя.
 **********************************************************************************************************************************************************************************************************
 *********************************************************************************************************************************************************************************************************/

namespace AreaFinder
{
    class Program
    {
        //требуемое количество токенов в строке
        const int NUMBER_OF_TOKENS = 6;
        static void Main(string[] args)
        {
            //проверяем количество аргументов командной строки
            if (args.Length != 2)
            {
                Console.WriteLine("Указано неверное количество аргументов вызова программы.\nПожалуйста, укажите правильный входный и выходной файл.");
            }
            else
            {
                var inFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, args[0]);
                Console.WriteLine(inFilePath);
                //проверяем существует ли файл с исходными данными
                if (!(File.Exists(inFilePath)))
                {
                    Console.WriteLine("Файла с исходными данными не существует.");
                }
                else
                {
                    Triangle biggest = new Triangle();
                    try
                    {
                        using (var readFile = new StreamReader(inFilePath))
                        {
                            string line = null;
                            long strCounter = 0;
                            while ((line = readFile.ReadLine()) != null)
                            {
                                try
                                {
                                    checked
                                    {
                                        strCounter++;
                                    }
                                }
                                catch
                                {
                                    strCounter = 0;
                                }

                                var stringParser = new StringParser(line, NUMBER_OF_TOKENS);
                                if (stringParser.areEnoughTokens)
                                {
                                    if (stringParser.areAllDigits)
                                    {
                                        Triangle newTriangle = new Triangle(stringParser.GetPoints());
                                        if (newTriangle.area != 0)
                                        {
                                            if (newTriangle.area > biggest.area)
                                            {
                                                biggest = newTriangle;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Вершины в строке {strCounter} не являются равнобедренным треугольником");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Данные вершин в строке {strCounter} не являются целыми числами.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Недостаточно вершин в строке {strCounter}.");
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Файл не может быть прочитан");
                    }

                    try
                    {
                        using (var outputFile = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), args[1]), FileMode.Create, FileAccess.Write))
                        {
                            using (var streamWriter = new StreamWriter(outputFile))
                            {
                                if (biggest.area != 0)
                                {
                                    streamWriter.WriteLine(biggest);
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка при записи данных в файл, вывожу результат сюда.");
                        if (biggest.area != 0)
                        {
                            Console.WriteLine(biggest);
                        }
                    }
                }
            }
            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }
}

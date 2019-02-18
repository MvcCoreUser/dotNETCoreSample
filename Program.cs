using System;
using System.IO;
using System.Linq;
using PerehodkoEA_Lab6.Infrastructure;
using System.Threading;
using System.Globalization;

namespace PerehodkoEA_Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               //устанавливаем формат для обработки чисел с плавающей точкой
            Thread.CurrentThread.CurrentCulture=new CultureInfo("en-US");
            //указываем каталог с файлами уравнений
            DirectoryInfo directory=new DirectoryInfo("tasks_50");
            //указываем результирующий файл
            FileInfo file= new FileInfo("result.xml");

            //сериализация
            Resources.SerializeModelInstance.Serialize(directory, file);
            Console.WriteLine("Сериализация выполнена успешно.");
            Console.WriteLine("Меню: 0- выход\n1-Вывести файл на экран\n2-Открыть файл");
            int menuItem=-1;
            string input=Console.ReadLine();
            while (!int.TryParse(input, out menuItem) || (menuItem<0 && menuItem>2))
            {
                Console.WriteLine("Меню: 0- выход\n1-Вывести файл на экран\n2-Открыть файл");
                input=Console.ReadLine();
            }  
            switch (menuItem)
            {
                case 0:
                    
                break;
                case 1:
                    File.ReadAllLines(file.FullName).ToList().ForEach(
                        line=> Console.WriteLine(line)
                    );
                break;
                case 2:
                    System.Diagnostics.Process.Start(file.FullName);
                break;
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n Stack Trace:\n{ex.StackTrace}");
            }
            
            
            Console.WriteLine("Для выхода нажмите любую клавишу..");    
            Console.ReadKey();
        }
    }
}

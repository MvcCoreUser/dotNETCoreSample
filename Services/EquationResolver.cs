using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using PerehodkoEA_Lab6.Entities;
using PerehodkoEA_Lab6.Interfaces;

namespace PerehodkoEA_Lab6.Services
{
    public class EquationResolver : IEquationResolver
    {
         ///<summary>
        ///вспомогательный метод, создает объекты решения уравнений из переданных ему файлов
        ///</summary>
        private IEnumerable<Result> ResultsFromFiles(IEnumerable<FileInfo> files)
        {
            List<Result> res=null;
            foreach (var file in files)
            {
                if (file.Exists)
                {
                    //файл существует на диске, открываем его
                    res=new List<Result>();
                    XDocument xDocument= XDocument.Load(file.FullName);
                    //для каждого узла Task (уравнение) создаем объект уравнения и объект его решения
                    xDocument.Descendants("Task").ToList().ForEach(
                        xElement=>
                        {
                            Equation equation= Equation.ParseFromXElement(xElement);
                            Result result= new Result(equation);
                            res.Add(result);
                        }
                    );
                }
                else
                {
                    throw new Exception("Файл отсутсвует на диске");
                }
            }
            return res;
        }

        ///<summary>
        ///асинхронная версия вспомогательного метода, создает поток (задачу) для обработки переданных файлов
        ///</summary>
        private async Task<IEnumerable<Result>> ResultsFromFilesAsync(IEnumerable<FileInfo> files)
        => await Task.Run(()=>this.ResultsFromFiles(files));


        public int TaskNum { get; set; }=5;
        public int FilesPerTask { get; set; }=10;

        public IEnumerable<FileInfo> GetFiles()
        {
            if (Directory.Exists)
            {
                return Directory.EnumerateFiles("*.xml");
            }
            else
            {
                throw new Exception($"{Directory.FullName} не существует");
            }
        }

       

        public Task<IEnumerable<Result>>[] TaskResults() 
         { 
            //инициализируем список с потоками (задачами) 
            List<Task<IEnumerable<Result>>> res=new List<Task<IEnumerable<Result>>>();
            //получаем список всех файлов из каталога
            IEnumerable<FileInfo> allFiles= GetFiles();
            //формирует потоки (задачи), всего должно получиться 5 потоков по 10 файлов в каждом потоке
            for (int i = 0; i < allFiles.Count(); i+=FilesPerTask)
            {
                var files= allFiles.Skip(i).Take(FilesPerTask);
                res.Add(this.ResultsFromFilesAsync(files));
            } 
            return res.ToArray();
         }

        public DirectoryInfo Directory {get; set;}

        
    }
}
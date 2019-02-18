using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PerehodkoEA_Lab6.Entities;
using PerehodkoEA_Lab6.Interfaces;
using PerehodkoEA_Lab6.Services;

namespace PerehodkoEA_Lab6.Infrastructure
{
    ///<summary>
    /// Класс-оболочка для синглтона модели сериализации
    ///</summary>
    public static class Resources
    {
        public static SerializeModel SerializeModelInstance=> SerializeModel.Current;
    }

    
    // public class TaskList
    // { 
    //     [XmlArray] 
    //     [XmlArrayItem("Task")] 
    //     public Equation[] Equations { get; set; } 
    // } 
 
    ///<summary>
    /// класс-оболочка для сериализации решений квадратных уравнений
    ///</summary>
    public class ResultList 
    { 
        [XmlArray] 
        [XmlArrayItem("Result")] 
        public Result[] Results { get; set; } 
    } 

     ///<summary>
    /// модель для сериализации решений квадратных уравнений
    ///</summary>
    public class SerializeModel
    {
        private SerializeModel()
        {
            
        }

        public static SerializeModel Current=> new SerializeModel();

         ///<summary>
        /// корневой элемент Data результирующего XML документа
        ///</summary>
        ///<returns>
        /// возвращает решения всех уравнения для сериализации
        ///</returns>
        [XmlElement("Data")]
        public ResultList Results 
        { 
            get
            {
                //инициализируем список решений всех уравнений из файлов
                List<Result> res=new List<Result>();
                //ждем пока выполнятся все задания (потоки)
                Task.WhenAll(EquationResolver.TaskResults());
                //получаем список результатов выполнения задач (потоков)
                var tasks=  EquationResolver.TaskResults();
                //добавляем решения уравнений из каждого потока в список
                foreach (var item in tasks)
                {
                    res.AddRange(item.Result.ToArray());
                }
                return new ResultList(){Results= res.ToArray()};
            }
             set{;} 
        }
         ///<summary>
        /// внедряем зависимость для абстрактной фабрики
        ///</summary>
        [XmlIgnore]
        IServiceFactory ServiceFactory
        => new ServiceFactory();

        [XmlIgnore]
        public DirectoryInfo Directory { get; set; }

         ///<summary>
        /// внедряем зависимость для решателя уравнений
        ///</summary>
        [XmlIgnore]
        public IEquationResolver EquationResolver
        =>ServiceFactory.CreateEquationResolver(Directory);

         ///<summary>
        ///метод выполняет сериализацию 
        ///</summary>
        ///<param name="inputDir">
        ///каталог с файлами уравнений
        ///</param>
        ///<param name="outputFile">
        ///результирующий XML файл с решениями 
        ///</param>
        public FileInfo Serialize(DirectoryInfo inputDir, FileInfo outputFile)
        {
            Directory= inputDir;
            if (outputFile.Exists)
            {
                XmlSerializer xmlSerializer=new XmlSerializer(typeof(SerializeModel));
                FileStream fileStream=new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write);
                xmlSerializer.Serialize(fileStream, this);
                fileStream.Close();
                fileStream.Dispose();
                return outputFile; 
            }
            else
            {
               throw new Exception("Файл для записи отсутствует");
            }    
        }

    }

}
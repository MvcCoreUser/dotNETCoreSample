using System;
using System.Collections.Generic;
using System.IO;
using PerehodkoEA_Lab6.Entities;
using System.Threading.Tasks;

namespace PerehodkoEA_Lab6.Interfaces
{
    ///<summary>
    /// интерфейс описывающий решатель квадратного уравнения
    ///</summary>
    public interface IEquationResolver
    {
        ///<summary>
        /// кол-во задач (потоков), выполняющих считывание файлов
         ///</summary>
        int TaskNum { get; set; }
        
         ///<summary>
        /// кол-во файлов, обрабатываемых в одной задаче (потоке)
         ///</summary>
        int FilesPerTask {get; set;}

        ///<summary>
        /// каталог с исходными файлами в формает XML
         ///</summary>
        DirectoryInfo Directory{get;}

        ///<summary>
        /// метод считывает список файлов из потока
         ///</summary>
        IEnumerable<FileInfo> GetFiles();
        ///<summary>
        /// массив задач (потоков) с решениями квадратных уравнений
         ///</summary>
        Task<IEnumerable<Result>>[] TaskResults();
    }
}
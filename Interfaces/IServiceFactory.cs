using System;
using PerehodkoEA_Lab6.Interfaces;
using System.IO;

namespace PerehodkoEA_Lab6.Interfaces
{
    ///<summary>
    ///интерфейс для абстрактной фабрики
    ///</summary>
    public interface IServiceFactory
    {
        IEquationResolver CreateEquationResolver(DirectoryInfo directory);
    }
}
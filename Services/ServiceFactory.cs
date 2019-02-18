using System;
using System.IO;
using PerehodkoEA_Lab6.Interfaces;

namespace PerehodkoEA_Lab6.Services
{
    ///<summary>
    ///класс, реализующий абстрактную фабрику
    ///</summary>
    public class ServiceFactory : IServiceFactory
    {

        public IEquationResolver CreateEquationResolver(DirectoryInfo directory)
        {
            return new EquationResolver(){Directory= directory};
        }
    }
} 
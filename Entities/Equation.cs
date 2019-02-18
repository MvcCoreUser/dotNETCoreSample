using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PerehodkoEA_Lab6.Entities
{
    ///<summary>
    /// Сущность квадратное уравнение
    ///</summary>
    public class Equation
    {
        ///<summary>
        ///Коэффициент А
        ///</summary>
        [XmlAttribute] 
        public double A { get; set; } 

        ///<summary>
        ///Коэффициент В
        ///</summary>
        [XmlAttribute] 
        public double B { get; set; } 

        ///<summary>
        ///Коэффициент С
        ///</summary>
        [XmlAttribute] 
        public double C { get; set; } 

        ///<summary>
        ///статический метод, создает объект квадратное уравнение
        ///</summary>
        ///<param name="elem">
        ///узел в файле исходных данных в формате XML, представляющий уравнение 
        ///</param>
        public static Equation ParseFromXElement(XElement elem)
        {
            //считываем значение коэффициента A из аттрибута и пытаемся преобразоовать в число 
            string aStr= elem.Attribute(nameof(A)).Value;
            double aVal;
            if (!double.TryParse(aStr, out aVal))
            {
                throw new Exception("Неправильное значение для коэффициента A");
            }
            //аналогично делаем для коэффициента B
            string bStr= elem.Attribute(nameof(B)).Value;
            double bVal;
            if (!double.TryParse(aStr, out bVal))
            {
                throw new Exception("Неправильное значение для коэффициента B");
            }
            //аналогично делаем для коэффициента C
            string cStr= elem.Attribute(nameof(C)).Value;
            double cVal;
            if (!double.TryParse(cStr, out cVal))
            {
                throw new Exception("Неправильное значение для коэффициента C");
            }

            return new Equation(){A=aVal, B=bVal, C=cVal};

        }

        public override string ToString()
        => $"{A}x*x"+ (B.ToString().StartsWith('-')? $"{B}x" : $"+{B}x")+(C.ToString().StartsWith('-')? $"{C}" : $"+{C}");
    } 

}
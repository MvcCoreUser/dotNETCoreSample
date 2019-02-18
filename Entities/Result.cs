using System;
using System.Xml.Serialization;

namespace PerehodkoEA_Lab6.Entities
{
    ///<summary>
    ///перечисление, указывает на наличие корней в решении квадратного уравнения
    ///</summary>
    public enum ResultType 
    { 
        ///<summary>
        ///нет корней
        ///</summary>
        [XmlEnum("NoRoots")] 
        NoRoots,

        ///<summary>
        ///один корень
        ///</summary>
        [XmlEnum("OneRoot")] 
        OneRoot, 

         ///<summary>
        ///два корня
        ///</summary>
        [XmlEnum("TwoRoots")] 
        TwoRoots
       
    } 
 
 ///<summary>
/// Сущность решение квадратного уравенения
///</summary>
public class Result 
{ 
    ///<summary>
    /// Ссылка на квадратное уравнение
    ///</summary>  
  [XmlElement] 
  public Equation Equation { get; set; } 

  ///<summary>
  /// первый корень квадратного уравнения
  ///</summary>  
  [XmlAttribute] 
  public double X1 { get; set; } 

  ///<summary>
  /// второй корень квадратного уравнения
  ///</summary>  
  [XmlAttribute] 
  public double X2 { get; set; } 

  ///<summary>
  /// наличие корней в квадратном уравнении
  ///</summary> 
  [XmlAttribute] 
  public ResultType ResultType { get; set; } 

  public Result()
  {
      
  }

    ///<summary>
    ///конструктор
    ///</summary>
    ///<param name="equation">
    ///объект уравнение
    ///</param>
  public Result(Equation equation)
  {
      //вычисляем дискриминант
      double D= equation.B*equation.B-4*equation.A*equation.C;

      //определяем наличие корней
      int compare= Convert.ToInt32(Math.Abs(D)==D)+Convert.ToInt32(D>0);
      ResultType= (ResultType)compare;

      //вычисляем корни уравнения
      switch (ResultType)
      {
          case ResultType.NoRoots:
              X1=X2= double.PositiveInfinity;
          break;
          case ResultType.OneRoot:
              X1=X2=-equation.B/(2*equation.A);
          break;
          case ResultType.TwoRoots:
             X1=(-equation.B+Math.Sqrt(D))/(2*equation.A);
             X2=(-equation.B-Math.Sqrt(D))/(2*equation.A);
          break;
      }
      //инициализируем ссылку на квадратное уравнения, связанное с данным решением
      Equation=equation;
  }
} 

}
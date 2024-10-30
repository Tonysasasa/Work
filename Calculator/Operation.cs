using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Calculator
{
    public enum Operator
    {
        Plus,
        Subtraction,
        Multiplication,
        Division,
    }

    // As per requirement, create four interface methods to showcase the different Math’s operations
    // Another approach is to initialise one interface but specify four different classes (+,-,x,/) to implement. 
    public interface IMathOperation
    {
        double Add(double a, double b);
        double Minus(double a, double b);
        double Multiply(double a, double b);
        double Divide(double a, double b);
    }

    [XmlRoot("MyMaths")]
    public class MyMaths
    {
        [XmlElement("MyOperation")]
        public Operation Operation { get; set; }
    }

    // This class implements the interface and initialise the values and operational symbols
    public class Operation : IMathOperation
    {
        public double Add(double a, double b) => a + b;
          
        public double Minus(double a, double b) => a - b;
     
        public double Multiply(double a, double b) => a * b;

        public double Divide(double a, double b) {
            if (b == 0)
                throw new DivideByZeroException("Exception! Cannot divide by zero.");
            return a / b;
        }

        // Constructors
        [XmlElement("Value")]
        public List<double> Value { get; set; }

        // Use XmlAttribute here since ID is an attribute
        [XmlAttribute(AttributeName = "ID")]
        public Operator ID { get; set; }

        // Use XmlAttribute here since ID is an attribute
        [XmlElement("MyOperation")]
        public Operation MyOperation { get; set; }

    }
}

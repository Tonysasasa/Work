using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using Calculator;
using System;

namespace TestProject
{
    public class Tests
    {     
        [SetUp]
        public void Setup()
        {
        }

        // Basic testings
        [Test]
        public void TestAddition()
        {
            var operation = new Operation
            {
                ID = Operator.Plus,
                Value = new List<double> { 2, 3 }
            };
            double result = CalcMath.Calculate(operation);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void TestMultiplication()
        {
            var operation = new Operation
            {
                ID = Operator.Multiplication,
                Value = new List<double> { 4, 5 }
            };
            double result = CalcMath.Calculate(operation);
            Assert.AreEqual(20, result);
        }

        [Test]
        public void TestDivision()
        {
            var operation = new Operation
            {
                ID = Operator.Division,
                Value = new List<double> { 5, 2.5 }
            };
            double result = CalcMath.Calculate(operation);
            Assert.AreEqual(2, result);
        }
        [Test]
        public void TestSubtraction()
        {
            var operation = new Operation
            {
                ID = Operator.Subtraction,
                Value = new List<double> { 5, 10 }
            };
            double result = CalcMath.Calculate(operation);
            Assert.AreEqual(-5, result);
        }

        [Test]
        public void TestDivisionByZero()
        {
            var operation = new Operation
            {
                ID = Operator.Division,
                Value = new List<double> { 5, 0 }
            };
            Assert.Throws<DivideByZeroException>(() => CalcMath.Calculate(operation));
        }

        // XML testings

        [Test]
        public void TestComplexOperation()
        {
            string xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        <MyMaths>
            <MyOperation ID='Plus'>
                <Value>2</Value>
                <Value>3</Value>
                <MyOperation ID='Multiplication'>
                    <Value>4</Value>
                    <Value>5</Value>
                </MyOperation>
            </MyOperation>
        </MyMaths>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            double result = XMLhelper.ParseXml(xmlDoc.DocumentElement);
            Assert.AreEqual(25, result); // 2 + 3 + (4 * 5)
        }

        [Test]
        public void TestComplexOperation1()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Test.xml");

            double result = XMLhelper.ParseXml(xmlDoc.DocumentElement);
            Assert.AreEqual(45, result); 
        }

        [Test]
        public void TestComplexOperation2()
        {
            string xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        <MyMaths>
            <MyOperation ID='Plus'>
                <Value>A</Value>
                <Value>3</Value>
                <MyOperation ID='Multiplication'>
                    <Value>4</Value>
                    <Value>5</Value>
                </MyOperation>
            </MyOperation>
        </MyMaths>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            Assert.Throws<Exception>(() => XMLhelper.ParseXml(xmlDoc.DocumentElement));
        }

        [Test]
        public void TestComplexOperation3()
        {
            string xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        <MyMaths>
            <MyOperation ID='Plus'>
                <Value>1</Value>
            </MyOperation>
        </MyMaths>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            double result = XMLhelper.ParseXml(xmlDoc.DocumentElement);
            Assert.AreEqual(1, result);
        }
        [Test]
        public void TestComplexOperation4()
        {
            string xmlString = @"<?xml version='1.0' encoding='UTF-8'?>
        <MyMaths>
            <MyOperation ID='Plus'>
                <Value></Value>
            </MyOperation>
        </MyMaths>";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            Assert.Throws<Exception>(() => XMLhelper.ParseXml(xmlDoc.DocumentElement));

        }
        [Test]
        public void TestComplexOperation5()
        {
            XmlDocument xmlDoc = new XmlDocument();
            double result;

            // Test: Serialize operation to XML then deserialize back to XML and calculate
            XmlFunction httpfunction = new XmlFunction();

            Operation operation = new Operation
            {
                ID = Operator.Plus,
                Value = new List<double> { 2, 3 },
                MyOperation = new Operation
                {
                    ID = Operator.Multiplication,
                    Value = new List<double> { 4, 5 },
                    MyOperation = new Operation
                    {
                        ID = Operator.Division,
                        Value = new List<double> { 4, 2 }
                    }
                }
            };

            MyMaths myMaths = new MyMaths { Operation = operation };
            string serializedXml = httpfunction.SerializeOperation(myMaths);

            xmlDoc.LoadXml(serializedXml);
            result = XMLhelper.ParseXml(xmlDoc.DocumentElement);

            Assert.AreEqual(45, result);

        }
        [Test]
        public void TestComplexOperation6()
        {
            XmlFunction httpfunction = new XmlFunction();
            MyMaths mymaths = new MyMaths();
            mymaths = httpfunction.DeserializeObject1("Test.xml");

            double result = CalcMath.Calculate(mymaths.Operation);

            Assert.AreEqual(45, result);

        }

        [Test]
        public void TestComplexOperation7()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"Test1.xml");

            double result = XMLhelper.ParseXml(xmlDoc.DocumentElement);
            result = Math.Round(result, 4);
            Assert.AreEqual(0.4571, result);
        }
    }
}
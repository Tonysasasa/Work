//#define debug

using System;
using System.Collections.Generic;
using System.Xml;

namespace Calculator
{

    public class XMLhelper
    {
        /// <summary>
        /// This method aims to extract the data node from XML document, calculate and return the result
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static double ParseXml(XmlNode node)
        {
            Operator op = Operator.Plus; //default 
#if debug
            // For debug
            Console.WriteLine(node.Name);
#endif
            if (node.Name == "Value")
            {
                try
                {
                    double cur_val = double.Parse(node.InnerText);
                    return cur_val;
                }
                catch
                {
                    throw new Exception("Invalid number in node: " + node.InnerText.ToString());
                }
            }
            else if (node.Name == "MyOperation")
            {
                string operationId = node.Attributes["ID"].Value;
                switch (operationId)
                {
                    case "Plus":
                        op = Operator.Plus;
                        break;
                    case "Subtraction":
                        op = Operator.Subtraction;
                        break;
                    case "Multiplication":
                        op = Operator.Multiplication;
                        break;
                    case "Division":
                        op = Operator.Division;
                        break;
                    default:
                        throw new Exception("Invalid mathematical operation: " + operationId);
                }
            }
#if debug
            else
            {
                Console.WriteLine(node.Name);
            }
#endif

            // Recursivly call the parse method until hit the last node, do the calculation and return
            // the answer back to the previous level. 
            List<double> values = new List<double>();
            foreach (XmlNode child in node.ChildNodes)
            {
                values.Add(ParseXml(child));
#if debug
                Console.WriteLine(string.Join(",", values));
#endif
            }

            Operation operation = new Operation { ID = op, Value = values };
            double result = CalcMath.Calculate(operation);

            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Xml;

namespace Calculator
{
    public class CalcMath
    {
        /// <summary>
        /// This function only performs the calculation
        /// </summary>
        /// <param name="operation"> Passed-in parameter contains values(List) and ID(Operator enum)</param>
        /// <returns> return a double result </returns>
        public static double Calculate(Operation operation)
        {
            
            if (operation.Value.Count == 0)
            {
                throw new Exception("No value entered.");
            }
            
            // Do calculation at this level only
            double result = operation.Value[0];


            // Do a recusice calculation if it has sub-levels, and return back from each level. 
            // This part is not executed in parseXML since in parseXML it already calls recursivly from base
            // If there is a nested operation, calculate its result first
            if (operation.MyOperation != null)
            {
                double nestedResult = Calculate(operation.MyOperation);

                // Operation between the current result and the nested result
                switch (operation.ID)
                {
                    case Operator.Plus:
                        result = operation.Add(result, nestedResult);
                        break;
                    case Operator.Subtraction:
                        result = operation.Minus(result, nestedResult);
                        break;
                    case Operator.Multiplication:
                        result = operation.Multiply(result, nestedResult);
                        break;
                    case Operator.Division:
                        // Ensure the correct order of division
                        result = operation.Divide(nestedResult, result);
                        break;
                }
            }

            // Apply the operation to the remaining values in the list
            for (int i = 1; i < operation.Value.Count; i++)
            {
                switch (operation.ID)
                {
                    case Operator.Plus:
                        result = operation.Add(result, operation.Value[i]);
                        break;
                    case Operator.Subtraction:
                        result = operation.Minus(result, operation.Value[i]);
                        break;
                    case Operator.Multiplication:
                        result = operation.Multiply(result, operation.Value[i]);
                        break;
                    case Operator.Division:
                        result = operation.Divide(result, operation.Value[i]);
                        break;
                    default:
                        throw new Exception("Invalid mathematical operation: " + operation.ID.ToString());
                }
            }

            return result;
        }

        // Start here
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}



using System;
using System.IO; // to write a log
using System.Diagnostics; // to write a log
using Newtonsoft.Json;// cannot use in replit

namespace Calculator
{
class Calculator
{

    ///Calculator 생성자 로그 버전
    /*
    public Calculator(){
      StreamWriter logFile = File.CreateText("calculator.log");
      Trace.Listeners.Add(new TextWriterTraceListener(logFile));
      Trace.AutoFlush = true;
      Trace.WriteLine("Starting Calculator Log");
      Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
    }
    */

    ///Json 파일 버전

    JsonWriter writer;

    public Calculator(){
      StreamWriter logFile = File.CreateText("calculatorlog.json");
      logFile.AutoFlush = true;
      writer = new JsonTextWriter(logFile);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartObject();
      writer.WritePropertyName("Operations");
      writer.WriteStartArray();
    }
    
    ///Log 버전
    /*
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                break;
            case "s":
                result = num1 - num2;
                Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                break;
            case "m":
                result = num1 * num2;
                Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }
*/

  ///Json 버전
  public double DoOperation(double num1, double num2, string op)
     {
         double result = double.NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.
         writer.WriteStartObject();
         writer.WritePropertyName("Operand1");
         writer.WriteValue(num1);
         writer.WritePropertyName("Operand2");
         writer.WriteValue(num2);
         writer.WritePropertyName("Operation");
         // Use a switch statement to do the math.
         switch (op)
         {
             case "a":
                 result = num1 + num2;
                 writer.WriteValue("Add");
                 break;
             case "s":
                 result = num1 - num2;
                 writer.WriteValue("Subtract");
                 break;
             case "m":
                 result = num1 * num2;
                 writer.WriteValue("Multiply");
                 break;
             case "d":
                 // Ask the user to enter a non-zero divisor.
                 if (num2 != 0)
                 {
                     result = num1 / num2;
                     writer.WriteValue("Divide");
                 }
                 break;
             // Return text for an incorrect option entry.
             default:
                 break;
         }
         writer.WritePropertyName("Result");
         writer.WriteValue(result);
         writer.WriteEndObject();

         return result;
     }
}


class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        // And call to close the JSON writer before return
         calculator.Finish();
        return;
    }


    public void Finish()
 {
     writer.WriteEndArray();
     writer.WriteEndObject();
     writer.Close();
 }
}
}
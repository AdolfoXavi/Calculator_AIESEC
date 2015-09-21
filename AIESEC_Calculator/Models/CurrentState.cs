using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AIESEC_Calculator.Models
{
   public class CurrentState
   {
      // Actual result in the calculator
      private double Result { get; set; }
      // Number (without sign) in the display
      public string ActualNumber { get; set; }
      // Sign of the number in the display
      public string ActualNumberCurrentSign { get; set; }
      // Last key pressed
      private string LastKey { get; set; }
      // Last operation (+, -, /, *) pressed
      private string LastOperation { get; set; }

      /// <summary>
      /// This object help us to keep all the values in the calculator.
      /// 
      ///  - The result.
      ///  - The selected operation.
      ///  - The number that the user is writting.
      /// </summary>
      public CurrentState()
      {
         Init();
      }

      /// <summary>
      /// Initial State
      /// </summary>
      private void Init()
      {
         this.Result = 0;
         this.ActualNumber = "0";
         this.LastKey = "0";
         this.ActualNumberCurrentSign = "+";
         this.LastOperation = "";
      }

      /// <summary>
      /// Validates if the digit is [0-9] or [.], in that case returns true.
      /// </summary>
      /// <param name="s"></param>
      private bool IsDigit(string s)
      {
         if (s.Equals("0") || s.Equals("1") || s.Equals("2") || s.Equals("3") || s.Equals("4") || s.Equals("5") || s.Equals("6") || s.Equals("7") || s.Equals("8") || s.Equals("9") || s.Equals("."))
            return true;
         return false;
      }

      /// <summary>
      /// Add a Digit to the current number
      /// </summary>
      /// <param name="s"></param>
      private void ProcessDigit(string s)
      {
         if (s.Equals("0") && ActualNumber.Equals("0"))
            return;
         if (s.Equals(".") && ActualNumber.Contains("."))
            return;
         if (s.Equals(".") && ActualNumber.Equals("0"))
         {
            ActualNumber += s;
            return;
         }
         if (ActualNumber.Equals("0"))
         {
            ActualNumber = s;
            return;
         }

         ActualNumber += s;
      }

      /// <summary>
      /// Delete the last pressed key, it will delete also the [-] sign if there's only one digit in the ActualNumber
      /// </summary>
      private void DeleteLastKey()
      {
         if (ActualNumber.Length > 1)
            ActualNumber = ActualNumber.Substring(0, ActualNumber.Length - 1);
         else
         {
            ActualNumber = "0";
            ActualNumberCurrentSign = "+";
         }
      }

      /// <summary>
      /// Function for show the [-] sign and for make a substraction
      /// </summary>
      private void MakeSubstraction()
      {
         if (IsDigit(LastKey) || LastKey.Equals("+") ||  LastKey.Equals("*") ||  LastKey.Equals("/"))
         {
            Result = Double.Parse(ActualNumber);

            if (ActualNumberCurrentSign == "-")
               Result *= -1;

            ActualNumber = "0";
         }

         ActualNumberCurrentSign = "-";
         LastOperation = "-";
      }

      /// <summary>
      /// Process a key.
      /// </summary>
      /// <param name="s">Key text</param>
      /// <returns></returns>
      public bool ProcessKey(string s)
      {
         if (s.Equals("CE"))
         {
            Init();
            return true;
         }
         else if (s.Equals("C"))
         {
            LastKey = "0";
            ActualNumber = "0";
            ActualNumberCurrentSign = "+";
            return true;
         }
         else if (s.Equals("←"))
         {
            DeleteLastKey();
            return true;
         }
         else if (s.Equals("-"))
         {
            MakeSubstraction();            
            return true;
         }

         if (IsDigit(s))
         {
            if (!IsDigit(LastKey))
            {
               ActualNumber = "0";
               ActualNumberCurrentSign = "+";
            }

            ProcessDigit(s);            
            LastKey = s;
         }
         else if(s.Equals("/") || s.Equals("*") || s.Equals("+"))
         {
            if (LastOperation == "")
            {
               Result = Double.Parse(ActualNumber);

               if (ActualNumberCurrentSign == "-")
                  Result *= -1;
            }
            else if(IsDigit(LastKey))
            {
               switch (LastOperation)
               {
                  case "/": Result /= Double.Parse(ActualNumber); break;
                  case "*": Result *= Double.Parse(ActualNumber); break;
                  case "+": Result += Double.Parse(ActualNumber); break;
                  case "-": Result -= Double.Parse(ActualNumber); break;
               }

               if (Result < 0)
                  ActualNumberCurrentSign = "-";
               else
                  ActualNumberCurrentSign = "+";

               // For show numbers without scientific notation
               ActualNumber = (Math.Abs(Result)).ToString("#####################################################################################################################################################################################################################################################################################################################################.#####################################################################################################################################################################################################################################################################################################################################");
            }
            
            LastOperation = s;
            LastKey = s;
         }
         else if (s.Equals("=") && IsDigit(LastKey))
         {
            switch (LastOperation)
            {
               case "/": Result /= Double.Parse(ActualNumber); break;
               case "*": Result *= Double.Parse(ActualNumber); break;
               case "+": Result += Double.Parse(ActualNumber); break;
               case "-": Result -= Double.Parse(ActualNumber); break;
               case "": 
                  Result = Double.Parse(ActualNumber);
                  if (ActualNumberCurrentSign == "-")
                     Result *= -1;
                  break;
            }

            if (Result < 0)
               ActualNumberCurrentSign = "-";
            else
               ActualNumberCurrentSign = "+";

            // For show numbers without scientific notation
            ActualNumber = (Math.Abs(Result)).ToString("#####################################################################################################################################################################################################################################################################################################################################.#####################################################################################################################################################################################################################################################################################################################################");
            LastOperation = "";
         }
      
         return true; 
      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Numerics;
using AIESEC_Calculator.Models;

namespace AIESEC_Calculator
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      CurrentState cs;

      public MainWindow()
      {
         InitializeComponent();
         cs = new CurrentState();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         string s = ((Button)sender).Content.ToString();

         cs.ProcessKey(s);

         if(cs.ActualNumberCurrentSign.Equals("-"))
            ActualNumberTextBlock.Text = "-" + cs.ActualNumber;
         else
            ActualNumberTextBlock.Text = cs.ActualNumber;
      }
   }
}

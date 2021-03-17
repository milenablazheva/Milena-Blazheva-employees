using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_Team_Longest_Period
{
    class Employee
    {
        private int difference;
        public Employee(int EmpID1, int ProjectID1, DateTime DateFrom1, DateTime DateTo1)
        {
            EmpID = EmpID1;
            ProjectID = ProjectID1;
            DateFrom = DateFrom1;
            DateTo = DateTo1;
        }
        
        public int EmpID { get; set; }
        public int ProjectID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public int Difference { 
             get {  TimeSpan diffResult = DateTo.Subtract(DateFrom);
                return difference = diffResult.Days; ; }
           
        }

        public override string ToString()
       {
           StringBuilder sb = new StringBuilder();
     
           sb.AppendLine($"{EmpID}");
           sb.AppendLine($"{ProjectID}");
           sb.AppendLine($"{DateFrom.ToString("yyyy-MM-dd")}");
           sb.AppendLine($"{DateTo.ToString("yyyy-MM-dd")}");
     
           return sb.ToString();
       }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Assignment_Team_Longest_Period
{
    class Program
    {
        static void Main(string[] args)
        {
            //Тhe solution is presented by finding the two workers who have been working on a common project for the longest time.

            List<Employee> workList = new List<Employee>(1);

            Console.WriteLine("Put your path to the .txt file to read the data");

            string input = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(input);

            foreach (var line in lines)
            {
                string[] formats = { "yyyy-MM-dd" };
                var dataLine = line.Split(", ").ToList();

                int employeeId = int.Parse(dataLine[0]);
                int projectId = int.Parse(dataLine[1]);
                var dateFrom = DateTime.ParseExact(dataLine[2], formats, new CultureInfo("en-US"), DateTimeStyles.None);

                DateTime dateTo;
                if (dataLine[3] != "NULL")
                {
                    dateTo = DateTime.ParseExact(dataLine[3], formats, new CultureInfo("en-US"));
                }
                else
                {
                    dateTo = DateTime.Now;
                }


                Employee currentEmployee = new Employee(employeeId, projectId, dateFrom, dateTo);
                workList.Add(currentEmployee);
            }

            int recordDays = 0;
            TimeSpan res = TimeSpan.Zero;

            for (int i = 0; i < workList.Count - 1; i++)
            {
                for (int j = i + 1; j <= workList.Count - 1; j++)
                {
                    int empIId = workList[i].EmpID;
                    int empJId = workList[j].EmpID;
                    int currentDays = 0;

                    if (i < j && empIId != empJId)
                    {
                        int empIProject = workList[i].ProjectID;
                        int empJProject = workList[j].ProjectID;

                        var resultFrom = DateTime.Compare(workList[i].DateFrom, workList[j].DateFrom);
                        var resultTo = DateTime.Compare(workList[i].DateTo, workList[j].DateTo);

                        if (empIProject == empJProject)
                        {
                            if (resultFrom == 1)
                            {
                                if (workList[j].DateTo >= workList[i].DateFrom)
                                { //Match
                                    if (workList[i].DateTo >= workList[j].DateTo)
                                    {
                                        res = workList[j].DateTo.Subtract(workList[i].DateFrom);
                                        currentDays += res.Days + 1;

                                    }
                                    else
                                    {
                                        res = workList[i].DateTo.Subtract(workList[i].DateFrom);
                                        currentDays += res.Days + 1;

                                    }
                                }
                            }
                            else if (resultFrom == -1)
                            {
                                if (workList[i].DateTo >= workList[j].DateFrom)
                                {   //Match
                                    if (workList[j].DateTo >= workList[i].DateTo)
                                    {
                                        res = workList[i].DateTo.Subtract(workList[j].DateFrom);
                                        currentDays += res.Days + 1;
                                    }
                                    else
                                    {
                                        res = workList[j].DateTo.Subtract(workList[j].DateFrom);
                                        currentDays += res.Days + 1;
                                    }
                                }
                                else if (resultFrom == 0)
                                {
                                    res = workList[i].DateTo.Subtract(workList[i].DateFrom);
                                    currentDays += res.Days + 1;
                                }

                                if (currentDays > recordDays)
                                {
                                    recordDays = currentDays;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"The longest period of time in which two employees have worked together on a project is {recordDays} days.");
        }
    }
}


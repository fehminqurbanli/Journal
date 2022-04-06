using Journal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Journal
{
    class Program
    {
        public static bool returnChecker;
        public static void Run()
        {
            var context = new EfCoreDbContext();

            Console.WriteLine("Enter subject's name...");
            string subject = Console.ReadLine();
            while (!subject.All(Char.IsLetter))
            {
                Console.WriteLine("You must enter alphabetic digits");
                Console.WriteLine("Enter subject's name correctly...");
                subject = Console.ReadLine();
                if (subject.All(Char.IsLetter))
                {
                    break;
                }

            }

            bool checkSubjectExistingInDb = context.Subjects.ToList().Exists(a => a.SubjectName == subject);
            while (checkSubjectExistingInDb)
            {
                Console.WriteLine("This subject already exists, enter another subject name...");
                Console.WriteLine("Enter subject's name...");
                subject = Console.ReadLine();
                checkSubjectExistingInDb = context.Subjects.ToList().Exists(a => a.SubjectName == subject);

                if (!checkSubjectExistingInDb)
                {
                    break;
                }
            }

            var subjectModel = new Subject()
            {
                SubjectName = subject
            };
            context.Subjects.Add(subjectModel);
            context.SaveChanges();



            bool flag = true;
            while (true)
            {

                Console.WriteLine("Will you enter another subject ? Yes(1), No(0)");
                int integerToBoolean = Convert.ToInt32(Console.ReadLine());
                while (true)
                {
                    if (integerToBoolean != 1 && integerToBoolean != 0)
                    {
                        Console.WriteLine("You didn't enter correct number, please try again");
                        Console.WriteLine("Will you enter another subject ? Yes(1), No(0)");
                        integerToBoolean = Convert.ToInt32(Console.ReadLine());
                    }
                    else
                    {
                        flag = Convert.ToBoolean(integerToBoolean);

                        break;
                    }
                }


                if (flag == true)
                {
                    Console.WriteLine("Enter another subject :");
                    subject = Console.ReadLine();
                    while (!subject.All(Char.IsLetter))
                    {
                        Console.WriteLine("You must enter alphabetic digits");
                        Console.WriteLine("Enter subject's name correctly...");
                        subject = Console.ReadLine();
                        if (subject.All(Char.IsLetter))
                        {
                            break;
                        }

                    }
                    checkSubjectExistingInDb = context.Subjects.ToList().Exists(a => a.SubjectName == subject);
                    while (checkSubjectExistingInDb)
                    {
                        Console.WriteLine("This subject already exists, enter another subject name...");
                        Console.WriteLine("Enter subject's name...");
                        subject = Console.ReadLine();
                        checkSubjectExistingInDb = context.Subjects.ToList().Exists(a => a.SubjectName == subject);

                        if (!checkSubjectExistingInDb)
                        {
                            break;
                        }
                    }

                    subjectModel = new Subject()
                    {
                        SubjectName = subject
                    };
                    context.Subjects.Add(subjectModel);
                    context.SaveChanges();
                }
                else
                {
                    break;
                }

            }

            Dictionary<string, int> myDict =
           new Dictionary<string, int>();
            for (int i = 0; i < context.Subjects.ToList().Count; i++)
            {
                string subjectNameInDictionary = context.Subjects.ToList().ElementAt(i).SubjectName;

                myDict.Add(subjectNameInDictionary, i + 1);
            }

            int count = context.Subjects.ToList().Count;

            Console.WriteLine("Subject name -- button code");
            foreach (KeyValuePair<string, int> item in myDict)
            {
                Console.WriteLine(item.Key + " -- " + item.Value);
            }


            Console.WriteLine();
            
            Console.WriteLine("Enter button code for subject to add points :");
            string buttonCodeWithString = Console.ReadLine();
            while (!(buttonCodeWithString.All(char.IsDigit) && (Convert.ToInt32(buttonCodeWithString) > 0 && Convert.ToInt32(buttonCodeWithString) <= count)))
            {
                Console.WriteLine("Enter correct button code");
                buttonCodeWithString = Console.ReadLine();

            }
            int buttonCode = Convert.ToInt32(buttonCodeWithString);

            int buttonCodeToDb = context.Subjects.ToList().ElementAt(buttonCode - 1).Id;


            Console.WriteLine("Enter subject's points: [0-100]");
            string numberWithString = Console.ReadLine();

            while (!(numberWithString.All(char.IsDigit) && Convert.ToInt32(numberWithString)>=0 && Convert.ToInt32(numberWithString)<=100))
            {
                Console.WriteLine("Enter correct point [0-100]...");
                numberWithString = Console.ReadLine();
            }
            int number = Convert.ToInt32(numberWithString);
            var pointModel = new Point()
            {
                StudentPoint = number,
                SubjectId = buttonCodeToDb
            };
            context.Points.Add(pointModel);
            context.SaveChanges();
            bool flagIdentifier = true;
            int integerToBool;
            while (flagIdentifier == true)
            {

                Console.WriteLine("Do you want to enter another point ? Yes(1) , No(0)");
                integerToBool = Convert.ToInt32(Console.ReadLine());

                while (true)
                {
                    if (integerToBool!=1&& integerToBool!=0)
                    {
                        Console.WriteLine("You didn't enter correct number, please try again");
                        Console.WriteLine("Will you enter another subject ? Yes(1), No(0)");
                        integerToBool = Convert.ToInt32(Console.ReadLine());
                    }
                    else
                    {
                        flagIdentifier = Convert.ToBoolean(integerToBool);

                        break;
                    }
                }
                if (flagIdentifier == true)
                {
                    Console.WriteLine("Enter subject's another point: ");
                    number = Convert.ToInt32(Console.ReadLine());
                    pointModel = new Point()
                    {
                        StudentPoint = number,
                        SubjectId = buttonCodeToDb
                    };
                    context.Points.Add(pointModel);
                    context.SaveChanges();
                }
                else
                {
                    break;
                }

            }



            Console.WriteLine("Subject name -- button code");
            foreach (KeyValuePair<string, int> item in myDict)
            {
                Console.WriteLine(item.Key + " -- " + item.Value);
            }
            Console.WriteLine();

            Console.WriteLine("Enter button code for subject to show its points :");
            buttonCodeWithString = Console.ReadLine();
            

            while (!(buttonCodeWithString.All(char.IsDigit)&& (Convert.ToInt32(buttonCodeWithString) > 0 && Convert.ToInt32(buttonCodeWithString) <= count)))
            {
                Console.WriteLine("Enter correct button code");
                buttonCodeWithString = Console.ReadLine();

                
            }
            buttonCode = Convert.ToInt32(buttonCodeWithString);



            List<int> array = new List<int>();
            buttonCodeToDb = context.Subjects.ToList().ElementAt(buttonCode - 1).Id;

            List<Point> points = context.Points.Where(a => a.SubjectId == buttonCodeToDb).ToList();

            int counter = 0;
            int sum = 0;
            int average = 0;
            foreach (var item in points)
            {

                array.Add(item.StudentPoint);
                counter += 1;
                sum += item.StudentPoint;
                average = sum / counter;
            }

            Console.WriteLine(context.Subjects.ToList().FirstOrDefault(a => a.Id == buttonCodeToDb).SubjectName + "'s maximum point is " +
                array.Max() + ", minimum point is " +
                array.Min() + " and average of subject's point is " + average);
            Console.WriteLine("Would you want to return back ? Yes(1), No(0)");
            returnChecker = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
        }
        static void Main(string[] args)
        {

            var context = new EfCoreDbContext();


            Console.WriteLine("What is your name ?");
            string askName = Console.ReadLine();

            while (!askName.All(Char.IsLetter))
            {
                Console.WriteLine("You must enter alphabetic digits");
                Console.WriteLine("Enter your name correctly...");
                askName = Console.ReadLine();
                if (askName.All(Char.IsLetter))
                {
                    break;
                }

            }

            Console.WriteLine("Hi, {0}. You are welcome...", askName);


            //-------

            //------------


            Run();
            while (true)
            {
                if (returnChecker)
                {
                    Run();
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Thank you");

            var deletedSubjects = context.Subjects.ToList();
            var deletedPoints = context.Points.ToList();
            context.Subjects.RemoveRange(deletedSubjects);
            context.Points.RemoveRange(deletedPoints);
            context.SaveChanges();
            Console.ReadKey();
        }
    }
}

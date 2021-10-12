using System;
using System.Collections.Generic;
using System.IO;

namespace Relatives
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string path = @"C:\\Users\\Nick Petrenko\\source\repos\\Relatives\\relatives.txt";
            PersonNode rootNode; 
            List<Exception> exceptions = new List<Exception>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line = sr.ReadLine();
                Console.WriteLine(line);
                rootNode = new PersonNode(new Person(line), null);
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        PersonNode.AddChild(rootNode, new Person(line));
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                    Console.WriteLine(line);
                }

            }

            var returnRequest = ReturnRequest();
            var nodePerson  = PersonNode.FindPersonNode(rootNode, returnRequest.Item1);

            if (nodePerson == null) Console.WriteLine("Человек не найден");
            else if (nodePerson.person.relationDegree < returnRequest.Item3) Console.WriteLine("Неправильный ввод степени родства");
            else
            {
                var result = PersonNode.FindPersons(nodePerson, returnRequest.Item2, returnRequest.Item3);

                foreach (var i in result)
                {
                    Console.WriteLine(i.fullName);
                }
                foreach (var i in exceptions)
                {
                    Console.WriteLine("Error: " + i.Message);
                }
            }
            Console.ReadKey();
            
        }
        //Returns information from the query 
        public static (string,char,int) ReturnRequest()
        {
            Console.Write("Запрос: ");
            string line = Console.ReadLine();
            line = line.Replace(" тип родства ","");
            line = line.Replace(" степень родства ", "");
            var itemsRequest = line.Split(',');
            return (itemsRequest[0], itemsRequest[1].ToCharArray()[0], Int32.Parse(itemsRequest[2]));
        }
    }
}

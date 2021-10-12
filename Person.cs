using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatives
{
    enum Sex
    {
        M,
        W
    }

    class Person
    {
        public String fullName { get; set; }
        public Sex sex { get; set; }
        private int yearOfBirth;
        public int YearOfBirth {
            get
            {
                return yearOfBirth;
            }
            set {
            if ((value > 999) && (value < 10000))
                {
                    yearOfBirth = value;
                }
            }
        }

        public int relationDegree { get; set; }


        public Person(string fullName, Sex sex, int age,int relationDegree)
        {
            this.fullName = fullName;
            this.sex = sex;
            this.YearOfBirth = age;
            this.relationDegree = relationDegree;
        }

        public Person(string line)
        {
            int count = 0;
            Sex sex;

            while (line[count] == '\t')
            {
                count++;
            }

            line = line.Remove(0, count);
            string[] stringElements = line.Split("|");
            Enum.TryParse(stringElements[1], out sex);

            this.fullName = stringElements[0];
            this.sex = sex;
            this.yearOfBirth = Int32.Parse(stringElements[2]);
            this.relationDegree = count;
            
        }

  
    }
}

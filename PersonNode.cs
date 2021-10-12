using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatives
{
    class PersonNode
    {
        public PersonNode parent;
        public Person person;
        public List<PersonNode> children;

        public PersonNode(Person person,PersonNode parent)
        {
            this.person = person;
            this.parent = parent;
            this.children = new List<PersonNode>();
        }

        
        public static void AddChild(PersonNode personNode, Person person)
        {
            if ((person.relationDegree - personNode.person.relationDegree) == 1)
            {
                if(person.YearOfBirth>personNode.person.YearOfBirth) personNode.children.Add(new PersonNode(person,personNode));
                else
                {
                    person.YearOfBirth = personNode.person.YearOfBirth + 1;
                    personNode.children.Add(new PersonNode(person, personNode));
                    throw new Exception($"{person.fullName} не мог родиться раньше своего отца {personNode.person.fullName}");
                }
            }
            else if ((person.relationDegree - personNode.person.relationDegree) > 1)
            {
                int count = personNode.children.Count - 1;
                AddChild(personNode.children[count], person);
            }
        }
        //Search for a person in the tree by full name
        public static  PersonNode FindPersonNode( PersonNode humanNode, string fullName)
        {
            PersonNode a = null;
            if (humanNode.person.fullName == fullName) return humanNode;

            foreach (var i in humanNode.children)
            {
                
                var b = FindPersonNode(i, fullName);
                if (b != null) a = b;
            }
            return a;
           
        }
        //Search for all relatives by relationship and gender
        public static List<Person> FindPersons(PersonNode personNode, char typeOfRelationship, int relationDegree)
        {
            PersonNode parent = personNode;
            PersonNode blockedNode = null; //Node that will not be searched for

            Sex sex;
            for (int i = relationDegree; i > 0; i--)
            {
                
                if (i == 1) blockedNode = parent;
                parent = parent.parent;
            }

            List<Person> person = new List<Person>();

            foreach(var i in parent.children)
            {
                if (i == blockedNode) continue;
                if (relationDegree > 1)
                {
                    person = person.Concat(FindAllPersons(i, --relationDegree)).ToList<Person>();
                }
                else
                {
                    person.Add(i.person);

                }
            }

            if (typeOfRelationship == 'Б') sex = Sex.M;
            else sex = Sex.W;

            person = person.Where<Person>(i=>i.sex==sex).ToList();
            return person;

        }
        //Search for all people at a specified depth
        public static List<Person> FindAllPersons(PersonNode personNode,int count)
        {
            List<Person> person = new List<Person>();
            if (count == 1)
            {
                foreach(var i in personNode.children)
                {
                    person.Add(i.person);
                }
            }
            else
            {
                count--;
                foreach (var i in personNode.children)
                {
                    person =  person.Concat(FindAllPersons(i, count)).ToList<Person>();
                }
                
            }
            return person;
        }
    }
}

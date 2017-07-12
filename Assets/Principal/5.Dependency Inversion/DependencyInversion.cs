using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Principal
{
    public class RelationshipClass
    {
        public Person Person1;
        public RelationshipType Relation;
        public Person Person2;

        public RelationshipClass()
        {

        }

        public RelationshipClass(Person person1, RelationshipType relation, Person person2)
        {
            Person1 = person1;
            Relation = relation;
            Person2 = person2;
        }

    }


    public enum RelationshipType
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;

    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class RelationShip : IRelationshipBrowser //low level
    {
        //private List<Tuple<Person, RelationshipType, Person>> abc
        //= new List<Tuple<Person, RelationshipType, Person>>();

        //	public void AddParentAndChild(Person parent, Person child)
        //	{
        //		relations.Add(new Tuple<Person,RelationshipType,Person>(parent,RelationshipType.Parent,child));
        //		relations.Add(new Tuple<Person,RelationshipType,Person>(child, RelationshipType.child,parent));
        //	}

        //	public List<Tuple<Person,RelationshipType,Person>> Relations()
        //	{
        //		return relations;
        //	}

        private readonly List<RelationshipClass> _relations = new List<RelationshipClass>();



        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add(new RelationshipClass(parent, RelationshipType.Parent, child));
            _relations.Add(new RelationshipClass(child, RelationshipType.Child, parent));
        }

        public List<RelationshipClass> Relations()
        {
            return _relations;
        }


        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return _relations.Where
            (x => x.Person1.Name == name &&
                  x.Relation == RelationshipType.Parent).Select(r => r.Person2);
        }


    }


    public class Research
    {


        public Research(IRelationshipBrowser browser, string name)
        {
            foreach (var p in browser.FindAllChildrenOf(name))
            {
                Debug.Log(string.Format("{0} has a child call {1}", name, p.Name));
            }

        }
    }

    public class DependencyInversion : MonoBehaviour
    {


        void Start()
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Lala"};
            var child2 = new Person {Name = "lili"};

            var relationships = new RelationShip();

            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships, "John");
        }


    }

}
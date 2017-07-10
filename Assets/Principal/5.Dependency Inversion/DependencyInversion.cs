using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public enum RelationshipType
{
	Parent,
	child,
	Sibling
}

public class Person
{
	public string name;
}

public interface IRelationshipBrowser
{
	IEnumerable<Person> FindAllChildrenOf(string name);
}

public class RelationShip//low level
{
	private List<Tuple<Person,RelationshipType,Person>> relations 
	= new List<Tuple<Person, RelationshipType, Person>>();

	public void AddParentAndChild(Person parent, Person child)
	{
		relations.Add();
		relations.Add(child, RelationshipType.child, parent);
	}

	public List<Tuple<Person,RelationshipType,Person>> Relations()
	{
		return relations;
	}

	public IEnumerable<Person> FindAllChildrenOf(string name)
	{
		return relations.Where
			(x => x.Item1 == name &&
				x.Item2 == RelationshipType.Parent).Select(r =>r.Item3);
	}
}

public class DependencyInversion : MonoBehaviour 
{


	void Start ()
	{
		
	}
	

}

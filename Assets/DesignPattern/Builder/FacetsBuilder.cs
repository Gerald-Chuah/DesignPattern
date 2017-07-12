using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExtensionTool;

namespace DesignPattern
{
    public class Person
    {
        public string Address, PostCode, City;
        public string CompanyName, Position;
        public int Income;


        public override string ToString()
        {
            return string.Format(MemberInfoGetting.GetMemberName(()=>Address)+": {0}, "+
                                 MemberInfoGetting.GetMemberName(()=>PostCode) + ": {1}, " +
                                 MemberInfoGetting.GetMemberName(()=>City) + ": {2}, " +
                                 MemberInfoGetting.GetMemberName(()=>CompanyName) + ": {3}, " +
                                 MemberInfoGetting.GetMemberName(()=>Position) + ": {4}, " +
                                 MemberInfoGetting.GetMemberName(()=>Income) + ": {5}.",
                                 Address,PostCode,City,CompanyName,Position,Income);
            
        }
    }

    public class PersonBuilder
    {
        public Person Person = new Person();

        public AddressBuilder Lives
        {
            get
            {
                return new AddressBuilder(Person);
            }

        }

        public JobBuilder Works
        {
            get
            {
                return new JobBuilder(Person);
            }

        }

        //Pawa full OwO
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.Person;
        }
    }


    public class AddressBuilder : PersonBuilder
    {
        public AddressBuilder(Person person)
        {
            this.Person = person;
        }

        public AddressBuilder At(string address)
        {
            this.Person.Address = address;
            return this;
        }

        public AddressBuilder WithPostCode(string postcode)
        {
            this.Person.PostCode = postcode;

            return this;
        }

        public AddressBuilder In(string city)
        {
            this.Person.City = city;
            return this;
        }
    }

    public class JobBuilder : PersonBuilder
    {
        public JobBuilder(Person person)
        {
            this.Person = person;
        }

        public JobBuilder At(string companyName)
        {
            this.Person.CompanyName = companyName;
            return this;
        }

        public JobBuilder AsA(string position)
        {
            this.Person.Position = position;
            return this;
        }

        public JobBuilder Earn(int income)
        {
            this.Person.Income = income;
            return this;
        }

    }


    public class FacetsBuilder : MonoBehaviour
    {

        void Start()
        {
            PersonBuilder pb= new PersonBuilder();
            Person person = pb.Lives.At("taman").WithPostCode("12345").In("KL")
                              .Works.At("Roti Canai Kedai").AsA("tukang masak").Earn(1000);
           
            Debug.Log(person);
        }

    }

}

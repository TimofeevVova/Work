using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class OOP_Example
    {
        public OOP_Example()
        {
            Animal python = new Animal();  //Вызов Animal
            python.Type = "Питон";
            python.Height = 5;
            python.Weight = 2;
            python.ShowData();

            Animal dog = new Animal();//Вызов Animal
            dog.Type = "Пес";
            dog.Height = 25;
            dog.Weight = 13;
            dog.ShowData();

            Cat myCat = new Cat(); // Вызов Cat
            //Параметры Animal
            myCat.Type = "Кот";
            myCat.Height = 18;
            myCat.Weight = 8;
            //Параметры Cat
            myCat.Breed = "Персидская";

            myCat.ShowData();// метод Animal
            myCat.Meow();    // метод Cat
        }
    }

    public class Animal
    {
        private string type; // Закрытое поле
        private int height;  // Закрытое поле
        private int weight;  // Закрытое поле

        public string Type // Свойство для доступа к типу
        {
            get { return type; }
            set { type = value; }
        }
        public int Height // Свойство для доступа к росту
        {
            get { return height; }
            set { height = value; }
        }
        public int Weight // Свойство для доступа к весу
        {
            get { return weight; }
            set { weight = value; }
        }
        public void ShowData()
        {
            Console.WriteLine(type);
            Console.WriteLine("Рост - " + height + " Вес - " + weight);
        }
    }

    public class Cat : Animal
    {
        private string breed { get; set; }
        public string Breed // Свойство для доступа к типу
        {
            get { return breed; }
            set { breed = value; }
        }
        public void Meow()
        {
            Console.WriteLine("Мяу!");
        }
    }
}

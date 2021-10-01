namespace DataClasses {
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public Person() { }

        public Person(string name, string surname) : this(name, surname, 5)
        { }

        public Person(string name, string surname, int age)
        {
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
        }

        // derived getter
        public string GetFullName()
        {
            return this.Name + " " + this.Surname;
        }
    }
}
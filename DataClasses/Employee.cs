namespace DataClasses
{
    public class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public Employee() { }

        public Employee(string name, string surname) : this(name, surname, 5)
        { }

        public Employee(string name, string surname, int age)
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
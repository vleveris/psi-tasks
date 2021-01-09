namespace FileTask
{
    public class Person
    {
        public string Name
        { get; set; }
        public int Age
        { set; get; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}

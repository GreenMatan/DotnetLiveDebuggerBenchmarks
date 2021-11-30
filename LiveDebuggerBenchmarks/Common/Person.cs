namespace LiveDebugger.Common
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public static Person Create(string name, int age) => new Person { Name = name, Age = age };

        public override string ToString() => $"{Name}, {Age} y/o";
    }
}

namespace ProjectAdvanced
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            ItemAdvanced<string> item = new ItemAdvanced<string>();
            string[] strings = { "AAA","CCC", "MMM","LLL", "AApA", "CCpC", "MMpM", "LLpL", "AAA", "CCC", "MMM", "LLL", "AApA", "CCpC", "MMpM", "LLpL" };
    
          //  item[1] = "Ahmed";
            //item[2] = "Amr";
            //item[3] = "AAA";

            item.Add("Ali");
            item.Add("Ahmed");
            item.Add("Alaa");
            item.Add("Amr");
            item.Add("ABO");
            //item = new ItemAdvanced<string>(strings);
            // item.AddRange(strings);
            //  item.RemoveRange(2,3);
            //item.RemoveAt(1);

            // item.Insert(4, "Amer");
            item.InsertRange(2,strings);
            //object[] ss = item.ToArray();
            // item.RemoveAll();
            // item.Clear();
            //item.Reverse();
            item.Sort();
        

            List<string> newIndexesString = item["1,2,3"];

            ItemAdvanced<Person> person = new ItemAdvanced<Person>(new PersonEquality());
            person.Add(new Person() { Id = 1 });
            person.Add(new Person() { Id = 2 });
          
            person.Add(new Person() { Id = 4 });
            person.Add(new Person() { Id = 3 });


            //Console.WriteLine(person.IndexOf(new Person() { Id = 4 }));
            // person.Remove(new Person() { Id = 4 });
            person.Sort();


        }
    }
}

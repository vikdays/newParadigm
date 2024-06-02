namespace newParadigms
{
    public class Employee : Person
    {
        private string Position { get; set; }
        public Employee(string name, char gender, string position)
        {
            Name = name;
            Gender = gender;
            Position = position;
        }

        public void Edit(string newName, char newGender, string newPosition)
        {
            Name = newName;
            Gender = newGender;
            Position = newPosition;
        }

        public void ReplenishFood(IEnclosure enclosure, int amount)
        {
            var animal = enclosure.Animals[0];
            if (enclosure != null)
            {
                enclosure.ReplenishFood(amount);
                Console.WriteLine($"{Name} пополнил запасы еды в {enclosure.Name} едой {animal.FirstFoodType} и {animal.SecondFoodType}.");
            }
            else
            {
                Console.WriteLine("Invalid enclosure provided.");
            }
        }

        public string CheckStatus()
        {
            return $"Employee {Name}, Position: {Position}, Gender: {Gender}";
        }
    }
}

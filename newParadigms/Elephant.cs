namespace newParadigms
{
    class Elephant : Animal
    {
        public Elephant() : base("слон", 100, "Трррр", new Apple(50), new Banana(20))
        {
        }
        public override void Feed()
        {
            Console.WriteLine("Слон покормлен!");
        }
        public string CheckStatus()
        {
            return $"Животное вида Слон, уровень голода: {HungerLevel}, статус: {(HungerLevel <= 30 ? "Голоден" : "Сыт")}";
        }
        public override void MakeSound()
        {
            Console.WriteLine($"Слон идает свой звук: {Voice} !");
        }
        public override void FeedFromEnclosure(IEnclosure enclosure)
        {
            if (enclosure != null && enclosure.FoodSupply != 0)
            {
                enclosure.FeedAnimals();
                HungerLevel = 100; // Устанавливаем уровень голода обратно на максимальный после кормления
            }
            else
            {
                Console.WriteLine($"Животное {Species} не может поесть из этого вольера. Вольер пуст или закончилась еда.");
            }
        }

    }
}

namespace newParadigms
{
    class Wolf : Animal
    {
        public Wolf() : base("волк", 100, "Ауф", new Apple(10), new Meat(30))
        {
        }
        public override void Feed()
        {
            Console.WriteLine($"Волк покормлен!");
        }
        public string CheckStatus()
        {
            return $"Животное вида Волк, уровень голода: {HungerLevel}, статус: {(HungerLevel <= 50 ? "Голоден" : "Сыт")}";
        }
        public override void MakeSound()
        {
            Console.WriteLine($"Волк идает свой звук: {Voice} !");
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

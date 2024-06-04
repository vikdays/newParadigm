namespace newParadigms
{
    class Tiger : Animal
    {
        public Tiger() : base("тигр", 100, "Рррр", new Meat(30), new Fish(25))
        {
        }
        public override void Feed()
        {
            Console.WriteLine("Тигр покормлен!");
        }
        public string CheckStatus()
        {
            return $"Животное вида Тигр, уровень голода: {HungerLevel}, статус: {(HungerLevel <= 70 ? "Голоден" : "Сыт")}";
        }
        public override void MakeSound()
        {
            Console.WriteLine($"Тигр идает свой звук: {Voice} !");
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

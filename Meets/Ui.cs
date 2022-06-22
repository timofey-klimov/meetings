using Meets.BL.Entities;
using Meets.UI.Dto;

namespace Meets.UI
{
    public class Ui
    {
        private static Dictionary<int, Action> _commands;
        public event Func<CreateMeetingDto, Result> OnCreateMeeting;

        public Ui()
        {
            _commands = new Dictionary<int, Action>()
            {
                {1, CreateMeeting }
            };
        }

        public void Run()
        {
            GreetingMessage();

            while (true)
            {
                var command = int.Parse(Console.ReadLine());
                _commands[command].Invoke();
            }

        }

        private void GreetingMessage()
        {
            Console.WriteLine("Привет, я твой личный помощник для встреч");
            Console.WriteLine("Вот что я умею");
            Console.WriteLine("1.Добавить встречу в календарь");
            Console.WriteLine("2.Просмотреть встречи на день");
            Console.WriteLine("Выбери что мне сделать");
        }

        private void CreateMeeting()
        {
            Console.WriteLine("Введите через ; название встречи, дату и время начала и конца встречи в формате DD/MM/YYYY HH:MM");
            var result = Console.ReadLine();
            var items = result.Split(';');
            var name = items.ElementAt(0);
            var startDate = DateTime.Parse(items.ElementAt(1));
            var endDate = DateTime.Parse(items.ElementAt(2));

            var creationResult = OnCreateMeeting?.Invoke(new CreateMeetingDto(name, startDate, endDate));

            if (!creationResult.Success)
            {
                Console.WriteLine("Произошла ошибка");
                Console.WriteLine(creationResult.ReasonPhrase);
            }
        }
    }
}

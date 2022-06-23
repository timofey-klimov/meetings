using Meets.BL.Entities;
using Meets.UI.Dto;
using Meets.UI.Helpers;

namespace Meets.UI
{
    public class Ui
    {
        private static Dictionary<int, Action> _commands;
        public event Func<CreateMeetingDto, Result> OnCreateMeeting;
        public event Func<DateOnly, bool, Result<IEnumerable<Meeting>>> OnGetMeetingsByDay;
        public event Func<Meeting, Result> OnRemoveMeeting;

        public Ui()
        {
            _commands = new Dictionary<int, Action>()
            {
                { 1, CreateMeeting },
                { 2, GetMeetingByDay },
                { 3, RemoveMeeting }
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
            Console.WriteLine("3.Удалить встречу");
            Console.WriteLine("Выбери что мне сделать");
        }

        /// <summary>
        /// Создать встречу
        /// </summary>
        private void CreateMeeting()
        {
            Console.WriteLine("Введите через ; название встречи, дату и время начала и конца встречи в формате DD/MM/YYYY HH:MM");
            var result = Console.ReadLine().Trim();
            var items = result.Split(';');
            var name = items.ElementAt(0).Trim();
            var startDate = DateTime.Parse(items.ElementAt(1).Trim());
            var endDate = DateTime.Parse(items.ElementAt(2).Trim());

            var creationResult = OnCreateMeeting?.Invoke(new CreateMeetingDto(name, startDate, endDate));

            if (!creationResult.Success)
            {
                Console.WriteLine("Произошла ошибка");
                Console.WriteLine(creationResult.ReasonPhrase);
            }
            else
            {
                Console.WriteLine("Встреча добавлена в календарь");
                EndOfCommand();
            }
        }

        /// <summary>
        /// Получить встречи по дате
        /// </summary>
        private void GetMeetingByDay()
        {
            Console.WriteLine("Введите дату в формате DD/MM/YYYY");
            var result = Console.ReadLine().Trim();
            var date = DateOnly.Parse(result);

            var meetingsResult = OnGetMeetingsByDay?.Invoke(date, true);

            if (meetingsResult.Success && meetingsResult.Value.Any())
                meetingsResult.Value.ToList().ForEach(Console.WriteLine);
            else if (!meetingsResult.Value.Any())
                Console.WriteLine("Встреч нет");
            else
            {
                Console.WriteLine("Произошла ошибка");
                Console.WriteLine(meetingsResult.ReasonPhrase);
            }

            EndOfCommand();

        }
        
        /// <summary>
        /// Удалить встречу
        /// </summary>
        private void RemoveMeeting()
        {
            Console.WriteLine("Введите дату собрания в формате DD/MM/YYYY");

            var result = Console.ReadLine().Trim();
            var date = DateOnly.Parse(result);

            var meetingsResult = OnGetMeetingsByDay?.Invoke(date, true);

            if (meetingsResult.Success && !meetingsResult.Value.Any()) 
            {
                Console.WriteLine("В этот день встреч нет");
                EndOfCommand();
                return;
            }

          
            meetingsResult.Value.Select((value, index) => new { Value = value, Index = index })
                .ToList()
                .ForEach(x => 
                    Console.WriteLine($"{x.Index + 1}.{x.Value.Name} Дата {x.Value.MeetingStartTime.GetDateToShortString()} Время {x.Value.MeetingStartTime.GetTimeToShortlString()} - {x.Value.MeetingEndTime.GetTimeToShortlString()}"));

            Console.WriteLine("Выберите номер встречи, которую необходимо удалить");

            var index = int.Parse(Console.ReadLine().Trim());

            var element = meetingsResult.Value.ElementAtOrDefault(index - 1);

            if (element == null)
            {
                Console.WriteLine("Встречи с таким номером не существует");
                Console.WriteLine("Попробуйте еще раз");
                return;
            }

            var removeResult = OnRemoveMeeting?.Invoke(element);

            if (removeResult.Success)
            {
                Console.WriteLine("Встреча удалена");
                EndOfCommand();
            }
            else
                Console.WriteLine(removeResult.ReasonPhrase);
        }

        private void EndOfCommand() 
            => Console.WriteLine("Что еще мне сделать?");
    }
}

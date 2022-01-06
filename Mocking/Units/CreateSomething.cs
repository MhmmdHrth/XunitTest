namespace Mocking.Units
{
    public class CreateSomething
    {
        public record CreateSomethingResult(bool Success, string Error = "");

        private readonly IStore _store;

        public CreateSomething(IStore store)
        {
            _store = store;
        }

        public class Something
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public interface IStore
        {
            bool Save(Something something);
        }

        public class Store : IStore
        {
            public int SaveAttempts { get; set; }
            public bool SaveResult { get; set; }
            public Something LastSavedSomething { get; set; }

            public bool Save(Something something)
            {
                SaveAttempts++;
                LastSavedSomething = something;
                return SaveResult;
            }
        }

        public CreateSomethingResult Create(Something something)
        {
            if (something is { Name: { Length: > 0 } })
            {
                return new(_store.Save(something));
            }

            return new(false, "Somethings not valid.");
        }
    }
}
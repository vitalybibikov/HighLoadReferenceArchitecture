namespace Api.DomainModels
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; }

        public Team(string name)
        {
            Name = name;
        }
    }
}

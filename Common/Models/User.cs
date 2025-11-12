namespace WebApplication1.Models
{
    public class User
    {
        //public required string Name { get; set; }
        //public required string Surname { get; set; }
        //public required string Username { get; set; }
        //public required string Email { get; set; }
        //public required int Tel { get; set; }
        //public required string Address { get; set; }

        //private string _ch = string.Empty;
        //public required string ChorobyInput {
        //    get => _ch;
        //    set => _ch = value;
        //}

        //public List<string> ChorobyList
        //{
        //    get => _ch.Split(',').ToList();
        //}

        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

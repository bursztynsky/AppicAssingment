namespace AppicAssingment.Models
{
    public class User : EntityBase
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname);
        }

        public static IEnumerable<User> GetValidUsers(IEnumerable<User> users)
        {
            return users.Where(x => x.IsValid());
        }

        public override string ToString()
        {
            return $"{nameof(User)} properties -> \nId:{Id} \nName:{Name}\nSurname:{Surname}";
        }
    }
}

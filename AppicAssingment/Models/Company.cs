using System.ComponentModel;

namespace AppicAssingment.Models
{
    public class Company : EntityBase
    {
        [DisplayName("Company Name")]
        public string? CompanyName { get; set; }
        public string? Address { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(CompanyName) && !string.IsNullOrEmpty(Address);
        }

        public static IEnumerable<Company> GetValidUsers(IEnumerable<Company> users)
        {
            return users.Where(x => x.IsValid());
        }

        public override string ToString()
        {
            return $"{nameof(Company)} properties -> \nId:{Id} \nCompanyName:{CompanyName}\nAddress:{Address}";
        }
    }
}

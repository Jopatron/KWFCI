namespace KWFCI.Models
{
    public class StaffProfile : Person
    {
        public StaffUser User { get; set; }
        public string Role { get; set; }
    }
}

namespace BomerFormApp.Models
{
    public class UserBomerViewModel
    {
        public string FullName { get; set; }
        public int StudentNumber { get; set; }
        public string Department { get; set; }
        public string Class { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; } // PhoneNumber tipini string yapın
        public IFormFile Dekont { get; set; }
        public string University { get; set; }

    }
}

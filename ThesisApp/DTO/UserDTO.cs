namespace ThesisApp.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public PreThesisDTO PreThesis { get; set; }
        public ThesisDTO Thesis { get; set; }
    }
}

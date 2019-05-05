using Logic.Students;

namespace Api.Controllers
{
    public sealed class RegisterStudentCommand : ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Course1 { get; set; }
        public string Course1Grade { get; set; }

        public string Course2 { get; set; }
        public string Course2Grade { get; set; }
    }
}
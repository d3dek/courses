using Logic.Students;

namespace Api.Controllers
{
    public sealed class EnrollStudentCommand : ICommand
    {
        public EnrollStudentCommand(string course, string grade, long id)
        {
            Course = course;
            Grade = grade;
            Id = id;
        }

        public long Id { get; set; }
        public string Course { get; }
        public string Grade { get; }
    }
}
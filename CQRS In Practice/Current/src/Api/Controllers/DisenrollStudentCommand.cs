using Logic.Students;

namespace Api.Controllers
{
    public sealed class DisenrollStudentCommand : ICommand
    {
        public DisenrollStudentCommand(string comment, long id, int enrollmentNumber)
        {
            Comment = comment;
            Id = id;
            EnrollmentNumber = enrollmentNumber;
        }

        public string Comment { get; }
        public long Id { get; }
        public int EnrollmentNumber { get; }
    }
}
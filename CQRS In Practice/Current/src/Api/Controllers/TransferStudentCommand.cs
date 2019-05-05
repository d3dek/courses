using Logic.Students;

namespace Api.Controllers
{
    public sealed class TransferStudentCommand : ICommand
    {
        public TransferStudentCommand(long id, string course, string grade, int enrollmentNumber)
        {
            Id = id;
            Course = course;
            Grade = grade;
            EnrollmentNumber = enrollmentNumber;
        }

        public long Id { get; set; }
        public string Course { get; set; }
        public string Grade { get; set; }
        public int EnrollmentNumber { get; set; }
    }
}
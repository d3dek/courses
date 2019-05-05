using CSharpFunctionalExtensions;
using Logic.Students;
using Logic.Utils;

namespace Api.Controllers
{
    public sealed class DisenrollStudentCommandHandler : ICommandHandler<DisenrollStudentCommand>
    {
        private readonly UnitOfWork _unitOfWork;

        public DisenrollStudentCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Handle(DisenrollStudentCommand command)
        {
            var studentRepository = new StudentRepository(_unitOfWork);
            Student student = studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            if (string.IsNullOrWhiteSpace(command.Comment))
                return Result.Fail("Disenrollment comment is required");

            var enrollment = student.GetEnrollment(command.EnrollmentNumber);
            if (enrollment == null)
                return Result.Fail($"No enrollment found with number: '{command.EnrollmentNumber}'");

            student.RemoveEnrollment(enrollment, command.Comment);

            _unitOfWork.Commit();
            return Result.Ok();
        }
    }
}
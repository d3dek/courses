using CSharpFunctionalExtensions;
using Logic.Students;
using Logic.Utils;

namespace Api.Controllers
{
    public sealed class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
    {
        private readonly UnitOfWork _unitOfWork;

        public DeleteStudentCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Handle(DeleteStudentCommand command)
        {
            var studentRepository = new StudentRepository(_unitOfWork);
            Student student = studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            studentRepository.Delete(student);
            _unitOfWork.Commit();

            return Result.Ok();
        }
    }
}
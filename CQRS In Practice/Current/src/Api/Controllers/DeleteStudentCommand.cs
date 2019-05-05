using Logic.Students;

namespace Api.Controllers
{
    public sealed class DeleteStudentCommand : ICommand
    {
        public DeleteStudentCommand(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
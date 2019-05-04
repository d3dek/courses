using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Dtos;
using Logic.Utils;

namespace Logic.Students
{

    public interface ICommand
    {
    }

    public interface IQuery<TResult>
    {
    }

    public interface ICommandHandler<TCommand> where TCommand: ICommand
    {
        Result Handle(TCommand command);
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery: IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

    public sealed class GetListQuery: IQuery<List<StudentDto>>
    {
        public GetListQuery(string enrollmentIn, int? numberOfCourses)
        {
            EnrollmentIn = enrollmentIn;
            NumberOfCourses = numberOfCourses;
        }

        public string EnrollmentIn { get; }
        public int? NumberOfCourses { get;  } 
    }

    public sealed class GetListQueryHandler : IQueryHandler<GetListQuery, List<StudentDto>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetListQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<StudentDto> Handle(GetListQuery query)
        {
            var studentRepository = new StudentRepository(_unitOfWork);
            IReadOnlyList<Student> students = studentRepository.GetList(query.EnrollmentIn, query.NumberOfCourses);
            return students.Select(x => ConvertToDto(x)).ToList();
        }

        private StudentDto ConvertToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Course1 = student.FirstEnrollment?.Course?.Name,
                Course1Grade = student.FirstEnrollment?.Grade.ToString(),
                Course1Credits = student.FirstEnrollment?.Course?.Credits,
                Course2 = student.SecondEnrollment?.Course?.Name,
                Course2Grade = student.SecondEnrollment?.Grade.ToString(),
                Course2Credits = student.SecondEnrollment?.Course?.Credits,
            };
        }
    }


    public sealed class EditPersonalInfoCommand: ICommand
    {
        public EditPersonalInfoCommand(long id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
    }

    public sealed class EditPersonalInfoCommandHandler: ICommandHandler<EditPersonalInfoCommand>
    {
        private readonly Utils.UnitOfWork _unitOfWork;
        private object _studentRepository;

        public EditPersonalInfoCommandHandler(Utils.UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Result Handle(EditPersonalInfoCommand command) 
        {
            var studentRepository = new StudentRepository(_unitOfWork);
            Student student = studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            student.Name = command.Name;
            student.Email = command.Email;

            _unitOfWork.Commit();

            return Result.Ok();
        }
    }
}

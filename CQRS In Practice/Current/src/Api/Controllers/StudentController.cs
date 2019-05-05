using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Students;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly Messages _messages;

        public StudentController(Messages messages)
        {
            _messages = messages;
        }
        
        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            List<StudentDto> studentDtos = _messages.Dispatch(new GetListQuery(enrolled, number));
            return Ok(studentDtos);
        }

        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            var command = new RegisterStudentCommand
            {
                Email = dto.Email,
                Name = dto.Name,
                Course1 =  dto.Course1,
                Course1Grade = dto.Course1Grade,
                Course2 = dto.Course2,
                Course2Grade = dto.Course2Grade
            };

            Result result = _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var command = new DeleteStudentCommand(id);
            Result result = _messages.Dispatch(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            var command = new EnrollStudentCommand(dto.Course, dto.Grade, id);
            Result result = _messages.Dispatch(command);

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPut("{id}/enrollemtns/{enrollmentNumber}")]
        public IActionResult Transfer(long id, int enrollmentNo, [FromBody] StudentTransferDto dto)
        {
            var command = new TransferStudentCommand(id, dto.Course, dto.Grade, enrollmentNo);
            Result result = _messages.Dispatch(command);

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPut("{id}/enrollemtns/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(long id, int enrollmentNo, [FromBody] StudentDisenrollmentDto dto)
        {
            var command = new DisenrollStudentCommand(dto.Comment, id, enrollmentNo);
            Result result = _messages.Dispatch(command);

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto dto)
        {
            var command = new EditPersonalInfoCommand(id, dto.Name, dto.Email);
            var result = _messages.Dispatch(command);

            return result.IsSuccess ? Ok() : Error(result.Error);
        }
    }
}

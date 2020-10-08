using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Context;
using University.Entities;
using University.Models;

namespace University.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private UniversityContext db;

        public StudentGroupController(UniversityContext db)
        {
            this.db = db;
        }

        [HttpPost()]
        public IActionResult Add(AddDeleteStudentFromGroup model)
        {
            var student = db.Students.Include(s=>s.StudentGroups).First(s => s.Id == model.StudentId);

            var group = db.Groups.First(g => g.Id == model.GroupId);

            var studentGroup = new StudentGroup()
            {
                StudentId = model.StudentId,
                Student = student,
                GroupId = model.GroupId,
                Group = group
            };

            student.StudentGroups.Add(studentGroup);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(AddDeleteStudentFromGroup model)
        {
            var student = db.Students.Include(s => s.StudentGroups).First(s => s.Id == model.StudentId);

            var studentGroup = student.StudentGroups.FirstOrDefault(sg => sg.GroupId == model.GroupId);
            student.StudentGroups.Remove(studentGroup);
            db.SaveChanges();

            return Ok();
        }
    }
}

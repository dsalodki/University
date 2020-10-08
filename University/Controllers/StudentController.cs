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
    public class StudentController : ControllerBase
    {
        private UniversityContext db;

        public StudentController(UniversityContext db)
        {
            this.db = db;
        }

        [HttpPost()]
        public IActionResult Create(CreateUpdateStudent model)
        {
            var student = new Student()
            {
                UniqueIdentifier = model.UniqueIdentifier,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Sex = model.Sex
            };

            db.Add(student);
            db.SaveChanges();

            return Ok(student);
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            var student = db.Students.First(s => s.Id == id);
            db.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, CreateUpdateStudent model)
        {
            var student = db.Students.First(s => s.Id == id);

            student.UniqueIdentifier = model.UniqueIdentifier;
            student.LastName = model.LastName;
            student.FirstName = model.FirstName;
            student.MiddleName = model.MiddleName;
            student.Sex = model.Sex;

            db.Update(student);
            db.SaveChanges();

            return Ok(student);
        }

        [HttpGet]
        public IActionResult Filter(FilterStudents model, int pageSize, int pageNumber)
        {
            var students = db.Students.Include(s => s.StudentGroups).AsNoTracking();
            if (model.Sex.HasValue)
            {
                students = students.Where(s => s.Sex == model.Sex);
            }

            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                students = students.Where(s => EF.Functions.Like(s.FirstName, $"%{model.FirstName}%"));
            }

            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                students = students.Where(s => EF.Functions.Like(s.LastName, $"%{model.LastName}%"));
            }

            if (!string.IsNullOrWhiteSpace(model.MiddleName))
            {
                students = students.Where(s => EF.Functions.Like(s.MiddleName, $"%{model.MiddleName}%"));
            }

            if (!string.IsNullOrWhiteSpace(model.UniqueIdentifier))
            {
                students = students.Where(s => EF.Functions.Like(s.UniqueIdentifier, $"%{model.UniqueIdentifier}%"));
            }

            if (!string.IsNullOrWhiteSpace(model.GroupName))
            {
                var groupIds = db.Groups.AsNoTracking().Where(g => EF.Functions.Like(g.Name, $"%{model.GroupName}%")).Select(g=>g.Id);

                students = students.Where(s => s.StudentGroups.Any(sg => groupIds.Contains(sg.GroupId)));
            }

            students = students.Skip(pageSize * pageNumber).Take(pageSize);

            var viewModels = students.Select(s => new ViewModels.FilterStudentsViewModel()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                MiddleName = s.MiddleName,
                LastName = s.LastName,
                UniqueIdentifier = s.UniqueIdentifier,
                GroupNames = string.Join(',', s.StudentGroups.Select(sg=>sg.Group.Name))
            }).ToList();
            return Ok(viewModels);
        }
    }
}

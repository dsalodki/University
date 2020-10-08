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
    public class GroupController : ControllerBase
    {
        private UniversityContext db;

        public GroupController(UniversityContext db)
        {
            this.db = db;
        }

        [HttpPost()]
        public IActionResult Create(CreateUpdateGroup model)
        {
            var group = new Group()
            {
                Name = model.Name
            };

            db.Add(group);
            db.SaveChanges();

            return Ok(group);
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            var group = db.Groups.First(s => s.Id == id);
            db.Remove(group);
            db.SaveChanges();

            return Ok(group);
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, CreateUpdateGroup model)
        {
            var group = db.Groups.First(s => s.Id == id);

            group.Name = model.Name;

            db.Update(group);
            db.SaveChanges();

            return Ok(group);
        }

        [HttpGet]
        public IActionResult Filter(FilterGroups model)
        {
            var groups = db.Groups.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                groups = groups.Where(s => EF.Functions.Like(s.Name, $"%{model.Name}%"));
            }

            var viewModels = groups.Select(g => new ViewModels.FilterGroupsViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                StudentsCount = g.StudentGroups.Count
            }).ToList();

            return Ok(viewModels);
        }
    }
}

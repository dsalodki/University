using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Entities;

namespace University.Context
{
    public class SampleData
    {
        public static void Initialize(UniversityContext context)
        {
            if (context.Students.Any())
                return;

            var students = new List<Student>()
            {
                new Student()
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MiddleName = "Иванович",
                    UniqueIdentifier = "Иванов",
                    Sex = true,
                },
                new Student()
                {
                    FirstName = "Петр",
                    LastName = "Петров",
                    MiddleName = "Петрович",
                    UniqueIdentifier = "Петров",
                    Sex = true,
                },
                new Student()
                {
                    FirstName = "Себастьян",
                    LastName = "Бах",
                    MiddleName = "Альбертович",
                    Sex = true,
                },
                new Student()
                {
                    FirstName = "Иоган",
                    LastName = "Кеплер",
                    MiddleName = "Францевич",
                    Sex = true,
                },
            };
            context.AddRange(students);

            var groups = new List<Group>()
            {
                new Group()
                {
                    Name = "русские"
                },
                new Group()
                {
                    Name = "иностранцы"
                },
                new Group()
                {
                    Name = "все"
                },
            };
            context.AddRange(groups);

            context.SaveChanges();

            var relations = new List<StudentGroup>
            {
                new StudentGroup()
                {
                    StudentId = students[0].Id,
                    GroupId = groups[0].Id
                },
                new StudentGroup()
                {
                    StudentId = students[1].Id,
                    GroupId = groups[0].Id
                },
                new StudentGroup()
                {
                    StudentId = students[2].Id,
                    GroupId = groups[1].Id
                },
                new StudentGroup()
                {
                    StudentId = students[3].Id,
                    GroupId = groups[1].Id
                },
                new StudentGroup()
                {
                    StudentId = students[0].Id,
                    GroupId = groups[2].Id
                },
                new StudentGroup()
                {
                    StudentId = students[1].Id,
                    GroupId = groups[2].Id
                },
                new StudentGroup()
                {
                    StudentId = students[2].Id,
                    GroupId = groups[2].Id
                },
                new StudentGroup()
                {
                    StudentId = students[3].Id,
                    GroupId = groups[2].Id
                },
            };

            context.AddRange(relations);
            context.SaveChanges();
        }

    }
}

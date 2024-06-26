﻿using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon.Factories;
using Silicon.Models;

namespace Silicon.Controllers
{
    public class CourseController(DataContext dataContext) : Controller
    {
        private readonly DataContext _dataContext = dataContext;

        #region Courses
        public async Task<IActionResult> Courses(string category = "", string searchQuery = "", int pageNumber = 1,
            int pageSize = 6)
        {
            var viewModel = new CoursesViewModel
            {
                Categories = CategoryFactory.Create(await _dataContext.Categories.ToListAsync())
            };

            var quary = _dataContext.Courses
                .Include(i => i.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                quary = quary.Where(x => x.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrEmpty(category) && category != "all")
            {
                quary = quary.Where(x => x.Category!.CategoryName == category);
            }

            var filteredCourses = await quary.ToListAsync();

            if (filteredCourses.Count != 0 && filteredCourses != null)
            {

                var totalItemsCount = await quary.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
                viewModel.Paginering = new Paginering
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = totalPages,
                    TotalCount = totalItemsCount

                };
                var coursesForPage = quary.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                viewModel.Courses = CourseFactory.Create(coursesForPage);
                return View(viewModel);
            }
            else
            {
                return View(new List<CourseViewModel>()); // Om det inte finns några kurser, skicka en tom lista till vyn
            }
        }
        #endregion

        #region SingleCourse
        public async Task<IActionResult> SingleCourse(string Id)
        {

            if (!string.IsNullOrEmpty(Id))
            {
                var entity = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == Id);

                if (entity != null)
                {
                    var viewModel = new SingleCourseViewModel();
                    var teacherEntity = await _dataContext.Teachers.FirstOrDefaultAsync(x => x.Id == entity.TeacherId);
                    if (teacherEntity != null)
                    {
                        viewModel.Teacher = TeacherFactory.CreateTeacher(teacherEntity);
                    }
                    viewModel.SingleCourseView = CourseFactory.CreateSingle(entity);
                    return View(viewModel);
                }

            }

            return RedirectToAction("Home", "Default");
        }
        #endregion
    }
}

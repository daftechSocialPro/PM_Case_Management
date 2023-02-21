﻿using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Models.PM;
using System.Net.Sockets;

namespace PM_Case_Managemnt_API.Services.PM
{
    public class TaskService :ITaskService
    {

        private readonly DBContext _dBContext;
        public TaskService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> CreateTask(TaskDto task)
        {

            var task1 = new PM_Case_Managemnt_API.Models.PM.Task
            {
                Id = Guid.NewGuid(),
                TaskDescription = task.TaskDescription,
                PlanedBudget = task.PlannedBudget,
                HasActivityParent = task.HasActvity,
                CreatedAt = DateTime.Now,
                PlanId = task.PlanId,

            };
            await _dBContext.AddAsync(task1);
            await _dBContext.SaveChangesAsync();
            return 1;

        }

        public async Task<int> AddTaskMemo(TaskMemoRequestDto taskMemo)


        {

            var taskMemo1 = new TaskMemo 
            {
                Id = Guid.NewGuid(),
                CreatedAt= DateTime.Now,
                EmployeeId = taskMemo.EmployeeId,
                TaskId = taskMemo.TaskId,
                Description = taskMemo.Description,
                

                
            };
       
            await _dBContext.AddAsync(taskMemo1);
            await _dBContext.SaveChangesAsync();
            return 1;

        }

        public async Task<TaskVIewDto> GetSingleTask(Guid taskId)
        {

            var task = _dBContext.Tasks.Where(x => x.Id == taskId).ToList();

            var taskMembers = (from t in _dBContext.TaskMembers.Include(x => x.Employee).Where(x => x.TaskId == taskId)

                              select new SelectListDto
                              {
                                  Id = t.Id,
                                  Name = t.Employee.FullName,
                                  Photo = t.Employee.Photo,
                                  EmployeeId = t.EmployeeId.ToString()
                              }).ToList();

            var taskMemos = (from t in _dBContext.TaskMemos.Include(x => x.Employee).Where(x => x.TaskId == taskId)
                             select new TaskMemoDto
                             {
                                 Employee =new SelectListDto
                                 {
                                     Id = t.EmployeeId,
                                     Name = t.Employee.FullName,
                                     Photo = t.Employee.Photo,
                                 },
                                 DateTime = t.CreatedAt,
                                 Description = t.Description

                             }).ToList();
                


            return  (from t in task
                    select new TaskVIewDto
                    {

                        Id = t.Id,
                        TaskName= t.TaskDescription,
                        TaskMembers = taskMembers,
                        TaskMemos = taskMemos


                    }).FirstOrDefault();
            

            
        }

        public async Task<int> AddTaskMemebers(TaskMembersDto taskMembers)
        {
            foreach (var e in taskMembers.Employee)
            {
                var taskMemebers1 = new TaskMembers
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    EmployeeId = e.Id,
                    TaskId = taskMembers.TaskId
                };
                await _dBContext.AddAsync(taskMemebers1);
                await _dBContext.SaveChangesAsync();
            }

          // await _dBContext.AddAsync(taskMemebers1);
            return 1;
        }
        public async Task<List<SelectListDto>> GetEmployeesNoTaskMembersSelectList(Guid taskId)
        {
            var taskMembers = _dBContext.TaskMembers.Where(x => x.TaskId == taskId).Select(x => x.EmployeeId).ToList();

            var EmployeeSelectList = await (from e in _dBContext.Employees
                                            where !(taskMembers.Contains(e.Id))
                                            select new SelectListDto
                                            {
                                                Id = e.Id,
                                                Name = e.FullName
                                            }).ToListAsync();

            return EmployeeSelectList;
        }

    }
}
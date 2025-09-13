using Exam2025.DAL.BaseEntity;
using Exam2025.DAL.Contexts;
using Exam2025.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Exam2025.DAL.Contexts
{
    public class ExamDbContextSeed
    {
        public static async Task SeedAsync(ExamDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,  ILoggerFactory loggerFactory)
        {
            try
            {

                if (!roleManager.Roles.Any())
                {
                    var role1 = new AppRole
                    {
                        Name = "Admin"
                    };
                    var role2 = new AppRole
                    {
                        Name = "User"
                    };

                    await roleManager.CreateAsync(role1);
                    await roleManager.CreateAsync(role2);
                }

                if (!userManager.Users.Any())
                {
                    var user1 = new AppUser
                    {
                        NormalizedUserName = "AymanElbatata".ToUpper(),
                        Email = "Ayman.Fathy.Elbatata@gmail.com",
                        UserName = "Ayman.Elbatata",
                        FirstName = "Ayman",
                        LastName = "Elbatata",
                        IsActivated = true,
                        LockoutEnabled = false,
                        PhoneNumber = "201284878483"
                    };
                    var user2 = new AppUser
                    {
                        NormalizedUserName = "AymanFathy".ToUpper(),
                        Email = "Ayman_Elbatata@outlook.com",
                        UserName = "Ayman.Fathy",
                        FirstName = "Ayman",
                        LastName = "Fathy",
                        IsActivated = true,
                        LockoutEnabled = false,
                        PhoneNumber = "201202025251"
                    };


                    await userManager.CreateAsync(user1, "Aym@9841");
                    await userManager.CreateAsync(user2, "Aym@9841");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "User");
                    await context.SaveChangesAsync();


                    if (!context.ExamTBLs.Any())
                    {
                        var Exams = File.ReadAllText("../Exam2025.DAL/Contexts/SeedData/Exam.json");
                        var ExamCollection = JsonSerializer.Deserialize<List<ExamTBL>>(Exams);

                        for (int i = 0; i < ExamCollection?.Count; i++)
                        {
                            ExamCollection[i].CreatedUserID =  context.Users.Where(x=> x.UserName == "Ayman.Elbatata").FirstOrDefault()?.Id;
                            context.ExamTBLs.Add(ExamCollection[i]);
                        }
                        await context.SaveChangesAsync();
                    }
                    if (!context.QuestionTBLs.Any())
                    {
                        var Questions = File.ReadAllText("../Exam2025.DAL/Contexts/SeedData/Question.json");
                        var QuestionCollection = JsonSerializer.Deserialize<List<QuestionTBL>>(Questions);
                        var ExamCollection = context.ExamTBLs.Where(a => a.IsDeleted == false).ToList();
                        foreach (var exam in ExamCollection)
                        {
                            var numberOfQuestions = exam.NumberofQuestions;

                            for (int i = 0; i < numberOfQuestions; i++)
                            {
                                var newQuestion = new QuestionTBL
                                {
                                    ExamTBLId = exam.ID,
                                    Title = QuestionCollection[i].Title
                                };

                                context.QuestionTBLs.Add(newQuestion);
                            }

                        }
                        await context.SaveChangesAsync();

                    }
                    if (!context.AnswerTBLs.Any())
                    {
                        var Answers = File.ReadAllText("../Exam2025.DAL/Contexts/SeedData/Answer.json");
                        var AnswerCollection = JsonSerializer.Deserialize<List<AnswerTBL>>(Answers);
                        var QuestionCollection = context.QuestionTBLs.Where(a => a.IsDeleted == false).ToList();

                        foreach (var exam in QuestionCollection)
                        {

                            for (int i = 0; i < AnswerCollection.Count; i++)
                            {
                                var newAnswer = new AnswerTBL
                                {
                                    QuestionTBLId = exam.ID,
                                    Name = AnswerCollection[i].Name,
                                    IsRight = AnswerCollection[i].IsRight,
                                };

                                context.AnswerTBLs.Add(newAnswer);
                            }

                        }
                        await context.SaveChangesAsync();

                    }


                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ExamDbContext>();
                logger.LogError(ex.Message);
            }
        }



    }
}

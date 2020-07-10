using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configurataion
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasData
            (
                new Assignment
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705498a4b"),
                    Title = "Module 1",
                    Description = "Entity and databae connection",
                    EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497c6e")
                },
                 new Assignment
                 {
                     Id = new Guid("80abbca8-664d-4b20-b5de-024705498b4a"),
                     Title = "Module 2",
                     Description = "Get Operation ",
                     EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497c6e")
                 },
                  new Assignment
                  {
                      Id = new Guid("80abbca8-664d-4b20-b5de-024705498c4c"),
                      Title = "Module 3",
                      Description = "Post Operation ",
                      EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497c6e")
                  },
                   new Assignment
                   {
                       Id = new Guid("80abbca8-664d-4b20-b5de-024705498e2a"),
                       Title = "Module1",
                       Description = "Get Operation ",
                       EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497c7e")
                   },
                    new Assignment
                    {
                        Id = new Guid("80abbca8-664d-4b20-b5de-024705498e3a"),
                        Title = "Module2",
                        Description = "Post Operation ",
                        EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497c7e")
                    },
                     new Assignment
                     {
                         Id = new Guid("80abbca8-664d-4b20-b5de-024705498a3b"),
                         Title = "Module3",
                         Description = "Delete Operation ",
                         EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497c7e")
                     },

                     new Assignment
                     {
                         Id = new Guid("80abbca8-664d-4b20-b5de-024705497c3e"),
                         Title = "Module1",
                         Description = "Get Operation ",
                         EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497d9e")
                     },
                      new Assignment
                      {
                          Id = new Guid("80abbca8-564d-4b20-b5de-024705496d3e"),
                          Title = "Module2",
                          Description = "Post Operation ",
                          EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497d9e")
                      },

                        new Assignment
                        {
                            Id = new Guid("80abbca8-634d-4b20-b5de-024705495a1e"),
                            Title = "Module3",
                            Description = "Update Operation ",
                            EnrollmentId = new Guid("80abbca8-664d-4b20-b5de-024705497d9e")
                        },
                         new Assignment
                         {
                             Id = new Guid("80abbca8-624d-4b20-b5de-024705495a1e"),
                             Title = "Module2",
                             Description = "Get Operation ",
                             EnrollmentId = new Guid("80abbca8-664d-4b20-b6de-024705497c1e")
                         },
                          new Assignment
                          {
                              Id = new Guid("80abbca8-66cd-4b20-b5de-024705495a1a"),
                              Title = "Module3",
                              Description = "Update Operation ",
                              EnrollmentId = new Guid("80abbca8-664d-4b20-b6de-024705497c1e")
                          },
                           new Assignment
                           {
                               Id = new Guid("80abbca8-66ed-4b20-b5de-024705495e1e"),
                               Title = "Module3",
                               Description = "Update Operation ",
                               EnrollmentId = new Guid("80abbca8-664d-4b20-b6de-024705497c1e")
                           },
                            new Assignment
                            {
                                Id = new Guid("80abbca8-662d-4b20-b5de-024705495a2e"),
                                Title = "Module3",
                                Description = "Update Operation ",
                                EnrollmentId = new Guid("80abbca8-664d-4b20-e5de-024705497c2e")
                            },
                              new Assignment
                              {
                                  Id = new Guid("80abbca8-664d-3a20-b5de-024705494a2e"),
                                  Title = "Module3",
                                  Description = "Teacher assigns to students homework",
                                  EnrollmentId = new Guid("80abbca8-664d-4b20-d6de-024705497d9e")
                              },

                              new Assignment
                              {
                                  Id = new Guid("80abbca8-664d-40b2-b5de-024705494a2e"),
                                  Title = "Module3",
                                  Description = "Admin created assignment ",
                                  EnrollmentId = new Guid("80abbca8-664d-4b20-c6de-024705497d8e")
                              }






                );
        }
    }

}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisionGroupUnitTestProject
{
    using System.Linq;

    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Factories;

    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void CreateNewProject_Project_True()
        {
            // Arrange
            ProjectFactory pFactory = new ProjectFactory();
            Project p = new Project();
            p.Name = "Create Test Project";
            p.CostumerId = CostumerCatalog.Instance.CostumerList[0].CostumerId;
            p.Deadline = DateTime.Now;

            // Act
            if (ProjectCatalog.Instance.ProjectList.Exists((project => project.Name == p.Name)))
            {
                ProjectCatalog.Instance.Remove(ProjectCatalog.Instance.ProjectList.Find((project => project.Name == p.Name)));
            }
            pFactory.CanCreate(p);
            pFactory.Create();

            // Assert
            ProjectCatalog.Instance.Load();
            Assert.IsTrue(ProjectCatalog.Instance.ProjectList.Exists((project => project.Name == p.Name)));
            ProjectCatalog.Instance.Remove(p);
        }

        [TestMethod]
        public void DeleteTestProject_Project_True()
        {
            // Arrange
            Project p = new Project();
            p.Name = "Delete Test Project";
            p.CostumerId = CostumerCatalog.Instance.CostumerList[0].CostumerId;
            p.Deadline = DateTime.Now;
            ProjectCatalog.Instance.Add(p);
            ProjectCatalog.Instance.Load();
            
            // Act
            ProjectCatalog.Instance.Remove(p);

            // Assert
            ProjectCatalog.Instance.Load();
            Assert.IsFalse(ProjectCatalog.Instance.ProjectList.Contains(p));
        }
    }
}

using System.Collections.Generic;
using VisionGroup.Interfaces;

namespace VisionGroup
{
    public class ProjectCatalog : ICatalog<Project>
    {

        
        public List<Project> ProjectList { get; set; }

        public void Add(Project item)
        {
            ProjectList.Add(item);
        }

        public void Remove(Project item)
        {
            ProjectList.Remove(item);
        }


        // Finder index for gamle item, som har et bestemt ProjectId
        // Den erstatter med ny item med samme ProjectId
        public void Update(Project item)
        {
            ProjectList[ProjectList
                .FindIndex(a => a.ProjectId == item.ProjectId)] = item;
        }

        public void Load(Project item)
        {
            throw new System.NotImplementedException();
        }
    }
}
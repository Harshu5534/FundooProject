using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBl
    {
        public CollabEntity AddCollab(long noteid, long userid, string email);
        public bool Remove(long collabid);
        IEnumerable<CollabEntity> GetAllByNoteID(long noteid);
    }
}

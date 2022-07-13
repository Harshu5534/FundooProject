using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRl
    {
        public CollabEntity AddCollab(long noteid, long userid, string email);
        public bool Remove(long collabid);
        IEnumerable<CollabEntity> GetAllByNoteID(long noteid);
    }
}

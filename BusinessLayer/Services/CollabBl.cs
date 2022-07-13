using BusinessLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBl: ICollabBl
    { 
        ICollabRl CollabRl;
        public CollabBl(ICollabRl collabRl)
        {
            this.CollabRl = collabRl;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                return this.CollabRl.AddCollab(noteid, userid, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Remove(long collabid)
        {
            try
            {
                return this.CollabRl.Remove(collabid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollabEntity> GetAllByNoteID(long noteid)
        {
            try
            {
                return this.CollabRl.GetAllByNoteID(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

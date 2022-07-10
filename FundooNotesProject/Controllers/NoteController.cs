using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooNotesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBl inoteBl;
        public NoteController(INoteBl inoteBl)
        {
                this.inoteBl = inoteBl;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote)
        {
            try
            {
                long noteid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);
                var result = inoteBl.AddNote(addnote, noteid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Added Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to add note"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long noteid)
        {
            try
            {
                if (inoteBl.DeleteNote(noteid))
                {
                    return this.Ok(new 
                    { 
                        Success = true, 
                        message = "Note Deleted Successfully" 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    { 
                        Success = false, 
                        message = "Unable to delete note" 
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Update")]
        public IActionResult updateNotes(NoteModel addnote, long noteid)
        {
            try
            {
                var result = inoteBl.UpdateNotes(addnote, noteid);
                if (result != null)
                {
                    return this.Ok(new 
                    { 
                        Success = true, 
                        message = "Note Updated Successfully",
                        Response = result 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    { 
                        Success = false, 
                        message = "Unable to Update note" 
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Pin")]
        public IActionResult Ispinnedornot(long noteid)
        {
            try
            {
                var result = inoteBl.IsPinnedORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new 
                    { 
                        message = "Note unPinned ", 
                        Response = result 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    { 
                        message = "Note Pinned Successfully" 
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Archive")]
        public IActionResult IsArchivedORNot(long noteid)
        {
            try
            {
                var result = inoteBl.IsArchivedORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new 
                    { 
                        message = "Note Unarchived ", 
                        Response = result 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    { 
                        message = "Note Archived Successfully" 
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("AllNotes")]
        public IEnumerable<NoteEntity> GetAllNote()
        {
            try
            {
                return inoteBl.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("ByUser")]
        public IEnumerable<NoteEntity> GetAllNotesbyuser(long userid)
        {
            try
            {
                return inoteBl.GetAllNotesbyuserid(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

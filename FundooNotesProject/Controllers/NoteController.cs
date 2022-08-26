using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBl inoteBl;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly fundooContext context;
        public NoteController(INoteBl inoteBl, IMemoryCache memoryCache, fundooContext context, IDistributedCache distributedCache)
        {
            this.inoteBl = inoteBl;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);
                var result = inoteBl.AddNote(addnote, userid);
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
                        success = true,
                        message = "Note Pinned Successfully", 
                        Response = result 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    { 
                        message = "Note unPinned "
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("{noteid}")]
        public IActionResult IsArchivedORNot(long noteid)
        {
            try
            {
                var result = inoteBl.IsArchivedORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new 
                    {
                        message = "Note Archived Successfully", 
                        Response = result 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    {
                        message = "Note Unarchived ",
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
        [HttpGet("Userid")]
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
        [Authorize]
        [HttpPut("Upload")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                var result = inoteBl.UploadImage(noteid, img);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Color")]
        public IActionResult Color(long noteid, string color)
        {
            try
            {
                var result = inoteBl.Color(noteid, color);
                if (result != null)
                {
                    return this.Ok(new 
                    { 
                        message = "Color is changed ", 
                        Response = result 
                    });
                }
                else
                {
                    return this.BadRequest(new 
                    { 
                        message = "Unable to change color" 
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NodeList";
            string serializedNotesList;
            var NotesList = new List<NoteEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = await context.NotesTable.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }

    }
}

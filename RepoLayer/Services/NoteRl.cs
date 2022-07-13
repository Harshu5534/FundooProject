using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class NoteRl:INoteRl
    {
        FundooContext fundooContext;
        private readonly IConfiguration config;
        public const string CLOUD_NAME = "imageupl";
        public const string API_KEY = "913737481261618";
        public const string API_Secret = "aedXJOOgdxKLFdmWGx8p6_RT6vQ";
        public static Cloudinary cloud;
        public NoteRl(FundooContext fundooContext, IConfiguration config)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public NoteEntity AddNote(NoteModel notes, long userid)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = notes.Title;
                noteEntity.Description = notes.Description;
                noteEntity.Reminder = notes.Reminder;
                noteEntity.Color = notes.Color; 
                noteEntity.Image = notes.Image;
                noteEntity.IsArchived = notes.IsArchived;
                noteEntity.IsPinned = notes.IsPinned;
                noteEntity.IsDeleted = notes.IsDeleted;
                noteEntity.UserId = userid;
                noteEntity.CreatedAt = notes.CreatedAt;
                noteEntity.EditedAt = notes.EditedAt;
                this.fundooContext.NotesTable.Add(noteEntity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(long noteid)
        {
            try
            {
                var result = this.fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteid);
                fundooContext.Remove(result);
                int deletednote = this.fundooContext.SaveChanges();
                if (deletednote > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNotes(NoteModel notes, long noteid)
        {
            try
            {
                NoteEntity result = fundooContext.NotesTable.Where(e => e.NoteId == noteid).FirstOrDefault();
                if (result != null)
                {
                    //NoteEntity noteEntity = new NoteEntity();
                    result.Title = notes.Title;
                    result.Description = notes.Description;
                    result.Color = notes.Color;
                    result.Image = notes.Image;
                    result.IsArchived = notes.IsArchived;
                    result.IsPinned = notes.IsPinned;
                    fundooContext.NotesTable.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity IsPinnedORNot(long noteid)
        {
            try
            {
                NoteEntity result = this.fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteid);
                if (result.IsPinned == true)
                {
                    result.IsPinned = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.IsPinned = true;
                this.fundooContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity IsArchivedORNot(long noteid)
        {
            try
            {
                NoteEntity result = this.fundooContext.NotesTable.FirstOrDefault(x => x.NoteId == noteid);
                if (result.IsArchived == true)
                {
                    result.IsArchived = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.IsArchived = true;
                this.fundooContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            return fundooContext.NotesTable.ToList();
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid)
        {
            return fundooContext.NotesTable.Where(n => n.UserId == userid).ToList();
        }
        public NoteEntity UploadImage(long noteid, IFormFile img)
        {
            try
            {
                var noteId = this.fundooContext.NotesTable.FirstOrDefault(e => e.NoteId == noteid);
                if (noteId != null)
                {
                    Account acc = new Account(CLOUD_NAME, API_KEY, API_Secret);
                    cloud = new Cloudinary(acc);
                    var imagePath = img.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, imagePath)
                    };
                    var uploadresult = cloud.Upload(uploadParams);
                    noteId.Image = img.FileName;
                    fundooContext.NotesTable.Update(noteId);
                    int upload = fundooContext.SaveChanges();
                    if (upload > 0)
                    {
                        return noteId;
                    }
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

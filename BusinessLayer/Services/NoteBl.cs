using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBl:INoteBl
    {
        INoteRl noteRl;

        public NoteBl(INoteRl noteRL)
        {
            this.noteRl = noteRL;
        }

        public NoteEntity AddNote(NoteModel noteModel, long userid)
        {
            try
            {
                return this.noteRl.AddNote(noteModel, userid);
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
                return this.noteRl.DeleteNote(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity UpdateNotes(NoteModel noteModel, long noteid)
        {
            try
            {
                return this.noteRl.UpdateNotes(noteModel, noteid);
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
                return this.noteRl.IsPinnedORNot(noteid);
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
                return this.noteRl.IsArchivedORNot(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            try
            {
                return this.noteRl.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid)
        {
            try
            {
                return this.noteRl.GetAllNotesbyuserid(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity UploadImage(long noteid, IFormFile img)
        {
            try
            {
                return this.noteRl.UploadImage(noteid, img);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

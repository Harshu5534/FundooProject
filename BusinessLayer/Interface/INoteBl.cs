using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBl
    {
        public NoteEntity AddNote(NoteModel node, long UserId);
        public bool DeleteNote(long noteid);
        public NoteEntity UpdateNotes(NoteModel notes, long Noteid);
        public NoteEntity IsPinnedORNot(long noteid);
        public NoteEntity IsArchivedORNot(long noteid);
        IEnumerable<NoteEntity> GetAllNotes();
        IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid);
        public NoteEntity UploadImage(long noteid, IFormFile img);
    }
}

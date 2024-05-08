using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class NotesService
    {
        private readonly SalesWebMvcContext _context;

        public NotesService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task CreateNoteAsync(string title, string content)
        {
            var note = new Notes
            {
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(int id, string title, string content)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note != null)
            {
                note.Title = title;
                note.Content = content;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Notes> DeleteNoteAsync(int id)
        {
            try
            {
                var note = await _context.Notes.FindAsync(id);
                if (note != null)
                {
                    _context.Notes.Remove(note);
                    await _context.SaveChangesAsync();
                }
                return note; // Retorna a nota excluída
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete note because it's associated with other entities.");
            }
        }



        public async Task<Notes> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public List<Notes> GetAllNotes()
        {
            return _context.Notes.ToList();
        }

        // Adicione outros métodos conforme necessário para sua aplicação
    }
}

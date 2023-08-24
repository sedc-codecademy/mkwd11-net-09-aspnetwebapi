using Microsoft.Data.SqlClient;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.DataAccess.Implementation
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private string sqlConnectionString = "Server=.;Database=noteappdb;Trusted_Connection=True;Encrypt=False";

        public void Add(Note entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = $"Select * From dbo.Notes";

            List<Note> notes = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Note note = new Note
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (PriorityEnum)sqlDataReader["Priority"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                };

                notes.Add(note);
            }

            sqlConnection.Close();

            return notes;
        }

        public Note GetById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = $"Select * From dbo.Notes Where Id = {id}";

            List<Note> notes = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            while(sqlDataReader.Read())
            {
                Note note = new Note
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (PriorityEnum)sqlDataReader["Priority"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                };

                notes.Add(note);
            }

            sqlConnection.Close();

            return notes.FirstOrDefault();
        }

        public Note GetByTag(string tag)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            //command.CommandText = $"Select * From dbo.Notes Where Tag = {tag}"; -> sql injection
            command.CommandText = $"Select * From dbo.Notes Where Tag = @param1";
            command.Parameters.AddWithValue("@param1", tag);

            //Select * From dbo.Notes Where Tag = '2; Delete From dbo.Notes'

            List<Note> notes = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Note note = new Note
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (PriorityEnum)sqlDataReader["Priority"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                };

                notes.Add(note);
            }

            sqlConnection.Close();

            return notes.FirstOrDefault();
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}

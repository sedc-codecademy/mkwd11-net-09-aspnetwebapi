using Microsoft.Data.SqlClient;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Enums;
using NotesAndTagsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.DataAccess.Implementations
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private string _connectionString;

        public NoteAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            //bad approach, sql injection
            //command.CommandText ="INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +    
            //    "VALUES(" + entity.Text

            command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
               "VALUES(@text, @priority, @tag, @userId)";//value from outside, from entity

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            //1.Create new connection to SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            //2.open the connection
            sqlConnection.Open();

            //3.Create sql command
            SqlCommand command = new SqlCommand();

            //4.connect the command
            command.Connection = sqlConnection;

            //5.write the command
            command.CommandText = "SELECT * FROM dbo.Notes N INNER JOIN dbo.Users U ON U.Id = N.UserId ";

            List<Note> notesDb = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (PriorityEnum)sqlDataReader["Priority"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"],
                    User = new User
                    {
                        Firstname = (string)sqlDataReader["Firstname"],
                        Lastname = (string)sqlDataReader["Lastname"],
                    }
                });
            }

            //6.close the connection!!
            sqlConnection.Close();

            return notesDb;

        }

        public Note GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}

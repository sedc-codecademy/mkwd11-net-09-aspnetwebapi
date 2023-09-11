using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.NotesApp.DataAccess.Implementations
{
    public class NotesAdoRepository : IRepository<Note>
    {
        private string _connectionString;
        public NotesAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Delete(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "DELETE FROM dbo.Notes WHERE Id = @id";
            sqlCommand.Parameters.AddWithValue("@id", entity.Id);

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<Note> GetAll()
        {
            //1. Create a connection
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection to db
            sqlConnection.Open();
            //3. Create SQL command
            SqlCommand sqlCommand = new SqlCommand();
            //4. Connect the command with the connection
            sqlCommand.Connection = sqlConnection;
            //5. Write the query
            //sqlCommand.CommandText = "SELECT * FROM dbo.Notes n inner join dbo.User u on. u.Id = n.UserId";
            sqlCommand.CommandText = "SELECT * FROM dbo.Notes";

            //6. Get the data using SQLReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<Note> notesDb = new List<Note>();

            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Color = (string)sqlDataReader["Color"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                    //User = new User { Id = (int)sqlDataReader["UserId"],
                    //FirstName = (string) sqlDataReader["FirstName"]}

                });
            }
            //7. Close the connection
            sqlConnection.Close();
            return notesDb;
        }

        public Note GetById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            //sqlCommand.CommandText = $"SELECT * FROM dbo.Notes where Id = {id}"; - bad example, potential sql injection attack

            sqlCommand.CommandText = "SELECT * FROM dbo.Notes where Id = @id";
            sqlCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<Note> notesDb = new List<Note>();

            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Color = (string)sqlDataReader["Color"],
                    Tag = (TagEnum)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                    //User = new User { Id = (int)sqlDataReader["UserId"],
                    //FirstName = (string) sqlDataReader["FirstName"]}

                });
            }

            sqlConnection.Close();
            return notesDb.FirstOrDefault();


            //using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            //{
            //    sqlConnection.Open();
            //    SqlCommand sqlCommand = new SqlCommand();
            //    sqlCommand.Connection = sqlConnection;

            //    //sqlCommand.CommandText = $"SELECT * FROM dbo.Notes where Id = {id}"; - bad example, potential sql injection attack

            //    sqlCommand.CommandText = "SELECT * FROM dbo.Notes where Id = @id";
            //    sqlCommand.Parameters.AddWithValue("@id", id);

            //    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            //    List<Note> notesDb = new List<Note>();

            //    while (sqlDataReader.Read())
            //    {
            //        notesDb.Add(new Note
            //        {
            //            Id = (int)sqlDataReader["Id"],
            //            Text = (string)sqlDataReader["Text"],
            //            Color = (string)sqlDataReader["Color"],
            //            Tag = (TagEnum)sqlDataReader["Tag"],
            //            UserId = (int)sqlDataReader["UserId"]
            //            //User = new User { Id = (int)sqlDataReader["UserId"],
            //            //FirstName = (string) sqlDataReader["FirstName"]}

            //        });
            //    }
            //}
        }

        public void Insert(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "INSERT INTO dbo.Notes (Text, Color, Tag, UserId) " +
                "VALUES(@noteText, @noteColor, @noteTag, @noteUserId)";
            sqlCommand.Parameters.AddWithValue("@noteText", entity.Text);
            sqlCommand.Parameters.AddWithValue("@noteColor", entity.Color);
            sqlCommand.Parameters.AddWithValue("@noteTag", entity.Tag);
            sqlCommand.Parameters.AddWithValue("@noteUserId", entity.UserId);


            // BAD EXAMPLE - potential sql injection attack
            /*
            sqlCommand.CommandText = $@"INSERT INTO Notes (Text, Color, Tag, UserId) 
VALUES('{entity.Text}', '{entity.Color}', {entity.Tag}, {entity.UserId});";
            */

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void Update(Note entity)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;

            sqlCommand.CommandText = "UPDATE dbo.Notes SET Text = @noteText, Color = @noteColor, Tag = @noteTag, UserId =@noteUserId" +
                " WHERE Id = @id";
            sqlCommand.Parameters.AddWithValue("@noteText", entity.Text);
            sqlCommand.Parameters.AddWithValue("@noteColor", entity.Color);
            sqlCommand.Parameters.AddWithValue("@noteTag", entity.Tag);
            sqlCommand.Parameters.AddWithValue("@noteUserId", entity.UserId);
            sqlCommand.Parameters.AddWithValue("@id", entity.Id);

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}

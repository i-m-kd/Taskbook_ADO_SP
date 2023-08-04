using ADOTaskbook.Models;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ADOTaskbook.DataHelper
{
    public class ConnectionHelper
    {
        private readonly string _connectionString;

        public ConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region InsertData
        public void Add(TaskModel task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("dbo.TaskCRUD", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ActionType", "CREATE");
                    command.Parameters.AddWithValue("@Name", task.Name);
                    command.Parameters.AddWithValue("@AssignedBy", task.AssignedBy);
                    command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                    command.Parameters.AddWithValue("@Duration", task.Duration);
                    command.Parameters.AddWithValue("@Date", task.Date);

                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region ViewData
        public List<TaskModel> View()
        {
            List<TaskModel> tasks = new List<TaskModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("dbo.TaskCRUD", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ActionType", "READ");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskModel task = new TaskModel()
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                AssignedBy = reader["AssignedBy"].ToString(),
                                AssignedTo = reader["AssignedTo"].ToString(),
                                Duration = (decimal)reader["Duration"],
                                Date = (DateTime)reader["Date"]
                            };
                            tasks.Add(task);
                        }
                        reader.Close();
                    }
                }
                return tasks;
            }


        }
        #endregion

        #region UpdateData
        public void Update(TaskModel task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("dbo.TaskCRUD", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ActionType", "UPDATE");
                    command.Parameters.AddWithValue("@Id", task.Id);
                    command.Parameters.AddWithValue("@Name", task.Name);
                    command.Parameters.AddWithValue("@AssignedBy", task.AssignedBy);
                    command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                    command.Parameters.AddWithValue("@Duration", task.Duration);
                    command.Parameters.AddWithValue("@Date", task.Date);

                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region DeleteData
        public void Delete(int taskId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("dbo.TaskCRUD", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ActionType", "DELETE");
                    command.Parameters.AddWithValue("@Id", taskId);

                    command.ExecuteNonQuery();
                }
            }
        } 
        #endregion

    }


}


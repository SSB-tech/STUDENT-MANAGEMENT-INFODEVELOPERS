using Microsoft.Data.SqlClient;
using Student_Management__InfoDevelopers_.Models;

namespace Student_Management__InfoDevelopers_.Repository
{
	public class StudentRepo:IRepo
	{
		private readonly IConfiguration _configuration;

		public StudentRepo(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<students> getall()
		{
			using(var conn = new SqlConnection(_configuration.GetConnectionString("connection")))
			{
				var command = new SqlCommand("sp_getallinfo", conn);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				conn.Open();
				using(var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return new students
						{
							Sno = (int)reader["Sno"],
							EnrolledYear = (int)reader["EnrolledYear"],
							Faculty = (string)reader["Faculty"],
							Course = (string)reader["Course"],
							FirstName = (string)reader["FirstName"],
							MiddleName = (string)reader["MiddleName"],
							LastName = (string)reader["LastName"],
							Age = (int)reader["Age"],
							Address = (string)reader["Address"],
							Contact = (string)reader["Contact"],
						};
					}
				}
			}
		}

		public IEnumerable<students> getinfobyid(int id)
		{
			using(var conn = new SqlConnection(_configuration.GetConnectionString("connection")))
			{
				var command = new SqlCommand("sp_getinfobyid", conn);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@Sno",id);	
				conn.Open();
				using(var reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
                        yield return new students
                        {
							Sno = (int)reader["Sno"],
                            EnrolledYear = (int)reader["EnrolledYear"],
                            Faculty = (string)reader["Faculty"],
                            Course = (string)reader["Course"],
                            FirstName = (string)reader["FirstName"],
                            MiddleName = (string)reader["MiddleName"],
                            LastName = (string)reader["LastName"],
                            Age = (int)reader["Age"],
                            Address = (string)reader["Address"],
                            Contact = (string)reader["Contact"],
                        };
                    }
					else
					{
						yield return null;
					}
				}

			}
		}

		public bool insertinfo(students student)
		{
			int id = 0;
			using (var conn = new SqlConnection(_configuration.GetConnectionString("connection")))
			{
				var command = new SqlCommand("sp_insertinfo", conn);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				command.Parameters.AddWithValue("@EnrolledYear", student.EnrolledYear);
				command.Parameters.AddWithValue("@Faculty", student.Faculty);
				command.Parameters.AddWithValue("@Course", student.Course);
				command.Parameters.AddWithValue("@FirstName", student.FirstName);
				command.Parameters.AddWithValue("@MiddleName", student.MiddleName);
				command.Parameters.AddWithValue("@LastName", student.LastName);
				command.Parameters.AddWithValue("@Age", student.Age);
				command.Parameters.AddWithValue("@Address", student.Address);
				command.Parameters.AddWithValue("@Contact", student.Contact);

				conn.Open();
				id = command.ExecuteNonQuery();
				if(id> 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
		}

		public bool updateinfo(students student)
		{
			int id=0;
			using (var conn = new SqlConnection(_configuration.GetConnectionString("connection")))
			{
				var command = new SqlCommand("sp_updateinfo", conn);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.AddWithValue("Sno", student.Sno);
				command.Parameters.AddWithValue("@EnrolledYear", student.EnrolledYear);
				command.Parameters.AddWithValue("@Faculty", student.Faculty);
				command.Parameters.AddWithValue("@Course", student.Course);
				command.Parameters.AddWithValue("@FirstName", student.FirstName);
				command.Parameters.AddWithValue("@MiddleName", student.MiddleName);
				command.Parameters.AddWithValue("@LastName", student.LastName);
				command.Parameters.AddWithValue("@Age", student.Age);
				command.Parameters.AddWithValue("@Contact", student.Contact);
				command.Parameters.AddWithValue("@Address", student.Address);

				conn.Open();
				id=command.ExecuteNonQuery();
				if (id > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public bool deleteinfobyid(int id)
		{
			int i = 0;
			using (var conn = new SqlConnection(_configuration.GetConnectionString("connection")))
			{
				var command = new SqlCommand("sp_deleteinfo", conn);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@Sno", id);
			
				conn.Open();
				i = command.ExecuteNonQuery();
				if (i > 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}

		}
	}
}

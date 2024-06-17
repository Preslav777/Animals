using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsProject.Controllers
{
    public class AnimalsLogic
    {
        private string connectionString;

        public AnimalsLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetAllAnimals()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter animalsAdapter = new SqlDataAdapter(@"SELECT a.*, b.Name AS BreedName FROM Animals a JOIN Breeds b ON a.BreedId = b.Id", conn);
                DataTable animalsTable = new DataTable();
                animalsAdapter.Fill(animalsTable);
                return animalsTable;
            }
        }

        public void AddAnimal(string name, string description, float age, int breedId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = "INSERT INTO Animals (Name, Description, Age, BreedId) VALUES (@Name, @Description, @Age, @BreedId)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@BreedId", breedId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAnimal(int id, string name, string description, float age, int breedId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string updateQuery = "UPDATE Animals SET Name = @Name, Description = @Description, Age = @Age, BreedId = @BreedId WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Age", age);
                    cmd.Parameters.AddWithValue("@BreedId", breedId);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAnimal(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Animals WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsProject.Controllers
{
    public class BreedsLogic
    {
        private string connectionString;

        public BreedsLogic(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetAllBreeds()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter breedsAdapter = new SqlDataAdapter("SELECT * FROM Breeds", conn);
                DataTable breedsTable = new DataTable();
                breedsAdapter.Fill(breedsTable);
                return breedsTable;
            }
        }

        public string GetBreedNameById(int breedId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Name FROM Breeds WHERE Id = @BreedId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BreedId", breedId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
    }
}

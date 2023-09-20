using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class UserService : IUserService
    {
        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private string csvFilePath;

        // Initiate Constructor
        public UserService()
        {
            csvFilePath = Path.Combine(projectDirectory, "healthcare_system", "app_data", "user.csv");
        }

        // authenticateUser() is used to make sure that the user is signed in and will return the users role
        public UserDTO AuthenticateUser(int userId, string password)
        {
            List<UserDTO> users = LoadUserList();

            foreach (UserDTO user in users)
            {
                if (user.UserId == userId && user.Password == password)
                {
                    return user;
                }
            }

            return null; // Return null if no matching user is found
        }

        // loadUserList() will get load up the CSV file to get the user data when authenticated
        public List<UserDTO> LoadUserList()
        {
            List<UserDTO> userList = new List<UserDTO>();

            // Read the file line by line
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                // Skip the header row
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Split each line by comma to get the fields
                    string[] fields = line.Split(',');

                    // Create a new UserDTO object and populate its properties
                    UserDTO user = new UserDTO
                    {
                        UserId = int.Parse(fields[0]),
                        Role = fields[1],
                        FirstName = fields[2],
                        LastName = fields[3],
                        Email = fields[4],
                        Password = fields[5],
                        Address = fields[6],
                        Phone = fields[7],
                        Description = fields[8]
                    };

                    // Add the UserDTO object to the list
                    userList.Add(user);
                }
            }

            return userList;
        }
    }
}

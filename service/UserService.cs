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
        public UserDTO AuthenticateUser(int userId, string password)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string csvFilePath = Path.Combine(projectDirectory, "healthcare_system", "app_data", "user.csv");

            List<UserDTO> users = LoadData(csvFilePath);

            foreach (UserDTO user in users)
            {
                if (user.UserId == userId && user.Password == password)
                {
                    return user;
                }
            }

            return null; // Return null if no matching user is found
        }

        // This will List ALL users
        public void ListAllUsers()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string csvFilePath = Path.Combine(projectDirectory, "healthcare_system", "app_data", "user.csv");

            UserService userService = new UserService();
            List<UserDTO> users = userService.LoadData(csvFilePath);

            foreach (UserDTO user in users)
            {
                Console.WriteLine($"UserId: {user.UserId}, Role: {user.Role}, FirstName: {user.FirstName}, LastName: {user.LastName}, Email: {user.Email}, Password: {user.Password}");
            }
        }
        
        // This will load all data from the CSV
        public List<UserDTO> LoadData(string csvFilePath)
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
                        Password = fields[5]
                    };

                    // Add the UserDTO object to the list
                    userList.Add(user);
                }
            }

            return userList;
        }

       
    }
}

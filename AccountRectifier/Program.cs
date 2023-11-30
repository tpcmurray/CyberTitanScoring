using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management;
using System.IO;
using System.Linq;
using System.Collections;

namespace AccountRectifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CyberTitan GFW Account Rectifier");

            Console.WriteLine(" - Reading text files ...");
            List<string> adminList = ReadFile("admins.txt");
            List<string> userList = ReadFile("users.txt");

            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);

            List<string> currentAdmins = GetWindowsAdministrators(localMachine);
            List<string> currentUsers = GetWindowsUsers(currentAdmins, localMachine);

            UpdateWindowsAdministrators(currentAdmins, adminList);
            UpdateWindowsUsers(currentUsers, userList);

            Console.WriteLine("Windows 10 user and admin lists have been synchronized.");
            Console.ReadLine();
        }

        static List<string> ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath).ToList();
            } else
            {
                Console.WriteLine($"File not found: {filePath}");
                return new List<string>();
            }
        }

        static List<string> GetWindowsAdministrators(DirectoryEntry localMachine)
        {
            Console.WriteLine(" - Finding existing admins ...");
            List<string> admins = new List<string>();

            DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                admins.Add(member.Name);
            }

            Console.WriteLine("    - Found: " + String.Join(",", admins.Select(x => x.ToString()).ToArray()));
            return admins;
        }

        static List<string> GetWindowsUsers(List<string> currentAdmins, DirectoryEntry localMachine)
        {
            Console.WriteLine(" - Finding existing users ...");
            List<string> users = new List<string>();

            DirectoryEntry admGroup = localMachine.Children.Find("users", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                if (member.Name != "INTERACTIVE" && member.Name != "Authenticated Users" && !currentAdmins.Contains(member.Name))
                {
                    users.Add(member.Name);
                }
            }

            Console.WriteLine("    - Found: " + String.Join(",", users.Select(x => x.ToString()).ToArray()));
            return users;
        }

        static void UpdateWindowsUsers(List<string> currentUsers, List<string> desiredUsers)
        {
            AddMissingUsers(currentUsers, desiredUsers);
            RemoveExtraUsers(currentUsers, desiredUsers);
        }

        static void AddMissingUsers(List<string> currentUsers, List<string> desiredUsers)
        {
            var usersToAdd = desiredUsers.Except(currentUsers).ToList();

            foreach (var user in usersToAdd)
            {
                // Implement logic to add user to Windows
                CreateUser(user);
                Console.WriteLine($"Adding user: {user}");
            }
        }

        static void RemoveExtraUsers(List<string> currentUsers, List<string> desiredUsers)
        {
            var usersToRemove = currentUsers.Except(desiredUsers).ToList();

            foreach (var user in usersToRemove)
            {
                // Implement logic to remove user from Windows
                DeleteUser(user);
                Console.WriteLine($"Removing user: {user}");
            }
        }

        static void UpdateWindowsAdministrators(List<string> currentAdmins, List<string> desiredAdmins)
        {
            AddMissingAdmins(currentAdmins, desiredAdmins);
            RemoveExtraAdmins(currentAdmins, desiredAdmins);
        }

        static void AddMissingAdmins(List<string> currentAdmins, List<string> desiredAdmins)
        {
            var adminsToAdd = desiredAdmins.Except(currentAdmins).ToList();

            foreach (var admin in adminsToAdd)
            {
                // Implement logic to add admin to Windows administrators group
                AddUserToAdministratorsGroup(admin);
                Console.WriteLine($"Adding admin: {admin}");
            }
        }

        static void RemoveExtraAdmins(List<string> currentAdmins, List<string> desiredAdmins)
        {
            var adminsToRemove = currentAdmins.Except(desiredAdmins).ToList();

            foreach (var admin in adminsToRemove)
            {
                // Implement logic to remove admin from Windows administrators group
                RemoveUserFromAdministratorsGroup(admin);
                Console.WriteLine($"Removing admin: {admin}");
            }
        }

        // Helper methods for user and administrator management

        static void CreateUser(string username)
        {
            // Implement logic to create a new user
            // For demonstration purposes, let's print a message
            Console.WriteLine($"Creating user: {username}");
        }

        static void DeleteUser(string username)
        {
            // Implement logic to delete a user
            // For demonstration purposes, let's print a message
            Console.WriteLine($"Deleting user: {username}");
        }

        static void AddUserToAdministratorsGroup(string username)
        {
            // Implement logic to add a user to the administrators group
            // For demonstration purposes, let's print a message
            Console.WriteLine($"Adding user to administrators group: {username}");
        }

        static void RemoveUserFromAdministratorsGroup(string username)
        {
            // Implement logic to remove a user from the administrators group
            // For demonstration purposes, let's print a message
            Console.WriteLine($"Removing user from administrators group: {username}");
        }
    }
}
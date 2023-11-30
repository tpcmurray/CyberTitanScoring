using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management;
using System.IO;
using System.Linq;
using System.Collections;
using System.DirectoryServices.AccountManagement;

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
            Console.WriteLine("    - Desired Admins: " + String.Join(", ", adminList.Select(x => x.ToString().Trim()).ToArray()));
            Console.WriteLine("    - Desired Users: " + String.Join(", ", userList.Select(x => x.ToString().Trim()).ToArray()));

            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);

            Console.WriteLine(" - Reading account data ...");
            List<string> currentAdmins = GetWindowsAdministrators(localMachine);
            List<string> currentUsers = GetWindowsUsers(currentAdmins, localMachine);

            Console.WriteLine();
            UpdateUsers(adminList, userList, currentAdmins, currentUsers);

            Console.WriteLine(" - Windows user and admin lists have been synchronized.");
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
            List<string> admins = new List<string>();

            DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                admins.Add(member.Name);
            }

            Console.WriteLine("    - Existing admins: " + String.Join(", ", admins.Select(x => x.ToString()).ToArray()));
            return admins;
        }

        static List<string> GetWindowsUsers(List<string> currentAdmins, DirectoryEntry localMachine)
        {
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

            Console.WriteLine("    - Existing users: " + String.Join(", ", users.Select(x => x.ToString()).ToArray()));
            return users;
        }

        static void UpdateUsers(List<string> adminList, List<string> userList, List<string> currentAdmins, List<string> currentUsers)
        {
            Console.WriteLine(" - Fixing accounts ...");

            // admin missing: add or promote
            foreach (string admin in adminList) { 
                if (!currentAdmins.Contains(admin))
                {
                    if (currentUsers.Contains(admin))
                    {
                        Console.WriteLine("    - Promoting user to admin: " + admin);
                        AddUserToAdminGroup(admin);
                    } else
                    {
                        Console.WriteLine("    - Creating new admin: " + admin);
                        CreateAdminUser(admin, "1234!@#$bbUU");
                    }
                }
            }

            // admin and shouldn't exist: disable or demote
            foreach (string admin in currentAdmins)
            {
                if (!adminList.Contains(admin))
                {
                    if (currentUsers.Contains(admin))
                    {
                        Console.WriteLine("    - Demote admin to user: " + admin);
                        RemoveUserFromAdminGroup(admin);
                    } else
                    {
                        Console.WriteLine("    - Disabling admin: " + admin);
                        RemoveUserFromAdminGroup(admin);
                        DisableUser(admin);
                    }
                }
            }

            // user missing: add or demote
            foreach (string user in userList)
            {
                if (!currentUsers.Contains(user))
                {
                    if (currentAdmins.Contains(user))
                    {
                        Console.WriteLine("    - Removing user from admins: " + user);
                        RemoveUserFromAdminGroup(user);
                    } else
                    {
                        Console.WriteLine("    - Creating new user: " + user);
                        CreateNonAdminUser(user, "1234!@#$bbUU");
                    }
                }
            }

            // userand shouldn't exist: disable 
            foreach (string user in currentUsers)
            {
                if (!userList.Contains(user))
                {
                    Console.WriteLine("    - Disabling user: " + user);
                    DisableUser(user);
                }
            }
        }

        public static bool CreateAdminUser(string username, string password)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry NewUser = AD.Children.Add(username, "user");
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", "Cyber Titan User" });
                NewUser.CommitChanges();

                DirectoryEntry grp;
                grp = AD.Children.Find("Users", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
                grp = AD.Children.Find("Administrators", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
            } catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public static bool CreateNonAdminUser(string username, string password)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry NewUser = AD.Children.Add(username, "user");
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", "Cyber Titan User" });
                NewUser.CommitChanges();

                DirectoryEntry grp;
                grp = AD.Children.Find("Users", "group");
                if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
            } catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public static bool DisableUser(string username)
        {
            try
            {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntries MyEntries = AD.Children;
                DirectoryEntry User = MyEntries.Find(username, "user");
                User.Properties["UserFlags"].Value = (int)User.Properties["UserFlags"].Value | 0x0002;
                User.CommitChanges();
                User.Close();
            } catch
            {
                return false;
            }
            return true;
        }

        public static bool RemoveUserFromAdminGroup(string username)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, null))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, "Administrators");
                group.Members.Remove(pc, IdentityType.Name, username);
                group.Save();
            }

            return true;
        }

        public static bool AddUserToAdminGroup(string username)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, null))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, "Administrators");
                group.Members.Add(pc,IdentityType.Name, username);
                group.Save();
            }

            return true;
        }
    }
}
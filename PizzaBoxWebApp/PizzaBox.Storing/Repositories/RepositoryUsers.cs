﻿using System;
using System.Collections.Generic;
using System.Text;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace PizzaBox.Storing.Repositories
{
    public class RepositoryUsers : IRepository<Users>
    {
        PizzaDBContext db;

        public RepositoryUsers()
        {
            db = new PizzaDBContext();
        }
        public RepositoryUsers(PizzaDBContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(Users item)
        {


            if (db.Users.Any(e => e.Email == item.Email) || item.Email == null)
            {
                Console.WriteLine("user already with this email exists");
                return;
            }
            else
            {
                db.Users.Add(item);
                db.SaveChanges();
                Console.WriteLine("User craeted successfully");
                
            }
            
        }

        public IEnumerable<Users> GetItems()
        {
            var query = from e in db.Users
                        select e;

            return query;
        }

        public void Modify(Users item)
        {
            if (db.Users.Any(e => e.Email == item.Email))
            {
               
                Users updateUser = db.Users.FirstOrDefault(e => e.Email == item.Email);
                updateUser.Password = item.Password;
                updateUser.FirstName = item.FirstName;
                updateUser.LastName = item.LastName;
                
                db.Users.Update(updateUser);
                db.SaveChanges();
                Console.WriteLine("User updated Successfully");
            }
            else
            {
                Console.WriteLine("Could not update user because it does not exists");
            }
            
        }

        public void Remove(string id)
        {
            Users u = db.Users.FirstOrDefault(e => e.Email == id);
            if (u.Email == id)
            {
                db.Remove(u);
                db.SaveChanges();
                Console.WriteLine("User Removed Successfully");
            }
            else
            {
                Console.WriteLine("Could not find user with this email");
            }
        }

        public void SignIn(string email, string password, ref Users u)
        {
            foreach(Users user in db.Users)
            {
                if (user.Email == email)
                {
                    if (user.Password == password)
                    {
                        u = new Users()
                        {
                          Email = user.Email,
                          Password = user.Password,
                          FirstName = user.FirstName,
                          LastName = user.LastName,
                          
                        };//email and password matched
                        return;
                    }
                    u = null; //email matched but not the password
                    return;
                }
            }
            u = null; //no email found

        }

        
    }
}

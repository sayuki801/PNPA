using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using PNPA.Data.Entities;
using PNPA.Models;
using System.Data;
using System;
using System.Text;
using XSystem.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;

namespace PNPA.Data.Repo
{
    public class Repo 
    {
        DataContext? dataContext;

        public UsersModel GetUserByUsernameAndPassword(UsersModel model)
        {
            try
            {
                User user = new User();

                using (dataContext = new DataContext())
                {
                    user = dataContext.Users.Where(u => u.Username == model.Username)
                                            .Include(r => r.Roles)
                                            .FirstOrDefault();

                    if (user is not null)
                    {
                        if (Verify(model.Password, user.Password))
                        {
                            return MapUserEntityToModel(user);
                        }
                        else
                        {
                            return null;
                        }
                    }

                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        #region Private Methods
        private static string PasswordHash(string password)
        {
            //Using MD5 to encrypt a string
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                //Hash data
                byte[] data = md5.ComputeHash(utf8.GetBytes(password));
                return Convert.ToBase64String(data);
            }
        }

        private bool Verify(string password, string storedPassword)
        {
            var pass = PasswordHash(password);
            if (pass.Equals(storedPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region EntityToModelMapper
        private UsersModel MapUserEntityToModel(User entity)
        {
            UsersModel model = new UsersModel()
            {
                Id = entity.ID,
                Username = entity.Username,
                Password = entity.Password,
            };

            if (entity.Roles is not null)
            {
                model.Role = MapRoleEntityToModel(entity.Roles);
            }


            return model;
        }

        private RankModel MapRankEntityToModel(Rank entity)
        {
            RankModel model = new RankModel()
            {
                Id = entity.ID,
                RankABBR = entity.RankABBR,
                Description = entity.Description
            };

            return model;
        }

        private RoleModel MapRoleEntityToModel(Role entity)
        {
            RoleModel model = new RoleModel()
            {
                Id = entity.ID,
                Description = entity.Description
            };

            return model;
        }
        #endregion

        #region ModelToEntityMapper

        private Rank MapRankModelToEntity(RankModel model)
        {
            Rank entity = new Rank()
            {
                ID = model.Id,
                RankABBR = model.RankABBR,
                Description = model.Description
            };

            return entity;
        }

        private Role MapRoleModelToEntity(RoleModel model)
        {
            Role entity = new Role()
            {
                ID = model.Id,
                Description = model.Description
            };

            return entity;
        }

        #endregion
    }
}

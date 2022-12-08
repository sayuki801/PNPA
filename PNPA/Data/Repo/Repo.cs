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

        public bool userRegistration(PersonsInfoModel model)
        {
            using (dataContext = new DataContext())
            {
                try
                {
                    var test1 = PasswordHash("Password");
                    

                    var userId = dataContext.Users
                        .Where(u => u.Username == model)
                        .FirstOrDefault().ID;

                    var userInfo = MapUserInfoModelToEntity(model);
                    userInfo.Rank = null;
                    userInfo.RankID = model.Rank.Id;
                    userInfo.User = null;
                    userInfo.UserID = userId;

                    dataContext.UsersInfo.Add(userInfo);
                    dataContext.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                    throw;
                }
            }
            return true;
        }

        public List<UserInfoModel> GetUserList()
        {
            using (dataContext = new dataContext())
            {
                try
                {


                    var userinfo = dataContext.UsersInfo
                       .Include(r => r.Rank)
                       .Include(u => u.User)
                           .ThenInclude(s => s.Sections)
                       .Include(u => u.User)
                           .ThenInclude(ro => ro.Roles)
                       .ToList();

                    return MapListUserInfoEntityToModel(userinfo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<RankModel> GetRank()
        {
            using (dataContext = new dataContext())
            {
                List<Ranks> rank = new List<Ranks>();
                rank = dataContext.Ranks.ToList();
                return MapListRankEntityToModel(rank);
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
        private UserModel MapUserEntityToModel(Users entity)
        {
            UserModel model = new UserModel()
            {
                Id = entity.ID,
                Username = entity.Username,
                Password = entity.Password,
            };

            if (entity.Roles is not null)
            {
                model.Role = MapRoleEntityToModel(entity.Roles);
            }

            if (entity.Sections is not null)
            {
                model.Section = MapSectionEntityToModel(entity.Sections);
            }

            return model;
        }

        private UserInfoModel MapUserInfoEntityToModel(UsersInfo entity)
        {
            UserInfoModel model = new UserInfoModel()
            {
                Id = entity.ID,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Age = entity.Age,
                Gender = entity.Gender,
                EmailAddress = entity.EmailAddress,
                MobileNum = entity.MobileNum,
                BirthDate = entity.BirthDate,
                BadgeNumber = entity.BadgeNumber
            };

            if (entity.User is not null)
            {
                model.User = MapUserEntityToModel(entity.User);
            }

            if (entity.Rank is not null)
{
model.Rank = MapRankEntityToModel(entity.Rank);
}
return model;
}

        private SectionModel MapSectionEntityToModel(Sections entity)
{
            SectionModel model = new SectionModel()
            {
                Id = entity.ID,
                Description = entity.Description
};
return model;
}

        private RankModel MapRankEntityToModel(Ranks entity)
        {
            RankModel model = new RankModel()
            {
                Id = entity.ID,
                RankABBR = entity.RankABBR,
                Description = entity.Description
            };
return model;
}

        private RoleModel MapRoleEntityToModel(Roles entity)
        {
            RoleModel model = new RoleModel()
            {
                Id = entity.ID,
                Description = entity.Description
            };

            return model;
        }

        private UnitModel MapUnitEntityToModel(Units entity)
        {
            UnitModel model = new UnitModel()
            {
                Id = entity.ID,
                UnitABBR = entity.UnitABBR,
                Description = entity.Description
            };

            return model;
        }

        private ItemModel MapItemEntityToModel(Items entity)
        {
            ItemModel model = new ItemModel()
            {
                Id = entity.ID,
                ItemName = entity.ItemName
            };

            return model;
        }

        private List<UserInfoModel> MapListUserInfoEntityToModel(List<UsersInfo> entities)
        {
            List<UserInfoModel> models = new List<UserInfoModel>();

            foreach (var entity in entities)
            {
                models.Add(new UserInfoModel
                {
                    Id = entity.ID,
                    FirstName = entity.FirstName,
                    MiddleName = entity.MiddleName,
                    LastName = entity.LastName,
                    Age = entity.Age,
                    Gender = entity.Gender,
                    EmailAddress = entity.EmailAddress,
                    MobileNum = entity.MobileNum,
                    BirthDate = entity.BirthDate,
                    BadgeNumber = entity.BadgeNumber,
                    User = MapUserEntityToModel(entity.User),
                    Rank = entity.Rank != null ? MapRankEntityToModel(entity.Rank) : null
                }); ;
            }

            return models;
}
private List<SectionModel> MapListSectionEntityToModel(List<Sections> entities)
{
List<SectionModel> models = new List<SectionModel>();
foreach (var entity in entities)
{
                models.Add(new SectionModel
                {
Id = entity.ID,
                    Description = entity.Description
                });
            }

            return models;
        }

        private List<RankModel> MapListRankEntityToModel(List<Ranks> entities)
{
            List<RankModel> models = new List<RankModel>();

            foreach (var entity in entities)
            {
                models.Add(new RankModel
                {
                    Id = entity.ID,
                    RankABBR = entity.RankABBR,
                    Description = entity.Description
                });

            }
            return models;
        }

        private List<RoleModel> MapListRoleEntityToModel(List<Roles> entities)
{
            List<RoleModel> models = new List<RoleModel>();

            foreach (var entity in entities)
            {
                models.Add(new RoleModel
                {
                    Id = entity.ID,
                    Description = entity.Description
});
            }

            return models;
        }
        #endregion

        #region ModelToEntityMapper
        private Users MapUserModelToEntity(UserModel model)
        {
            Users entity = new Users()
            {
                ID = model.Id,
                Username = model.Username,
Password = model.Password,
                RoleID = model.Role.Id,
                SectionID = model.Section.Id

            };

            if (model.Role is not null)
            {
                entity.Roles = MapRoleModelToEntity(model.Role);
            }

            if (model.Section is not null)
            {
                entity.Sections = MapSectionModelToEntity(model.Section);
            }

            return entity;
        }

        private UsersInfo MapUserInfoModelToEntity(UserInfoModel model)
        {
            UsersInfo entity = new UsersInfo()
            {
                ID = model.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Age = model.Age,
                Gender = model.Gender,
                EmailAddress = model.EmailAddress,
                MobileNum = model.MobileNum,
                BirthDate = model.BirthDate,
                BadgeNumber = model.BadgeNumber
            };

            if (model.User is not null)
            {
                entity.User = MapUserModelToEntity(model.User);
            }

            if (model.Rank is not null)
{
entity.Rank = MapRankModelToEntity(model.Rank);
}
return entity;
        }

        private Sections MapSectionModelToEntity(SectionModel model)
        {
            Sections entity = new Sections()
            {
                ID = model.Id,
                Description = model.Description
            };

            return entity;
}
private Ranks MapRankModelToEntity(RankModel model)
{
            Ranks entity = new Ranks()
{
                ID = model.Id,
RankABBR = model.RankABBR,
                Description = model.Description
            };

            return entity;
        }
private Roles MapRoleModelToEntity(RoleModel model)
{
            Roles entity = new Roles()
            {
                ID = model.Id,
                Description = model.Description
            };

            return entity;
        }

        private Units MapUnityModelToEntity(UnitModel model)
{
Units entity = new Units()
{
                ID = model.Id,
                UnitABBR = model.UnitABBR,
                Description = model.Description
            };

            return entity;
        }

        private Items MapItemModelToEntity(ItemModel model)
        {
            Items entity = new Items()
            {
                ID = model.Id,
                ItemName = model.ItemName
            };

            return entity;
        }
        #endregion
    }
}

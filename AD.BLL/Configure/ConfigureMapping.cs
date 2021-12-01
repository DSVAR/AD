using AD.BLL.ModelsDTO;
using AD.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AD.BLL.Configure
{
    public class ConfigureMapping:Profile
    {
        public ConfigureMapping()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}

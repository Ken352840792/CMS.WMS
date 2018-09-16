using AutoMapper;
using Sy.Base;
using Sy.Module.DTOModel;
using Sy.Module.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Module.Mapper
{
   public class UserMapper: Profile, IProfile
    {
        public UserMapper()
        {
            CreateMap<InDto_AddUser, User>().ReverseMap();
        }
    }
}

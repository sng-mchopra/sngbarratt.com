using AutoMapper;
using jCtrl.Services.Core.Domain;
using jCtrl.WebApi.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jCtrl.WebApi.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x => 
            {
                x.CreateMap<Tweet, TweetReturnModel>();
            });

        }
    }
}
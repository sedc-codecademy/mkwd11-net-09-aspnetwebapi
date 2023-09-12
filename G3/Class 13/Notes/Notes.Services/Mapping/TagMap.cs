﻿using AutoMapper;
using Notes.Data.Domain;
using Notes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Mapping
{
    public class TagMap : Profile
    {
        public TagMap()
        {
            CreateMap<Tag, TagModel>();
        }
    }
}

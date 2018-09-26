using Grocerly.Database;
using Grocerly.Database.Pocos;
using ServiceStack;
using System;
using System.Net;
using System.Linq;
using Grocerly.ServiceModel;

namespace Grocerly.Interface
{
    public class TagService : Service
    {
        private GrocerlyContext Orm;

        public TagService(GrocerlyContext orm)
        {
            this.Orm = orm;
        }

        public HttpResult Get(GetTags request)
        {
            var tags = (from s in Orm.Tags
                        select FillObject(s)).ToList();

            return new HttpResult(tags, HttpStatusCode.OK);
        }

        public HttpResult GetTag(GetTag request)
        {
            var tag = (from s in Orm.Tags
                       select FillObject(s));

            return new HttpResult(tag, HttpStatusCode.OK);
        }

        public HttpResult GetTagByName(GetTagByName request)
        {
            var tag = (from s in Orm.Tags
                       select FillObject(s));

            return new HttpResult(tag, HttpStatusCode.OK);
        }

        private TagsResponse FillObject(Tags data)
        {
            return new TagsResponse
            {
                Id = data.Id,
                Name = data.Name
            };
        }

        
    }
}

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
            var tags = Orm.Tags.Select(t => FillObject(t)).ToList();
            return new HttpResult(tags, HttpStatusCode.OK);
        }

        public HttpResult Get(GetTag request)
        {
            var tag = Orm.Tags.FirstOrDefault(t => t.Id.Equals(request.Id));
            return new HttpResult(FillObject(tag), HttpStatusCode.OK);
        }

        public HttpResult Get(GetTagByName request)
        {
            var tag = Orm.Tags.FirstOrDefault(t => t.Name.Equals(request.Name));
            return new HttpResult(FillObject(tag), HttpStatusCode.OK);
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

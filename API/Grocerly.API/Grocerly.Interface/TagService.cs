using ServiceStack;
using System;
using System.Net;
using static Grocerly.ServiceModel.Tag;

namespace Grocerly.Interface
{
    public class TagService : Service
    {
        public HttpResult Get(GetTags request)
        {
            //FIX LATER
            return new HttpResult(HttpStatusCode.OK);
        }
    }
}

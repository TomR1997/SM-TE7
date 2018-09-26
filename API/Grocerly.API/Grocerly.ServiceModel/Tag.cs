using ServiceStack;
using System;
using System.Collections.Generic;

namespace Grocerly.ServiceModel
{
    [Route("/tags/", "GET")]
    public class GetTags : IReturn<List<TagsResponse>>
    {
    }

    public class TagsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

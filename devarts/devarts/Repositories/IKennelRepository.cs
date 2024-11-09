using devarts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Repositories
{
    public interface IKennelRepository
    {
        IQueryable<Document> GetDocumetsList();        

        IQueryable<PuppieOfBitch> GetAllPuppiesList();
        IQueryable<PuppieOfBitch> GetPuppiesByBitchList(int bitchId);
    }
}
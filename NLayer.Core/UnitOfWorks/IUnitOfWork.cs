using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync(); // DbContext 'in SaveChangesAsync() metodu

        void Commit(); // DbContext 'in SaveChanges() metodu
    }
}

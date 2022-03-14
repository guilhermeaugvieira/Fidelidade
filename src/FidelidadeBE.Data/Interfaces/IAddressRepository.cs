using System.Linq.Expressions;
using FidelidadeBE.Business.Entities;

namespace FidelidadeBE.Data.Interfaces;

public interface IAddressRepository
{
    void Update(Address address);
}
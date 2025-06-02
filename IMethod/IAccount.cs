using ProjectPlanner.Models;

namespace ProjectPlanner.IMethod
{
    public interface IAccount
    {
        int SaveNewEntry(RegistrationViewModel regs);
        List<RegistrationModel> GetRegistrations();
    }
}

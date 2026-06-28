using LekuTrans.Data.Models;

namespace LekuTrans.Services.Interfaces;

public interface IAssignmentService
{
    Task<Assignment> AssignVehicleAsync(long orderId, long vehicleId, long driverId);
    Task CompleteAssignmentAsync(long assignmentId);
}
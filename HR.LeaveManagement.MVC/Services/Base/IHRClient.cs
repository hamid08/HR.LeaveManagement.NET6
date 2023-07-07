namespace HR.LeaveManagement.MVC.Services.Base
{
    public partial interface IHRClient
    {
        public HttpClient HttpClient { get; }

        Task<AuthResponse> LoginAsync(AuthRequest body);

        Task<AuthResponse> LoginAsync(AuthRequest body, CancellationToken cancellationToken);

        Task<RegistrationResponse> RegisterAsync(RegistrationRequest body);

        Task<RegistrationResponse> RegisterAsync(RegistrationRequest body, CancellationToken cancellationToken);

        Task<ICollection<LeaveAllocationDto>> LeaveAllocationsAllAsync(bool? isLoggedInUser);

        Task<ICollection<LeaveAllocationDto>> LeaveAllocationsAllAsync(bool? isLoggedInUser, CancellationToken cancellationToken);

        Task<BaseCommandResponse> LeaveAllocationsPOSTAsync(CreateLeaveAllocationDto body);

        Task<BaseCommandResponse> LeaveAllocationsPOSTAsync(CreateLeaveAllocationDto body, CancellationToken cancellationToken);

        Task LeaveAllocationsPUTAsync(UpdateLeaveAllocationDto body);

        Task LeaveAllocationsPUTAsync(UpdateLeaveAllocationDto body, CancellationToken cancellationToken);

        Task<LeaveAllocationDto> LeaveAllocationsGETAsync(int id);

        Task<LeaveAllocationDto> LeaveAllocationsGETAsync(int id, CancellationToken cancellationToken);

        Task LeaveAllocationsDELETEAsync(int id);

        Task LeaveAllocationsDELETEAsync(int id, CancellationToken cancellationToken);

        Task<ICollection<LeaveRequestListDto>> LeaveRequestsAllAsync(bool? isLoggedInUser);

        Task<ICollection<LeaveRequestListDto>> LeaveRequestsAllAsync(bool? isLoggedInUser, CancellationToken cancellationToken);

        Task<BaseCommandResponse> LeaveRequestsPOSTAsync(CreateLeaveRequestDto body);

        Task<BaseCommandResponse> LeaveRequestsPOSTAsync(CreateLeaveRequestDto body, CancellationToken cancellationToken);

        Task<LeaveRequestDto> LeaveRequestsGETAsync(int id);

        Task<LeaveRequestDto> LeaveRequestsGETAsync(int id, CancellationToken cancellationToken);

        Task LeaveRequestsPUTAsync(int id, UpdateLeaveRequestDto body);

        Task LeaveRequestsPUTAsync(int id, UpdateLeaveRequestDto body, CancellationToken cancellationToken);

        Task LeaveRequestsDELETEAsync(int id);

        Task LeaveRequestsDELETEAsync(int id, CancellationToken cancellationToken);

        Task ChangeapprovalAsync(int id, ChangeLeaveRequestApprovalDto body);


        Task ChangeapprovalAsync(int id, ChangeLeaveRequestApprovalDto body, CancellationToken cancellationToken);


        Task<ICollection<LeaveTypeDto>> LeaveTypesAllAsync();

        Task<ICollection<LeaveTypeDto>> LeaveTypesAllAsync(CancellationToken cancellationToken);


        Task<BaseCommandResponse> LeaveTypesPOSTAsync(CreateLeaveTypeDto body);


        Task<BaseCommandResponse> LeaveTypesPOSTAsync(CreateLeaveTypeDto body, CancellationToken cancellationToken);


        Task<LeaveTypeDto> LeaveTypesGETAsync(int id);


        Task<LeaveTypeDto> LeaveTypesGETAsync(int id, CancellationToken cancellationToken);


        Task LeaveTypesPUTAsync(string id, LeaveTypeDto body);


        Task LeaveTypesPUTAsync(string id, LeaveTypeDto body, CancellationToken cancellationToken);


        Task LeaveTypesDELETEAsync(int id);


        Task LeaveTypesDELETEAsync(int id, CancellationToken cancellationToken);

    }
}

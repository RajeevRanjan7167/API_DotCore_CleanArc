using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Application.Responces;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommonResponce>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
            ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponce> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var responce = new BaseCommonResponce();
            var validator = new CreateLeaveRequestDtoValidator(this._leaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request.LeaveRequestDto);
            if (!validatorResult.IsValid)
            {
                responce.Success = false;
                responce.Message = "Creation Failed";
                responce.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ValidationException(validatorResult);
            }

            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

            responce.Success= true;
            responce.Message = "Creation Successful";
            responce.Id = leaveRequest.Id;
            return responce;
        }
    }
}

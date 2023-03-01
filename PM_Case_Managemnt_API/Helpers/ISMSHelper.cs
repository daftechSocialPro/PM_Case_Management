namespace PM_Case_Managemnt_API.Helpers
{
    public interface ISMSHelper
    {
        public Task<bool> MessageSender(string reciver, string message, string UserId, Guid? orgId = null);
        public Task<bool> UnlimettedMessageSender(string reciver, string message, string UserId, Guid? orgId = null);
        public Task<bool> SendSmsForCase(Guid caseId, Guid caseHistoryId, string userId);

    }
}

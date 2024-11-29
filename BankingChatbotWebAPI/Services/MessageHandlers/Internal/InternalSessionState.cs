﻿namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal;

public enum InternalSessionState
{
    GREETING,
    PRINCIPAL_MENU,
    ACCOUNT_INQUIRY_PRINCIPAL_MENU,
    TRANSACTION_PRINCIPAL_MENU,
    SERVICE_PRINCIPAL_MENU,
    CLAIM_PRINCIPAL_MENU,
    BRANCH_PRINCIPAL_MENU,
    ACCOUNT_INQUIRY_BALANCE_CHOOSE_ACCOUNT_MENU,
    ACCOUNT_INQUIRY_RIB_CHOOSE_ACCOUNT_MENU,
    ACCOUNT_INQUIRY_HISTORY_CHOOSE_ACCOUNT_MENU,
}

using System.Transactions;

namespace BankingChatbotWebAPI.Services.MessageHandlers.Internal;

public static class InternalReturnMessage
{
    public const string WELCOME_MESSAGE = $@"Désolé je ne comprends pas le contexte de votre demande
                                            Choisissez parmi les options suivantes :\n\n
                                            1. Consulter votre compte\n
                                            2. Gérer les transactions\n
                                            3. Demander un service\n
                                            4. Gérer les réclamations\n
                                            5. Où sont nos agences et GAB?";

    public const string DEFAULT_NO_MATCH_MESSAGE = $@"Bonjour, Je suis Aiwa et je suis chargé de répondre à vos demandes.
                                            Comment puis-je vous aider? :\n\n
                                            1. Consulter votre compte\n
                                            2. Gérer les transactions\n
                                            3. Demander un service\n
                                            3. Gérer les réclamations\n
                                            3. Où sont nos agences et GAB?";

    public const string PRINCIPAL_MENU_ACCOUNT_INQUIRY_CHOICES = $@"Consultation de compte :\n\n
                                            1. Solde actuel\n
                                            2. Relevé d'identité bancaire\n
                                            3. Informations sur votre gestionnaire\n
                                            4. Mini-relevé de compte\n";

    public const string ACCOUNT_INQUIRY_CHOOSE_ACCOUNT = $@"Selection du compte :\n\n
                                            1. Compte épargne\n
                                            2. Compte courant\n";

    public const string ACCOUNT_BALANCE_TEMPLATE = $@"Votre solde est de @Amount @Currency depuis @From.";

    public const string ACCOUNT_RIB_TEMPLATE = $@"Votre RIB est le suivant: \n\n
                                            Intitulé du compte : @Title\n
                                            RIB : @RIB\n
                                            IBAN : @IBAN\n
                                            BIC : @BIC";

    public const string ACCOUNT_MANAGER_DETAILS_TEMPLATE = $@"Votre gestionnaire de compte est @Fullname.\n\n
                                            Contact : @Contact\n
                                            Adresse : @Address\n
                                            [Appeler] (https://wa.me/@Whatsapp)";

    public const string ACCOUNT_HISTORY_TEMPLATE = $@"Dernières transactions: \n\n
                                            @AccountHistory";

    public const string ACCOUNT_HISTORY_LINE_TEMPLATE = $@"@Date - @Type : @Amount @Currency - @Title\n";

    public const string PRINCIPAL_MENU_TRANSACTION_CHOICES = $@"Gestion des transactions :\n\n
                                            1. Virement\n
                                            2. Transfert Wallet\n
                                            3. Paiement Facture\n
                                            4. Historique Transactions Whatsapp";

    public const string PRINCIPAL_MENU_SERVICE_CHOICES = $@"Demande de service :\n\n
                                            1. Carte bancaire
                                            2. Chèquier
                                            3. Mise en opposition
                                            4. Lever D'opposition
                                            5. Suspension Prélèvement
                                            6. Modification Contact
                                            7. Demande RDV
                                            8. Simulation Prêt
                                            9. Coupe file d'attente";

    public const string PRINCIPAL_MENU_CLAIM_CHOICES = $@"Gestion des réclamations:
                                            1. Nouvelle réclamation
                                            2. Suivi réclamation
                                            3. Historique des réclamations";

    public const string PRINCIPAL_MENU_BRANCH_CHOICES = $@"Localisation Agences et GAB
                                            1. Agences
                                            2. Guichets";

}
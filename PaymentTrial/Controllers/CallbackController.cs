using Microsoft.AspNetCore.Mvc;

namespace PaymentTrial.Controllers
{
    public class CallbackController : Controller
    {
        public IActionResult Index(string id, bool pending, int amount_cents, bool success, bool is_auth, bool is_capture,
                               bool is_standalone_payment, bool is_voided, bool is_refunded, bool is_3d_secure,
                               int integration_id, int profile_id, bool has_parent_transaction, int order,
                               string created_at, string currency, int merchant_commission, string discount_details,
                               bool is_void, bool is_refund, bool error_occured, int refunded_amount_cents,
                               int captured_amount, string updated_at, bool is_settled, bool bill_balanced, bool is_bill,
                               int owner, string data_message, string source_data_type, string source_data_pan,
                               string source_data_sub_type, string acq_response_code, string txn_response_code,
                               string hmac)
        {
            // Create a ViewModel to pass data to the view
            var callbackData = new CallbackViewModel
            {
                Id = id,
                Pending = pending,
                AmountCents = amount_cents,
                Success = success,
                IsAuth = is_auth,
                IsCapture = is_capture,
                IsStandalonePayment = is_standalone_payment,
                IsVoided = is_voided,
                IsRefunded = is_refunded,
                Is3dSecure = is_3d_secure,
                IntegrationId = integration_id,
                ProfileId = profile_id,
                HasParentTransaction = has_parent_transaction,
                Order = order,
                CreatedAt = created_at,
                Currency = currency,
                MerchantCommission = merchant_commission,
                DiscountDetails = discount_details,
                IsVoid = is_void,
                IsRefund = is_refund,
                ErrorOccurred = error_occured,
                RefundedAmountCents = refunded_amount_cents,
                CapturedAmount = captured_amount,
                UpdatedAt = updated_at,
                IsSettled = is_settled,
                BillBalanced = bill_balanced,
                IsBill = is_bill,
                Owner = owner,
                DataMessage = data_message,
                SourceDataType = source_data_type,
                SourceDataPan = source_data_pan,
                SourceDataSubType = source_data_sub_type,
                AcqResponseCode = acq_response_code,
                TxnResponseCode = txn_response_code,
                Hmac = hmac
            };

            return View(callbackData); // Pass the ViewModel to the view
        }
    }
    public class CallbackViewModel
    {
        public string Id { get; set; }
        public bool Pending { get; set; }
        public int AmountCents { get; set; }
        public bool Success { get; set; }
        public bool IsAuth { get; set; }
        public bool IsCapture { get; set; }
        public bool IsStandalonePayment { get; set; }
        public bool IsVoided { get; set; }
        public bool IsRefunded { get; set; }
        public bool Is3dSecure { get; set; }
        public int IntegrationId { get; set; }
        public int ProfileId { get; set; }
        public bool HasParentTransaction { get; set; }
        public int Order { get; set; }
        public string CreatedAt { get; set; }
        public string Currency { get; set; }
        public int MerchantCommission { get; set; }
        public string DiscountDetails { get; set; }
        public bool IsVoid { get; set; }
        public bool IsRefund { get; set; }
        public bool ErrorOccurred { get; set; }
        public int RefundedAmountCents { get; set; }
        public int CapturedAmount { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsSettled { get; set; }
        public bool BillBalanced { get; set; }
        public bool IsBill { get; set; }
        public int Owner { get; set; }
        public string DataMessage { get; set; }
        public string SourceDataType { get; set; }
        public string SourceDataPan { get; set; }
        public string SourceDataSubType { get; set; }
        public string AcqResponseCode { get; set; }
        public string TxnResponseCode { get; set; }
        public string Hmac { get; set; }
    }
}

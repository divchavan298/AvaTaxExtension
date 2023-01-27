using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalara.AvaTax.RestClient;
using AvaTax.TaxModule.Core;
using AvaTax.TaxModule.Core.Services;
using AvaTax.TaxModule.Data.Model;
using AvaTax.TaxModule.Data.Services;
using Microsoft.Extensions.Options;
using VirtoCommerce.InventoryModule.Core.Services;
using VirtoCommerce.OrdersModule.Core.Model;
using VirtoCommerce.OrdersModule.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Settings;
using VirtoCommerce.StoreModule.Core.Services;
using VirtoCommerce.TaxModule.Core.Services;

namespace NB.AvaTaxExtension.Data.Services
{
    public class NBAvaTaxExtension : OrdersSynchronizationService
    {
        private readonly ISettingsManager _settingsManager;
        public NBAvaTaxExtension(ICustomerOrderService orderService, IStoreService storeService, IFulfillmentCenterService fulfillmentCenterService, IOrderTaxTypeResolver orderTaxTypeResolver, Func<IAvaTaxSettings, AvaTaxClient> avaTaxClientFactory, ITaxProviderSearchService taxProviderSearchService, IOptions<AvaTaxSecureOptions> options, ISettingsManager settingsManager) : base(orderService, storeService, fulfillmentCenterService, orderTaxTypeResolver, avaTaxClientFactory, taxProviderSearchService, options)
        {
            _settingsManager = settingsManager;
        }

        //protected override VirtoCommerce.OrdersModule.Core.Model.LineItem ToOrderModel(VirtoCommerce.CartModule.Core.Model.LineItem lineItem)
        //{
        //    var result = base.ToOrderModel(lineItem) as NBCustomerOrderLineItem;

        //    var cartLineItem2 = lineItem as NB.CartModule.Core.Models.NBCartLineItem;
        //    if (cartLineItem2 != null)
        //    {
        //        result.PrescriptionId = cartLineItem2.PrescriptionId;
        //    }
        //    return result;
        //}

        protected override async Task SendOrderToAvaTax(CustomerOrder order, string companyCode, Address sourceAddress, AvaTaxClient avaTaxClient)
        {
            var cmt = await _settingsManager.GetValueAsync(NB.AvaTaxExtension.Core.ModuleConstants.Settings.General.SynchronizationIsCommited.Name, false);
            var status = await _settingsManager.GetValueAsync(VirtoCommerce.OrdersModule.Core.ModuleConstants.Settings.General.OrderStatus.Name, false);
            if (order.InPayments.FirstOrDefault().Status == "")
            {

            }
            if (!order.IsCancelled)
            {
                var createOrAdjustTransactionModel = AbstractTypeFactory<AvaCreateOrAdjustTransactionModel>.TryCreateInstance();
                createOrAdjustTransactionModel.FromOrder(order, companyCode, sourceAddress);
                createOrAdjustTransactionModel.createTransactionModel.commit = cmt;
                var transactionModel = await avaTaxClient.CreateOrAdjustTransactionAsync(string.Empty, createOrAdjustTransactionModel);
            }
            else
            {
                var voidTransactionModel = new VoidTransactionModel { code = VoidReasonCode.DocVoided };
                var transactionModel = await avaTaxClient.VoidTransactionAsync(companyCode, order.Number, DocumentType.Any, voidTransactionModel);
            }

        }


    }
    //public class ExtendAvaCreateTransactionModel : AvaCreateTransactionModel
    //{


    //    public override AvaCreateTransactionModel FromOrder(CustomerOrder order, string requiredCompanyCode, VirtoCommerce.CoreModule.Core.Common.Address sourceAddress)
    //    {
    //        //    var cmt = await _settingsManager.GetValueAsync(NB.AvaTaxExtension.Core.ModuleConstants.Settings.General.SynchronizationIsCommited.Name, false);
    //        var orderTaxDetail = new AvaCreateTransactionModel();
    //        if (order.Status == "Completed")
    //        {
    //            orderTaxDetail = base.FromOrder(order, requiredCompanyCode, sourceAddress);
    //            orderTaxDetail.commit = true;
    //        }
    //        else
    //        {
    //            orderTaxDetail.commit = false;
    //        }
    //        return orderTaxDetail;
    //    }
    //}
}

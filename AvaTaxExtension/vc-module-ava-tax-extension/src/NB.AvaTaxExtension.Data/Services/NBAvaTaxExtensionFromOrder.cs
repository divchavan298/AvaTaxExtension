using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaTax.TaxModule.Data.Model;
using VirtoCommerce.OrdersModule.Core.Model;

namespace NB.AvaTaxExtension.Data.Services
{
    //class NBAvaTaxExtensionFromOrder
    //{
    //}
    public class ExtendAvaCreateTransactionModel : AvaCreateTransactionModel
    {


        public override AvaCreateTransactionModel FromOrder(CustomerOrder order, string requiredCompanyCode, VirtoCommerce.CoreModule.Core.Common.Address sourceAddress)
        {
            //    var cmt = await _settingsManager.GetValueAsync(NB.AvaTaxExtension.Core.ModuleConstants.Settings.General.SynchronizationIsCommited.Name, false);
            var orderTaxDetail = new AvaCreateTransactionModel();
            if (order.Status == "Completed")
            {
                orderTaxDetail = base.FromOrder(order, requiredCompanyCode, sourceAddress);
                orderTaxDetail.commit = true;
            }
            else
            {
                orderTaxDetail.commit = false;
            }
            return orderTaxDetail;
        }
    }
}

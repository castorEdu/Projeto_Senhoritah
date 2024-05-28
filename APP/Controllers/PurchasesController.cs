using APP.Models;
using APP.Models.ViewModel;
using APP.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class PurchasesController : Controller
    {
        private IPurchaseService _purchaseService;
        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        public async Task<ActionResult> PurchaseIndex()
        {
            var viewModel = new PurchaseViewModel
            {
                ListBills = await _purchaseService.FindAll(),
            };
            return View(viewModel);
        }
        public async Task<ActionResult> CreateBill(PurchaseViewModel bill)
        {
            var newBill = await _purchaseService.Create(bill.Bills);
            return RedirectToAction(nameof(PurchaseIndex));
        }

        public async Task<ActionResult> DeleteBill(long id)
        {
            var status = await _purchaseService.Delete(id);
            if(!status) return BadRequest();
            return RedirectToAction(nameof(PurchaseIndex));
        }

        public ActionResult ShoppingIndex()
        {
            return View();
        }
    }
}

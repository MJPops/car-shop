﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IAllCars _carRepository;
        private readonly ShopCart _shopCart;
        private readonly IShopCart _shopCartDelete;

        public ShopCartController(IAllCars carRepository, ShopCart shopCart, IShopCart shopCartDelete)
        {
            _carRepository = carRepository;
            _shopCart = shopCart;
            _shopCartDelete = shopCartDelete;
        }

        [Route("~/ShopCart/Index")]
        public ViewResult Index()
        {
            var items = _shopCart.GetShopItems();
            _shopCart.listShopCarItem = items;

            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart
            };

            return View(obj);
        }

        [Route("~/ShopCart/AddToCart")]
        public RedirectToActionResult AddToCart(int id)
        {
            var item = _carRepository.Cars.FirstOrDefault(i => i.id == id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

        [Route("~/ShopCart/DeleteCart/{id}")]
        public RedirectToActionResult DelitCart(int id)
        {
            _shopCartDelete.DeleteCart(id);

            return RedirectToAction("Index");
        }
    }
}

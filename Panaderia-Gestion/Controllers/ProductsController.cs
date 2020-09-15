﻿using Microsoft.Ajax.Utilities;
using Panaderia_Gestion.Models;
using Panaderia_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panaderia_Gestion.Controllers
{
    public class ProductsController : Controller
    {
        ProductoViewModel PVM = new ProductoViewModel();
        public ActionResult Index()
        {
            IEnumerable<Products> lista = PVM.ProductsList();

            return View(lista);
        }

        public ActionResult Create()
        {
            IEnumerable<Products> List = PVM.SaleTypeListing();
            ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type", List.First());

            return View();
        }
        [HttpPost]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                if (PVM.ExistDataBase(products))
                {
                    ModelState.AddModelError("Name", "El producto ya existe");

                    ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

                    return View(products);
                }
                else if (PVM.ExistBarCode(products))
                {
                    ModelState.AddModelError("bar_code", "El código ya le pertenece a otro producto");

                    ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

                    return View(products);
                }
                else
                { 
                    PVM.Create(products);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");
                return View(products);
            }
        }

        public ActionResult Edit(int id)
        {
           Products products= PVM.GetProducts(id);
            
            ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type", products.sale_type_ID);

            return View(products);
        }
        [HttpPost]
        public ActionResult Edit(Products p)
        {
            if (ModelState.IsValid)
            {
                if (PVM.ExistDataBase(p))
                {
                    ModelState.AddModelError("name", "El nombre ingresado ya existe");

                    ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

                    return View(p);
                }
                else if (PVM.ExistBarCode(p))
                {
                    ModelState.AddModelError("bar_code", "El código ingresado ya existe");

                    ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

                    return View(p);
                }
                else
                {
                    PVM.Edit(p);
                    return RedirectToAction("Index", "Products");
                }
            }

            else
            {
                ViewBag.TypeList = new SelectList(PVM.SaleTypeListing(), "sale_type_ID", "sale_type");

                return View(p);
            }
        }

        public ActionResult Delete(int id)
        {
            
           Products product =  PVM.GetProducts(id);

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)

        {
            PVM.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
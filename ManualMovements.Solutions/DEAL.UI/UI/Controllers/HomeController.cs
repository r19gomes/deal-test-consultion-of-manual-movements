﻿using Deal.UI.Models.ManualMovements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Newtonsoft.Json;

namespace Deal.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? page = 1)
        {
            int pageSize = Convert.ToInt32
                (ConfigurationManager.AppSettings["QuantityRegistrationPage"]);
            int pageNumber = (page ?? 1);

            var viewModel = new ViewModel();

            if (TempData["FilterManualMoviments"] != null)
            {
                var filter = (Models.ManualMovements.Filter)TempData["FilterManualMoviments"];
                var response = ReturnData(filter).ToPagedList(pageNumber, pageSize);
                if (response != null)
                    viewModel.Indexes = response;
            }
            else
            {
                var response = ReturnData(viewModel.Filter).ToPagedList(pageNumber, pageSize);
                if (response != null)
                    viewModel.Indexes = response;
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ret = Services.ManualMovements.Update.Create(model);
                if (!ret.Success)
                {
                    return View(ret);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(Update edit)
        {
            if (edit == null)
                edit = new Update();

            return View(new ViewModel
            {
                Message = string.Empty,
                Success = false,
                PersistFields = false,
                Update = edit
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private IList<Index> ReturnData(Models.ManualMovements.Filter filter)
        {
            var request = new Index();

            request.Mes = filter.Mes;
            request.Ano = filter.Ano;
            request.Numero = filter.Numero;
            request.ProdutoCodigo = filter.ProdutoCodigo;
            request.ProdutoNome = filter.ProdutoNome;
            request.Descricao = filter.Descricao;
            request.Valor = filter.Valor;

            var data = Services.ManualMovements.Index.List(request);

            TempData["FilterManualMoviments"] = filter;

            return data;
        }

    }
}
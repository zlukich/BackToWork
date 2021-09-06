using BackToWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackToWork.DAClasses;
using System.Globalization;

namespace BackToWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDBDao _dba;
        public HomeController(ILogger<HomeController> logger,IDBDao dba)
        {
            _logger = logger;
            _dba = dba;
        }
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            string role = null ;
            string name=null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
                
            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            return View();
           
        }
        [Route("/Home/EditFreelancerPage/{login_freelancer}")]
        [HttpGet]
        public async Task<ActionResult> EditFreelancerPage(string login_freelancer)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DbResultViewModel l = _dba.FreelancerSet;
            int p = 0;
            for (int i = 0; i < l.column_names.Length; i++)
            {
                if (l.column_names[i] == "login_freelancer") p = i;
            }
            List<object[]> new_list = new List<object[]>();
            for (int i = 0; i < l.exemplars.Count; i++)
            {
                if (Convert.ToString(l.exemplars[i][p]) == login_freelancer)
                    new_list.Add(l.exemplars[i]);
            }
            EditFreelancerViewModel evm = new EditFreelancerViewModel
            {
                Activated = Convert.ToBoolean(new_list[0][0]),
                Age = Convert.ToInt32(new_list[0][1]),
                Name = Convert.ToString(new_list[0][2]).Trim(),
                Login = Convert.ToString(new_list[0][3]).Trim(),
                Description = Convert.ToString(new_list[0][4]).Trim(),
                Region = Convert.ToString(new_list[0][5]).Trim(),
                Email = Convert.ToString(new_list[0][6]).Trim(),
 
            };
            return View(evm);
        }


        [Route("/Home/EditFreelancerPage/{login_freelancer}")]
        [HttpPost]
        public async Task<ActionResult> EditFreelancerPage(EditFreelancerViewModel editModel)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            if (ModelState.IsValid)
            {
                DAContext context = new DAContext();
                context.condition = $"where login_freelancer = '{editModel.Login}'";
                context.content = new object[7];
                context.content[0] = true;
                context.content[1] = editModel.Age;
                context.content[2] = editModel.Name;
                context.content[3] = editModel.Login;
                context.content[4] = editModel.Description;
                context.content[5] = editModel.Region;
                context.content[6] = editModel.Email;

                _dba.UpdateAsync("freelancers", new string[0], context);

                return RedirectToAction("Index","Home");
            }
            return View(editModel);
        }
        [Route("/Home/EditOrdererPage/{login_orderer}")]
        [HttpGet]
        public async Task<ActionResult> EditOrdererPage(string login_orderer)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DbResultViewModel l = _dba.OrdererSet;
            int p = 0;
            for (int i = 0; i < l.column_names.Length; i++)
            {
                if (l.column_names[i] == "login_orderer") p = i;
            }
            List<object[]> new_list = new List<object[]>();
            for (int i = 0; i < l.exemplars.Count; i++)
            {
                if (Convert.ToString(l.exemplars[i][p]) == login_orderer)
                    new_list.Add(l.exemplars[i]);
            }
            EditOrdererViewModel evm = new EditOrdererViewModel
            {
                Activated = Convert.ToBoolean(new_list[0][0]),
                Login = Convert.ToString(new_list[0][1]),
                Email = Convert.ToString(new_list[0][2]).Trim(),
                Name = Convert.ToString(new_list[0][3]).Trim(),
                Number = Convert.ToString(new_list[0][4]),
                Description = Convert.ToString(new_list[0][5]).Trim(),

            };
            return View(evm);
        }

        [Route("/Home/EditOrdererPage/{login_orderer}")]
        [HttpPost]
        public async Task<ActionResult> EditOrdererPage(EditOrdererViewModel editModel)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            if (ModelState.IsValid)
            {
                DAContext context = new DAContext();
                context.condition = $"where login_orderer = '{editModel.Login}'";
                context.content = new object[6];
                context.content[0] = true;
                context.content[1] = editModel.Login;
                context.content[2] = editModel.Email;
                context.content[3] = editModel.Name;
                context.content[4] = editModel.Number;
                context.content[5] = editModel.Description;

                _dba.UpdateAsync("orderers", new string[0], context);

                return RedirectToAction("Index", "Home");
            }
            return View(editModel);
        }
        public IActionResult PostComment(int id_orders, string comment,string login_freelancer,string file)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DAContext context = new DAContext();
           
            _dba.ExecuteQuery("select nextval('order_comments_id_comment_seq')", context);
            int last_id = Convert.ToInt32(context.Result.exemplars[0][0]);
            context = new DAContext();
            
            context.content = new object[]
            {
                DateTime.Now,
                last_id,
                DateTime.Now.TimeOfDay,
                id_orders,
                comment,
                file,
                login_freelancer
            };
            _dba.InsertAsync("order_comments", new string[0], context);
            return RedirectToAction("OrderPage",new { id = id_orders});

        }
        [Route("Home/Orders/{id}")]
        public IActionResult OrderPage(int id)
        {

            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DAContext context = new DAContext();
            context.condition = $"where id_orders={id}";
            _dba.SelectAsync("order_comments", new string[0], context);
            List<OrderComment> order_comments = new List<OrderComment>();
            /*DateTime.Now,
                last_id,
                DateTime.Now.ToString("h:mm:ss"),
                id_orders,
                comment,
                file,
                login_freelancer*/
            for (int i = 0; i < context.Result.exemplars.Count; i++)
            {
                OrderComment comm = new OrderComment();
                comm.comment_date = Convert.ToDateTime(context.Result.exemplars[i][0]);
                DateTime time = DateTime.Parse(Convert.ToString(context.Result.exemplars[i][2]));
                comm.comment_time = time;
                comm.id_comment = Convert.ToInt32(context.Result.exemplars[i][1]);
                comm.id_orders = id;
                comm.comment_msg = Convert.ToString(context.Result.exemplars[i][4]);
                comm.link_file = Convert.ToString(context.Result.exemplars[i][5]);
                comm.login_freelancer = Convert.ToString(context.Result.exemplars[i][6]);
                order_comments.Add(comm);    
            }

            context = new DAContext();
            context.condition = $"where id_orders={id}";
            _dba.SelectAsync("Orders_full",new string[0], context);
            DbResultViewModel vm = context.Result;

            context = new DAContext();
            context.condition = $"where id_orders={id}";
            _dba.SelectAsync("order_with_tech", new string[0], context);
            DbResultViewModel techvm = context.Result;
            List<PLModel> plList = new List<PLModel>();
            List<TechModel> techList = new List<TechModel>();
            foreach(object[] tm in techvm.exemplars)
            {
                techList.Add(new TechModel { id_tech = Convert.ToInt32(tm[1]), tech_desc = Convert.ToString(tm[2]) });
            }

            context = new DAContext();
            context.condition = $"where id_orders={id}";
            _dba.SelectAsync("order_with_pl", new string[0], context);
            foreach (object[] tm in context.Result.exemplars)
            {
                plList.Add(new PLModel { id_pl = Convert.ToInt32(tm[1]), pl_desc = Convert.ToString(tm[2]) });
            }



            OrderViewModel ovm=null;
            List<PLModel> plCatalog = new List<PLModel>();
            List<TechModel> techCatalog = new List<TechModel>();
            context = new DAContext();
            _dba.SelectAsync("programming_languages_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                plCatalog.Add(new PLModel { id_pl = Convert.ToInt32(list[0]), pl_desc = Convert.ToString(list[1]) });
            }
            context = new DAContext();
            _dba.SelectAsync("technologies_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                techCatalog.Add(new TechModel { id_tech = Convert.ToInt32(list[0]), tech_desc = Convert.ToString(list[1]) });
            }
            if (vm.exemplars.Count != 0)
            {
                ovm = new OrderViewModel
                {
                    OrderDescription = Convert.ToString(vm.exemplars[0][8]).Trim(),
                    ProjectDescription = Convert.ToString(vm.exemplars[0][10]).Trim(),
                    login_freelancer = Convert.ToString(vm.exemplars[0][6]).Trim(),
                    fileLink = Convert.ToString(vm.exemplars[0][2]).Trim(),
                    login_orderer = Convert.ToString(vm.exemplars[0][7]).Trim(),
                    Time = Convert.ToDateTime(vm.exemplars[0][0]),
                    Days = Convert.ToInt32(vm.exemplars[0][1]),
                    Payment = Convert.ToInt32(vm.exemplars[0][9]),
                    projectId = Convert.ToInt32(vm.exemplars[0][4]),
                    orderId = Convert.ToInt32(vm.exemplars[0][3]),
                    isCompleted = Convert.ToBoolean(vm.exemplars[0][5]),
                    comments = order_comments,
                    plList = plList,
                    techList= techList,
                    plCatalog=plCatalog,
                    techCatalog=techCatalog
                };
            }
            else
            {
                return View(ovm);
            }
            return View(ovm);
        }
        [Route("/Home/AddCatalog/")]
        [HttpPost]
        public ActionResult AddCatalog(List<int>selectedPl,List<int>selectedTech,int id_orders)
        {
            DAContext context = new DAContext();
            foreach(int id_technology in selectedTech)
            {
                context = new DAContext();
                context.content = new object[] { id_orders, id_technology };
                _dba.InsertAsync("required_tech_binding", new string[0], context);
            }
            foreach (int id_technology in selectedPl)
            {
                context = new DAContext();
                context.content = new object[] { id_orders, id_technology };
                _dba.InsertAsync("required_pl_binding", new string[0], context);
            }
            return RedirectToAction("OrderPage",new { id = id_orders });
        }
            [Route("/Home/DeletePLFromOrder/{id_pl}&{id_orders}")]
        [Authorize(Roles = "orderer")]
        public IActionResult DeletePLFromOrder(int id_pl,int id_orders)
        {
            DAContext context = new DAContext();
            context.condition = $"where id_pl={id_pl} and id_orders={id_orders}";
            _dba.DeleteAsync("required_pl_binding", new string[0], context);
            return RedirectToAction("OrderPage", new { id = id_orders });
        }
        [Route("/Home/DeleteTechFromOrder/{id_tech}&{id_orders}")]
        [Authorize(Roles = "orderer")]
        public IActionResult DeleteTechFromOrder(int id_tech, int id_orders)
        {
            DAContext context = new DAContext();
            context.condition = $"where id_technology={id_tech} and id_orders={id_orders}";
            _dba.DeleteAsync("required_tech_binding", new string[0], context);
            return RedirectToAction("OrderPage", new { id = id_orders });
        }

        public IActionResult AppointFreelancer(int id_orders,string login_freelancer,bool flag)
        {

            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DAContext context = new DAContext();
            if (flag == false)
            {
                login_freelancer = "null";
                context.content = new object[] { DBNull.Value };
            }
            else
            {
                context.content = new object[] { login_freelancer.Trim()};
            }
            context.condition = $"where id_orders = {id_orders}";
            _dba.UpdateAsync("orders", new string[] { "login_freelancer" }, context);
            return RedirectToAction("OrderPage",new {id = id_orders});
        }
        [HttpGet]
        [Route("/Home/AddOrder/")]
        [Authorize(Roles = "orderer")]
        public async Task<ActionResult> AddOrder()
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DAContext context = new DAContext();
            context.condition = $"where login_orderer = '{name}'";
            _dba.SelectAsync("projects",new string[0],context);
            DbResultViewModel rvm = context.Result;
            AddOrderViewModel avm = new AddOrderViewModel();
            avm.projects = rvm.exemplars;
            avm.login_orderer = name;

            context = new DAContext();
            _dba.SelectAsync("programming_languages_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                avm.plList.Add(new PLModel { id_pl = Convert.ToInt32(list[0]), pl_desc = Convert.ToString(list[1]) });
            }
            context = new DAContext();
            _dba.SelectAsync("technologies_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                avm.techList.Add(new TechModel { id_tech = Convert.ToInt32(list[0]), tech_desc = Convert.ToString(list[1])});
            }


            return View(avm);
        }

        [HttpPost]
        [Route("/Home/AddOrder/")]
        [Authorize(Roles = "orderer")]
        public async Task<ActionResult> AddOrder(AddOrderViewModel avm)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            DAContext context = new DAContext();
            if (ModelState.IsValid)
            {
                context = new DAContext();
                if (avm.id_project == 0)
                {
                    _dba.ExecuteQuery("select nextval('projects_id_project_seq')", context);
                    int last_pj_id = Convert.ToInt32(context.Result.exemplars[0][0]);
                    avm.id_project = last_pj_id + 1;
                    context = new DAContext();
                    context.content = new object[3] { avm.id_project,avm.projectDescription,avm.login_orderer};
                    _dba.InsertAsync("projects", new string[0], context);
                }
                context = new DAContext();
                _dba.ExecuteQuery("select nextval('orders_id_orders_seq')", context);
                int last_id = Convert.ToInt32(context.Result.exemplars[0][0]);
                context = new DAContext();
                context.content = new object[9] { last_id, DateTime.UtcNow, avm.Days, avm.is_completed, avm.Payment, avm.id_project, avm.Link, avm.workDescription, null};
                _dba.InsertAsync("orders",new string[0],context);
                
                if (avm.selectedPl.Count != 0)
                {
                    foreach(int pl in avm.selectedPl)
                    {
                        context = new DAContext();
                        context.content = new object[] {last_id, pl};
                        _dba.InsertAsync("required_pl_binding",new string[0],context);
                    }
                }
                if (avm.selectedTech.Count != 0)
                {
                    foreach (int tech in avm.selectedTech)
                    {
                        context = new DAContext();
                        context.content = new object[] { last_id, tech };
                        _dba.InsertAsync("required_tech_binding", new string[0], context);
                    }
                }
                return RedirectToAction("Orders","Home");
            }

            context = new DAContext();
            context.condition = $"where login_orderer = '{name}'";
            _dba.SelectAsync("projects", new string[0], context);
            DbResultViewModel rvm = context.Result;
            avm = new AddOrderViewModel();
            avm.projects = rvm.exemplars;
            avm.login_orderer = name;
            return View(avm);

        }
        
        [HttpPost]
        public IActionResult OrderSearch(string desc,List<int>selectedPl,List<int>selectedTech,bool is_complited)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            string pllist = "";
            string techlist = "";
            for(int i = 0; i < selectedPl.Count; i++)
            {
                pllist = pllist + $"and id_pl similar to ('%{selectedPl[i]}%')";
            }
            for (int i = 0; i < selectedTech.Count; i++)
            {
                techlist = techlist + $"and id_technology similar to ('%{selectedTech[i]}%')";
            }
            string filter = $"where order_description similar to ('%{((desc!=null || desc!="")?desc:"")}%') and is_complited={is_complited} {pllist} {techlist} order by date_publication desc";
            DAContext context = new DAContext();
            context.condition = filter;
            _dba.SelectAsync("orders_wc", new string[0], context);
            List<OrderViewModel> ovmList = new List<OrderViewModel>();
            DbResultViewModel vm = context.Result;
            for (int i = 0; i < vm.exemplars.Count; i++)
            {

                ovmList.Add(new OrderViewModel
                {
                    OrderDescription = Convert.ToString(vm.exemplars[i][7]).Trim(),
                    ProjectDescription = Convert.ToString(vm.exemplars[i][8]).Trim(),
                    login_freelancer = Convert.ToString(vm.exemplars[i][12]).Trim(),
                    fileLink = Convert.ToString(vm.exemplars[i][11]).Trim(),
                    login_orderer = Convert.ToString(vm.exemplars[i][9]).Trim(),
                    Time = Convert.ToDateTime(vm.exemplars[i][2]),
                    Days = Convert.ToInt32(vm.exemplars[i][3]),
                    Payment = Convert.ToInt32(vm.exemplars[i][5]),
                    projectId = Convert.ToInt32(vm.exemplars[i][0]),
                    orderId = Convert.ToInt32(vm.exemplars[i][1]),
                    isCompleted = Convert.ToBoolean(vm.exemplars[i][4])

                });
            }

            return PartialView(ovmList);
        }
        [Route("/Home/DeleteCatalog/{id}&{type}")]
        public IActionResult DeleteCatalog(int id,string type)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            if (role != "admin") return RedirectToAction("Login", "Account");
            if (type == "pl")
            {
                DAContext context = new DAContext();
                context.condition = $"where id_pl={id}";
                _dba.DeleteAsync("programming_languages_catalog", new string[0], context);
            }
            else
            {
                DAContext context = new DAContext();
                context.condition = $"where id_technology={id}";
                _dba.DeleteAsync("technologies_catalog", new string[0], context);
            }
            return RedirectToAction("ManageCatalogs");
        }
            [Route("Home/DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DAContext context = new DAContext();
            context.condition = $"where id_orders={id}";
            _dba.DeleteAsync("orders",new string[0],context);
            return RedirectToAction("Orders","Home");
        }
        [HttpGet]
        [Route("Home/ReportOrder/{id}")]
        public IActionResult ReportOrder(int id)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            ReportViewModel rvm = new ReportViewModel();
            rvm.id_orders = id;
            rvm.login_freelancer = name;
            DAContext context = new DAContext();
            context.condition = $"where id_orders={id}";
            _dba.SelectAsync("Orders_full", new string[0], context);
            if(context.Result.exemplars.Count == 0)
            {
                throw new ArgumentException(
            $"Заказ с таким id не был найдет {id}.", nameof(id));
            }
            rvm.order_desc = Convert.ToString(context.Result.exemplars[0][8]);

            return View(rvm);
        }
        [HttpPost]
        [Route("Home/ReportOrder/{id}")]
        public IActionResult ReportOrder(ReportViewModel rvm)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            if (ModelState.IsValid)
            {
                DAContext context = new DAContext();
                _dba.ExecuteQuery("select nextval('order_reports_id_report_seq')", context);
                int last_id = Convert.ToInt32(context.Result.exemplars[0][0]);
                context = new DAContext();
                context.content = new object[] {last_id,rvm.id_orders,rvm.comment,rvm.answer,rvm.login_freelancer};
                _dba.InsertAsync("order_reports", new string[0], context);
                return RedirectToAction("Orders", "Home");
            }
            return View(rvm);
        }
        public IActionResult Orderers()
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            return View(_dba.OrdererSet);
        }
        [Route("/Home/Orders/")]
        public IActionResult Orders()
        {


            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            ViewBag.Name = name;
            ViewBag.Role = role;

            CatalogViewModel cvm = new CatalogViewModel();

            DAContext context = new DAContext();
            _dba.SelectAsync("programming_languages_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                cvm.plCatalog.Add(new PLModel { id_pl = Convert.ToInt32(list[0]), pl_desc = Convert.ToString(list[1]) });
            }
            context = new DAContext();
            _dba.SelectAsync("technologies_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                cvm.techCatalog.Add(new TechModel { id_tech = Convert.ToInt32(list[0]), tech_desc = Convert.ToString(list[1]) });
            }


            return View(cvm);

        }

        [Route("/Home/OrdererPage/{login_orderer}")]
        public IActionResult OrdererPage(string login_orderer)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            DbResultViewModel l = _dba.OrdererSet;
            int p = 0;
            for(int i = 0; i < l.column_names.Length; i++)
            {
                if (l.column_names[i] == "login_orderer") p = i;
            }
            List<object[]> new_list = new List<object[]>();
            for (int i = 0; i < l.exemplars.Count; i++)
            {
                if (Convert.ToString(l.exemplars[i][p]) == login_orderer)
                    new_list.Add(l.exemplars[i]);
            }

            if (name == login_orderer)
            {
                ViewBag.Owner = true;
                
            }
            else
            {
                ViewBag.Owner = false;
                
            }
            return View(new DbResultViewModel(l.column_names, new_list));
        }
        //[Authorize(Roles =("freelancer, orderer"))]
        [Route("/Home/FreelancerPage/{login_freelancer}")]
        public IActionResult FreelancerPage(string login_freelancer)
        {
            string role = null;
            string name=null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            }
            ViewBag.name = name;
            ViewBag.role = role;
            DbResultViewModel l = _dba.FreelancerSet;
            int p = 0;
            for (int i = 0; i < l.column_names.Length; i++)
            {
                if (l.column_names[i] == "login_freelancer") p = i;
            }
            List<object[]> new_list = new List<object[]>();
            for (int i = 0; i < l.exemplars.Count; i++)
            {
                if (Convert.ToString(l.exemplars[i][p]) == login_freelancer)
                    new_list.Add(l.exemplars[i]);
            }
            DisplayFreelancerViewModel dvm = new DisplayFreelancerViewModel();
            dvm.vm = new DbResultViewModel(l.column_names, new_list);

            DAContext context = new DAContext();
            context.condition = $"where login_freelancer='{login_freelancer}'";
            _dba.SelectAsync("orderer_reviews", new string[0], context);

            List<FreelancerReview> reviews = new List<FreelancerReview>();
            foreach(object[] o in context.Result.exemplars)
            {
                reviews.Add(new FreelancerReview
                {
                    id_review = Convert.ToInt32(o[0]),
                    mark = Convert.ToInt32(o[1]),
                    time = Convert.ToDateTime(o[2]),
                    review_commet = Convert.ToString(o[3]),
                    login_freelancer = Convert.ToString(o[4]),
                    login_orderer = Convert.ToString(o[5]),

                });
            }
            dvm.reviews = reviews;

            if(name == login_freelancer)
            {
                ViewBag.Owner = true;
                return View(dvm);
            }
            else
            {
                ViewBag.Owner = false;
                return View(dvm);
            }
        }
        //[Authorize]
        [Route("/Home/DeleteReview/{id}&{login_freelancer}&{login_orderer}")]
        public IActionResult DeleteReview(int id,string login_freelancer,string login_orderer)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            if (name != login_orderer)
            {
                RedirectToAction("Login", "Account");
            }
            DAContext context = new DAContext();
            context.condition = $"where id_review={id}";
            _dba.DeleteAsync("orderer_reviews", new string[0], context);

            return RedirectToAction("FreelancerPage", new { login_freelancer = login_freelancer.Trim() });
        }

        [Route("/Home/Freelancers")]
        public IActionResult Freelancers()
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            return View(_dba.FreelancerSet);
        }
        [HttpPost]
        public IActionResult SetCompleted(int id_orders)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }

            DAContext context = new DAContext();
            context.condition = $"where id_orders={id_orders}";
            context.column_names = new string[] { "is_complited" };
            context.content = new object[] { true };
            _dba.UpdateAsync("orders", context.column_names, context);
            return RedirectToAction("OrderPage", new { id = id_orders });
        }
        [Authorize(Roles ="orderer")]
        [Route("/Home/PostReview")]
        [HttpPost]
        public IActionResult PostReview(string login_freelancer,string login_orderer,string review_comment,int rating)
        {
            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;

            DAContext context = new DAContext();
            _dba.ExecuteQuery("select nextval('orderer_reviews_id_review_seq')", context);
            int last_id = Convert.ToInt32(context.Result.exemplars[0][0]);

            context = new DAContext();
            context.content = new object[] {last_id,rating,DateTime.UtcNow,review_comment,login_freelancer,login_orderer };
            _dba.InsertAsync("orderer_reviews", new string[0], context);

            return RedirectToAction("FreelancerPage",new { login_freelancer=login_freelancer});
        }
        [Route("/Home/Reports/")]
        [Authorize(Roles = "admin")]
        public IActionResult Reports()
        {

            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            return View(_dba.Reports);
        
        }
        [Route("/Home/ManageCatalogs/")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ManageCatalogs()
        {

            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            AddCatalogViewModel avm = new AddCatalogViewModel();
            DAContext context = new DAContext();
            _dba.SelectAsync("programming_languages_catalog", new string[0],context);
            foreach(object[] list in context.Result.exemplars)
            {
                avm.plList.Add(new PLModel { id_pl = Convert.ToInt32(list[0]), pl_desc = Convert.ToString(list[1]) });
            }
            context = new DAContext();
            _dba.SelectAsync("technologies_catalog", new string[0], context);
            foreach (object[] list in context.Result.exemplars)
            {
                avm.techList.Add(new TechModel { id_tech = Convert.ToInt32(list[0]), tech_desc = Convert.ToString(list[1]) });
            }

            return View(avm);

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult ManageCatalogs(AddCatalogViewModel avm)
        {

            string role = null;
            string name = null;
            if (User.Identity.IsAuthenticated)
            {
                role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                name = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            }
            ViewBag.Name = name;
            ViewBag.Role = role;
            if (ModelState.IsValid)
            {
                if (avm.type == "tech")
                {
                    DAContext context = new DAContext();

                    _dba.ExecuteQuery("select nextval('technologies_catalog_id_technology_seq')", context);
                    int last_id = Convert.ToInt32(context.Result.exemplars[0][0]);
                    context = new DAContext();
                    context.content = new object[]
                    {
                    last_id,
                    avm.desc
                    };
                    _dba.InsertAsync("technologies_catalog", new string[0], context);
                    return RedirectToAction("ManageCatalogs");
                }
                else if (avm.type == "pl")
                {
                    DAContext context = new DAContext();

                    _dba.ExecuteQuery("select nextval('programming_languages_catalog_id_pl_seq')", context);
                    int last_id = Convert.ToInt32(context.Result.exemplars[0][0]);
                    context = new DAContext();
                    context.content = new object[]
                    {
                    last_id,
                    avm.desc
                    };
                    _dba.InsertAsync("programming_languages_catalog", new string[0], context);
                    return RedirectToAction("ManageCatalogs");
                }
            }
            return View(avm);

        }
    }
}

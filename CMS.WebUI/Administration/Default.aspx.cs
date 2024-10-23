using CMS.Core.Manager;
using CMS.DataAsscess;
using CMS.WebUI.Administration.AdminUserControl;
using Newtonsoft.Json;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebUI.Administration
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataTree();

            }
        }
        private bool IsAddMenu
        {
            get
            {
                try
                {
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private bool IsEditMenu
        {
            get
            {
                try
                {
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        private void BindDataTree()
        {
            try
            {
                List<ItemTreeView> lstTree = new List<ItemTreeView>();
                List<DanhMuc> lst = DanhMucBaiVietBLL.GetAllNoPaging();
                lst= lst.Where(x=>x.Id != 4).ToList();



                if (lst != null && lst.Count > 0)
                {
                    Func<int, List<ItemTreeView>> func = null;
                    func = (parentId) =>
                    {
                        List<ItemTreeView> lstTreeChild = new List<ItemTreeView>();
                        List<DanhMuc> lstChild = lst.Where(t => t.DanhMucChaId == parentId).ToList();
                        if (lstChild != null && lstChild.Count > 0)
                        {
                            foreach (var item in lstChild)
                            {
                                if (item != null)
                                {
                                    ItemTreeView itemTree = new ItemTreeView();
                                    itemTree.MenuId = item.Id;
                                    itemTree.text = string.Format("{0} {1} ({2})", item.Ten, "Active", true);
                                    itemTree.icon = "fa fa-link";
                                    itemTree.state = new ItemState { opened = true };
                                    if (true || true)
                                        itemTree.a_attr = new { href = string.Format("/Administration/QuanLyBaiViet/DanhMucBaiViet.aspx?modal=openEdit&idCategory={0}", item.Id) };
                                    else
                                        itemTree.a_attr = null;
                                    itemTree.children = func(item.Id);
                                    lstTreeChild.Add(itemTree);
                                }
                            }
                        }

                        return lstTreeChild;
                    };
                    lstTree = func(0);
                }
                if (IsEditMenu)
                {
                    ItemTreeView addChild = new ItemTreeView();
                    addChild.MenuId = 0;
                    addChild.text = "Thêm mới";
                    addChild.icon = "fa fa-plus";
                    addChild.state = new ItemState { opened = true };
                    if (IsEditMenu)
                        addChild.a_attr = new { href = "/Administration/QuanLyBaiViet/DanhMucBaiViet?modal=openModal" };
                    else
                        addChild.a_attr = null;
                    addChild.children = null;
                    lstTree.Add(addChild);
                }
                hdfRightsTreeViewData.Value = JsonConvert.SerializeObject(new { MenuId = 0, text = "Danh sách danh mục", children = lstTree, icon = "fa fa-list-ul", state = new { opened = true } });
            }
            catch (Exception exc)
            {
                lblLabel.Text = "Thông báo : " + exc.Message;
            }
        }


        private class ItemTreeView
        {
            public int MenuId { get; set; }
            public string text { get; set; }
            public ItemState state { get; set; }
            public string icon { get; set; }
            public object a_attr { get; set; }
            public List<ItemTreeView> children { get; set; }
        }
        private class ItemState
        {
            public bool opened { get; set; }
        }


    }






}
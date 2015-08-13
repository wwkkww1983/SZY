﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using RuRo.Common;
using LTP.Accounts.Bus;
namespace RuRo.Web.EmpiInfo
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int Id=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(Id);
				}
			}
		}
			
	private void ShowInfo(int Id)
	{
		RuRo.BLL.EmpiInfo bll=new RuRo.BLL.EmpiInfo();
		RuRo.Model.EmpiInfo model=bll.GetModel(Id);
		this.lblId.Text=model.Id.ToString();
		this.txtPatientName.Text=model.PatientName;
		this.txtSex.Text=model.Sex;
		this.txtBirthday.Text=model.Birthday;
		this.txtCardId.Text=model.CardId;
		this.chkisDel.Checked=model.isDel;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtPatientName.Text.Trim().Length==0)
			{
				strErr+="姓名不能为空！\\n";	
			}
			if(this.txtSex.Text.Trim().Length==0)
			{
				strErr+="性别不能为空！\\n";	
			}
			if(this.txtBirthday.Text.Trim().Length==0)
			{
				strErr+="出生日期不能为空！\\n";	
			}
			if(this.txtCardId.Text.Trim().Length==0)
			{
				strErr+="身份证号不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int Id=int.Parse(this.lblId.Text);
			string PatientName=this.txtPatientName.Text;
			string Sex=this.txtSex.Text;
			string Birthday=this.txtBirthday.Text;
			string CardId=this.txtCardId.Text;
			bool isDel=this.chkisDel.Checked;


			RuRo.Model.EmpiInfo model=new RuRo.Model.EmpiInfo();
			model.Id=Id;
			model.PatientName=PatientName;
			model.Sex=Sex;
			model.Birthday=Birthday;
			model.CardId=CardId;
			model.isDel=isDel;

			RuRo.BLL.EmpiInfo bll=new RuRo.BLL.EmpiInfo();
			bll.Update(model);
			RuRo.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
